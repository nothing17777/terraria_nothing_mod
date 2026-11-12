using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// Acorn Slingshot ammo projectile. Sprite is a placeholder (Seed).
	public class AcornShot : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Seed;

		public override void SetDefaults()
		{
			Projectile.width = 12;
			Projectile.height = 12;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.tileCollide = true;
		}

		public override void AI()
		{
			Projectile.rotation += 0.25f * System.Math.Sign(Projectile.velocity.X == 0 ? 1 : Projectile.velocity.X);
			Projectile.ai[0]++;
			if (Projectile.ai[0] > 8f)
			{
				Projectile.velocity.Y += 0.25f;
				if (Projectile.velocity.Y > 14f)
					Projectile.velocity.Y = 14f;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			// Each bounce keeps 40% of speed (wiki).
			if (Projectile.velocity.X != oldVelocity.X)
				Projectile.velocity.X = -oldVelocity.X * 0.4f;
			if (Projectile.velocity.Y != oldVelocity.Y)
				Projectile.velocity.Y = -oldVelocity.Y * 0.4f;
			if (Projectile.velocity.Length() < 1.2f && oldVelocity.Y > 0f)
				Projectile.Kill();
			return false;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			Projectile.ai[1] = 1f; // hit an enemy: drops as an item instead of planting
		}

		public override void OnKill(int timeLeft)
		{
			if (Main.myPlayer != Projectile.owner)
				return;
			bool planted = false;
			if (Projectile.ai[1] == 0f)
			{
				Point p = Projectile.Center.ToTileCoordinates();
				for (int dy = 0; dy <= 1 && !planted; dy++)
				{
					int x = p.X, y = p.Y + dy; // y is the empty tile that would hold the sapling
					Tile ground = Framing.GetTileSafely(x, y + 1);
					Tile here = Framing.GetTileSafely(x, y);
					if (!here.HasTile && !Framing.GetTileSafely(x, y - 1).HasTile && ground.HasTile && !ground.IsHalfBlock && ground.Slope == Terraria.ID.SlopeType.Solid
						&& (ground.TileType == TileID.Dirt || ground.TileType == TileID.Grass) && here.LiquidAmount == 0)
					{
						if (WorldGen.PlaceTile(x, y, TileID.Saplings, true, false, -1, 0))
						{
							planted = true;
							if (Main.netMode == NetmodeID.MultiplayerClient)
								NetMessage.SendTileSquare(-1, x, y, 1);
						}
					}
				}
			}
			if (!planted)
				Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.Center, ItemID.Acorn);
		}
	}
}
