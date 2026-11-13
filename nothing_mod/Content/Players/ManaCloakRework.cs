using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Players
{
	// 1.4.5.7 Mana Cloak rework: summons a falling mana star on magic hits (instead of on being hurt).
	// Picking the star up gives Mana Surge through ManaStarSurge (ManaRework.cs).
	public class ManaCloakRework : ModPlayer
	{
		private const int CooldownTicks = 60; // UNVERIFIED: the changelog gives no rate limit
		private int cooldown;

		private bool HasManaCloak() {
			for (int i = 3; i < 10; i++) {
				if (Player.armor[i].type == ItemID.ManaCloak)
					return true;
			}
			return false;
		}

		public override void PostUpdateEquips() {
			if (cooldown > 0)
				cooldown--;
			// Remove vanilla's on-hurt stars from the Mana Cloak.
			if (Player.starCloakItem != null && Player.starCloakItem.type == ItemID.ManaCloak) {
				Player.starCloakItem = null;
				Player.starCloakItem_manaCloakOverrideItem = null;
			}
		}

		private void OnMagicHit(NPC target) {
			if (Player.whoAmI != Main.myPlayer || cooldown > 0 || target.friendly || target.immortal || !HasManaCloak())
				return;
			cooldown = CooldownTicks;
			Vector2 pos = new Vector2(target.Center.X + Main.rand.Next(-200, 201), Player.Center.Y - 700f);
			Vector2 vel = new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(8f, 12f));
			// UNVERIFIED: ManaCloakStar projectile ai (ai[1] = elevation at which the star breaks, as vanilla's Star Cloak does).
			Projectile.NewProjectile(Player.GetSource_Accessory(Player.armor[3]), pos, vel, ProjectileID.ManaCloakStar, 0, 0f, Player.whoAmI, 0f, Player.Center.Y);
		}

		public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone) {
			if (item.CountsAsClass(DamageClass.Magic))
				OnMagicHit(target);
		}

		public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone) {
			if (proj.CountsAsClass(DamageClass.Magic))
				OnMagicHit(target);
		}
	}
}
