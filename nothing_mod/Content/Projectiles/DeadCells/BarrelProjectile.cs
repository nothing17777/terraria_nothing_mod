using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.DeadCells
{
	// Bounces off blocks, explodes on enemy contact or after 3s. After the first bounce it glows and deals 133% damage (wiki). Sprite placeholder (Grenade).
	public class BarrelProjectile : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Grenade;

		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 180;
			Projectile.tileCollide = true;
		}

		public override void AI()
		{
			Projectile.velocity.Y += 0.3f; // UNVERIFIED gravity strength
			if (Projectile.velocity.Y > 16f)
				Projectile.velocity.Y = 16f;
			Projectile.rotation += Projectile.velocity.X * 0.05f;
			if (Projectile.ai[0] > 0f)
				Lighting.AddLight(Projectile.Center, 0.8f, 0.5f, 0.1f);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Projectile.velocity.X != oldVelocity.X)
				Projectile.velocity.X = -oldVelocity.X * 0.7f;
			if (Projectile.velocity.Y != oldVelocity.Y)
				Projectile.velocity.Y = -oldVelocity.Y * 0.7f;
			Projectile.ai[0] = 1f; // has bounced
			return false;
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			if (Projectile.ai[0] > 0f)
				modifiers.SourceDamage *= 1.33f;
		}

		public override void OnKill(int timeLeft)
		{
			// TODO: area explosion (currently only the direct hit deals damage); custom explosion sound/dust.
			for (int i = 0; i < 15; i++)
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 0, default, 1.5f);
		}
	}
}
