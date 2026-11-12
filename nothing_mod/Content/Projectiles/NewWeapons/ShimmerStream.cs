using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// Shimmer Gun stream. Sprite is a placeholder (Slime Gun stream).
	public class ShimmerStream : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.SlimeGun;

		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Default;
			Projectile.penetrate = 3; // UNVERIFIED
			Projectile.timeLeft = 30; // UNVERIFIED stream length
			Projectile.extraUpdates = 2; // effective velocity 30 from shootSpeed 10
			Projectile.ignoreWater = true;
			Projectile.alpha = 255;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override void AI()
		{
			for (int i = 0; i < 2; i++)
			{
				Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PinkTorch, 0f, 0f, 100, default, 1.4f);
				d.noGravity = true;
				d.velocity *= 0.3f;
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.Shimmer, 100); // 1.67 s, 100% (wiki)
		}
		// TODO: shimmer transformations (town NPC alt look, enemy/critter/slime transmutation, torch biome conversion).
	}
}
