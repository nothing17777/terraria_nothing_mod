using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Items.Accessories;

namespace nothing_mod.Content.Systems
{
	// The vanilla Frozen Wings is the middle tier (2.17 s). It becomes craftable by hand:
	// Frozen Wings -1 + 5 Palladium or Mythril Bars. No crafting station.
	public class FrozenWingsRecipes : ModSystem
	{
		public override void AddRecipes()
		{
			Recipe.Create(ItemID.FrozenWings)
				.AddIngredient<FrozenWingsMinus1>()
				.AddIngredient(ItemID.PalladiumBar, 5)
				.Register();
			Recipe.Create(ItemID.FrozenWings)
				.AddIngredient<FrozenWingsMinus1>()
				.AddIngredient(ItemID.MythrilBar, 5)
				.Register();
		}
	}
}
