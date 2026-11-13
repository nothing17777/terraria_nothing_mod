using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Projectiles.Palworld
{
	// Digtoise pet. Left click with the item orders it to mine (see DigtoiseAI); it follows the player otherwise.
	public class DigtoisePet : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			Main.projFrames[Type] = 11;
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

		public override bool? CanCutTiles() => false;
		public override bool OnTileCollide(Vector2 oldVelocity) => false;

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
			{
				Projectile.Center = owner.Center;
				Projectile.ai[0] = 0f;
			}
			DigtoiseAI.Run(Projectile, owner, false);
		}
	}
}
