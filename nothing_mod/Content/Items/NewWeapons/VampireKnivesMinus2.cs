using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Vampire Knives -2: 10 damage. Hand recipe: 10 Wood.
	public class VampireKnivesMinus2 : VampireKnivesTier
	{
		protected override int TierDamage => 10;
		protected override int TierRarity => ItemRarityID.White;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddRecipeGroup(RecipeGroupID.Wood, 10)
				.Register(); // no AddTile: crafted by hand
		}
	}
}
