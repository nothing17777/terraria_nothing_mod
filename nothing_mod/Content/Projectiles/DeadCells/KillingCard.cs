using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.DeadCells
{
	// Wiki (Killing Deck): flies serpentine for 0.5 s then falls; sticks to the first block/enemy it hits and stops dealing damage;
	// returns 5 s after the impact (or when recalled), flying straight through blocks, piercing, at 150% damage.
	// ai[0] = flight ticks, ai[1] = state (0 flying, 1 stuck, 2 returning). localAI[0] = stuck timer. ai[2] = stuck NPC index + 1 (0 = stuck in a block).
	public class KillingCard : ModProjectile
	{
		public const float Flying = 0f, Stuck = 1f, Returning = 2f;

		public override void SetStaticDefaults()
		{
			Main.projFrames[Type] = 8;
		}

		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 3600;
			Projectile.tileCollide = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1; // wiki: -1 tick; reset when the card returns
		}

		public override bool? CanDamage() => Projectile.ai[1] == Stuck ? false : null;

		private void StartReturn()
		{
			Projectile.ai[1] = Returning;
			Projectile.ai[2] = 0f;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.ResetLocalNPCHitImmunity();
			Projectile.netUpdate = true;
		}

		public override void AI()
		{
			Projectile.timeLeft = 3600;
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 3)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Type];
			}
			Player owner = Main.player[Projectile.owner];
			if (!owner.active || owner.dead)
			{
				Projectile.Kill();
				return;
			}
			if (Projectile.ai[1] == Flying)
			{
				Projectile.ai[0]++;
				if (Projectile.ai[0] < 30f) // 0.5 s of serpentine flight
					Projectile.velocity = Projectile.velocity.RotatedBy(System.Math.Sin(Projectile.ai[0] * 0.5f) * 0.12f);
				else
					Projectile.velocity.Y = System.Math.Min(Projectile.velocity.Y + 0.4f, 16f);
				if (Projectile.ai[0] > 900f && Projectile.owner == Main.myPlayer) // safety: never leave a card flying forever
					StartReturn();
			}
			else if (Projectile.ai[1] == Stuck)
			{
				Projectile.velocity = Vector2.Zero;
				if (Projectile.ai[2] > 0f)
				{
					NPC n = Main.npc[(int)Projectile.ai[2] - 1];
					if (n.active)
						Projectile.position += n.position - n.oldPosition; // ride along with the enemy
					else
						Projectile.ai[2] = 0f;
				}
				Projectile.localAI[0]++;
				if (Projectile.localAI[0] >= 300f && Projectile.owner == Main.myPlayer) // 5 s
					StartReturn();
			}
			else
			{
				Vector2 to = owner.Center - Projectile.Center;
				if (to.Length() < 24f)
				{
					Projectile.Kill();
					return;
				}
				Projectile.velocity = Vector2.Normalize(to) * 24f; // straight line
			}
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			if (Projectile.ai[1] == Returning)
				modifiers.FinalDamage *= 1.5f; // wiki: 150% of the weapon's damage while returning
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (Projectile.ai[1] == Flying)
			{
				Projectile.ai[1] = Stuck;
				Projectile.ai[2] = target.whoAmI + 1;
				Projectile.velocity = Vector2.Zero;
				Projectile.netUpdate = true;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Projectile.ai[1] == Flying)
			{
				Projectile.ai[1] = Stuck;
				Projectile.ai[2] = 0f;
				Projectile.velocity = Vector2.Zero;
				Projectile.netUpdate = true;
			}
			return false;
		}
	}
}
