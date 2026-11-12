using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Whips
{
	// "Vision" bolt fired when a minion/sentry hits an enemy marked by Possession.
	// Behavior from the 1.4.5.8 game (via the Open-Terraria reference): curved path that ends through
	// the target, fixed 20 damage, 50 armor penetration, dies on first hit and chains up to 3 times.
	public class PossessionVision : ModProjectile
	{
		public override string Texture => "nothing_mod/Content/Projectiles/Whips/PossessionVision";

		public override void SetDefaults() {
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.DamageType = DamageClass.Default;
			Projectile.penetrate = 1;
			Projectile.extraUpdates = 1;
			Projectile.timeLeft = 48; // 24 ticks at 2 updates per tick
			Projectile.ArmorPenetration = 50;
			Projectile.alpha = 255;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}

		public override void OnSpawn(IEntitySource source) {
			Projectile.scale = 1f + Main.rand.Next(30) * 0.01f;
			if (Projectile.ai[1] == 0f)
				Terraria.Audio.SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
		}

		public override void AI() {
			Projectile.alpha = System.Math.Max(0, Projectile.alpha - 50);
			// ai[0] = turn amount over the flight; rotate every update, then slow slightly.
			Projectile.velocity = Projectile.velocity.RotatedBy(Projectile.ai[0] / 48f) * 0.96f;
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			// Chain: each vision that lands can release another, three in total.
			if (Projectile.ai[1] < 2f && Projectile.owner == Main.myPlayer)
				Spawn(Projectile.GetSource_OnHit(target), target.Hitbox, target.velocity, Projectile.owner, (int)Projectile.ai[1] + 1);
		}

		public static void Spawn(IEntitySource source, Rectangle target, Vector2 targetVelocity, int owner, int generation) {
			Rectangle box = target;
			box.Inflate(30, 16);
			box.Y -= 8;

			Vector2 start = new Vector2(box.X + Main.rand.NextFloat() * box.Width, box.Y + Main.rand.NextFloat() * box.Height);
			Vector2 center = box.Center.ToVector2();
			Vector2 direction = (center - start).SafeNormalize(-Vector2.UnitY);
			float speed = 7f + Main.rand.NextFloat() * 8f;
			float arc = (Main.rand.NextBool() ? -1f : 1f) * (MathHelper.Pi / 5f + MathHelper.Pi * 4f / 5f * Main.rand.NextFloat()) * 0.5f;

			// Walk backwards along the curve so the flight ends through the target's center.
			Vector2 velocity = direction.RotatedBy(MathHelper.PiOver4) * speed;
			Vector2 position = center;
			for (int j = 0; j < 24; j++) {
				position -= velocity;
				velocity = velocity.RotatedBy(-arc / 24f);
			}
			position += targetVelocity * 16f;

			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<PossessionVision>(), 20, 0f, owner, arc, generation);
		}
	}

	// Per-enemy count of armed visions (one per Possession lash that hit it, max 3).
	public class PossessionVisionNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		public int PendingVisions;

		public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone) {
			if (PendingVisions <= 0 || projectile.owner != Main.myPlayer)
				return;
			bool minionHit = projectile.minion || projectile.sentry
				|| ProjectileID.Sets.MinionShot[projectile.type] || ProjectileID.Sets.SentryShot[projectile.type];
			if (!minionHit || ProjectileID.Sets.IsAWhip[projectile.type])
				return;

			PendingVisions--;
			PossessionVision.Spawn(projectile.GetSource_OnHit(npc), npc.Hitbox, npc.velocity, projectile.owner, 0);
		}
	}
}
