using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Accessories
{
	// Frozen Wings +2: 3 s flight (180 ticks). Hand recipe: Frozen Wings +1 + 5 Spectre Bars.
	public class FrozenWingsPlus2 : FrozenWingsTier
	{
		protected override int FlightTicks => 180; // 3 s * 60
		protected override int TierRarity => ItemRarityID.Cyan;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<FrozenWingsPlus1>()
				.AddIngredient(ItemID.SpectreBar, 5)
				.Register();
		}
	}
}
