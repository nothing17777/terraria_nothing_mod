using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// Mystic Bloom petal: slow homing, bounces, fades over 2 s. Sprite is a placeholder (Leaf).
	public class MysticPetal : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Leaf;

		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 120;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}

		public override void AI()
		{
			Projectile.rotation += 0.3f;
			if (Projectile.timeLeft < 60)
				Projectile.alpha = (int)(255 * (1f - Projectile.timeLeft / 60f));
			float bright = 1f - Projectile.alpha / 255f;
			Lighting.AddLight(Projectile.Center, 0.2f * bright, 0.7f * bright, 0.3f * bright);

			NPC target = null;
			float best = 400f * 400f; // 25 tiles
			foreach (NPC n in Main.ActiveNPCs)
			{
				if (!n.CanBeChasedBy(Projectile))
					continue;
				float d = Projectile.Center.DistanceSQ(n.Center);
				if (d < best)
				{
					best = d;
					target = n;
				}
			}
			if (target != null && Projectile.timeLeft < 105)
			{
				Vector2 want = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 10f; // UNVERIFIED top speed
				Projectile.velocity = Vector2.Lerp(Projectile.velocity, want, 0.06f);
			}
			else if (target == null)
			{
				Projectile.velocity *= 0.94f; // lingers when nothing is in range
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Projectile.velocity.X != oldVelocity.X) Projectile.velocity.X = -oldVelocity.X;
			if (Projectile.velocity.Y != oldVelocity.Y) Projectile.velocity.Y = -oldVelocity.Y;
			return false;
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			// Always knocks enemies directly away from the player.
			modifiers.HitDirectionOverride = target.Center.X > Main.player[Projectile.owner].Center.X ? 1 : -1;
		}
	}
}
