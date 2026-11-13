using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.DeadCells
{
	// One pillar of Flint's wave of fire. 13 pillars, 2 tiles apart, each lasts 15 ticks (0.25 s), the next appears 2 ticks later (wiki: 0.03 s).
	// ai[0] = direction (+1/-1), ai[1] = pillar index (-1 = the item's first pillar, whose surface search starts at its spawn point).
	public class FlintShot : ModProjectile
	{
		private const int Pillars = 13;
		private const int Life = 15;

		public override void SetStaticDefaults()
		{
			Main.projFrames[Type] = 8;
		}

		public override void SetDefaults()
		{
			Projectile.width = 24;
			Projectile.height = 90;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = 4; // wiki: each pillar can hit 4 enemies
			Projectile.timeLeft = Life + 1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 25; // wiki: 25 ticks of local invincibility
			Projectile.hide = false;
		}

		public override bool ShouldUpdatePosition() => false;

		// Wiki (FindBlockSurface): from 10 tiles down; if the start tile is solid (not a platform) search 10 tiles up for a solid tile with air above.
		private static bool FindSurface(int tx, int ty, out int surfaceY)
		{
			surfaceY = ty;
			if (!WorldGen.InWorld(tx, ty, 10))
				return false;
			Tile start = Framing.GetTileSafely(tx, ty);
			if (start.HasUnactuatedTile && Main.tileSolid[start.TileType] && !Main.tileSolidTop[start.TileType])
			{
				for (int k = 0; k <= 10; k++)
				{
					int y = ty - k;
					Tile t = Framing.GetTileSafely(tx, y);
					Tile above = Framing.GetTileSafely(tx, y - 1);
					bool solid = t.HasUnactuatedTile && (Main.tileSolid[t.TileType] || Main.tileSolidTop[t.TileType]);
					bool airAbove = !(above.HasUnactuatedTile && Main.tileSolid[above.TileType]);
					if (solid && airAbove || k == 10)
					{
						surfaceY = y;
						return true;
					}
				}
				return false;
			}
			for (int k = 0; k <= 10; k++)
			{
				int y = ty + k;
				Tile t = Framing.GetTileSafely(tx, y);
				if (t.HasUnactuatedTile && (Main.tileSolid[t.TileType] || Main.tileSolidTop[t.TileType]))
				{
					surfaceY = y;
					return true;
				}
			}
			return false;
		}

		public override void OnSpawn(Terraria.DataStructures.IEntitySource source)
		{
			if (Projectile.owner != Main.myPlayer)
				return;
			int dir = (int)Projectile.ai[0];
			int tx = (int)(Projectile.Center.X / 16f);
			int ty = (int)(Projectile.Center.Y / 16f);
			if (!FindSurface(tx, ty, out int sy))
			{
				Projectile.Kill();
				return;
			}
			float bottom = sy * 16f;
			Projectile.Bottom = new Vector2(tx * 16f + 8f, bottom);
			Projectile.ai[1] += 1f; // this pillar's index (first is 0)
			float index = Projectile.ai[1];
			Projectile.height = (int)(80 + 32 * index / (Pillars - 1)); // wiki: ~5 tiles growing to ~7 tiles
			Projectile.Bottom = new Vector2(tx * 16f + 8f, bottom);
			Projectile.netUpdate = true;
		}

		public override void AI()
		{
			Projectile.localAI[0]++;
			Projectile.frame = (int)(Projectile.localAI[0] * Main.projFrames[Type] / (Life + 1f));
			if (Projectile.frame >= Main.projFrames[Type])
				Projectile.frame = Main.projFrames[Type] - 1;
			Lighting.AddLight(Projectile.Center, 1f, 0.8f, 0.3f);
			if (Main.rand.NextBool(3))
			{
				Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, -2f, 100, default, 1.3f);
				d.noGravity = true;
			}
			// After 2 ticks the owner spawns the next pillar, 2 tiles ahead, searching from this pillar's surface.
			if (Projectile.owner == Main.myPlayer && Projectile.localAI[0] == 2f && Projectile.ai[1] < Pillars - 1)
			{
				int dir = (int)Projectile.ai[0];
				Vector2 next = new Vector2(Projectile.Center.X + dir * 32f, Projectile.Bottom.Y - 8f);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), next, Vector2.Zero, Type, Projectile.damage, Projectile.knockBack, Projectile.owner, dir, Projectile.ai[1]);
			}
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D tex = Terraria.GameContent.TextureAssets.Projectile[Type].Value;
			int fh = tex.Height / Main.projFrames[Type];
			Rectangle src = new Rectangle(0, Projectile.frame * fh, tex.Width, fh);
			float scale = Projectile.height / (float)tex.Width; // sprite is a horizontal flame; rotated to stand upright
			Main.EntitySpriteDraw(tex, Projectile.Bottom - Main.screenPosition, src, Color.White, -MathHelper.PiOver2, new Vector2(0f, fh / 2f), new Vector2(scale, 0.8f), SpriteEffects.None, 0);
			return false;
		}
	}
}
