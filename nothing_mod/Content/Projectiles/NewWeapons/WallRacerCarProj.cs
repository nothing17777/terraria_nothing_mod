using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Items.NewWeapons;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// Small harmless RC car controlled with the movement keys; the player is frozen while it exists.
	// Sprite is a placeholder (Bouncy Grenade).
	public class WallRacerCarProj : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.BouncyGrenade;

		Vector2 anchor;
		bool anchored;

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 12;
			Projectile.friendly = false;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 2;
		}

		public override void AI()
		{
			Player p = Main.player[Projectile.owner];
			if (!p.active || p.dead || p.HeldItem.type != ModContent.ItemType<WallRacerCar>())
			{
				Projectile.Kill();
				return;
			}
			Projectile.timeLeft = 2;
			if (!anchored)
			{
				anchor = p.position;
				anchored = true;
			}
			p.position = anchor; // lock the player in place
			p.velocity = Vector2.Zero;
			p.fallStart = (int)(p.position.Y / 16f);

			if (Main.myPlayer == Projectile.owner)
			{
				float accel = 0.35f;
				if (p.controlLeft) Projectile.velocity.X -= accel;
				else if (p.controlRight) Projectile.velocity.X += accel;
				else Projectile.velocity.X *= 0.9f;
				Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X, -6f, 6f); // UNVERIFIED top speed
				Projectile.velocity.Y += 0.4f;
				if (Projectile.velocity.Y > 12f) Projectile.velocity.Y = 12f;
				Projectile.netUpdate = true;
			}
			if (Projectile.velocity.X != 0f)
				Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
			Lighting.AddLight(Projectile.Center, 0.5f, 0.5f, 0.6f);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Projectile.velocity.X != oldVelocity.X) Projectile.velocity.X = 0f;
			if (Projectile.velocity.Y != oldVelocity.Y) Projectile.velocity.Y = 0f;
			return false;
		}
		// TODO: wall driving (Jump key), Teal Pressure Pad triggering, FPV Goggles camera, extra light on destroy.
	}
}
