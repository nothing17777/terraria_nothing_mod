using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// Harmless spark that, after 0.5 s, becomes a lightning explosion (8x8 tiles). Sprite is a placeholder (Thunder Staff shot).
	public class LightningStrikeSpark : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.ThunderStaffShot;

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 40;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override bool? CanDamage() => Projectile.ai[0] >= 30f ? null : false;

		public override void AI()
		{
			Projectile.ai[0]++;
			if (Projectile.ai[0] < 30f)
			{
				// attach to the first enemy within 0.75 tiles and follow it
				int idx = (int)Projectile.ai[1] - 1;
				if (idx < 0)
				{
					foreach (NPC n in Main.ActiveNPCs)
					{
						if (n.CanBeChasedBy(Projectile) && Projectile.Center.Distance(n.Center) < 12f + n.width * 0.5f)
						{
							Projectile.ai[1] = n.whoAmI + 1;
							break;
						}
					}
				}
				else if (Main.npc[idx].active)
				{
					Projectile.Center = Main.npc[idx].Center;
				}
				Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
				d.noGravity = true;
			}
			else if (Projectile.ai[0] == 30f)
			{
				// strike: bolt of dust from above plus an 8x8 tile explosion box
				Vector2 c = Projectile.Center;
				for (float y = 0; y < 900f; y += 12f)
				{
					Dust d = Dust.NewDustPerfect(c - new Vector2(Main.rand.NextFloat(-6f, 6f), y), DustID.Electric, Vector2.Zero);
					d.noGravity = true;
					d.scale = 1.5f;
				}
				Projectile.Resize(128, 128);
				Projectile.Center = c;
				Projectile.timeLeft = 4;
				Terraria.Audio.SoundEngine.PlaySound(SoundID.Item122, c);
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.OnFire3, Main.rand.Next(240, 421)); // Hellfire 4-7 s
			target.AddBuff(ModContent.BuffType<Zapped>(), Main.rand.Next(240, 421));
			Projectile.damage = (int)(Projectile.damage * 0.8f); // -20% per enemy struck
		}
		// TODO: lightning electrifying water (31.25 tile radius), self-damage when striking liquid, falling from screen top through blocks, cursor-blocked spawn.
	}
}
