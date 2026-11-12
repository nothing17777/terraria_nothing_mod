using System.Collections.Generic;
using Microsoft.Xna.Framework;
using nothing_mod.Content.Buffs.Whips;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Whips
{
	// Slime Whip (game projectile 1104). Real sprite 14x140 = 5 rows of 28 px.
	public class SlimeWhipProjectile : WhipProjectileBase
	{
		public override string Texture => "nothing_mod/Content/Projectiles/Whips/SlimeWhipProjectile";

		protected override int TagBuffType => ModContent.BuffType<SlimeWhipDebuff>();
		protected override float MultihitPenalty => 0.6f;
		protected override int WhipSegments => 36;
		protected override float WhipRange => 0.75f;

		// game: Color(78,136,255) halved on all four channels
		protected override Color LineTint => new Color(39, 68, 127, 127);

		protected override void OnWhipHit(NPC target) {
			target.AddBuff(BuffID.Slimed, 180); // whole whip, 3s
			// 1.4.5 fix: no ignition while the enemy is underwater.
			if (!target.wet && HitWithTip(target))
				target.AddBuff(BuffID.OnFire, 240 + Main.rand.Next(121)); // tip only, 3-5s
		}

		// Game DrawWhip_SlimeWhip: first row 0, last row 4, middle rows lerp(1,3) but only every third point;
		// everything drawn at half translucency (127,127,127,127).
		protected override void DrawWhipTip(List<Vector2> pts) {
			Microsoft.Xna.Framework.Graphics.Texture2D tex = TextureAssets.Projectile[Type].Value;
			int n = pts.Count - 1;
			Color tint = new Color(127, 127, 127, 127);
			for (int i = 0; i < n; i++) {
				int row;
				if (i == 0) row = 0;
				else if (i == n - 1) row = 4;
				else {
					row = (int)System.Math.Round(MathHelper.Lerp(1f, 3f, (i - 1) / (float)(n - 2)));
					if (i % 3 != 0)
						continue;
				}
				DrawSeg(tex, pts, i, 1, 5, row, 0f, 1f, 1f, false, tint);
			}
		}

		// Blue slime droplets shed along the lash.
		protected override void WhipTrailDust(List<Vector2> pts, float t, float env, int facing) {
			if (!(env > 0.15f) || Main.rand.NextFloat() >= env || !Main.rand.NextBool(2))
				return;
			int n = pts.Count;
			for (int k = n - 1; k >= 1; k -= 3) {
				if (k != n - 1 && !Main.rand.NextBool(20))
					continue;
				Vector2 seg = SegDir(pts[k - 1], pts[k]);
				Dust d = BoxDust(pts[k], 30, 4, 84, new Color(78, 136, 255, 127), 1f + Main.rand.NextFloat() * 0.3f);
				d.velocity += seg * Main.rand.NextFloat() * 5f;
				if (Main.rand.NextBool(4))
					d.noGravity = true;
				else {
					d.velocity = d.velocity * 0.3f + Projectile.velocity * 0.5f;
					d.scale -= 0.4f;
				}
			}
		}
	}
}
