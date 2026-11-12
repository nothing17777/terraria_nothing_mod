using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Players
{
	// Harpy Charm / Seraph Necklace: periodically turn one of the player's arrows toward an enemy.
	// Numbers from the reference port (Arrow.ts harpyCharmEffect / applyHarpyCharm):
	//   arrow distance from player in [400, 1200] px, closest such arrow, enemy with line of sight within 1600 px of the arrow,
	//   cooldown 90 ticks (72 with Seraph), charmed arrows keep homing (full turn, speed kept) until no target is left, timeLeft capped at 300.
	public static class HarpyCharmLogic
	{
		public static NPC FindTarget(Vector2 from, int w, int h, Vector2 pos, float range)
		{
			NPC best = null;
			float bestDist = range;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (!npc.active || !npc.CanBeChasedBy())
					continue;
				float d = Vector2.Distance(from, npc.Center);
				if (d < bestDist && Collision.CanHit(pos, w, h, npc.position, npc.width, npc.height))
				{
					bestDist = d;
					best = npc;
				}
			}
			return best;
		}

		public static void TryRedirectArrow(Player player, AccessoryEffectsPlayer fx)
		{
			Projectile bestProj = null;
			NPC bestTarget = null;
			float bestDist = float.MaxValue;
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile p = Main.projectile[i];
				if (!p.active || p.owner != player.whoAmI || !p.arrow || p.aiStyle != ProjAIStyleID.Arrow)
					continue;
				if (p.GetGlobalProjectile<HarpyCharmProjectile>().Charmed)
					continue;
				float d = Vector2.Distance(p.Center, player.Center);
				if (d < 400f || d >= bestDist || d > 1200f)
					continue;
				NPC t = FindTarget(p.Center, p.width, p.height, p.position, 1600f);
				if (t != null)
				{
					bestDist = d;
					bestProj = p;
					bestTarget = t;
				}
			}
			if (bestProj == null)
				return;
			float speed = bestProj.velocity.Length();
			bestProj.velocity = Vector2.Normalize(bestTarget.Center - bestProj.Center) * speed;
			bestProj.GetGlobalProjectile<HarpyCharmProjectile>().Charmed = true;
			if (bestProj.timeLeft > 300)
				bestProj.timeLeft = 300;
			bestProj.netUpdate = true;
			fx.HarpyCharmCooldown = fx.SeraphCharm ? 72 : 90;
		}
	}

	public class HarpyCharmProjectile : GlobalProjectile
	{
		public override bool InstancePerEntity => true;

		public bool Charmed;
		public bool Consumed;

		public override void PostAI(Projectile projectile)
		{
			if (!Charmed || Consumed || projectile.owner != Main.myPlayer)
				return;
			NPC t = HarpyCharmLogic.FindTarget(projectile.Center, projectile.width, projectile.height, projectile.position, 1600f);
			if (t == null)
			{
				Consumed = true; // ref: target lost -> stop homing for good
				return;
			}
			float speed = projectile.velocity.Length();
			if (speed < 1f)
				projectile.velocity = Vector2.UnitY;
			else
				projectile.velocity = Vector2.Normalize(t.Center - projectile.Center) * speed;
			if (speed < 12f)
				projectile.velocity *= 1.05f;
			if (projectile.timeLeft % 4 == 0)
				projectile.netUpdate = true;
		}
	}
}
