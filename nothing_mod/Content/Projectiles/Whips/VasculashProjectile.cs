using System.Collections.Generic;
using Microsoft.Xna.Framework;
using nothing_mod.Content.Buffs.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Whips
{
	// Vasculash (game "CrimsonWhip", projectile 1030). Real sprite 30x192 = 6 rows of 32 px.
	public class VasculashProjectile : WhipProjectileBase
	{
		public override string Texture => "nothing_mod/Content/Projectiles/Whips/VasculashProjectile";

		protected override int TagBuffType => ModContent.BuffType<VasculashDebuff>();
		protected override float MultihitPenalty => 0.4f; // reference table: falloff 0.60
		protected override int WhipSegments => 20;
		protected override float WhipRange => 0.85f;

		protected override Color LineTint => new Color(255, 0, 0, 255); // game: Color.Red
		protected override bool LineShortByOne => true;

		protected override void DrawWhipTip(List<Vector2> pts) => DrawGenericTip(pts, 6, 0.5f, 0.5f);

		// Blood-drip dust along the back half of the whip (dust 115 / 5), plus the owner's velocity.
		protected override void WhipTrailDust(List<Vector2> pts, float t, float env, int facing) {
			if (!(env > 0.6f) || Main.rand.NextBool(4))
				return;
			int n = pts.Count;
			Player owner = Main.player[Projectile.owner];
			for (int k = n / 2; k < n; k++) {
				if (!Main.rand.NextBool(6) && k != n - 1)
					continue;
				int count = k == n - 1 ? 4 : 1;
				Vector2 seg = SegDir(pts[k - 1], pts[k]);
				for (int m = 0; m < count; m++) {
					int type = Main.rand.NextBool(2) ? 115 : 5;
					float scale = 0.3f + 0.3f * Main.rand.NextFloat();
					Dust d = BoxDust(pts[k], 30, type, 0, default, scale);
					d.velocity += seg * 3f * Main.rand.NextFloat();
					d.velocity *= 0.5f;
					d.velocity.X += facing * 2f;
					d.velocity += new Vector2(owner.velocity.X * Main.rand.NextFloat(), owner.velocity.Y * Main.rand.NextFloat());
					d.fadeIn = d.scale * 1.1f;
					if (m > 0)
						d.alpha = Main.rand.Next(200);
				}
			}
		}
	}
}
