using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Projectiles.Palworld
{
	// Digtoise. Simplified follower. TODO: mining on attack/direct, recall, petting.
	public class DigtoisePet : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.BabySlime;

		public override void SetStaticDefaults()
		{
			Main.projPet[Type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 24;
			Projectile.height = 24;
			Projectile.penetrate = -1;
			Projectile.netImportant = true;
			Projectile.timeLeft *= 5;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
		}

		public override bool? CanDamage() => false;

		public override void AI()
		{
			Player owner = Main.player[Projectile.owner];
			if (!owner.active || owner.dead)
			{
				Projectile.Kill();
				return;
			}
			Projectile.timeLeft = 2;
			if (Projectile.Distance(owner.Center) > 2000f)
				Projectile.Center = owner.Center;
			SimpleMinionAI.MoveToward(Projectile, owner.Center + new Vector2(-owner.direction * 40f, -30f), 8f, 20f);
			Projectile.spriteDirection = Projectile.velocity.X >= 0 ? 1 : -1;
		}
	}
}
