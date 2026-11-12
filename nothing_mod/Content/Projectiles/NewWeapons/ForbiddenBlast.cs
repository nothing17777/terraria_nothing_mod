using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// 7x7 tile explosion spawned by a Forbidden Bolt. Invisible; texture is a placeholder.
	public class ForbiddenBlast : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.AmberBolt;

		public override void SetDefaults()
		{
			Projectile.width = 112;
			Projectile.height = 112;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 3;
			Projectile.tileCollide = false;
			Projectile.alpha = 255;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override void OnSpawn(Terraria.DataStructures.IEntitySource source)
		{
			SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
			for (int i = 0; i < 20; i++)
			{
				Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GoldFlame, 0f, 0f, 0, default, 1.6f);
				d.noGravity = true;
				d.velocity *= 2f;
			}
		}
	}
}
