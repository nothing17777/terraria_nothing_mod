using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Vampire Knives +2: 64 damage. Hand recipe: Vampire Knives +1 + 5 Spectre Bars.
	// The request said "vampire knives and 5 spectre bars"; read as +1, matching the other upgrade chains.
	public class VampireKnivesPlus2 : VampireKnivesTier
	{
		protected override int TierDamage => 64;
		protected override int TierRarity => ItemRarityID.Red;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<VampireKnivesPlus1>()
				.AddIngredient(ItemID.SpectreBar, 5)
				.Register();
		}
	}
}
