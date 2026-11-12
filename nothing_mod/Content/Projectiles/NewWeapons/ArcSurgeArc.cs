using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// Near-instant red lightning arc, pierces without loss. Sprite is a placeholder (Thunder Staff shot).
	public class ArcSurgeArc : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.ThunderStaffShot;

		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = -1;
			Projectile.extraUpdates = 15;
			Projectile.timeLeft = 120; // 120 updates * 8px = 60 tiles
			Projectile.tileCollide = true;
			Projectile.alpha = 255;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override void AI()
		{
			if (Projectile.wet)
			{
				Projectile.Kill();
				return;
			}
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.RedTorch, 0f, 0f, 0, default, 1.2f);
			d.noGravity = true;
			d.velocity *= 0.1f;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<ZappedRed>(), Main.rand.Next(240, 421));
		}
		// TODO: up to 2 extra arcs jumping to nearby enemies, 120x75 tile reach, no-lava passage.
	}
}
