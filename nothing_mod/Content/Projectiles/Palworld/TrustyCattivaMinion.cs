using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.Palworld;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Projectiles.Palworld
{
	// Trusty Cattiva minion. Simplified hover + contact damage, sprite placeholder. TODO: ground AI, petting, pal skills.
	public class TrustyCattivaMinion : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.BabySlime;

		public override void SetStaticDefaults()
		{
			Main.projPet[Type] = true;
			ProjectileID.Sets.MinionSacrificable[Type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 24;
			Projectile.height = 24;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.minionSlots = 0f;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override bool? CanCutTiles() => false;
		public override bool MinionContactDamage() => true;

		public override void AI()
		{
			Player owner = Main.player[Projectile.owner];
			if (!SimpleMinionAI.Upkeep(Projectile, owner, ModContent.BuffType<TrustyCattivaBuff>()))
				return;
			NPC target = SimpleMinionAI.FindTarget(Projectile, owner, 31.25f * 16f);
			if (target != null)
				SimpleMinionAI.MoveToward(Projectile, target.Center, 8f, 20f);
			else
				SimpleMinionAI.MoveToward(Projectile, SimpleMinionAI.IdlePosition(Projectile, owner), 10f, 20f);
			Projectile.rotation = Projectile.velocity.X * 0.05f;
			Projectile.spriteDirection = Projectile.velocity.X >= 0 ? 1 : -1;
		}
	}
}
