using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// Fast, decelerating bolt that explodes (7x7 tiles) after 0.62 s. Sprite is a placeholder (Amber Bolt).
	public class ForbiddenBolt : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.AmberBolt;

		public override void SetDefaults()
		{
			Projectile.width = 12;
			Projectile.height = 12;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 37; // 0.62 s
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
		}

		public override void AI()
		{
			Projectile.velocity *= 0.96f;
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GoldFlame);
			d.noGravity = true;
			d.velocity *= 0.2f;
		}

		public override void OnKill(int timeLeft)
		{
			if (Projectile.owner == Main.myPlayer)
			{
				// TODO: the blast should skip the enemy hit directly by the bolt.
				Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero,
					ModContent.ProjectileType<ForbiddenBlast>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
			}
		}
	}
}
