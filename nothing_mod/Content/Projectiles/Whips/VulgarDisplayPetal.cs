using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Whips
{
	// Petal fired by Vulgar Display of Flower's proc (game: FlowerWhipPetal, projectile 1038).
	// Real SetDefaults (reference data): 18x18, penetrate 1, timeLeft 120, armor penetration 50,
	// ignores tiles/water, local immunity with cooldown -1.
	public class VulgarDisplayPetal : ModProjectile
	{
		public override void SetStaticDefaults() {
			Main.projFrames[Type] = 8; // sprite is 18x160 = 8 frames of 20 px
		}

		public override void SetDefaults() {
			Projectile.width = 18;
			Projectile.height = 18;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 120;
			Projectile.ArmorPenetration = 50;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
			Projectile.DamageType = DamageClass.Summon;
		}

		// WhipTagEffect_ViolentDisplayOfFlower.SpawnFlowerExplosionOn: 3-6 petals evenly spread from the target's
		// centre at speed max(w,h)/8, total damage = hit damage x 2.25 split evenly (rounded up), and the target
		// itself cannot be hit for the first 30 ticks.
		public static void Burst(IEntitySource source, NPC target, int hitDamage, int owner) {
			int count = 3 + Main.rand.Next(4);
			float baseAngle = Main.rand.NextFloat() * MathHelper.TwoPi;
			float speed = System.Math.Max(1f, System.Math.Max(target.width, target.height) / 8f);
			int each = (int)System.Math.Ceiling((int)(hitDamage * 2.25f) / (float)count);
			for (int i = 0; i < count; i++) {
				float angle = (float)i / count * MathHelper.TwoPi + baseAngle + Main.rand.NextFloatDirection() * 0.3927f;
				Vector2 vel = angle.ToRotationVector2() * speed;
				int idx = Projectile.NewProjectile(source, target.Center, vel, ModContent.ProjectileType<VulgarDisplayPetal>(),
					each, 0f, owner, Main.rand.NextFloat() * -20f);
				if (idx >= 0 && idx < Main.maxProjectiles)
					Main.projectile[idx].localNPCImmunity[target.whoAmI] = 30;
			}
		}

		// UNVERIFIED: the real AI is aiStyle 196 (not in tModLoader 1.4.4.9). Approximation: drift outward
		// during the negative ai[0] delay, then home in on the nearest enemy ("chase significantly faster,
		// and from farther away"). Range and speeds below are guesses.
		public override void AI() {
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			if (++Projectile.frameCounter >= 3) {
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 8;
			}
			Projectile.alpha = (int)Utils.Remap(Projectile.timeLeft, 30f, 0f, 0f, 255f); // UNVERIFIED fade-out
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] < 0f) {
				Projectile.velocity *= 0.96f;
				return;
			}
			NPC best = null;
			float bestDist = 600f; // UNVERIFIED homing range
			for (int i = 0; i < Main.maxNPCs; i++) {
				NPC n = Main.npc[i];
				if (!n.CanBeChasedBy(Projectile) || Projectile.localNPCImmunity[i] > 0)
					continue;
				float d = Vector2.Distance(Projectile.Center, n.Center);
				if (d < bestDist) { bestDist = d; best = n; }
			}
			if (best != null) {
				Vector2 want = (best.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 18f; // UNVERIFIED speed
				Projectile.velocity = Vector2.Lerp(Projectile.velocity, want, 0.15f);
			}
		}
	}
}
