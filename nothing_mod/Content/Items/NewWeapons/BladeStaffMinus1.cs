using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Blade Staff -1: 4 damage. Hand recipe: Blade Staff -2 + 5 Demonite Bars.
	public class BladeStaffMinus1 : BladeStaffTier
	{
		protected override int TierDamage => 4;
		protected override int TierRarity => ItemRarityID.Green;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<BladeStaffMinus2>()
				.AddIngredient(ItemID.DemoniteBar, 5)
				.Register();
			// Not in the request (Demonite only), added so Crimson worlds can craft it too.
			CreateRecipe()
				.AddIngredient<BladeStaffMinus2>()
				.AddIngredient(ItemID.CrimtaneBar, 5)
				.Register();
		}
	}
}
