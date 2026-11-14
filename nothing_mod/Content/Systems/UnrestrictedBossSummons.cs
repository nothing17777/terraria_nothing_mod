using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems
{
    public class UnrestrictedBossSummons : ModSystem
    {
        // Only direct vanilla boss summons are changed. Event summons and fishing bait retain their behavior.
        internal static readonly Dictionary<int, int[]> Bosses = new()
        {
            [ItemID.SlimeCrown] = new int[] { NPCID.KingSlime },
            [ItemID.SuspiciousLookingEye] = new int[] { NPCID.EyeofCthulhu },
            [ItemID.WormFood] = new int[] { NPCID.EaterofWorldsHead },
            [ItemID.BloodySpine] = new int[] { NPCID.BrainofCthulhu },
            [ItemID.Abeemination] = new int[] { NPCID.QueenBee },
            [ItemID.DeerThing] = new int[] { NPCID.Deerclops },
            [ItemID.MechanicalEye] = new int[] { NPCID.Retinazer, NPCID.Spazmatism },
            [ItemID.MechanicalWorm] = new int[] { NPCID.TheDestroyer },
            [ItemID.MechanicalSkull] = new int[] { NPCID.SkeletronPrime },
            [ItemID.QueenSlimeCrystal] = new int[] { NPCID.QueenSlimeBoss },
            [ItemID.LihzahrdPowerCell] = new int[] { NPCID.Golem },
            [ItemID.CelestialSigil] = new int[] { NPCID.MoonLordCore },
            [ItemID.MechdusaSummon] = new int[] { NPCID.SkeletronPrime, NPCID.TheDestroyer, NPCID.Retinazer, NPCID.Spazmatism },
        };

        internal static bool SpawningUnrestrictedBoss { get; private set; }
        internal static bool Supports(int itemType) => Bosses.ContainsKey(itemType);

        public override void Load()
        {
            On_Player.ItemCheck_CheckCanUse += CanUseBossSummon;
            On_Player.ItemCheck_UseBossSpawners += UseBossSummon;
            On_Player.ItemCheck_UseEventItems += UseEventItem;
            MethodInfo spawnMechQueen = typeof(NPC).GetMethod(nameof(NPC.SpawnMechQueen));
            MonoModHooks.Modify(spawnMechQueen, AllowMechdusaInAnyWorld);
        }

        public override void Unload()
        {
            On_Player.ItemCheck_CheckCanUse -= CanUseBossSummon;
            On_Player.ItemCheck_UseBossSpawners -= UseBossSummon;
            On_Player.ItemCheck_UseEventItems -= UseEventItem;
            SpawningUnrestrictedBoss = false;
        }

        private static bool BossAlreadyAlive(int itemType)
        {
            foreach (int npcType in Bosses[itemType])
                if (NPC.AnyNPCs(npcType)) return true;
            // A split Eater can have lost its original head.
            return itemType == ItemID.WormFood &&
                (NPC.AnyNPCs(NPCID.EaterofWorldsBody) || NPC.AnyNPCs(NPCID.EaterofWorldsTail));
        }

        private static bool CanUseBossSummon(On_Player.orig_ItemCheck_CheckCanUse orig, Player player, Item item)
        {
            if (!Supports(item.type)) return orig(player, item);
            // Bypass vanilla environment checks, while respecting hooks from other mods.
            return !BossAlreadyAlive(item.type) && CombinedHooks.CanUseItem(player, item);
        }

        private void UseBossSummon(On_Player.orig_ItemCheck_UseBossSpawners orig, Player player, int playerIndex, Item item)
        {
            if (!Supports(item.type))
            {
                orig(player, playerIndex, item);
                return;
            }
            if (player.whoAmI != Main.myPlayer || !player.ItemTimeIsZero || player.itemAnimation <= 0)
                return;

            // Match vanilla use timing so the normal item pipeline consumes exactly one item.
            player.ApplyItemTime(item);
            SoundEngine.PlaySound(SoundID.Roar, player.Center);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)global::nothing_mod.nothing_mod.PacketType.BossSummon);
                packet.Write((ushort)item.type);
                packet.Send();
            }
            else
            {
                SpawnBoss(player, item.type);
            }
        }

        private static void UseEventItem(On_Player.orig_ItemCheck_UseEventItems orig, Player player, Item item)
        {
            // The sigil is handled above rather than starting the vanilla impending-doom event.
            if (item.type != ItemID.CelestialSigil) orig(player, item);
        }

        internal static void ReceiveSummon(BinaryReader reader, int sender)
        {
            int itemType = reader.ReadUInt16();
            if (Main.netMode != NetmodeID.Server || sender < 0 || sender >= Main.maxPlayers || !Supports(itemType))
                return;
            Player player = Main.player[sender];
            if (!player.active || player.dead) return;
            // Bind the request to the sending player, never to a client-supplied player or NPC ID.
            SpawnBoss(player, itemType);
        }

        private static void SpawnBoss(Player player, int itemType)
        {
            if (BossAlreadyAlive(itemType)) return;
            SpawningUnrestrictedBoss = true;
            try
            {
                if (itemType == ItemID.MechdusaSummon)
                {
                    NPC.SpawnMechQueen(player.whoAmI);
                }
                else if (itemType == ItemID.LihzahrdPowerCell)
                {
                    // SpawnOnPlayer searches for a temple altar for Golem; create it nearby instead.
                    int x = (int)MathHelper.Clamp(player.Center.X + player.direction * 240f, 160f, Main.maxTilesX * 16f - 160f);
                    int y = (int)MathHelper.Clamp(player.Center.Y - 160f, 160f, Main.maxTilesY * 16f - 160f);
                    int index = NPC.NewNPC(player.GetSource_ItemUse(player.HeldItem), x, y, NPCID.Golem, Target: player.whoAmI);
                    if (index < Main.maxNPCs)
                    {
                        NetworkText announcement = NetworkText.FromKey("Announcement.HasAwoken", NetworkText.FromKey("NPCName.Golem"));
                        if (Main.netMode == NetmodeID.Server)
                            ChatHelper.BroadcastChatMessage(announcement, new Color(175, 75, 255));
                        else
                            Main.NewText(announcement.ToString(), 175, 75, 255);
                    }
                }
                else
                {
                    foreach (int npcType in Bosses[itemType]) NPC.SpawnOnPlayer(player.whoAmI, npcType);
                }
            }
            finally
            {
                SpawningUnrestrictedBoss = false;
            }
        }

        private static void AllowMechdusaInAnyWorld(ILContext il)
        {
            var cursor = new ILCursor(il);
            int edits = 0;
            while (cursor.TryGotoNext(MoveType.After, instruction =>
                instruction.MatchLdsfld<Main>(nameof(Main.remixWorld)) ||
                instruction.MatchLdsfld<Main>(nameof(Main.getGoodWorld))))
            {
                cursor.EmitDelegate<Func<bool, bool>>(original => original || SpawningUnrestrictedBoss);
                edits++;
            }
            if (edits != 2) throw new InvalidOperationException("Mechdusa summon checks changed in this tModLoader version.");
        }
    }
}
