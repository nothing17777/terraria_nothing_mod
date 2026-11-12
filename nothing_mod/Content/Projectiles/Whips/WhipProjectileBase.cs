using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Whips
{
	// Shared whip behaviour (abstract, not autoloaded). API pattern from ExampleMod ExampleWhipProjectile.
	public abstract class WhipProjectileBase : ModProjectile
	{
		// PLACEHOLDER art: vanilla Leather Whip projectile sprite.
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.BlandWhip;

		protected abstract int TagBuffType { get; }
		protected virtual int TagBuffTime => 240;
		// Fraction of damage lost after each enemy hit.
		protected abstract float MultihitPenalty { get; }

		public override void SetStaticDefaults() {
			ProjectileID.Sets.IsAWhip[Type] = true;
		}

		// Real 1.4.5.8 whip length (segments and range multiplier), from the game's whip settings.
		protected virtual int WhipSegments => 20;
		protected virtual float WhipRange => 1f;

		public override void SetDefaults() {
			Projectile.DefaultToWhip();
			Projectile.WhipSettings.Segments = WhipSegments;
			Projectile.WhipSettings.RangeMultiplier = WhipRange;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			target.AddBuff(TagBuffType, TagBuffTime);
			Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
			Projectile.damage = (int)(Projectile.damage * (1f - MultihitPenalty));
			OnWhipHit(target);
		}

		protected virtual void OnWhipHit(NPC target) { }

		protected bool HitWithTip(NPC target) {
			List<Vector2> list = new List<Vector2>();
			Projectile.FillWhipControlPoints(Projectile, list);
			Rectangle box = target.Hitbox;
			box.Inflate(16, 16);
			return box.Contains(list[list.Count - 1].ToPoint());
		}

		// ---- Shared real-game whip drawing/dust helpers (1.4.5.8 DrawWhip family, via the reference port) ----

		// Fishing-line body tint (game: per-whip colour table). Default white.
		protected virtual Color LineTint => Color.White;
		// Game: whips 1028-1035 draw one line segment fewer.
		protected virtual bool LineShortByOne => false;
		// Game: Constellation draws its line without sampling light.
		protected virtual bool LineFullbright => false;
		// Whether this whip draws the fishing-line body at all (Possession does not; others do).
		protected virtual bool DrawLineBody => true;

		// Swing progress 0..1 (game: GetWhipAnimationPercent = ai[0] / timeToFlyOut).
		protected float SwingProgress {
			get {
				Projectile.GetWhipSettings(Projectile, out float timeToFlyOut, out _, out _);
				return timeToFlyOut <= 0f ? 0f : Projectile.ai[0] / timeToFlyOut;
			}
		}

		// Swing envelope used by every trail-dust recipe: GetLerpValue(0.1,0.7,t) * GetLerpValue(0.9,0.7,t).
		protected static float Envelope(float t) {
			return Utils.GetLerpValue(0.1f, 0.7f, t, true) * Utils.GetLerpValue(0.9f, 0.7f, t, true);
		}

		// Per-update trail dust, same cadence as the game's AI_165 (runs on every extra update).
		public override void AI() {
			if (Main.dedServ)
				return;
			List<Vector2> points = new List<Vector2>();
			Projectile.FillWhipControlPoints(Projectile, points);
			if (points.Count < 3)
				return;
			float t = SwingProgress;
			WhipTrailDust(points, t, Envelope(t), Main.player[Projectile.owner].direction);
		}

		protected virtual void WhipTrailDust(List<Vector2> pts, float t, float env, int facing) { }

		protected static Vector2 SegDir(Vector2 a, Vector2 b) => (b - a).SafeNormalize(Vector2.Zero);

		// Dust in a size x size box centred on p (game: CenteredRectangle(point, (30,30)) -> NewDust(TopLeft, w, h, ...)).
		protected static Dust BoxDust(Vector2 p, int size, int type, int alpha = 0, Color color = default, float scale = 1f, Vector2 speed = default) {
			return Dust.NewDustDirect(p - new Vector2(size / 2f), size, size, type, speed.X, speed.Y, alpha, color, scale);
		}

		protected static Color MulRGBA(Color a, Color b) {
			return new Color(a.R * b.R / 255, a.G * b.G / 255, a.B * b.B / 255, a.A * b.A / 255);
		}

		public override bool PreDraw(ref Color lightColor) {
			List<Vector2> points = new List<Vector2>();
			Projectile.FillWhipControlPoints(Projectile, points);
			if (DrawLineBody)
				DrawFishingLine(points);
			DrawWhipTip(points);
			return false;
		}

		// Default (placeholder fallback): the Leather Whip routine. Real whips override this.
		protected virtual void DrawWhipTip(List<Vector2> points) {
			Main.DrawWhip_WhipBland(Projectile, points);
		}

		// Game: FishingLine texture along the control points, origin (W/2, 2), scale (1, (len+2)/H).
		protected void DrawFishingLine(List<Vector2> pts) {
			Texture2D line = TextureAssets.FishingLine.Value;
			int count = pts.Count - 1 - (LineShortByOne ? 1 : 0);
			for (int i = 0; i < count; i++) {
				Vector2 diff = pts[i + 1] - pts[i];
				float rotation = diff.ToRotation() - MathHelper.PiOver2;
				Color c = LineFullbright ? LineTint : MulRGBA(Lighting.GetColor(pts[i].ToTileCoordinates()), LineTint);
				Vector2 scale = new Vector2(1f, (diff.Length() + 2f) / line.Height);
				Main.EntitySpriteDraw(line, pts[i] - Main.screenPosition, null, c, rotation,
					new Vector2(line.Width / 2f, 2f), scale, SpriteEffects.None, 0);
			}
		}

		// Game: Texture2D.Frame(cols, rows) cell with Height -= 2, origin = cell centre (optionally shifted up by oyShift).
		protected void DrawSeg(Texture2D tex, List<Vector2> pts, int i, int cols, int rows, int row,
			float oyShift, float sx, float sy, bool flip, Color tint, Vector2? posOverride = null) {
			int cellW = tex.Width / cols;
			int cellH = tex.Height / rows;
			Rectangle frame = new Rectangle(0, row * cellH, cellW, cellH - 2);
			Vector2 origin = new Vector2(frame.Width / 2f, frame.Height / 2f - oyShift);
			Vector2 diff = pts[i + 1] - pts[i];
			float rotation = diff.ToRotation() - MathHelper.PiOver2;
			Color c = MulRGBA(Lighting.GetColor(pts[i].ToTileCoordinates()), tint);
			Vector2 pos = posOverride ?? pts[i];
			Main.EntitySpriteDraw(tex, pos - Main.screenPosition, frame, c, rotation, origin,
				new Vector2(sx, sy), flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
		}

		// TODO: Main.DrawPrettyStarSparkle does not exist in 1.4.4.9. Approximation of the game's four-point sparkle
		// with the Extra_98 star texture (two crossed additive strokes). Stroke widths/lengths are UNVERIFIED.
		public static void DrawStarSparkle(Vector2 worldPos, Color shine, float opacity, float rotation, Vector2 length, Vector2 fatness) {
			Texture2D star = TextureAssets.Extra[98].Value;
			Vector2 origin = star.Size() / 2f;
			Vector2 pos = worldPos - Main.screenPosition;
			Color c = shine with { A = 0 };
			c *= opacity;
			Main.EntitySpriteDraw(star, pos, null, c, rotation, origin, new Vector2(fatness.X * 0.5f, length.X) * 0.25f, SpriteEffects.None, 0);
			Main.EntitySpriteDraw(star, pos, null, c, rotation + MathHelper.PiOver2, origin, new Vector2(fatness.Y * 0.5f, length.Y) * 0.25f, SpriteEffects.None, 0);
			Main.EntitySpriteDraw(star, pos, null, new Color(255, 255, 255, 0) * (opacity * 0.5f), rotation, origin, new Vector2(fatness.X * 0.3f, length.X * 0.5f) * 0.25f, SpriteEffects.None, 0);
		}

		protected static float Remap(float v, float fMin, float fMax, float tMin, float tMax) {
			return MathHelper.Lerp(tMin, tMax, Utils.GetLerpValue(fMin, fMax, v, true));
		}

		// Generic whip tip (Cobwhip/Soulscourge/Vasculash): game DrawWhip_GenericWhip.
		protected void DrawGenericTip(List<Vector2> pts, int segmentCount, float startWidth, float endWidth) {
			Texture2D tex = TextureAssets.Projectile[Type].Value;
			int n = pts.Count - 1;
			int num2 = System.Math.Max(1, segmentCount - 1);
			for (int i = 0; i < n; i++) {
				if (i == 1)
					continue;
				int row;
				float oy = 0f;
				if (i == 0) { oy = 4f; row = 0; }
				else if (i == n - 1) row = segmentCount - 1;
				else row = 1 + i % (num2 - 1);
				float sx = i == 0 ? 1f : Remap(i, 5f, n * 0.75f, startWidth, endWidth);
				DrawSeg(tex, pts, i, 1, segmentCount, row, oy, sx, 1f, false, Color.White);
			}
		}
	}
}
