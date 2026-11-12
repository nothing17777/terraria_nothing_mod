using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Players
{
	// 1.4.5 feature: recipes can use items from chests near the player.
	// The game asks every ModPlayer for extra crafting materials both when checking which recipes
	// are craftable and when actually consuming ingredients, so one hook covers both.
	public class ChestCraftingPlayer : ModPlayer
	{
		private const float RangePixels = 25 * 16; // 25 tiles

		// (chest index, slot) for each item handed to the game, in the same order.
		private readonly List<(int Chest, int Slot)> sources = new();

		public override IEnumerable<Item> AddMaterialsForCrafting(out ItemConsumedCallback itemConsumedCallback)
		{
			itemConsumedCallback = OnConsumed;
			sources.Clear();

			var items = new List<Item>();
			for (int c = 0; c < Main.maxChests; c++)
			{
				Chest chest = Main.chest[c];
				if (chest == null || c == Player.chest)
					continue; // the open chest is already handled by vanilla
				if (Vector2.Distance(Player.Center, new Vector2(chest.x * 16 + 16, chest.y * 16 + 16)) > RangePixels)
					continue;

				for (int s = 0; s < chest.item.Length; s++)
				{
					items.Add(chest.item[s]);
					sources.Add((c, s));
				}
			}
			return items;
		}

		private void OnConsumed(Item item, int index)
		{
			if (Main.netMode == NetmodeID.MultiplayerClient && index >= 0 && index < sources.Count)
				NetMessage.SendData(MessageID.SyncChestItem, -1, -1, null, sources[index].Chest, sources[index].Slot);
		}
	}
}
