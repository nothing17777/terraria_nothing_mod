using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// Ruinous Staff minion: hovers, keeps ~12.5 tiles from its target and fires Forbidden Bolts.
	// Sprite is a placeholder (Stardust Cell minion).
	public class ForbiddenRuinMinion : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.StardustCellMinion;

		public override void SetStaticDefaults()
		{
			Main.projPet[Type] = true;
			ProjectileID.Sets.MinionSacrificable[Type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 28;
			Projectile.height = 28;
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
		public override bool MinionContactDamage() => false;

		public override void AI()
		{
			Player owner = Main.player[Projectile.owner];
			if (!SimpleMinionAI.Upkeep(Projectile, owner, ModContent.BuffType<RuinousStaffBuff>()))
				return;
			NPC target = SimpleMinionAI.FindTarget(Projectile, owner, 50f * 16f);
			Vector2 idle = SimpleMinionAI.IdlePosition(Projectile, owner);
			if (target != null && Projectile.Distance(target.Center) > 12.5f * 16f)
			{
				Vector2 away = (Projectile.Center - target.Center).SafeNormalize(Vector2.UnitY);
				SimpleMinionAI.MoveToward(Projectile, target.Center + away * 12.5f * 16f, 12f, 15f);
			}
			else
			{
				SimpleMinionAI.MoveToward(Projectile, idle, 10f, 20f);
			}

			if (Projectile.ai[0] > 0f)
				Projectile.ai[0]--;
			if (target != null && Projectile.ai[0] <= 0f && Projectile.owner == Main.myPlayer)
			{
				Projectile.ai[0] = Main.rand.Next(105, 116); // 1.75-1.92 s
				Vector2 vel = (target.Center - Projectile.Center).SafeNormalize(Vector2.UnitY) * 16f;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel,
					ModContent.ProjectileType<ForbiddenBolt>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
			}
			Projectile.rotation += 0.05f;
		}
		// TODO: orbital formation for multiple minions.
	}
}
