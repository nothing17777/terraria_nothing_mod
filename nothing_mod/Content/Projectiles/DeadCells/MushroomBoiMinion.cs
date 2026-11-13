using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.DeadCells;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Projectiles.DeadCells
{
	// Mushroom Boi. Simplified hover + contact damage (no ground run/jump AI yet) plus the wiki's dive: when the target is >10 tiles above it, or it must return to a
	// distant player, it dives and explodes on impact (133% damage, 300% knockback), then teleports back (2.25 s cooldown).
	// ai[0] = state (0 normal, 1 dive to target, 2 dive to owner), ai[1] = dive ticks, localAI[0] = dive cooldown.
	public class MushroomBoiMinion : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			Main.projFrames[Type] = 16;
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

		private void Explode(Player owner, bool teleportBack)
		{
			if (Projectile.owner == Main.myPlayer)
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<MushroomBoiExplosion>(),
					(int)(Projectile.damage * 1.33f), Projectile.knockBack * 3f, Projectile.owner);
			Projectile.ai[0] = 0f;
			Projectile.ai[1] = 0f;
			Projectile.localAI[0] = 135f; // 2.25 s cooldown
			if (teleportBack)
				Projectile.Center = owner.Center + new Vector2(0f, -40f);
			Projectile.velocity = Vector2.Zero;
			Projectile.netUpdate = true;
		}

		public override void AI()
		{
			Player owner = Main.player[Projectile.owner];
			if (!SimpleMinionAI.Upkeep(Projectile, owner, ModContent.BuffType<MushroomBoiBuff>()))
				return;
			if (Projectile.localAI[0] > 0f)
				Projectile.localAI[0]--;
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 6)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 6; // frames 0-5 as a hover cycle (layout UNVERIFIED)
			}
			NPC target = SimpleMinionAI.FindTarget(Projectile, owner, 50f * 16f); // wiki: targets within 50 tiles
			bool farFromOwner = System.Math.Abs(Projectile.Center.Y - owner.Center.Y) > 18.75f * 16f || Projectile.Distance(owner.Center) > 31.25f * 16f;

			if (Projectile.ai[0] == 0f && Projectile.localAI[0] <= 0f && Projectile.owner == Main.myPlayer)
			{
				if (target != null && target.Center.Y < Projectile.Center.Y - 10f * 16f)
				{
					Projectile.ai[0] = 1f;
					Projectile.netUpdate = true;
				}
				else if (target == null && farFromOwner)
				{
					Projectile.ai[0] = 2f;
					Projectile.netUpdate = true;
				}
			}

			if (Projectile.ai[0] != 0f)
			{
				Projectile.ai[1]++;
				Vector2 dest = Projectile.ai[0] == 1f && target != null ? target.Center : owner.Center;
				Projectile.velocity = Projectile.Center.DirectionTo(dest) * 14f;
				bool hit = Projectile.Distance(dest) < 30f || Projectile.ai[1] > 90f || (Projectile.ai[1] > 8f && Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height));
				if (Projectile.ai[0] == 1f && target == null)
					hit = true;
				if (hit && Projectile.owner == Main.myPlayer)
					Explode(owner, Projectile.ai[0] == 1f);
			}
			else if (target != null)
				SimpleMinionAI.MoveToward(Projectile, target.Center, 8f, 20f);
			else
				SimpleMinionAI.MoveToward(Projectile, SimpleMinionAI.IdlePosition(Projectile, owner), 10f, 20f);
			Projectile.rotation = Projectile.velocity.X * 0.05f;
			Projectile.spriteDirection = Projectile.velocity.X >= 0 ? 1 : -1;
		}
	}
}
