using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
	// Texture is a placeholder (vanilla sprite).
	public class CatalystBand : ModItem
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
			player.pStone = true;
			player.GetModPlayer<AccessoryEffectsPlayer>().PoisonArrows = true;
			player.GetModPlayer<AccessoryEffectsPlayer>().CatalystBand = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ModContent.ItemType<PoisonBarb>()).AddIngredient(ItemID.PhilosophersStone).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
