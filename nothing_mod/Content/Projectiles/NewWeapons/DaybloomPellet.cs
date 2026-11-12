using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// Daybloom Staff pellet. Sprite is a placeholder (Amber Bolt).
	public class DaybloomPellet : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.AmberBolt;

		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 150; // 2.5 s
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
		}

		public override void AI()
		{
			Projectile.velocity *= 0.97f; // gradually slows
			Projectile.rotation += 0.2f;
			if (Projectile.wet)
				Projectile.Kill();
			if (Main.rand.NextBool(3))
			{
				Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.YellowTorch);
				d.noGravity = true;
				d.velocity *= 0.2f;
			}
		}
	}
}
