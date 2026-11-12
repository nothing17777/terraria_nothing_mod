using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Piranha Gun -1: 20 damage. Hand recipe: Piranha Gun -2 + 10 Hellstone Bars.
	public class PiranhaGunMinus1 : PiranhaGunTier
	{
		protected override int TierDamage => 20;
		protected override int TierRarity => ItemRarityID.Green;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<PiranhaGunMinus2>()
				.AddIngredient(ItemID.HellstoneBar, 10)
				.Register();
		}
	}
}
