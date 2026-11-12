using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// Thrown mud ball: gravity, infinite pierce, places a Mud Block where it lands. Sprite is a placeholder (Snowball).
	public class MudBallProj : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.SnowBallFriendly;

		public override void SetDefaults()
		{
			Projectile.width = 12;
			Projectile.height = 12;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged; // UNVERIFIED class
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
			Projectile.timeLeft = 600;
		}

		public override void AI()
		{
			Projectile.rotation += 0.2f;
			Projectile.ai[0]++;
			if (Projectile.ai[0] > 12f)
			{
				Projectile.velocity.Y += 0.3f;
				if (Projectile.velocity.Y > 16f)
					Projectile.velocity.Y = 16f;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (oldVelocity.Y > 0f && Projectile.velocity.Y != oldVelocity.Y)
			{
				// landed on top of a block: place mud
				if (Main.myPlayer == Projectile.owner)
				{
					Point p = Projectile.Center.ToTileCoordinates();
					if (!Framing.GetTileSafely(p.X, p.Y).HasTile && WorldGen.PlaceTile(p.X, p.Y, TileID.Mud, true))
					{
						if (Main.netMode == NetmodeID.MultiplayerClient)
							NetMessage.SendTileSquare(-1, p.X, p.Y, 1);
					}
					else
					{
						Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.Center, ItemID.MudBlock);
					}
				}
				Projectile.Kill();
				return false;
			}
			if (oldVelocity.Y < 0f && Projectile.velocity.Y != oldVelocity.Y)
				Projectile.velocity.Y = 0.5f; // bounces off the underside of blocks, then falls
			if (Projectile.velocity.X != oldVelocity.X)
				Projectile.velocity.X = 0f; // falls straight down after hitting a side
			return false;
		}
	}
}
