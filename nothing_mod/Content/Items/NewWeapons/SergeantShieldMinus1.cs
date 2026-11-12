using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Sergeant United Shield -1: 40 damage. Hand recipe: Shield -2 + 5 Demonite or Crimtane Bars.
	public class SergeantShieldMinus1 : SergeantShieldTier
	{
		protected override int TierDamage => 40;
		protected override int TierRarity => ItemRarityID.Green;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<SergeantShieldMinus2>()
				.AddIngredient(ItemID.DemoniteBar, 5)
				.Register();
			CreateRecipe()
				.AddIngredient<SergeantShieldMinus2>()
				.AddIngredient(ItemID.CrimtaneBar, 5)
				.Register();
		}
	}
}
