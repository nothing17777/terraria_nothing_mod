using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Palworld
{
	// Invisible hitbox that follows the player during Chillet's Rocket Slam. Hitting an enemy grants 0.17 s (10 ticks) of invincibility (wiki).
	public class RocketSlamHit : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + Terraria.ID.ProjectileID.WoodenArrowFriendly;

		public override void SetDefaults()
		{
			Projectile.width = 48;
			Projectile.height = 48;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 14;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
			Projectile.hide = true;
		}

		public override void AI()
		{
			Player p = Main.player[Projectile.owner];
			Projectile.Center = p.Center;
			if (!p.active || p.dead)
				Projectile.Kill();
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			Player p = Main.player[Projectile.owner];
			p.immune = true;
			p.immuneTime = System.Math.Max(p.immuneTime, 10);
			p.immuneNoBlink = true;
		}
	}
}
