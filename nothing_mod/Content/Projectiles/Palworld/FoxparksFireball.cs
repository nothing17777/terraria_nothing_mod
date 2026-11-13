using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Palworld
{
	// Foxparks fireball (wiki). ai[0]: 0 normal, 1 splits into 8 small fireballs when it dissipates (every 3rd Trusty shot), 2 small fireball. ai[1]: 1 = Trusty (Hellfire).
	// Lives 0.58 s (35 ticks); splitting ones 0.25 s (15); small ones 0.33 s (20). Passes through blocks for 0.33 s unless falling, then gravity applies.
	public class FoxparksFireball : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.MinionShot[Type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 35;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
		}

		public override void OnSpawn(Terraria.DataStructures.IEntitySource source)
		{
			if (Projectile.ai[0] == 1f)
				Projectile.timeLeft = 15;
			else if (Projectile.ai[0] == 2f)
			{
				Projectile.timeLeft = 20;
				Projectile.scale = 0.6f;
			}
		}

		public override void AI()
		{
			Projectile.localAI[0]++;
			int life = Projectile.ai[0] == 1f ? 15 : Projectile.ai[0] == 2f ? 20 : 35;
			float baseScale = Projectile.ai[0] == 2f ? 0.6f : 1f;
			Projectile.scale = baseScale * (1f - 0.6f * Projectile.localAI[0] / life); // visually shrinks as it travels
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			if (Projectile.localAI[0] > 20f)
			{
				Projectile.velocity.Y += 0.25f; // gravity strength UNVERIFIED
				Projectile.tileCollide = true;
			}
			else
				Projectile.tileCollide = Projectile.velocity.Y > 0f;
			Lighting.AddLight(Projectile.Center, 1f, 0.5f, 0.1f);
			if (Main.rand.NextBool(3))
			{
				Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1f);
				d.noGravity = true;
			}
			if (Projectile.wet && !Projectile.lavaWet) // dissipates on contact with liquids except lava
			{
				Projectile.ai[0] = 2f;
				Projectile.Kill();
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(Projectile.ai[1] == 1f ? BuffID.OnFire3 : BuffID.OnFire, Main.rand.Next(300, 601)); // wiki: 5-10 s
		}

		public override void OnKill(int timeLeft)
		{
			if (Projectile.ai[0] == 1f && Projectile.owner == Main.myPlayer && !Projectile.wet)
			{
				// Wiki: 8 smaller fireballs at 50% damage and knockback.
				for (int i = 0; i < 8; i++)
				{
					Vector2 v = Vector2.UnitX.RotatedBy(MathHelper.TwoPi * i / 8f) * 6f; // speed UNVERIFIED
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, v, Type, Projectile.damage / 2, Projectile.knockBack / 2f, Projectile.owner, 2f, Projectile.ai[1]);
				}
			}
		}
	}
}
