using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using nothing_mod.Content.Buffs.Whips;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Whips
{
	// Possession (Moon Lord whip). Behavior and drawing follow the 1.4.5.8 game, as documented by the
	// Open-Terraria reference port: 4x7 sprite grid, per-segment rows, 1.25x vertical stretch, a random
	// tail frame picked once per lash, and three extra lashes fired during each swing.
	public class PossessionProjectile : WhipProjectileBase
	{
		// Real 1.4.5.8 sprite (72x210), extracted from the installed game.
		public override string Texture => "nothing_mod/Content/Projectiles/Whips/PossessionProjectile";

		protected override int TagBuffType => ModContent.BuffType<PossessionDebuff>();
		protected override float MultihitPenalty => 0.05f;
		protected override int WhipSegments => 30;
		protected override float WhipRange => 2.2f;

		private const int Cols = 4, Rows = 7;
		private int tailRow;

		public override void OnSpawn(Terraria.DataStructures.IEntitySource source) {
			tailRow = 4 + Main.rand.Next(3); // rows 4..6, chosen once (game: localAI[1] = 1 + Next(3))
		}

		public override void AI() {
			// Extra lashes: while ai[2] < 10 this is a "mother" whip. Each time the swing progress passes
			// another third, it fires one more lash in the same direction (3 extra lashes in total).
			if (Projectile.ai[2] >= 10f || Projectile.owner != Main.myPlayer)
				return;

			Player owner = Main.player[Projectile.owner];
			float swingTime = owner.itemAnimationMax * Projectile.MaxUpdates;
			if (swingTime <= 0f)
				return;
			float progress = Projectile.ai[0] / swingTime;
			if (Projectile.ai[2] < progress * 3f) {
				Projectile.ai[2] += 1f;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), owner.Center, Projectile.velocity, Type,
					Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f, 10f + Projectile.ai[2]);
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			base.OnHitNPC(target, hit, damageDone);
			SpawnHitEffect(target);
			// Arm one vision for the next minion hit on this enemy.
			target.GetGlobalNPC<PossessionVisionNPC>().PendingVisions = System.Math.Min(3, target.GetGlobalNPC<PossessionVisionNPC>().PendingVisions + 1);
		}

		// Real hit effect (game's Spawn_MoonLordWhip): two short sparkle strokes along the swing direction
		// plus a mirrored pair of dust 229 sparks. Direction gets a small random wobble.
		private void SpawnHitEffect(NPC target) {
			if (Main.netMode == NetmodeID.Server)
				return;

			float wobble = Main.rand.NextFloatDirection() * MathHelper.TwoPi * 0.1f;
			Vector2 dir = Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(wobble);
			Vector2 hit = target.position + new Vector2(Main.rand.NextFloat(target.width), Main.rand.NextFloat(target.height));

			const int life = 30;
			Vector2 velocity = dir * 4f;
			Vector2 start = hit - velocity * life * 0.25f;
			AddStroke(start, velocity, life, Main.hslToRgb(0.3f, 0.6f, 0.7f) * 0.5f, 1.1f);
			AddStroke(start, velocity, life, Main.hslToRgb(0.2f, 1f, 0.4f), 0.7f);

			for (int side = 1; side >= -1; side -= 2) {
				Vector2 v = side * (velocity.RotatedBy(Main.rand.NextFloatDirection() * MathHelper.TwoPi * 0.025f) * Main.rand.NextFloat());
				Dust dust = Dust.NewDustPerfect(hit + v * 4f, 229, v, 0, default, 1.2f);
				dust.noGravity = true;
			}
		}

		private static void AddStroke(Vector2 position, Vector2 velocity, int life, Color color, float scale) {
			PrettySparkleParticle particle = new PrettySparkleParticle {
				ColorTint = color,
				LocalPosition = position,
				Velocity = velocity,
				Scale = new Vector2(scale * 1.5f, scale * 0.5f),
				Rotation = velocity.ToRotation(),
				FadeInNormalizedTime = 0.1f,
				FadeOutNormalizedTime = 0.9f,
				TimeToLive = life,
			};
			Main.ParticleSystem_World_OverPlayers.Add(particle);
		}

		public override bool PreDraw(ref Color lightColor) {
			List<Vector2> points = new List<Vector2>();
			Projectile.FillWhipControlPoints(Projectile, points);

			Texture2D texture = TextureAssets.Projectile[Type].Value;
			int cellW = texture.Width / Cols;
			int cellH = texture.Height / Rows;
			SpriteEffects effects = Projectile.spriteDirection < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			int last = points.Count - 2;

			for (int i = 0; i <= last; i++) {
				int row;
				float shiftY = 0f;
				if (i == 0) {
					row = 0;
					shiftY = 4f;
				}
				else if (i == last)
					row = tailRow;
				else
					row = 1 + i % 3;

				Rectangle frame = new Rectangle(0, row * cellH, cellW, cellH);
				Vector2 origin = new Vector2(cellW / 2f, cellH / 2f - shiftY);
				Vector2 diff = points[i + 1] - points[i];
				float rotation = diff.ToRotation() - MathHelper.PiOver2;
				Color color = Lighting.GetColor(points[i].ToTileCoordinates());
				Main.EntitySpriteDraw(texture, points[i] - Main.screenPosition, frame, color, rotation, origin,
					new Vector2(1f, 1.25f), effects, 0);
			}
			return false;
		}
	}
}
