using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
		// TODO: +1 tag slot, 3x tag duration
	// Texture is a placeholder (vanilla sprite).
	public class SilverShield : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(gold: 1, silver: 75);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense += 2;
			player.noKnockback = true;
			player.fireWalk = true;
			player.buffImmune[BuffID.OnFire] = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ModContent.ItemType<SilverBracer>()).AddIngredient(ItemID.ObsidianShield).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
