using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems
{
	// Marks banner items that dropped from an NPC kill so BannerMenuPlayer can intercept their pickup.
	// The flag is synced with the world item (NetSend/NetReceive) so the picking client can see it.
	public class BannerLootItem : GlobalItem
	{
		public override bool InstancePerEntity => true;
		protected override bool CloneNewInstances => true; // UNVERIFIED: member name/visibility for cloning in this tML version

		public bool FromLoot;

		public static bool IsBanner(Item item) => item.createTile == TileID.Banners;

		public override void OnSpawn(Item item, IEntitySource source)
		{
			// UNVERIFIED: vanilla banner drops use EntitySource_Loot with the NPC as the entity.
			if (IsBanner(item) && source is EntitySource_Loot)
				FromLoot = true;
		}

		// Let the item be picked up even with a full inventory, since we absorb it into the menu.
		public override bool ItemSpace(Item item, Player player) => FromLoot;

		public override void NetSend(Item item, BinaryWriter writer) => writer.Write(FromLoot);

		public override void NetReceive(Item item, BinaryReader reader) => FromLoot = reader.ReadBoolean();
	}
}
