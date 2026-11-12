using Microsoft.Xna.Framework;
using Terraria;

namespace nothing_mod.Content.Projectiles.NewWeapons
{
	// Small shared helper for the simplified hovering minions.
	public static class SimpleMinionAI
	{
		public static NPC FindTarget(Projectile p, Player owner, float range)
		{
			if (owner.HasMinionAttackTargetNPC)
			{
				NPC forced = Main.npc[owner.MinionAttackTargetNPC];
				if (forced.CanBeChasedBy(p) && p.Distance(forced.Center) < range * 1.5f)
					return forced;
			}
			NPC best = null;
			float bestDist = range;
			foreach (NPC n in Main.ActiveNPCs)
			{
				if (!n.CanBeChasedBy(p))
					continue;
				float d = p.Distance(n.Center);
				if (d < bestDist)
				{
					bestDist = d;
					best = n;
				}
			}
			return best;
		}

		public static void MoveToward(Projectile p, Vector2 target, float speed, float inertia)
		{
			Vector2 diff = target - p.Center;
			float dist = diff.Length();
			if (dist > speed)
				diff = diff / dist * speed;
			p.velocity = (p.velocity * (inertia - 1f) + diff) / inertia;
		}

		// Returns false (and kills the minion) when the owner is gone / buff expired.
		public static bool Upkeep(Projectile p, Player owner, int buffType)
		{
			if (!owner.active || owner.dead)
			{
				owner.ClearBuff(buffType);
				return false;
			}
			if (owner.HasBuff(buffType))
				p.timeLeft = 2;
			if (p.Distance(owner.Center) > 2000f)
			{
				p.Center = owner.Center;
				p.velocity = Vector2.Zero;
				p.netUpdate = true;
			}
			return true;
		}

		public static Vector2 IdlePosition(Projectile p, Player owner)
		{
			return owner.Center + new Vector2((p.minionPos - 1) * 40f * -owner.direction, -60f);
		}
	}
}
