using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.DeadCells
{
	// Red explosion made by the Mushroom Boi's dive (wiki: 133% damage, 300% knockback). The sprite is the vanilla 1x1 blank; the visual is dust. Sound/dust UNVERIFIED.
	public class MushroomBoiExplosion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.MinionShot[Type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 96;
			Projectile.height = 96;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 4;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 20;
		}

		public override void OnSpawn(Terraria.DataStructures.IEntitySource source)
		{
			SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
			for (int i = 0; i < 30; i++)
			{
				Dust d = Dust.NewDustDirect(Projectile.Center - new Vector2(30f), 60, 60, DustID.RedTorch, 0f, 0f, 100, default, 1.8f);
				d.velocity = Main.rand.NextVector2Circular(6f, 6f);
				d.noGravity = true;
			}
		}
	}
}
