using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Items.NewWeapons;

namespace nothing_mod.Content.Systems
{
	// The vanilla Blade Staff is the middle tier (6 damage). It becomes craftable by hand:
	// Blade Staff -1 + 5 Mythril or Orichalcum Bars. No crafting station.
	public class BladeStaffRecipes : ModSystem
	{
		public override void AddRecipes()
		{
			Recipe.Create(VanillaItemIds.BladeStaff)
				.AddIngredient<BladeStaffMinus1>()
				.AddIngredient(ItemID.MythrilBar, 5)
				.Register();
			Recipe.Create(VanillaItemIds.BladeStaff)
				.AddIngredient<BladeStaffMinus1>()
				.AddIngredient(ItemID.OrichalcumBar, 5)
				.Register();
		}
	}
}
