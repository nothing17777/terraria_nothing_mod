using System.Collections.Generic;
using Microsoft.Xna.Framework;
using nothing_mod.Content.Buffs.Whips;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Projectiles.Whips
{
	// 1.4.5 whip "proc" windows. A whip lash primes an enemy; the next tagged hit from a minion/sentry
	// consumes the window for the whip's special effect. Mechanics follow the reference port of the
	// game's TagEffectState / WhipTagEffect_* classes (Starcrash, Vulgar Display of Flower,
	// Electric Eel, Constellation).
	public class WhipProcNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		// tag buff type -> remaining proc ticks on this enemy
		private readonly Dictionary<int, int> procTime = new Dictionary<int, int>();

		public static void EnableProc(NPC npc, int tagBuffType, int ticks) {
			npc.GetGlobalNPC<WhipProcNPC>().procTime[tagBuffType] = ticks;
		}

		public static bool HasProc(NPC npc, int tagBuffType) {
			return npc.GetGlobalNPC<WhipProcNPC>().procTime.TryGetValue(tagBuffType, out int t) && t > 0;
		}

		public static void ClearProc(NPC npc, int tagBuffType) {
			npc.GetGlobalNPC<WhipProcNPC>().procTime.Remove(tagBuffType);
		}

		// Game: RemoveAllProcs for a tag type clears every enemy.
		public static void ClearAllProcs(int tagBuffType) {
			for (int i = 0; i < Main.maxNPCs; i++)
				if (Main.npc[i].active)
					Main.npc[i].GetGlobalNPC<WhipProcNPC>().procTime.Remove(tagBuffType);
		}

		public override void PostAI(NPC npc) {
			if (procTime.Count == 0)
				return;
			List<int> keys = new List<int>(procTime.Keys);
			foreach (int k in keys) {
				if (procTime[k] <= 1)
					procTime.Remove(k);
				else
					procTime[k]--;
			}
		}

		// A hit that is allowed to run tag/proc effects: a minion or sentry (or their shots), never a whip.
		private static bool IsMinionHit(Projectile p) {
			if (p.npcProj || p.trap || ProjectileID.Sets.IsAWhip[p.type])
				return false;
			return p.minion || p.sentry || ProjectileID.Sets.MinionShot[p.type] || ProjectileID.Sets.SentryShot[p.type];
		}

		public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers) {
			if (!IsMinionHit(projectile))
				return;
			int eel = ModContent.BuffType<ElectricEelDebuff>();
			if (!npc.HasBuff(eel) || !HasProc(npc, eel))
				return;

			// Electric Eel (WhipTagEffect_ElectricEel): +50 flat, +50 per resonating enemy (up to 5 within 450 px,
			// each takes 20 damage). The proc is always consumed.
			int hits = 0;
			if (projectile.owner == Main.myPlayer) {
				for (int i = 0; i < Main.maxNPCs && hits < 5; i++) {
					NPC other = Main.npc[i];
					if (!other.active || other.whoAmI == npc.whoAmI || !other.CanBeChasedBy(projectile))
						continue;
					if (npc.realLife >= 0 && npc.realLife == other.realLife)
						continue;
					Vector2 closest = other.Hitbox.ClosestPointInRect(npc.Center);
					if (Vector2.Distance(npc.Center, closest) > 450f)
						continue;
					hits++;
					other.SimpleStrikeNPC(20, 0, false, 0f, DamageClass.Summon, false, 0f, true);
					LightningLine(closest, npc.Center);
				}
			}
			Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCHit34, npc.Center);
			modifiers.FlatBonusDamage += 50f + hits * 50f;
			ClearProc(npc, eel);
		}

		// TODO: the real game plays the BlueLightningSmallLong particle orchestra (25 px steps of a
		// LightningSmall burst); that particle type does not exist in tModLoader 1.4.4.9, so electric dust is used.
		private static void LightningLine(Vector2 from, Vector2 to) {
			Vector2 step = to - from;
			float len = step.Length();
			if (len < 40f)
				return;
			step /= len;
			for (float s = 0f; s < len; s += 25f) {
				Vector2 pos = from + step * s;
				for (int k = 0; k < 2; k++) {
					Dust d = Dust.NewDustPerfect(pos + Main.rand.NextVector2Circular(6f, 6f) * 0.7f, 226,
						Main.rand.NextVector2Circular(4f, 4f), 0, default, 1f);
					d.noGravity = true;
					d.noLight = true;
				}
			}
		}

		public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone) {
			if (!IsMinionHit(projectile) || projectile.owner != Main.myPlayer)
				return;

			int starcrash = ModContent.BuffType<StarcrashDebuff>();
			if (npc.HasBuff(starcrash) && HasProc(npc, starcrash)) {
				ClearProc(npc, starcrash);
				// WhipTagEffect_Starcrash: a meteor falls 280 px above the target, damage = hit damage x 1.33, speed 8.
				Projectile.NewProjectile(projectile.GetSource_OnHit(npc), npc.Center + new Vector2(0f, -280f),
					new Vector2(0.4f, 8f), ModContent.ProjectileType<StarcrashMeteor>(),
					(int)(hit.SourceDamage * 1.33f), 2f, projectile.owner);
			}

			int flower = ModContent.BuffType<VulgarDisplayOfFlowerDebuff>();
			if (npc.HasBuff(flower) && HasProc(npc, flower)) {
				ClearProc(npc, flower);
				VulgarDisplayPetal.Burst(projectile.GetSource_OnHit(npc), npc, hit.SourceDamage, projectile.owner);
			}

			// Constellation: every tagged minion hit grows the cosmic energy (opens the proc window);
			// only the whip itself (ConstellationProjectile) consumes it.
			int constellation = ModContent.BuffType<ConstellationDebuff>();
			if (npc.HasBuff(constellation))
				EnableProc(npc, constellation, 480);
		}
	}
}
