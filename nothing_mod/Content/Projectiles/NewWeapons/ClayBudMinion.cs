using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// Clay Bud summon. Simplified: hovers/chases and deals contact damage. Sprite is a placeholder (Baby Slime).
	public class ClayBudMinion : ModProjectile
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
			Projectile.minionSlots = 1f;
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
			if (!SimpleMinionAI.Upkeep(Projectile, owner, ModContent.BuffType<ClayBudBuff>()))
				return;
			NPC target = SimpleMinionAI.FindTarget(Projectile, owner, 31.25f * 16f);
			if (target != null)
				SimpleMinionAI.MoveToward(Projectile, target.Center, 8f, 20f);
			else
				SimpleMinionAI.MoveToward(Projectile, SimpleMinionAI.IdlePosition(Projectile, owner), 10f, 20f);
			Projectile.rotation = Projectile.velocity.X * 0.05f;
			Projectile.spriteDirection = Projectile.velocity.X >= 0 ? 1 : -1;
		}

		// 1.4.5.8: Clay Bud now always knocks enemies away from the player.
		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			Player owner = Main.player[Projectile.owner];
			modifiers.HitDirectionOverride = target.Center.X >= owner.Center.X ? 1 : -1;
			if (modifiers.Knockback.Base < 1f)
				modifiers.Knockback.Base = 1f;
		}

		// TODO: ground walking/jumping AI, slam attack, leaf-flight return, petting.
	}
}
