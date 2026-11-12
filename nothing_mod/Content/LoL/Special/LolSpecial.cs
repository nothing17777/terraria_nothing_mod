using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace nothing_mod.Content.LoL.Special
{
	// Faithful per-champion spell implementations. TryCast returns true when the champion has a hand-built spell
	// for that slot; otherwise LolPlayer falls back to the generic archetype system.
	public static class LolSpecial
	{
		public static bool TryCast(LolPlayer lp, LolChampionItem champ, SpellDef sp, int slot, Vector2 dir, Vector2 mouse, int dmg, float kb, IEntitySource src, out float cooldown) {
			cooldown = LolPlayer.SpellCooldown;
			Player p = lp.Player;
			int me = p.whoAmI;
			float range = LolPlayer.RangePx(sp);
			if (champ.Name != "AhriChampion") return false;
			switch (slot) {
				case 0: // Orb of Deception
					Projectile.NewProjectile(src, p.Center, dir, ModContent.ProjectileType<AhriOrb>(), dmg, kb, me, range);
					return true;
				case 1: // Fox-Fire: three flames
					for (int i = 0; i < 3; i++)
						Projectile.NewProjectile(src, p.Center, Vector2.Zero, ModContent.ProjectileType<AhriFoxFire>(), (int)(dmg * 0.8f), kb, me, i);
					return true;
				case 2: // Charm
					Projectile.NewProjectile(src, p.Center, dir * (range / 38f) + p.velocity, ModContent.ProjectileType<AhriCharm>(), dmg, kb, me);
					return true;
				default: { // Spirit Rush: up to 3 dashes in 10s, each fires 3 bolts
					lp.StartDash(dir, 15f, 10);
					for (int i = 0; i < 3; i++) {
						Vector2 v = dir.RotatedBy((i - 1) * 0.5f) * 10f + p.velocity;
						Projectile.NewProjectile(src, p.Center, v, ModContent.ProjectileType<AhriSpiritBolt>(), (int)(dmg * 0.6f), kb, me);
					}
					if (lp.RCharges == 0) { lp.RCharges = 2; lp.RWindow = 10f; cooldown = 0.4f; }
					else { lp.RCharges--; cooldown = lp.RCharges == 0 ? LolPlayer.SpellCooldown : 0.4f; if (lp.RCharges == 0) lp.RWindow = 0; }
					return true;
				}
			}
		}
	}
}
