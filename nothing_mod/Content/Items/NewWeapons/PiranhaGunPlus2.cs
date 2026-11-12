using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Piranha Gun +2: 84 damage. Hand recipe: Piranha Gun +1 + 5 Shroomite Bars.
	public class PiranhaGunPlus2 : PiranhaGunTier
	{
		protected override int TierDamage => 84;
		protected override int TierRarity => ItemRarityID.Red;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<PiranhaGunPlus1>()
				.AddIngredient(ItemID.ShroomiteBar, 5)
				.Register();
		}
	}
}
