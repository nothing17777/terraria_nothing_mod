using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Items.NewWeapons;

namespace nothing_mod.Content.Systems
{
	// The vanilla Sergeant United Shield is the middle tier (80 damage). It becomes craftable by hand:
	// Shield -1 + 5 Cobalt or Palladium Bars. No crafting station.
	public class SergeantShieldRecipes : ModSystem
	{
		public override void AddRecipes()
		{
			Recipe.Create(VanillaItemIds.SergeantUnitedShield)
				.AddIngredient<SergeantShieldMinus1>()
				.AddIngredient(ItemID.CobaltBar, 5)
				.Register();
			Recipe.Create(VanillaItemIds.SergeantUnitedShield)
				.AddIngredient<SergeantShieldMinus1>()
				.AddIngredient(ItemID.PalladiumBar, 5)
				.Register();
		}
	}
}
