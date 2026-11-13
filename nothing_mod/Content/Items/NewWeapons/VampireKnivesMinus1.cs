using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Vampire Knives -1: 20 damage. Hand recipe: Vampire Knives -2 + 10 Hellstone Bars.
	public class VampireKnivesMinus1 : VampireKnivesTier
	{
		protected override int TierDamage => 20;
		protected override int TierRarity => ItemRarityID.Green;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<VampireKnivesMinus2>()
				.AddIngredient(ItemID.HellstoneBar, 10)
				.Register();
		}
	}
}
