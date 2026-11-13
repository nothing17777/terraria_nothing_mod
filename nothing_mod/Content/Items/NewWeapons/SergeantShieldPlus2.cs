using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Sergeant United Shield +2: 300 damage. Hand recipe: Shield +1 + 5 Shroomite Bars.
	public class SergeantShieldPlus2 : SergeantShieldTier
	{
		protected override int TierDamage => 300;
		protected override int TierRarity => ItemRarityID.Cyan;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<SergeantShieldPlus1>()
				.AddIngredient(ItemID.ShroomiteBar, 5)
				.Register();
		}
	}
}
