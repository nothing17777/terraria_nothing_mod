using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Whips
{
	// Starcrash proc meteor (game: MeteorWhipMeteor, projectile 1037). Real SetDefaults from the reference
	// data: 24x24, scale 0.8, extraUpdates 1, ignores tiles, local immunity with cooldown -1.
	public class StarcrashMeteor : ModProjectile
	{
		public override void SetDefaults() {
			Projectile.width = 24;
			Projectile.height = 24;
			Projectile.scale = 0.8f;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.extraUpdates = 1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.alpha = 254;       // real SetDefaults alpha
			Projectile.timeLeft = 240;    // UNVERIFIED (falls ~16 px/tick, only needs a few ticks)
		}

		public override void AI() {
			// Game aiStyle 5 branch: alpha fades -10 per update to 0, pointing along velocity.
			Projectile.alpha = System.Math.Max(0, Projectile.alpha - 10);
			Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
			// UNVERIFIED trail: Starcrash tip uses dust 6 (fire); same dust used here.
			if (Main.rand.NextBool(2)) {
				Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.2f);
				d.noGravity = true;
				d.velocity *= 0.3f;
			}
		}

		public override void OnKill(int timeLeft) {
			// UNVERIFIED impact burst.
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
			for (int i = 0; i < 12; i++) {
				Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
				d.noGravity = true;
				d.velocity *= 2f;
			}
		}

		public override Color? GetAlpha(Color lightColor) => Color.White * Projectile.Opacity;
	}
}
