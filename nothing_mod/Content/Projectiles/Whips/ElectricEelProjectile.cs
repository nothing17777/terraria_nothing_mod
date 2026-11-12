using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using nothing_mod.Content.Buffs.Whips;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Whips
{
	// Electric Eel (game "EelWhip", projectile 1033). Real sprite 22x120 = 5 rows of 24 px, plus the game's
	// additive glow mask (GlowMask[358], shipped as ElectricEelProjectile_Glow.png).
	// Proc: every lash hit primes the enemy; the next tagged minion hit consumes it (see WhipProcNPC).
	public class ElectricEelProjectile : WhipProjectileBase
	{
		public override string Texture => "nothing_mod/Content/Projectiles/Whips/ElectricEelProjectile";

		protected override int TagBuffType => ModContent.BuffType<ElectricEelDebuff>();
		protected override float MultihitPenalty => 0.05f;
		protected override int WhipSegments => 45;
		protected override float WhipRange => 2.0f;

		protected override Color LineTint => new Color(0, 0, 255, 255); // game: Color.Blue
		protected override bool LineShortByOne => true;

		protected override void OnWhipHit(NPC target) {
			WhipProcNPC.EnableProc(target, TagBuffType, TagBuffTime); // no first-hit gate for the eel
		}

		// Game DrawWhip_EelWhip: skip the first point, second segment gets a sideways wiggle
		// (-6 * triangle(progress)), rows by position (1 / 2 / 3), tail row 4, width 0.5->1,
		// glow mask drawn additively on the same rectangle; flipped when spriteDirection == 1.
		protected override void DrawWhipTip(List<Vector2> pts) {
			Texture2D tex = TextureAssets.Projectile[Type].Value;
			Texture2D glow = ModContent.Request<Texture2D>("nothing_mod/Content/Projectiles/Whips/ElectricEelProjectile_Glow").Value;
			bool flip = Projectile.spriteDirection == 1;
			float prog = SwingProgress;
			int n = pts.Count - 1;
			for (int i = 1; i < n; i++) {
				int row;
				float shift = 0f;
				if (i == 1) {
					row = 0;
					float tri = prog <= 0.5f ? prog * 2f : 2f - prog * 2f; // Utils.MultiLerp(p, 0, 1, 0)
					shift = -6f * MathHelper.Clamp(tri, 0f, 1f);
				}
				else if (i == n - 1) row = 4;
				else row = i < 25 ? 1 : i < 35 ? 2 : 3;

				Vector2 diff = pts[i + 1] - pts[i];
				Vector2 pos = pts[i] + diff.SafeNormalize(Vector2.Zero) * shift;
				float sx = Remap(i, 2f, n - 5f, 0.5f, 1f);
				DrawSeg(tex, pts, i, 1, 5, row, 0f, sx, 1f, flip, Color.White, pos);
				DrawGlow(glow, pts, i, row, sx, flip, pos);
			}
		}

		private void DrawGlow(Texture2D glow, List<Vector2> pts, int i, int row, float sx, bool flip, Vector2 pos) {
			int cellH = glow.Height / 5;
			Rectangle frame = new Rectangle(0, row * cellH, glow.Width, cellH - 2);
			float rotation = (pts[i + 1] - pts[i]).ToRotation() - MathHelper.PiOver2;
			Main.EntitySpriteDraw(glow, pos - Main.screenPosition, frame, new Color(255, 255, 255, 0), rotation,
				frame.Size() / 2f, new Vector2(sx, 1f), flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
		}

		// Electric sparks along the outer part of the whip (dust 217 / 226).
		protected override void WhipTrailDust(List<Vector2> pts, float t, float env, int facing) {
			int n = pts.Count;
			for (int k = 10; k < n - 1; k++) {
				if (!Main.rand.NextBool(5) || Main.rand.NextFloat() > t)
					continue;
				Vector2 seg = SegDir(pts[k], pts[k + 1]);
				if (Main.rand.NextBool(3)) {
					float ang = Main.rand.NextFloat() * MathHelper.Pi - MathHelper.PiOver2;
					float mag = Main.rand.Next(3, 8) * 2f;
					Dust d = BoxDust(pts[k + 1], 30, 217, 100, default, 1.4f, new Vector2((float)System.Math.Cos(ang), (float)System.Math.Sin(ang)) * mag);
					d.noGravity = true;
					d.noLight = true;
					d.velocity /= 4f;
					d.velocity += seg * 3f;
				}
				else {
					if (!Main.rand.NextBool(4))
						continue;
					Dust d = BoxDust(pts[k + 1], 30, 226, 0, default, 0.7f);
					d.noGravity = Main.rand.NextBool(3);
					if (d.noGravity)
						d.scale += 0.3f;
					d.velocity += seg.RotatedBy(Main.rand.NextFloatDirection() * 0.19634955f) * 2f;
				}
			}
		}
	}
}
