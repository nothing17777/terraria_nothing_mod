using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Whips
{
	// Star scattered by the Constellation whip (game: ConstellationStar, projectile 1039).
	// Real SetDefaults (reference data): 30x30, timeLeft 240, extraUpdates 1, collides with tiles,
	// ignores water, local immunity with cooldown -1. Changelog: homing range and chase speed doubled
	// in 1.4.5. Bounces off tiles on each blocked axis (reference port of aiStyle 171).
	public class ConstellationStar : ModProjectile
	{
		public override void SetDefaults() {
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.friendly = true;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 240;
			Projectile.extraUpdates = 1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.penetrate = -1; // UNVERIFIED (ids' penetrate not listed in the reference data; localNPCHitCooldown -1 limits it to one hit per enemy)
		}

		// UNVERIFIED homing numbers: the real aiStyle 171 range/speed are not in the reference.
		public override void AI() {
			Projectile.rotation += 0.1f * (Projectile.velocity.X >= 0f ? 1f : -1f);
			NPC best = null;
			float bestDist = 700f;
			for (int i = 0; i < Main.maxNPCs; i++) {
				NPC n = Main.npc[i];
				if (!n.CanBeChasedBy(Projectile) || Projectile.localNPCImmunity[i] != 0)
					continue;
				float d = Vector2.Distance(Projectile.Center, n.Center);
				if (d < bestDist) { bestDist = d; best = n; }
			}
			if (best != null) {
				Vector2 want = (best.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 12f;
				Projectile.velocity = Vector2.Lerp(Projectile.velocity, want, 0.06f);
			}
			if (Main.rand.NextBool(3)) {
				Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.BlueTorch, 0f, 0f, 100, default, 1f);
				d.noGravity = true;
				d.velocity *= 0.2f;
			}
		}

		// Reference: blocked axis is fully reversed, no bounce limit.
		public override bool OnTileCollide(Vector2 oldVelocity) {
			if (Projectile.velocity.X != oldVelocity.X)
				Projectile.velocity.X = -oldVelocity.X;
			if (Projectile.velocity.Y != oldVelocity.Y)
				Projectile.velocity.Y = -oldVelocity.Y;
			return false;
		}

		public override Color? GetAlpha(Color lightColor) => Color.White * Projectile.Opacity;

		public override bool PreDraw(ref Color lightColor) {
			Vector2 pos = Projectile.Center - Main.screenPosition;
			Color shine = Main.hslToRgb(0.6f, 1f, 0.5f);
			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, pos, null, Color.White * 0.8f, Projectile.rotation,
				TextureAssets.Projectile[Type].Size() / 2f, Projectile.scale, SpriteEffects.None, 0);
			// UNVERIFIED: the game draws these stars with its pretty-star sparkle; same call the whip's own glints use.
			WhipProjectileBase.DrawStarSparkle(Projectile.Center, shine, Projectile.Opacity, MathHelper.PiOver2, new Vector2(1.5f, 2.5f), Vector2.One);
			return false;
		}
	}
}
