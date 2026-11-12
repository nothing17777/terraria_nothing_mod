using System.Collections.Generic;
using Microsoft.Xna.Framework;
using nothing_mod.Content.Buffs.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Whips
{
	// Starcrash (game "MeteorWhip", projectile 1031). Real sprite 18x144 = 6 rows of 24 px.
	// Proc: the first enemy a lash hits is primed; the next tagged minion hit on it drops a meteor
	// (see WhipProcNPC / StarcrashMeteor).
	public class StarcrashProjectile : WhipProjectileBase
	{
		public override string Texture => "nothing_mod/Content/Projectiles/Whips/StarcrashProjectile";

		protected override int TagBuffType => ModContent.BuffType<StarcrashDebuff>();
		protected override float MultihitPenalty => 0.3f;
		protected override int WhipSegments => 20;
		protected override float WhipRange => 1.1f;

		protected override Color LineTint => new Color(88, 85, 155, 255);
		protected override bool LineShortByOne => true;

		private bool primed; // game: localAI[0] first-hit gate

		protected override void OnWhipHit(NPC target) {
			if (primed)
				return;
			primed = true;
			WhipProcNPC.EnableProc(target, TagBuffType, TagBuffTime);
		}

		protected override void DrawWhipTip(List<Vector2> pts) {
			Microsoft.Xna.Framework.Graphics.Texture2D tex = Terraria.GameContent.TextureAssets.Projectile[Type].Value;
			int n = pts.Count - 1;
			for (int i = 0; i < n; i++) {
				int row;
				if (i == 0) row = 0;
				else if (i == n - 1) row = 5;
				else {
					row = (int)System.Math.Round(MathHelper.Lerp(1f, 4f, (i - 1) / (float)(n - 2)));
					if (i % 3 != 0)
						continue;
				}
				DrawSeg(tex, pts, i, 1, 6, row, 0f, 1f, 1f, false, Color.White);
			}
		}

		protected override void WhipTrailDust(List<Vector2> pts, float t, float env, int facing) {
			if (!(env > 0.1f) || Main.rand.NextFloat() >= env)
				return;
			Vector2 tip = pts[pts.Count - 1];
			Vector2 dir = SegDir(pts[pts.Count - 2], tip);
			for (int i = 0; i < 4; i++) {
				if (!Main.rand.NextBool(3))
					continue;
				Dust d = BoxDust(tip, 20, 6);
				d.velocity += dir * 1.5f;
				if (!Main.rand.NextBool(3)) {
					d.velocity += dir;
					d.fadeIn = 0.7f + Main.rand.NextFloat() * 0.9f;
					d.scale = 0.7f;
					d.noGravity = true;
				}
			}
		}
	}
}
