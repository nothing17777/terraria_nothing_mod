using System;
using System.IO;
using System.Reflection;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace nothing_mod.Content.Systems
{
    public class UnrestrictedSummonedBoss : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool IgnoreSummonEnvironment;

        public override void Load()
        {
            // Change only the AI's local condition result, never the world's actual time or player biomes.
            foreach (string name in new[] { "VanillaAI_Inner", "AI_006_Worms", "AI_037_Destroyer" })
            {
                MethodInfo method = typeof(NPC).GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (method == null) throw new InvalidOperationException($"Boss AI method missing: {name}");
                MonoModHooks.Modify(method, PatchBossAI);
            }
        }

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            // Worm segments and the Brain's creepers inherit the summon flag from their parent.
            IgnoreSummonEnvironment = UnrestrictedBossSummons.SpawningUnrestrictedBoss ||
                (source is EntitySource_Parent parent && parent.Entity is NPC parentNPC &&
                 parentNPC.GetGlobalNPC<UnrestrictedSummonedBoss>().IgnoreSummonEnvironment);
            if (IgnoreSummonEnvironment) npc.netUpdate = true;
        }

        public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter) =>
            bitWriter.WriteBit(IgnoreSummonEnvironment);

        public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader) =>
            IgnoreSummonEnvironment = bitReader.ReadBit();

        private static bool IsMarked(NPC npc) => npc.GetGlobalNPC<UnrestrictedSummonedBoss>().IgnoreSummonEnvironment;

        private static bool IsEater(NPC npc) => npc.type == NPCID.EaterofWorldsHead ||
            npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail;

        private static bool IsDayForBoss(bool original, NPC npc)
        {
            if (!original || !IsMarked(npc)) return original;
            return !(IsEater(npc) || npc.type == NPCID.EyeofCthulhu ||
                npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism ||
                npc.type == NPCID.SkeletronPrime || npc.type == NPCID.PrimeCannon ||
                npc.type == NPCID.PrimeSaw || npc.type == NPCID.PrimeVice || npc.type == NPCID.PrimeLaser ||
                npc.type == NPCID.TheDestroyer || npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail);
        }

        private static void PatchBossAI(ILContext il)
        {
            var cursor = new ILCursor(il);
            while (cursor.TryGotoNext(MoveType.After, instruction =>
                instruction.MatchLdsfld<Main>(nameof(Main.dayTime)) || instruction.MatchCall<Main>(nameof(Main.IsItDay))))
            {
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.EmitDelegate<Func<bool, NPC, bool>>(IsDayForBoss);
            }

            cursor.Index = 0;
            while (cursor.TryGotoNext(MoveType.After, instruction => instruction.MatchCallOrCallvirt<Player>("get_ZoneCorrupt")))
            {
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.EmitDelegate<Func<bool, NPC, bool>>((original, npc) => original || (IsMarked(npc) && IsEater(npc)));
            }

            cursor.Index = 0;
            while (cursor.TryGotoNext(MoveType.After, instruction => instruction.MatchCallOrCallvirt<Player>("get_ZoneCrimson")))
            {
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.EmitDelegate<Func<bool, NPC, bool>>((original, npc) => original ||
                    (IsMarked(npc) && (npc.type == NPCID.BrainofCthulhu || npc.type == NPCID.Creeper)));
            }
        }
    }
}
