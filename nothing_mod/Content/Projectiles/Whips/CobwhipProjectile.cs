using System.Collections.Generic;
using Microsoft.Xna.Framework;
using nothing_mod.Content.Buffs.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Whips
{
	// Cobwhip (game projectile 1028). Real 1.4.5.8 sprite 24x168 = 6 rows of 28 px.
	public class CobwhipProjectile : WhipProjectileBase
	{
		public override string Texture => "nothing_mod/Content/Projectiles/Whips/CobwhipProjectile";

		protected override int TagBuffType => ModContent.BuffType<CobwhipDebuff>();
		protected override float MultihitPenalty => 0.6f;
		protected override int WhipSegments => 12;
		protected override float WhipRange => 0.75f;

		protected override Color LineTint => new Color(128, 128, 128, 255); // game: Color.Gray
		protected override bool LineShortByOne => true;

		// Game (Projectile.TryDoingOnHitEffects case 1028): hitting grants the spider buff (180 ticks) and
		// summons one Cobweb Spider (damage 2, knockback 0) at the enemy's centre if none exists yet.
		protected override void OnWhipHit(NPC target) {
			Player owner = Main.player[Projectile.owner];
			owner.AddBuff(ModContent.BuffType<CobwhipSpiderBuff>(), 180);
			if (Projectile.owner != Main.myPlayer)
				return;
			int spider = ModContent.ProjectileType<CobwhipSpider>();
			for (int i = 0; i < Main.maxProjectiles; i++) {
				Projectile p = Main.projectile[i];
				if (p.active && p.owner == Projectile.owner && p.type == spider)
					return;
			}
			Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center, Vector2.Zero, spider, 2, 0f, Projectile.owner);
		}

		protected override void DrawWhipTip(List<Vector2> pts) => DrawGenericTip(pts, 6, 1f, 1f);

		protected override void WhipTrailDust(List<Vector2> pts, float t, float env, int facing) {
			if (!(env > 0.15f) || Main.rand.NextFloat() >= env || !Main.rand.NextBool(2))
				return;
			Vector2 tip = pts[pts.Count - 1];
			Vector2 dir = SegDir(pts[pts.Count - 2], tip);
			Dust d = BoxDust(tip, 30, 30, 0, default, 0.7f);
			d.noGravity = Main.rand.NextBool(3);
			if (d.noGravity)
				d.scale += 0.3f;
			d.velocity += dir * 2f;
		}
	}
}
