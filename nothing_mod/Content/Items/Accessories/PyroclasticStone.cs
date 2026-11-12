using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
	// Texture is a placeholder (vanilla sprite).
	public class PyroclasticStone : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.sellPrice(gold: 2);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.magmaStone = true; // ref ApplyEquipFunctional 6178: magmaStone + pulse
			player.GetModPlayer<AccessoryEffectsPlayer>().PyroclasticPulse = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ModContent.ItemType<SnappingStone>()).AddIngredient(ItemID.MagmaStone).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
