using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Piranha Gun +1: 60 damage. Hand recipe: Piranha Gun (vanilla, 40 damage) + 5 Hallowed Bars.
	public class PiranhaGunPlus1 : PiranhaGunTier
	{
		protected override int TierDamage => 60;
		protected override int TierRarity => ItemRarityID.Cyan;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.PiranhaGun)
				.AddIngredient(ItemID.HallowedBar, 5)
				.Register();
		}
	}
}
