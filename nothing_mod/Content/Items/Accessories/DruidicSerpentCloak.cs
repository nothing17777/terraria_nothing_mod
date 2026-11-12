using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
		// TODO: extra whip swing (stars use vanilla Star Cloak damage, not 75/150/225)
	// Texture is a placeholder (vanilla sprite).
	public class DruidicSerpentCloak : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.sellPrice(gold: 2);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.starCloakItem = Item;
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ModContent.ItemType<SnakeBand>()).AddIngredient(ItemID.StarCloak).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
