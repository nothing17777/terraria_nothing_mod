using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems
{
	// Accessories only roll Warding, Menacing or Lucky. Only the best reforge modifier(s) can ever roll on an item, and crafted or browser-spawned
	// items start with it. "Best" = the prefix that gives the item the highest value
	// (ties all count). Works per item, so each weapon/accessory gets its own best prefix.
	public class BestPrefixesOnly : GlobalItem
	{
		private static bool checking; // Item.Prefix may call back into this hook

		// Set while the reforge panel applies a player-chosen prefix, so it is never blocked.
		public static bool Bypass;
		private static readonly Dictionary<int, (int Prefix, int Value)> bestByType = new();

		public override void Unload() => bestByType.Clear();

		// Accessories only ever roll one of these three.
		private static readonly int[] AccessoryPrefixes = { PrefixID.Warding, PrefixID.Menacing, PrefixID.Lucky };

		private static bool IsAccessory(Item item) => item.accessory && !item.vanity;

		// Returns the best prefix id for an item type (0 if none applies).
		public static int BestPrefix(int itemType)
		{
			if (ContentSamples.ItemsByType.TryGetValue(itemType, out Item sample) && IsAccessory(sample))
				return AccessoryPrefixes[Main.rand.Next(AccessoryPrefixes.Length)];

			bool wasChecking = checking;
			checking = true;
			try
			{
				return Best(itemType).Prefix;
			}
			finally
			{
				checking = wasChecking;
			}
		}

		private static (int Prefix, int Value) Best(int itemType)
		{
			if (bestByType.TryGetValue(itemType, out var cached))
				return cached;

			(int Prefix, int Value) best = (0, 0);
			for (int pre = 1; pre < PrefixLoader.PrefixCount; pre++)
			{
				Item test = new Item();
				test.SetDefaults(itemType);
				if (test.Prefix(pre) && test.value > best.Value)
					best = (pre, test.value);
			}
			bestByType[itemType] = best;
			return best;
		}

		public override bool AllowPrefix(Item item, int pre)
		{
			if (Bypass || checking || pre <= 0 || item.type <= 0)
				return true;

			if (IsAccessory(item))
				return System.Array.IndexOf(AccessoryPrefixes, pre) >= 0;

			checking = true;
			try
			{
				int best = Best(item.type).Value;
				if (best <= 0)
					return true;

				Item test = new Item();
				test.SetDefaults(item.type);
				if (!test.Prefix(pre))
					return true;

				return test.value >= best;
			}
			finally
			{
				checking = false;
			}
		}

		// Crafted items start with the best modifier.
		public override void OnCreated(Item item, ItemCreationContext context)
		{
			if (item.maxStack == 1 && item.prefix == 0)
			{
				int best = BestPrefix(item.type);
				if (best > 0)
					item.Prefix(best);
			}
		}
	}
}
