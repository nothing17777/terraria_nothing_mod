using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Accessory effects for the backported weapons workstream.
	public class NewWeaponsPlayer : ModPlayer
	{
		public bool sweetBarb;
		public bool phoenixQuiver;

		public override void ResetEffects()
		{
			sweetBarb = false;
			phoenixQuiver = false;
		}

		public override void OnHurt(Player.HurtInfo info)
		{
			if (!sweetBarb || Main.myPlayer != Player.whoAmI)
				return;
			int bees = 1;
			if (Main.rand.NextBool(3)) bees++;
			if (Main.rand.NextBool(3)) bees++;
			for (int i = 0; i < bees; i++)
			{
				Vector2 vel = new Vector2(Main.rand.Next(-35, 36), Main.rand.Next(-35, 36)) * 0.02f;
				Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, vel, Player.beeType(), Player.beeDamage(13), Player.beeKB(0f), Player.whoAmI); // UNVERIFIED base damage
			}
			Player.AddBuff(BuffID.Honey, 300);
		}

		public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
		{
			if (phoenixQuiver && item.useAmmo == AmmoID.Arrow)
				damage += 0.1f;
		}

		public override void ModifyWeaponKnockback(Item item, ref StatModifier knockback)
		{
			if (phoenixQuiver && item.useAmmo == AmmoID.Arrow)
				knockback += 0.1f;
		}

		public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (phoenixQuiver && item.useAmmo == AmmoID.Arrow)
			{
				velocity *= 1.2f; // UNVERIFIED: wiki says "+120% total velocity"
				if (type == ProjectileID.WoodenArrowFriendly)
					type = ProjectileID.FireArrow;
			}
		}
	}
}
