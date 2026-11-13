using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Mounts;
using nothing_mod.Content.Projectiles.Palworld;

namespace nothing_mod.Content.Items.Palworld
{
	// Chillet mount effects (wiki): +10% damage while mounted; double-tap a direction for Rocket Slam, a dash that deals 32 base summon damage
	// (Trusty: 150), knockback 6, and grants 0.17 s of invincibility on hitting an enemy. Dash speed/length/cooldown are UNVERIFIED.
	public class ChilletPlayer : ModPlayer
	{
		private int slamTicks;
		private int slamDir;
		private int cooldown;

		private bool OnChillet(out bool trusty)
		{
			trusty = false;
			if (!Player.mount.Active)
				return false;
			int t = Player.mount.Type;
			if (t == ModContent.MountType<TrustyChilletMount>() || t == ModContent.MountType<TrustyChilletIgnisMount>())
			{
				trusty = true;
				return true;
			}
			return t == ModContent.MountType<ChilletMount>() || t == ModContent.MountType<ChilletIgnisMount>();
		}

		public override void PostUpdateEquips()
		{
			if (OnChillet(out _))
				Player.GetDamage(DamageClass.Generic) += 0.10f;
		}

		public override void PreUpdateMovement()
		{
			if (cooldown > 0)
				cooldown--;
			if (!OnChillet(out bool trusty))
			{
				slamTicks = 0;
				return;
			}
			if (slamTicks <= 0 && cooldown <= 0 && Player.whoAmI == Main.myPlayer)
			{
				int dir = 0;
				if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[2] > 0 && Player.doubleTapCardinalTimer[2] < 15)
					dir = 1;
				else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[3] > 0 && Player.doubleTapCardinalTimer[3] < 15)
					dir = -1;
				if (dir != 0)
				{
					slamDir = dir;
					slamTicks = 14;
					cooldown = 45;
					SoundEngine.PlaySound(SoundID.Item74, Player.Center); // placeholder
					int dmg = (int)Player.GetTotalDamage(DamageClass.Summon).ApplyTo(trusty ? 150f : 32f);
					Projectile.NewProjectile(Player.GetSource_Misc("RocketSlam"), Player.Center, Vector2.Zero, ModContent.ProjectileType<RocketSlamHit>(), dmg, 6f, Player.whoAmI);
				}
			}
			if (slamTicks > 0)
			{
				slamTicks--;
				Player.velocity.X = slamDir * (trusty ? 14f : 12f);
				Player.direction = slamDir;
				Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Torch, -slamDir * 2f, 0f, 100, default, 1.4f);
				d.noGravity = true;
			}
		}
	}
}
