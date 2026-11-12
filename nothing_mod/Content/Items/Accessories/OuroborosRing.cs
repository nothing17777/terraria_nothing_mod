using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
		// TODO: +1 tag slot, extra whip swing
	// Texture is a placeholder (vanilla sprite).
	public class OuroborosRing : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.sellPrice(gold: 3, silver: 50);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<AccessoryEffectsPlayer>().WhipRange = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ModContent.ItemType<SnakeBand>()).AddIngredient(ModContent.ItemType<MobiusStrip>()).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
