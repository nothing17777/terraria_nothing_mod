using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
		// TODO: +1 tag slot (no tag-slot system in 1.4.4.9). Tag-hit pulse approximated in AccessoryPulse.cs.
	// Texture is a placeholder (vanilla sprite).
	public class ArmletOfRuin : ModItem
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
			player.GetModPlayer<AccessoryEffectsPlayer>().WhipKnockback = true;
			player.GetModPlayer<AccessoryEffectsPlayer>().ArmletPulse = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ModContent.ItemType<SnappingStone>()).AddIngredient(ModContent.ItemType<WickedArmlet>()).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
