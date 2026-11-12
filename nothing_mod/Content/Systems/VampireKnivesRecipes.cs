using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Items.NewWeapons;

namespace nothing_mod.Content.Systems
{
	// The vanilla Vampire Knives is the middle tier (29 damage). It becomes craftable by hand:
	// Vampire Knives -1 + 5 Mythril or Orichalcum Bars. No crafting station.
	public class VampireKnivesRecipes : ModSystem
	{
		public override void AddRecipes()
		{
			Recipe.Create(ItemID.VampireKnives)
				.AddIngredient<VampireKnivesMinus1>()
				.AddIngredient(ItemID.MythrilBar, 5)
				.Register();
			Recipe.Create(ItemID.VampireKnives)
				.AddIngredient<VampireKnivesMinus1>()
				.AddIngredient(ItemID.OrichalcumBar, 5)
				.Register();
		}
	}
}
