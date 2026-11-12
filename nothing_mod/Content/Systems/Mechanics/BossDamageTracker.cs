using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems.Mechanics
{
	// 1.4.5 boss damage tracker: records damage dealt by each player while any boss is alive.
	// Runs in single player and on the server only (clients would double count). Read with /bossdamage.
	public class BossDamageTracker : ModSystem
	{
		public class Fight
		{
			public string Boss = "";
			public int DurationTicks;
			public readonly Dictionary<string, long> Damage = new();
		}

		private const int MaxHistory = 5;
		public static readonly List<Fight> History = new();
		private static Fight current;

		public static bool Active => current != null;

		private static bool IsBoss(NPC n) => n.active && (n.boss || NPCID.Sets.ShouldBeCountedAsBoss[n.type]);

		public static void Record(Player player, int damage) {
			if (current == null || player == null || damage <= 0 || Main.netMode == NetmodeID.MultiplayerClient)
				return;
			current.Damage.TryGetValue(player.name, out long total);
			current.Damage[player.name] = total + damage;
		}

		public override void PostUpdateNPCs() {
			if (Main.netMode == NetmodeID.MultiplayerClient)
				return;

			NPC boss = null;
			for (int i = 0; i < Main.maxNPCs; i++) {
				if (IsBoss(Main.npc[i])) { boss = Main.npc[i]; break; }
			}

			if (boss != null) {
				current ??= new Fight { Boss = boss.GivenOrTypeName };
				current.DurationTicks++;
			}
			else if (current != null) {
				if (current.Damage.Count > 0) {
					History.Add(current);
					if (History.Count > MaxHistory)
						History.RemoveAt(0);
				}
				current = null;
			}
		}

		public override void OnWorldUnload() {
			History.Clear();
			current = null;
		}

		public override void Unload() {
			History.Clear();
			current = null;
		}
	}

	public class BossDamageNPC : GlobalNPC
	{
		public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone) {
			if (!npc.friendly)
				BossDamageTracker.Record(player, damageDone);
		}

		public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone) {
			if (!npc.friendly && projectile.owner >= 0 && projectile.owner < Main.maxPlayers)
				BossDamageTracker.Record(Main.player[projectile.owner], damageDone);
		}
	}
}
