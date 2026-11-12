using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Players
{
	// Snapping Stone (ranged), Pyroclastic Stone (melee/ranged) and Armlet of Ruin (tag hits) discharge a pulse.
	// Numbers come from the reference port's SnapStrike.ts (decompiled 1.4.5 TryMakingSnappingStoneStrike):
	//   kind 0: dmg 30 x ranged, range 120px, flat bonus base 40, mult = ranged damage
	//   kind 1: dmg 45 x melee,  range 300px, flat bonus base 70, mult = max(melee, ranged), Hellfire 180t on pulse targets and victim
	//   kind 2: dmg 150 x summon, range 400px, flat bonus base 150, mult = summon damage
	//   all: up to 2 other targets, +10 flat bonus per unused target slot, shared 360 tick cooldown, requires line of sight.
	public static class AccessoryPulse
	{
		public static void OnHit(Player player, AccessoryEffectsPlayer fx, NPC victim, Projectile source, Vector2 hitPoint, Vector2 boxPos, int boxW, int boxH,
			bool melee, bool ranged, bool summonTag, ref NPC.HitModifiers modifiers)
		{
			if (fx.PulseCooldown > 0)
				return;
			int kind;
			if (fx.PyroclasticPulse && (melee || ranged))
				kind = 1;
			else if (fx.SnappingStone && ranged)
				kind = 0;
			else if (fx.ArmletPulse && summonTag && HasTagDebuff(victim)) // UNVERIFIED: "tag hit" approximated as a minion/whip hit on an enemy that carries a whip tag
				kind = 2;
			else
				return;

			int dmg, range, projKind = kind, flatBase;
			float mult;
			switch (kind)
			{
				case 0:
					dmg = (int)player.GetTotalDamage(DamageClass.Ranged).ApplyTo(30);
					range = 120; flatBase = 40;
					mult = player.GetTotalDamage(DamageClass.Ranged).ApplyTo(1f);
					break;
				case 1:
					dmg = (int)player.GetTotalDamage(DamageClass.Melee).ApplyTo(45);
					range = 300; flatBase = 70;
					mult = System.Math.Max(player.GetTotalDamage(DamageClass.Melee).ApplyTo(1f), player.GetTotalDamage(DamageClass.Ranged).ApplyTo(1f));
					break;
				default:
					dmg = (int)player.GetTotalDamage(DamageClass.Summon).ApplyTo(150);
					range = 400; flatBase = 150;
					mult = player.GetTotalDamage(DamageClass.Summon).ApplyTo(1f);
					break;
			}

			fx.PulseCooldown = 360;
			var chosen = new List<int> { victim.whoAmI };
			int spawned = 0;
			for (int n = 0; n < 2; n++)
			{
				NPC best = null;
				float bestDist = range;
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (!npc.active || chosen.Contains(i) || !npc.CanBeChasedBy(player))
						continue;
					float d = Vector2.Distance(hitPoint, npc.Center);
					if (d < bestDist && Collision.CanHit(boxPos, boxW, boxH, npc.position, npc.width, npc.height))
					{
						bestDist = d;
						best = npc;
					}
				}
				if (best == null)
					break;
				chosen.Add(best.whoAmI);
				Projectile.NewProjectile(player.GetSource_Misc("nothing_mod:AccessoryPulse"), best.Center, Vector2.UnitY * 0.1f,
					ModContent.ProjectileType<AccessoryPulseProjectile>(), dmg, 0f, player.whoAmI, projKind);
				spawned++;
			}

			// Unused target slots turn into extra damage on the triggering hit (ref num13 = 2 - spawned, num14 = num3 + 10 * num13).
			modifiers.FlatBonusDamage += (int)((flatBase + 10 * (2 - spawned)) * mult);
			if (kind == 1)
				victim.AddBuff(BuffID.OnFire3, 180); // ref: BuffID 323 (Hellfire) 180 ticks on the victim
		}

		static bool HasTagDebuff(NPC npc)
		{
			for (int i = 0; i < NPC.maxBuffs; i++)
				if (npc.buffTime[i] > 0 && BuffID.Sets.IsATagBuff[npc.buffType[i]])
					return true;
			return false;
		}
	}

	// Invisible, one-shot damage zone spawned on a target's center (ref projectiles 1121/1131/1132, timeLeft 2).
	public class AccessoryPulseProjectile : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.PurificationPowder; // placeholder, never drawn

		public override void SetDefaults()
		{
			Projectile.width = 40; // UNVERIFIED hitbox size
			Projectile.height = 40; // UNVERIFIED
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 2;
			Projectile.alpha = 255;
			Projectile.DamageType = DamageClass.Default;
		}

		public override void OnSpawn(IEntitySource source)
		{
			Player owner = Main.player[Projectile.owner];
			int kind = (int)Projectile.ai[0];
			if (kind == 0)
				Projectile.DamageType = DamageClass.Ranged;
			else if (kind == 1) // ref: class decided per pulse by the higher crit chance
				Projectile.DamageType = owner.GetTotalCritChance(DamageClass.Melee) > owner.GetTotalCritChance(DamageClass.Ranged) ? DamageClass.Melee : DamageClass.Ranged;
			for (int i = 0; i < 10; i++)
			{
				Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, kind == 1 ? DustID.Torch : DustID.Stone, 0f, 0f, 100, default, 1.1f);
				d.noGravity = true;
				d.velocity *= 2f;
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (Projectile.ai[0] == 1f)
				target.AddBuff(BuffID.OnFire3, 180); // ref: Hellfire 180 ticks
		}

		public override bool PreDraw(ref Color lightColor) => false;
	}
}
