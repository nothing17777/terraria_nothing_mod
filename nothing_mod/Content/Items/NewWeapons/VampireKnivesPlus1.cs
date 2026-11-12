using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Vampire Knives +1: 42 damage. Hand recipe: Vampire Knives (vanilla, 29 damage) + 5 Hallowed Bars.
	public class VampireKnivesPlus1 : VampireKnivesTier
	{
		protected override int TierDamage => 42;
		protected override int TierRarity => ItemRarityID.Cyan;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.VampireKnives)
				.AddIngredient(ItemID.HallowedBar, 5)
				.Register();
		}
	}
}
