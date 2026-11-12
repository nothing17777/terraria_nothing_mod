using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Accessories
{
	// Frozen Wings -1: 1.67 s flight (100 ticks). Hand recipe: Frozen Wings -2 + 5 Hellstone Bars.
	public class FrozenWingsMinus1 : FrozenWingsTier
	{
		protected override int FlightTicks => 100; // 1.67 s * 60, rounded
		protected override int TierRarity => ItemRarityID.Green;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<FrozenWingsMinus2>()
				.AddIngredient(ItemID.HellstoneBar, 5)
				.Register();
		}
	}
}
