using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Blade Staff +1: 10 damage. Hand recipe: Blade Staff (vanilla, 6 damage) + 5 Hallowed Bars.
	public class BladeStaffPlus1 : BladeStaffTier
	{
		protected override int TierDamage => 10;
		protected override int TierRarity => ItemRarityID.Yellow;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Smolstar) // UNVERIFIED name: vanilla Blade Staff
				.AddIngredient(ItemID.HallowedBar, 5)
				.Register();
		}
	}
}
