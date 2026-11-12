using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Accessories
{
	// Frozen Wings +1: 2.67 s flight (160 ticks). Hand recipe: Frozen Wings (vanilla, 2.17 s) + 5 Hallowed Bars.
	public class FrozenWingsPlus1 : FrozenWingsTier
	{
		protected override int FlightTicks => 160; // 2.67 s * 60, rounded
		protected override int TierRarity => ItemRarityID.Yellow;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.FrozenWings)
				.AddIngredient(ItemID.HallowedBar, 5)
				.Register();
		}
	}
}
