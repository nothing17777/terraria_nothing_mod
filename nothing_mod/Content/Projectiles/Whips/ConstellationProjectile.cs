using System.Collections.Generic;
using Microsoft.Xna.Framework;
using nothing_mod.Content.Buffs.Whips;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Whips
{
	// Constellation (game "ConstellationWhip", projectile 1034). Real sprite 22x216 = 6 rows of 36 px.
	// Tagged minion hits grow the cosmic energy (proc window, see WhipProcNPC); a later lash hit on that
	// enemy scatters stars along the whip and spends every window.
	public class ConstellationProjectile : WhipProjectileBase
	{
		public override string Texture => "nothing_mod/Content/Projectiles/Whips/ConstellationProjectile";

		protected override int TagBuffType => ModContent.BuffType<ConstellationDebuff>();
		protected override float MultihitPenalty => 0.1f;
		protected override int WhipSegments => 20;
		protected override float WhipRange => 2.2f;
		protected override int TagBuffTime => 480; // game: the only 8 s tag

		// game: hslToRgb(0.6, 1, 0.5, 20) * 0.6 = (0, 61, 153, 12), drawn without sampling light
		protected override Color LineTint => new Color(0, 61, 153, 12);
		protected override bool LineFullbright => true;
		protected override bool LineShortByOne => true;

		private bool scattered; // game: localAI[0]

		protected override void OnWhipHit(NPC target) {
			if (scattered || !WhipProcNPC.HasProc(target, TagBuffType))
				return;
			scattered = true;
			WhipProcNPC.ClearAllProcs(TagBuffType);
			if (Projectile.owner != Main.myPlayer)
				return;

			// ConstellationWhip_SpawnConstellationStarsTowards: one star per 3 control points, leaving the whip
			// perpendicular to its curve at a random speed (can be negative = other side).
			List<Vector2> pts = new List<Vector2>();
			Projectile.FillWhipControlPoints(Projectile, pts);
			int starDamage = (int)((int)(Projectile.damage * 2) * 0.22f);
			int owner = Main.player[Projectile.owner].direction;
			for (int i = 2; i < pts.Count - 1; i += 3) {
				Vector2 d = (pts[i + 1] - pts[i]).SafeNormalize(Vector2.UnitX);
				float s = (1f + Main.rand.NextFloatDirection() * 3f) * (owner >= 0 ? 1f : -1f);
				Vector2 vel = new Vector2(d.Y, -d.X) * s;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), pts[i], vel,
					ModContent.ProjectileType<ConstellationStar>(), starDamage, Projectile.knockBack, Projectile.owner);
			}
		}

		// Game DrawWhip_Constellation: generic layout (skip point 1, row 1+i%4, tail row 5), middle segments
		// translucent (255,255,255,50) at half width, tail twice as fat, pretty-star sparkle every 3rd point.
		protected override void DrawWhipTip(List<Vector2> pts) {
			Microsoft.Xna.Framework.Graphics.Texture2D tex = TextureAssets.Projectile[Type].Value;
			const int segmentCount = 6;
			int num2 = System.Math.Max(1, segmentCount - 1);
			int n = pts.Count - 1;
			float prog = SwingProgress;
			Color shine = Main.hslToRgb(0.6f, 1f, 0.5f);
			for (int i = 0; i < n; i++) {
				if (i == 1)
					continue;
				float fat = 1f;
				int row;
				float oy = 0f;
				if (i == 0) { oy = 4f; row = 0; fat = 0f; }
				else if (i == n - 1) { row = segmentCount - 1; fat = 2f; }
				else row = 1 + i % (num2 - 1);
				float sx = 1f;
				fat *= 1f + i * 0.03f;
				Color tint = Color.White;
				if (i != 0 && i != n - 1) {
					tint = new Color(255, 255, 255, 50);
					sx *= 0.5f;
				}
				DrawSeg(tex, pts, i, 1, segmentCount, row, oy, sx, 1f, false, tint);
				if ((i + 1) % 3 == 0) {
					DrawStarSparkle(pts[i], shine, 1f - prog * 0.5f, MathHelper.PiOver2,
						new Vector2(Remap(prog, 0f, 1f, 2f, 0f), Remap(prog, 0f, 1f, 4f, 0f)), new Vector2(fat, fat));
				}
			}
		}

		protected override void WhipTrailDust(List<Vector2> pts, float t, float env, int facing) {
			if (!(env > 0.5f) || Main.rand.NextBool(3))
				return;
			int n = pts.Count;
			int idx = n - 10 + Main.rand.Next(10);
			if (idx < 1 || idx >= n)
				return;
			Vector2 p = pts[idx];
			Dust d = BoxDust(p, 30, 135, 100, Color.White, 1.5f);
			d.position = p;
			d.fadeIn = 0.3f;
			d.noGravity = true;
			d.velocity *= 0.5f;
			d.velocity += SegDir(pts[idx - 1], p).RotatedBy(facing * MathHelper.PiOver2);
			d.velocity *= 0.5f;
		}
	}
}
