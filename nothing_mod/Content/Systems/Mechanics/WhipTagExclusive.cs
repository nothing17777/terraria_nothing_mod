using System.Collections.Generic;
using nothing_mod.Content.Buffs.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems.Mechanics
{
	// 1.4.5.0 / 1.4.5.7: different whip tags no longer stack; hitting with a whip clears another whip's tag.
	// Implemented without touching the whip files: each tick, if an enemy carries more than one whip tag
	// and at least one of them appeared since last tick, the older tags are removed.
	// TODO: 1.4.5.7 "whip stacking re-added with accessories" (needs the accessory that enables it).
	public class WhipTagExclusive : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		private static readonly int[] VanillaTags = {
			BuffID.BlandWhipEnemyDebuff, BuffID.ThornWhipNPCDebuff, BuffID.BoneWhipNPCDebuff,
			BuffID.FlameWhipEnemyDebuff, BuffID.CoolWhipNPCDebuff, BuffID.SwordWhipNPCDebuff,
			BuffID.ScytheWhipEnemyDebuff, BuffID.MaceWhipNPCDebuff, BuffID.RainbowWhipNPCDebuff,
		};

		private readonly HashSet<int> known = new();

		private static bool IsWhipTag(int buffType) {
			if (ModContent.GetModBuff(buffType) is WhipTagBuff)
				return true;
			return System.Array.IndexOf(VanillaTags, buffType) >= 0;
		}

		public override void PostAI(NPC npc) {
			var present = new List<int>();
			for (int i = 0; i < NPC.maxBuffs; i++) {
				if (npc.buffTime[i] > 0 && IsWhipTag(npc.buffType[i]))
					present.Add(npc.buffType[i]);
			}

			if (present.Count > 1) {
				bool anyNew = false;
				foreach (int t in present)
					if (!known.Contains(t)) { anyNew = true; break; }

				if (anyNew) {
					for (int i = NPC.maxBuffs - 1; i >= 0; i--) { // DelBuff shifts later slots down
						int t = npc.buffType[i];
						if (npc.buffTime[i] > 0 && IsWhipTag(t) && known.Contains(t))
							npc.DelBuff(i);
					}
				}
			}

			known.Clear();
			for (int i = 0; i < NPC.maxBuffs; i++) {
				if (npc.buffTime[i] > 0 && IsWhipTag(npc.buffType[i]))
					known.Add(npc.buffType[i]);
			}
		}
	}
}
