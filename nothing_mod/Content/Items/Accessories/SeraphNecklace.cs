using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
	// Texture is a placeholder (vanilla sprite).
	public class SeraphNecklace : ModItem
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
			player.longInvince = true;
			player.GetModPlayer<AccessoryEffectsPlayer>().HarpyCharm = true;
			player.GetModPlayer<AccessoryEffectsPlayer>().SeraphCharm = true; // ref: cooldown 90 x 0.8 = 72 ticks
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ModContent.ItemType<HarpyCharm>()).AddIngredient(ItemID.CrossNecklace).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
