using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Blade Staff +2: 20 damage. Hand recipe: Blade Staff +1 + 5 Spectre Bars.
	public class BladeStaffPlus2 : BladeStaffTier
	{
		protected override int TierDamage => 20;
		protected override int TierRarity => ItemRarityID.Cyan;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<BladeStaffPlus1>()
				.AddIngredient(ItemID.SpectreBar, 5)
				.Register();
		}
	}
}
