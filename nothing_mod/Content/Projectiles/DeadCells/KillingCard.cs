using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.DeadCells
{
	// Flies serpentine for 0.5s then falls. Simplified: no sticking and no returning (wiki: sticks, returns after 5s for 150% damage). Sprite placeholder (Shuriken).
	public class KillingCard : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Shuriken;

		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 3;
			Projectile.timeLeft = 300;
			Projectile.tileCollide = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override void AI()
		{
			Projectile.rotation += 0.5f;
			if (Projectile.ai[0] < 30f) // 0.5s of flight
			{
				Projectile.ai[0]++;
				Projectile.velocity = Projectile.velocity.RotatedBy(System.Math.Sin(Projectile.ai[0] * 0.5f) * 0.12f);
			}
			else
				Projectile.velocity.Y += 0.4f;
		}

		// TODO: stick to blocks/enemies, stop damaging, return after 5s at 150% damage through blocks.
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.velocity = Vector2.Zero;
			Projectile.friendly = false;
			Projectile.timeLeft = 30;
			return false;
		}
	}
}
