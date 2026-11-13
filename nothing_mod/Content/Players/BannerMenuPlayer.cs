using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using nothing_mod.Content.Systems;

namespace nothing_mod.Content.Players
{
	// 1.4.5 Banner Menu: banners from kills are stored here instead of going into the inventory.
	// Counts are owned by the client (saved in the player file); the server keeps a mirror, sent as the full
	// table on join (SyncPlayer) and whenever it changes (CopyClientState/SendClientChanges).
	// The "came from loot" flag travels with the world item (see BannerLootItem).
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

		public override void CopyClientState(ModPlayer targetCopy) {
			var c = (BannerMenuPlayer)targetCopy;
			c.Stored.Clear();
			foreach (var kv in Stored)
				c.Stored[kv.Key] = kv.Value;
		}

		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) {
			if (Main.netMode == NetmodeID.MultiplayerClient && Player.whoAmI == Main.myPlayer)
				SendStored(toWho, fromWho);
		}

		public override void SendClientChanges(ModPlayer clientPlayer) {
			var old = ((BannerMenuPlayer)clientPlayer).Stored;
			if (old.Count != Stored.Count || Stored.Any(kv => !old.TryGetValue(kv.Key, out int v) || v != kv.Value))
				SendStored(-1, Player.whoAmI);
		}

		private void SendStored(int toWho, int fromWho) {
			ModPacket p = Mod.GetPacket();
			p.Write((byte)nothing_mod.PacketType.BannerSync);
			p.Write(Stored.Count);
			foreach (var kv in Stored) {
				p.Write(kv.Key);
				p.Write(kv.Value);
			}
			p.Send(toWho, fromWho);
		}

		// Server side: replace the sender's mirror with what the client reported.
		public static void ReceiveStored(BinaryReader reader, int sender) {
			int n = reader.ReadInt32();
			var mp = Main.player[sender].GetModPlayer<BannerMenuPlayer>();
			mp.Stored.Clear();
			for (int i = 0; i < n; i++) {
				int type = reader.ReadInt32();
				int count = reader.ReadInt32();
				if (count > 0 && type > 0 && type < ItemLoader.ItemCount)
					mp.Stored[type] = count;
			}
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
