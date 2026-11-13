using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using nothing_mod.Content.Systems;

namespace nothing_mod.Content.Players
{
	// 1.4.5 Banner Menu: banners from kills are stored here instead of going into the inventory.
	// Counts are owned by the client and saved with the player file, so there is no server-side copy to sync;
	// the "came from loot" flag travels with the world item (see BannerLootItem).
	public class BannerMenuPlayer : ModPlayer
	{
		public readonly Dictionary<int, int> Stored = new(); // banner item type -> count
		public readonly HashSet<int> Fresh = new();          // newly obtained, highlighted until hovered/withdrawn

		public override bool OnPickup(Item item)
		{
			if (!item.TryGetGlobalItem(out BannerLootItem loot) || !loot.FromLoot)
				return true;

			Stored.TryGetValue(item.type, out int have);
			int max = System.Math.Max(item.maxStack, 1);
			// Full unclaimed stack: the banner is not granted (it is still consumed).
			int add = System.Math.Min(item.stack, max - have);
			if (add > 0)
			{
				Stored[item.type] = have + add;
				Fresh.Add(item.type);
			}
			item.TurnToAir(); // UNVERIFIED: needed so the world item disappears when returning false
			return false;
		}

		public void Withdraw(int type, int amount)
		{
			if (!Stored.TryGetValue(type, out int have) || have <= 0)
				return;
			amount = System.Math.Min(amount, have);
			Player.QuickSpawnItem(Player.GetSource_Misc("nothing_mod:BannerMenu"), type, amount);
			if (have - amount <= 0)
				Stored.Remove(type);
			else
				Stored[type] = have - amount;
			Fresh.Remove(type);
		}

		public override void SaveData(TagCompound tag)
		{
			var types = Stored.Keys.ToList();
			tag["types"] = types;
			tag["counts"] = types.Select(t => Stored[t]).ToList();
			tag["fresh"] = Fresh.ToList();
		}

		public override void LoadData(TagCompound tag)
		{
			Stored.Clear();
			Fresh.Clear();
			var types = tag.GetList<int>("types");
			var counts = tag.GetList<int>("counts");
			for (int i = 0; i < types.Count && i < counts.Count; i++)
				if (counts[i] > 0)
					Stored[types[i]] = counts[i];
			foreach (int t in tag.GetList<int>("fresh"))
				if (Stored.ContainsKey(t))
					Fresh.Add(t);
		}
	}
}
