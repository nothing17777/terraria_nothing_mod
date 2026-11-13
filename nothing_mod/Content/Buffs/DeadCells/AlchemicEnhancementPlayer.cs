using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Buffs.DeadCells
{
	// Applies Alchemic Enhancement: a potion buff that is newly received (or re-drunk) gets +20% duration.
	// Wiki: affects buffs from consumable potions except food, Torch Blessing, Honey and flasks. The set is built from potion items (approximation, UNVERIFIED).
	public class AlchemicEnhancementPlayer : ModPlayer
	{
		private static HashSet<int> potionBuffs;
		private readonly int[] prevType = new int[Player.MaxBuffs];
		private readonly int[] prevTime = new int[Player.MaxBuffs];

		private static bool IsPotionBuff(int type)
		{
			if (potionBuffs == null)
			{
				potionBuffs = new HashSet<int>();
				foreach (Item it in ContentSamples.ItemsByType.Values)
				{
					if (it.consumable && it.buffType > 0 && it.buffTime > 0
						&& !BuffID.Sets.IsFedState[it.buffType] && !BuffID.Sets.IsAFlaskBuff[it.buffType] && it.buffType != BuffID.Honey)
						potionBuffs.Add(it.buffType);
				}
			}
			return potionBuffs.Contains(type);
		}

		public override void PostUpdate()
		{
			bool active = Player.HasBuff(ModContent.BuffType<AlchemicEnhancement>());
			for (int i = 0; i < Player.MaxBuffs; i++)
			{
				int type = Player.buffType[i];
				int time = Player.buffTime[i];
				if (active && type > 0 && time > 0 && (prevType[i] != type || time > prevTime[i] + 1) && IsPotionBuff(type))
				{
					time = (int)(time * 1.2f);
					Player.buffTime[i] = time;
				}
				prevType[i] = type;
				prevTime[i] = time;
			}
		}
	}
}
