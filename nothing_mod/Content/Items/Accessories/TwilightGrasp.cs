using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
		// TODO: +2 tag slots, 3x tag duration
	// Texture is a placeholder (vanilla sprite).
	public class TwilightGrasp : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.sellPrice(gold: 2, silver: 25);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<AccessoryEffectsPlayer>().WhipKnockback = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ModContent.ItemType<SilverBracer>()).AddIngredient(ModContent.ItemType<WickedArmlet>()).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
