using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.DeadCells
{
	public class ItemFlask : ModItem
	{

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<ItemFlaskTile>());
			Item.width = 20;
			Item.height = 28;
			Item.rare = ItemRarityID.White;
		}

		public override void AddRecipes()
		{
			// Wiki: 6 Glass + Chain at a Furnace (Chain quantity not shown, assumed 1; UNVERIFIED).
			CreateRecipe().AddIngredient(ItemID.Glass, 6).AddIngredient(ItemID.Chain).AddTile(TileID.Furnaces).Register();
		}
	}
}
