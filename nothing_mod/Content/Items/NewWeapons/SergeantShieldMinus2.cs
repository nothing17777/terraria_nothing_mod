using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Sergeant United Shield -2: 20 damage. Hand recipe: 10 Stone Block + 10 Wood.
	public class SergeantShieldMinus2 : SergeantShieldTier
	{
		protected override int TierDamage => 20;
		protected override int TierRarity => ItemRarityID.White;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.StoneBlock, 10)
				.AddRecipeGroup(RecipeGroupID.Wood, 10)
				.Register(); // no AddTile: crafted by hand
		}
	}
}
