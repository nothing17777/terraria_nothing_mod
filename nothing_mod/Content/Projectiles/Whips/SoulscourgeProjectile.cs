using System.Collections.Generic;
using Microsoft.Xna.Framework;
using nothing_mod.Content.Buffs.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Whips
{
	// Soulscourge (game "CorruptWhip", projectile 1029). Real sprite 24x168 = 6 rows of 28 px.
	public class SoulscourgeProjectile : WhipProjectileBase
	{
		public override string Texture => "nothing_mod/Content/Projectiles/Whips/SoulscourgeProjectile";

		protected override int TagBuffType => ModContent.BuffType<SoulscourgeDebuff>();
		protected override float MultihitPenalty => 0.4f;
		protected override int WhipSegments => 20;
		protected override float WhipRange => 0.95f;

		protected override Color LineTint => new Color(128, 0, 128, 255); // game: Color.Purple
		protected override bool LineShortByOne => true;

		protected override void DrawWhipTip(List<Vector2> pts) => DrawGenericTip(pts, 6, 0.5f, 0.5f);

		// No on-hit effect beyond the tag in the reference tables.
		protected override void WhipTrailDust(List<Vector2> pts, float t, float env, int facing) {
			if (!(env > 0.6f) || !Main.rand.NextBool(2))
				return;
			Vector2 tip = pts[pts.Count - 1];
			Vector2 dir = SegDir(pts[pts.Count - 2], tip);
			for (int k = 0; k < 2; k++) {
				int type = Main.rand.NextBool(2) ? 18 : 184;
				Dust d = BoxDust(tip, 30, type, 0, default, 0.7f);
				d.noGravity = type == 184 || Main.rand.NextBool(3);
				if (d.noGravity)
					d.scale += 0.3f;
				d.velocity += dir * 2f;
			}
		}
	}
}
