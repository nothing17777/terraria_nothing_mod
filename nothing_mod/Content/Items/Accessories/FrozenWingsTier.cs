using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
	// Shared base for the modded Frozen Wings tiers (-2, -1, +1, +2).
	// The middle tier is the vanilla Frozen Wings itself (2.17 s, 130 ticks), see FrozenWingsRecipes.
	// Everything except flight time and rarity is cloned from the vanilla item. Flight time is applied in
	// FrozenWingsPlayer. Abstract, so tModLoader does not load this class as content.
	public abstract class FrozenWingsTier : ModItem
	{
		// Flight time in ticks (60 ticks = 1 second).
		protected abstract int FlightTicks { get; }
		protected abstract int TierRarity { get; }

		// Placeholder sprite: the vanilla Frozen Wings icon.
		public override string Texture => "Terraria/Images/Item_" + ItemID.FrozenWings;

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.FrozenWings);
			Item.rare = TierRarity; // UNVERIFIED: rarity per tier is a design choice, not from the wiki
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<FrozenWingsPlayer>().FlightTicks = FlightTicks;
		}
	}
}
