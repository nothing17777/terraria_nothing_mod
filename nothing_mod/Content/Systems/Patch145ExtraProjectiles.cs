using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems
{
	// 1.4.5.x projectile tweaks for existing vanilla projectiles (changelog.txt shipped with the game).
	public class Patch145ExtraProjectiles : GlobalProjectile
	{
		public override void SetDefaults(Projectile projectile)
		{
			// 1.4.5.0 Magic Dagger: pierces up to 2 enemies, up from 1.
			if (projectile.type == ProjectileID.MagicDagger && projectile.penetrate == 1)
				projectile.penetrate = 2;
		}

		public override void OnSpawn(Projectile projectile, IEntitySource source)
		{
			switch (projectile.type)
			{
				case ProjectileID.SpikyBall:
					projectile.timeLeft = projectile.timeLeft * 4 / 3;   // 1.4.5.0 lifetime +~33%
					break;
				case ProjectileID.CandyCorn:
					projectile.timeLeft = projectile.timeLeft / 2;       // 1.4.5.0 lifetime 10s -> 5s (assumes vanilla 600 ticks)
					break;
			}
		}

		// 1.4.5.0: Anchor and Golem Fist have a 33% chance to Confuse (duration UNVERIFIED).
		public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if ((projectile.type == ProjectileID.Anchor || projectile.type == ProjectileID.GolemFist) && Main.rand.NextBool(3))
				target.AddBuff(BuffID.Confused, 120);
		}
	}
}
