using Terraria.ID;

namespace nothing_mod.Content.Systems
{
	// Vanilla items whose ItemID.* names could not be used directly.
	public static class VanillaItemIds
	{
		// ItemID.SergeantUnitedShield does not exist (error CS0117), so the wiki item ID is used.
		// Vanilla code itself checks this item by number (Player.cs: HeldItem.type == 4760).
		public const int SergeantUnitedShield = 4760;

		// ItemID.Smolstar is the Blade Staff. Confirmed: tModLoader's own source uses ItemID.Smolstar
		// (ItemDropDatabase, ProjAIStyleID docs). The wiki lists it as item 4758.
		public const int BladeStaff = ItemID.Smolstar;
	}
}
