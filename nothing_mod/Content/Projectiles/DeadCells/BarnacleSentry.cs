using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.DeadCells;

using nothing_mod.Content.Projectiles.NewWeapons;
namespace nothing_mod.Content.Projectiles.DeadCells
{
	// Simplified: stationary sentry that spits poison at the nearest enemy. Wiki: floats in midair, carried up by balloons. Sprite placeholder.
	public class BarnacleSentry : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			Main.projFrames[Type] = 4;
			ProjectileID.Sets.MinionTargettingFeature[Type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 28;
			Projectile.height = 40;
			Projectile.friendly = true;
			Projectile.sentry = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.timeLeft = Projectile.SentryLifeTime;
			Projectile.ignoreWater = true;
		}

		public override bool? CanDamage() => false;

		public override void AI()
		{
			Projectile.ai[0]++;
			if (Projectile.ai[0] < 60f)
				return;
			NPC target = SimpleMinionAI.FindTarget(Projectile, Main.player[Projectile.owner], 600f);
			if (target == null || !Collision.CanHitLine(Projectile.Center, 1, 1, target.Center, 1, 1))
				return;
			Projectile.ai[0] = 0f;
			if (Main.myPlayer == Projectile.owner)
			{
				Vector2 v = Projectile.Center.DirectionTo(target.Center) * 12f;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, v, ModContent.ProjectileType<BarnacleSpit>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
			}
		}
	}
}
