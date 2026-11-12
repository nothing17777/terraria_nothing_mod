using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
	// Texture is a placeholder (vanilla sprite).
	public class CrossedHeartNecklace : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.sellPrice(gold: 1, silver: 50);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.panic = true;
			player.longInvince = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ItemID.PanicNecklace).AddIngredient(ItemID.CrossNecklace).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
