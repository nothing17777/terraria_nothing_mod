using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// Freeze Bomb: 3s fuse, explodes on contact with water/lava/honey, turns water within 4.2 tiles (max 57 tiles) into Ice Blocks.
	// Sprite is a placeholder (Bomb).
	public class FreezeBombProj : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Bomb;

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Bomb);
			AIType = ProjectileID.Bomb;
			Projectile.DamageType = DamageClass.Default;
			Projectile.timeLeft = 180;
			Projectile.friendly = false;
			Projectile.hostile = false;
		}

		public override void PostAI()
		{
			if (Projectile.timeLeft > 3 && Collision.WetCollision(Projectile.position, Projectile.width, Projectile.height))
				Projectile.timeLeft = 3;
		}

		public override void PrepareBombToBlow()
		{
			Projectile.tileCollide = false;
			Projectile.alpha = 255;
			Projectile.Resize(134, 134); // 4.2 tile radius
			Projectile.damage = 0;
		}

		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			for (int i = 0; i < 30; i++)
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Ice, 0f, 0f, 100, default, 1.5f);

			if (Projectile.owner != Main.myPlayer)
				return;
			Point c = Projectile.Center.ToTileCoordinates();
			int count = 0;
			for (int x = c.X - 5; x <= c.X + 5 && count < 57; x++)
			{
				for (int y = c.Y - 5; y <= c.Y + 5 && count < 57; y++)
				{
					if (!WorldGen.InWorld(x, y) || Vector2.Distance(new Vector2(x, y), c.ToVector2()) > 4.2f)
						continue;
					Tile t = Main.tile[x, y];
					if (t.HasTile || t.LiquidAmount == 0 || t.LiquidType != LiquidID.Water)
						continue;
					t.LiquidAmount = 0;
					WorldGen.PlaceTile(x, y, TileID.IceBlock, true, true);
					count++;
				}
			}
			if (count > 0 && Main.netMode == NetmodeID.MultiplayerClient)
				NetMessage.SendTileSquare(-1, c.X, c.Y, 12);
		}
		// TODO: Shimmer bounce (fuse -1s per bounce), exploding-inside-liquid rule, 1 damage to thrower.
	}
}
