using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems.Mechanics
{
	// 1.4.5.0: enemies can suffer Bleeding, 12 damage per second (lifeRegen -24, 4 damage per batch).
	// Inflicted by Stylish Scissors, Bloody Machete and Harpoon.
	public class NpcBleeding : GlobalNPC
	{
		public override void UpdateLifeRegen(NPC npc, ref int damage) {
			if (!npc.HasBuff(BuffID.Bleeding))
				return;
			if (npc.lifeRegen > 0)
				npc.lifeRegen = 0;
			npc.lifeRegen -= 24;
			if (damage < 4)
				damage = 4;
		}

		public override void DrawEffects(NPC npc, ref Color drawColor) {
			if (!npc.HasBuff(BuffID.Bleeding))
				return;
			if (Main.rand.NextBool(2))
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood, 0f, 0f, 0, default, 1f);
			drawColor.G = (byte)(drawColor.G * 0.85f);
			drawColor.B = (byte)(drawColor.B * 0.85f);
		}

		public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone) {
			if (item.type == ItemID.StylistKilLaKillScissorsIWish)
				Inflict(npc);
		}

		public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone) {
			if (projectile.type == ProjectileID.BloodyMachete || projectile.type == ProjectileID.Harpoon)
				Inflict(npc);
		}

		// Scissors: 4-6 seconds (reference Player.StatusToNPC). Machete/Harpoon duration UNVERIFIED, reusing the same.
		private static void Inflict(NPC npc) {
			npc.AddBuff(BuffID.Bleeding, 60 * Main.rand.Next(4, 7));
		}
	}
}
