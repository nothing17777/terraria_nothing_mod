using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Sergeant United Shield +1: 160 damage. Hand recipe: Sergeant United Shield (vanilla, 80 damage) + 5 Hallowed Bars.
	public class SergeantShieldPlus1 : SergeantShieldTier
	{
		protected override int TierDamage => 160;
		protected override int TierRarity => ItemRarityID.Yellow;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.SergeantUnitedShield)
				.AddIngredient(ItemID.HallowedBar, 5)
				.Register();
		}
	}
}
