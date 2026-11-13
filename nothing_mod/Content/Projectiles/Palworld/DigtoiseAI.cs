using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Projectiles.Palworld
{
	// Shared Digtoise behaviour (wiki). ai[0]: 0 follow the player, 1 mine left, 2 mine right, 3 mine down. ai[1]: ticks without progress. localAI[0]: pick timer.
	// Mines 3 tiles at once (a 3-tall column ahead, or a 3-wide row below), pickaxe power 26% (Trusty: strongest pickaxe in the inventory), mining speed 20.
	public static class DigtoiseAI
	{
		public const int BasePower = 26;
		public const int MiningSpeed = 20;

		// Left-click order: cursor below the pet -> dig down, else left/right; clicking while mining stops it.
		public static void Order(Player player, int petType)
		{
			foreach (Projectile p in Main.ActiveProjectiles)
			{
				if (p.owner != player.whoAmI || p.type != petType)
					continue;
				if (p.ai[0] != 0f)
					p.ai[0] = 0f;
				else
				{
					Vector2 d = Main.MouseWorld - p.Center;
					p.ai[0] = d.Y > System.Math.Abs(d.X) ? 3f : d.X < 0f ? 1f : 2f;
				}
				p.ai[1] = 0f;
				p.localAI[0] = 0f;
				p.netUpdate = true;
				return;
			}
		}

		private static int PickPower(Player owner, bool trusty)
		{
			if (!trusty)
				return BasePower;
			int best = BasePower;
			foreach (Item it in owner.inventory)
				if (!it.IsAir && it.pick > best)
					best = it.pick;
			return best;
		}

		public static void Run(Projectile p, Player owner, bool trusty)
		{
			if (p.ai[0] == 0f)
			{
				p.tileCollide = false;
				SimpleMinionAI.MoveToward(p, owner.Center + new Vector2(-owner.direction * 40f, -30f), 8f, 20f);
				p.frame = 3; // shell/idle frame (layout UNVERIFIED)
				if (System.Math.Abs(p.velocity.X) > 0.3f)
					p.spriteDirection = p.velocity.X >= 0 ? 1 : -1;
				return;
			}

			// Mining mode: walks on the ground; only moves horizontally or downwards.
			p.tileCollide = true;
			p.velocity.Y = System.Math.Min(p.velocity.Y + 0.4f, 10f);
			int footRow = (int)((p.Bottom.Y - 1f) / 16f);
			int cx = (int)(p.Center.X / 16f);
			bool horizontal = p.ai[0] != 3f;
			int dir = p.ai[0] == 1f ? -1 : 1;
			int frontX = (int)((p.Center.X + dir * 18f) / 16f);
			System.Span<Point> targets = stackalloc Point[3];
			if (horizontal)
			{
				p.spriteDirection = dir;
				for (int i = 0; i < 3; i++)
					targets[i] = new Point(frontX, footRow - i);
			}
			else
			{
				p.velocity.X = 0f;
				for (int i = 0; i < 3; i++)
					targets[i] = new Point(cx - 1 + i, footRow + 1);
			}

			bool blocked = false;
			foreach (Point t in targets)
			{
				Tile tile = Framing.GetTileSafely(t.X, t.Y);
				if (tile.HasTile && Main.tileSolid[tile.TileType])
					blocked = true;
			}

			if (horizontal)
				p.velocity.X = blocked ? 0f : dir * 1.2f; // walking speed UNVERIFIED

			if (blocked)
			{
				p.ai[1]++;
				p.localAI[0]++;
				p.frame = 0;
				if (p.localAI[0] >= MiningSpeed && p.owner == Main.myPlayer)
				{
					p.localAI[0] = 0f;
					int power = PickPower(owner, trusty);
					foreach (Point t in targets)
					{
						Tile tile = Framing.GetTileSafely(t.X, t.Y);
						if (tile.HasTile && Main.tileSolid[tile.TileType])
							owner.PickTile(t.X, t.Y, power);
					}
				}
				if (p.ai[1] > MiningSpeed * 10 && p.owner == Main.myPlayer) // cannot dig it: give up
				{
					p.ai[0] = 0f;
					p.ai[1] = 0f;
					p.netUpdate = true;
				}
			}
			else
			{
				p.ai[1] = 0f;
				p.frameCounter++;
				if (p.frameCounter >= 8)
				{
					p.frameCounter = 0;
					p.frame = (p.frame + 1) % 3;
				}
				// Horizontal: stop at a downward slope or gap (nothing solid under the feet ahead).
				if (horizontal && p.owner == Main.myPlayer)
				{
					Tile below = Framing.GetTileSafely(frontX, footRow + 1);
					Tile below2 = Framing.GetTileSafely(frontX - dir, footRow + 1);
					if (!(below.HasTile && Main.tileSolid[below.TileType]) && !(below2.HasTile && Main.tileSolid[below2.TileType]) && p.velocity.Y > 3f)
					{
						p.ai[0] = 0f;
						p.netUpdate = true;
					}
				}
			}
		}
	}
}
