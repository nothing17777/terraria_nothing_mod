using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Piranha Gun -2: 10 damage. Hand recipe: 10 Dirt Block.
	public class PiranhaGunMinus2 : PiranhaGunTier
	{
		protected override int TierDamage => 10;
		protected override int TierRarity => ItemRarityID.White;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.DirtBlock, 10)
				.Register(); // no AddTile: crafted by hand
		}
	}
}
