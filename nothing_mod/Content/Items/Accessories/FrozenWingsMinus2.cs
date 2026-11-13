using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Accessories
{
	// Frozen Wings -2: 0.42 s flight (25 ticks). Hand recipe: 5 Dirt Block.
	public class FrozenWingsMinus2 : FrozenWingsTier
	{
		protected override int FlightTicks => 25; // 0.42 s * 60, rounded
		protected override int TierRarity => ItemRarityID.White;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.DirtBlock, 5)
				.Register(); // no AddTile: crafted by hand
		}
	}
}
