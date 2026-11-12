using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// Charged icicle lance. ai[0] = charge ticks, ai[1] = 0 charging / 1 fired. Sprite is a placeholder (Ice Spike).
	public class GlacierIcicle : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.IceSpike;

		const float FullCharge = 60f;
		float baseKnockback = -1f;

		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.friendly = false; // harmless while charging
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 3;
			Projectile.timeLeft = 600;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			if (baseKnockback < 0f)
				baseKnockback = Projectile.knockBack;

			if (Projectile.ai[1] == 0f)
			{
				if (Projectile.owner != Main.myPlayer)
					return;
				if (!player.active || player.dead)
				{
					Projectile.Kill();
					return;
				}
				Vector2 dir = (Main.MouseWorld - player.MountedCenter).SafeNormalize(Vector2.UnitX * player.direction);
				float prev = Projectile.ai[0];
				// Charging slows to 40% speed when out of mana.
				float rate = Projectile.localAI[1] == 1f ? 0.4f : 1f;
				Projectile.ai[0] += rate;
				float cur = Projectile.ai[0];
				// Extra mana is consumed at 1/3 and 2/3 of the charge.
				if ((prev < 20f && cur >= 20f) || (prev < 40f && cur >= 40f))
				{
					if (!player.CheckMana(player.HeldItem.mana, true))
						Projectile.localAI[1] = 1f;
					else
						player.manaRegenDelay = 0;
				}
				float t = MathHelper.Clamp(cur / FullCharge, 0f, 1f);
				int size = (int)MathHelper.Lerp(10f, 54f, t);
				Projectile.Resize(size, size);
				Projectile.Center = player.MountedCenter + dir * (20f + size * 0.5f);
				Projectile.velocity = Vector2.Zero;
				Projectile.rotation = dir.ToRotation() + MathHelper.PiOver2;

				player.heldProj = Projectile.whoAmI;
				player.ChangeDir(dir.X >= 0 ? 1 : -1);
				player.itemTime = 2;
				player.itemAnimation = 2;
				player.itemRotation = (dir * player.direction).ToRotation();

				if (!player.channel || cur >= FullCharge)
				{
					bool full = cur >= FullCharge;
					float mult = MathHelper.Lerp(1.5f, 4f, t);
					Projectile.damage = (int)(Projectile.originalDamage * mult);
					Projectile.knockBack = baseKnockback * mult;
					float speed = MathHelper.Lerp(8f, 30f, t) * (full ? 1.5f : 1f);
					Projectile.velocity = dir * speed;
					Projectile.penetrate = full ? 3 : (cur >= 30f ? 2 : 1);
					Projectile.friendly = true;
					Projectile.ai[1] = 1f;
					Projectile.netUpdate = true;
				}
				else if ((int)cur % 4 == 0)
				{
					Projectile.netUpdate = true;
				}
				return;
			}

			// fired
			Projectile.velocity.Y += 0.06f; // slight gravity
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			if (Main.rand.NextBool(2))
			{
				Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.IceTorch);
				d.noGravity = true;
				d.velocity *= 0.2f;
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			Projectile.damage = (int)(Projectile.damage * 0.66f); // -34% per enemy pierced
		}
	}
}
