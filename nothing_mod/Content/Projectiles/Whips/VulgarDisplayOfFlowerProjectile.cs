using System.Collections.Generic;
using Microsoft.Xna.Framework;
using nothing_mod.Content.Buffs.Whips;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Whips
{
	// Vulgar Display of Flower (game "FlowerWhip", projectile 1032). Real sprite 38x190 = 5 rows of 38 px.
	// Proc: first enemy hit per lash is primed; the next tagged minion hit bursts petals (VulgarDisplayPetal).
	public class VulgarDisplayOfFlowerProjectile : WhipProjectileBase
	{
		public override string Texture => "nothing_mod/Content/Projectiles/Whips/VulgarDisplayOfFlowerProjectile";

		protected override int TagBuffType => ModContent.BuffType<VulgarDisplayOfFlowerDebuff>();
		protected override float MultihitPenalty => 0.15f;
		protected override int WhipSegments => 20;
		protected override float WhipRange => 1.8f;

		protected override Color LineTint => new Color(143, 215, 29, 255);
		protected override bool LineShortByOne => true;

		private bool primed;

		protected override void OnWhipHit(NPC target) {
			if (primed)
				return;
			primed = true;
			WhipProcNPC.EnableProc(target, TagBuffType, TagBuffTime);
		}

		// Game DrawWhip_FlowerWhip: like the generic tip, but middle segments only every other point and
		// the last segment's origin is shifted 8 px.
		protected override void DrawWhipTip(List<Vector2> pts) {
			Microsoft.Xna.Framework.Graphics.Texture2D tex = TextureAssets.Projectile[Type].Value;
			const int segmentCount = 5;
			int n = pts.Count - 1;
			int num2 = System.Math.Max(1, segmentCount - 1);
			for (int i = 0; i < n; i++) {
				int row;
				float oy = 0f;
				if (i == 0) { oy = 4f; row = 0; }
				else if (i == n - 1) { row = segmentCount - 1; oy = 8f; }
				else {
					row = 1 + i % (num2 - 1);
					if (i % 2 != 0)
						continue;
				}
				float sx = i == 0 ? 1f : Remap(i, 5f, n * 0.75f, 0.5f, 0.5f);
				DrawSeg(tex, pts, i, 1, segmentCount, row, oy, sx, 1f, false, Color.White);
			}
		}

		protected override void WhipTrailDust(List<Vector2> pts, float t, float env, int facing) {
			if (!(env > 0.6f) || Main.rand.NextFloat() >= env || Main.rand.NextBool(3))
				return;
			int n = pts.Count;
			for (int l = 0; l < n - 1; l += 2) {
				float prob = l / (float)n;
				if (Main.rand.NextFloat() >= prob * prob)
					continue;
				Vector2 seg = SegDir(pts[l], pts[l + 1]);
				for (int k = 0; k < 4; k++) {
					int type = k == 2 ? 166 : k == 3 ? 167 : (Main.rand.NextBool(2) ? 167 : 166);
					Dust d = BoxDust(pts[l + 1], 30, type);
					d.noGravity = !Main.rand.NextBool(3);
					if (d.noGravity)
						d.scale += 0.2f;
					d.velocity += seg * 2f;
				}
			}
		}
	}
}
