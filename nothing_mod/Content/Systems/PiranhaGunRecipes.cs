using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Items.NewWeapons;

namespace nothing_mod.Content.Systems
{
	// The vanilla Piranha Gun is the middle tier (40 damage). It becomes craftable by hand:
	// Piranha Gun -1 + 5 Mythril or Orichalcum Bars. No crafting station.
	public class PiranhaGunRecipes : ModSystem
	{
		public override void AddRecipes()
		{
			Recipe.Create(ItemID.PiranhaGun)
				.AddIngredient<PiranhaGunMinus1>()
				.AddIngredient(ItemID.MythrilBar, 5)
				.Register();
			Recipe.Create(ItemID.PiranhaGun)
				.AddIngredient<PiranhaGunMinus1>()
				.AddIngredient(ItemID.OrichalcumBar, 5)
				.Register();
		}
	}
}
