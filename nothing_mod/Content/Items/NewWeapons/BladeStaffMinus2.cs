using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Blade Staff -2: 2 damage. Hand recipe: 10 Stone Block.
	public class BladeStaffMinus2 : BladeStaffTier
	{
		protected override int TierDamage => 2;
		protected override int TierRarity => ItemRarityID.White;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.StoneBlock, 10)
				.Register(); // no AddTile: crafted by hand
		}
	}
}
