using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// Power Bomb: 5s fuse, 17.5x17.5 tile damage box, 9-tile tile-destroying radius incl. early hardmode ores.
	// Sprite is a placeholder (Bomb).
	public class PowerBombProj : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Bomb;

		static readonly int[] HardmodeOres = { TileID.Cobalt, TileID.Palladium, TileID.Mythril, TileID.Orichalcum, TileID.Adamantite, TileID.Titanium };

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Bomb);
			AIType = ProjectileID.Bomb;
			Projectile.DamageType = DamageClass.Default;
			Projectile.timeLeft = 300; // 5 second fuse
		}

		public override void PrepareBombToBlow()
		{
			Projectile.tileCollide = false;
			Projectile.alpha = 255;
			Projectile.Resize(280, 280); // 17.5 tiles
			Projectile.damage = 200;
			Projectile.knockBack = 9f;
		}

		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			for (int i = 0; i < 60; i++)
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
			for (int i = 0; i < 40; i++)
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2.5f);

			if (Projectile.owner != Main.myPlayer)
				return;
			int radius = 9;
			Point c = Projectile.Center.ToTileCoordinates();
			int minX = System.Math.Max(c.X - radius, 0), maxX = System.Math.Min(c.X + radius, Main.maxTilesX - 1);
			int minY = System.Math.Max(c.Y - radius, 0), maxY = System.Math.Min(c.Y + radius, Main.maxTilesY - 1);
			Projectile.ExplodeTiles(Projectile.Center, radius, minX, maxX, minY, maxY, Projectile.ShouldWallExplode(Projectile.Center, radius, minX, maxX, minY, maxY));

			// vanilla ExplodeTiles spares early hardmode ores; the Power Bomb breaks them.
			for (int x = minX; x <= maxX; x++)
			{
				for (int y = minY; y <= maxY; y++)
				{
					if (Vector2.Distance(new Vector2(x, y), c.ToVector2()) > radius)
						continue;
					Tile t = Main.tile[x, y];
					if (t.HasTile && System.Array.IndexOf(HardmodeOres, (int)t.TileType) >= 0 && WorldGen.CanKillTile(x, y))
					{
						WorldGen.KillTile(x, y);
						if (Main.netMode == NetmodeID.MultiplayerClient)
							NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, x, y);
					}
				}
			}
		}
		// TODO: pick-up/cancel by right-clicking within 15 tiles (1.4.5.7 feature).
	}
}
