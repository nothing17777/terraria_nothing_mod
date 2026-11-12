using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using nothing_mod.Content.Buffs.Whips;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Whips
{
	// Cobweb spider spawned on every Cobwhip hit (game: CobWhipSpider, projectile 1036).
	// Real SetDefaults from the reference data: 18x10, penetrate -1, armor penetration 30,
	// local immunity cooldown 20, friendly. Lives as long as the owner has the spider buff; damage 2.
	public class CobwhipSpider : ModProjectile
	{
		public override void SetStaticDefaults() {
			// UNVERIFIED frame count: the extracted 24x56 sprite holds 4 frames of 14 px (the reference lists 20).
			Main.projFrames[Type] = 4;
		}

		public override void SetDefaults() {
			Projectile.width = 18;
			Projectile.height = 10;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.ArmorPenetration = 30;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 20;
			Projectile.tileCollide = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.minion = true;       // so whip tags apply to its hits
			Projectile.minionSlots = 0f;    // the reference spawns it without taking a minion slot
			Projectile.timeLeft = 2;
		}

		public override bool? CanCutTiles() => false;

		// TODO: the real spider runs the game's AI_067 ground-walk chase (jump targeting, drop-through,
		// 2000px return rules). This is a simplified walker: gravity, chase nearest enemy within 450px
		// (real range), otherwise follow the owner; accel 0.5 / max speed 4 are the AI_067 defaults.
		public override void AI() {
			Player owner = Main.player[Projectile.owner];
			if (!owner.active || owner.dead || !owner.HasBuff(ModContent.BuffType<CobwhipSpiderBuff>())) {
				Projectile.Kill();
				return;
			}
			Projectile.timeLeft = 2;

			NPC target = null;
			float best = 450f;
			for (int i = 0; i < Main.maxNPCs; i++) {
				NPC n = Main.npc[i];
				if (!n.CanBeChasedBy(Projectile))
					continue;
				float d = Vector2.Distance(Projectile.Center, n.Center);
				if (d < best) { best = d; target = n; }
			}
			Vector2 goal = target != null ? target.Center : owner.Center;
			float dx = goal.X - Projectile.Center.X;
			int dir = dx > 0f ? 1 : -1;
			if (System.Math.Abs(dx) > (target != null ? 4f : 48f)) {
				Projectile.velocity.X += 0.5f * dir;
				if (System.Math.Abs(Projectile.velocity.X) > 4f)
					Projectile.velocity.X = 4f * dir;
			}
			else
				Projectile.velocity.X *= 0.8f;

			bool grounded = Projectile.velocity.Y == 0f; // tile collision zeroes Y when standing
			Projectile.velocity.Y = System.Math.Min(10f, Projectile.velocity.Y + 0.4f);
			Projectile.spriteDirection = dir; // sprite faces right; flipped when walking left

			// Frames: idle = 0, walking cycles (every 5 ticks), airborne = 1.
			if (!grounded) {
				Projectile.frame = 1;
				Projectile.frameCounter = 0;
			}
			else if (Projectile.velocity.X == 0f)
				Projectile.frame = 0;
			else if (++Projectile.frameCounter > 4) {
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Type];
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			if (Projectile.velocity.X != oldVelocity.X && oldVelocity.Y >= 0f) // blocked sideways: hop
				Projectile.velocity.Y = -5f;
			if (Projectile.velocity.Y != oldVelocity.Y)
				Projectile.velocity.Y = 0f;
			if (Projectile.velocity.X != oldVelocity.X)
				Projectile.velocity.X = 0f;
			return false;
		}

		public override bool PreDraw(ref Color lightColor) {
			Texture2D tex = TextureAssets.Projectile[Type].Value;
			int cellH = tex.Height / Main.projFrames[Type];
			Rectangle frame = new Rectangle(0, Projectile.frame * cellH, tex.Width, cellH);
			SpriteEffects fx = Projectile.spriteDirection < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, 2f), frame, lightColor,
				0f, frame.Size() / 2f, 1f, fx, 0);
			return false;
		}
	}
}
