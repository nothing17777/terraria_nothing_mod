using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
	// Texture is a placeholder (vanilla sprite).
	public class MysticArtsSash : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.sellPrice(gold: 2);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.lifeRegen += 2;
			player.statManaMax2 += 40;
			player.manaRegenBonus += 60; // changelog: band regen 30 mana/s; ref accfx manaRegenBonus 60
			player.blackBelt = true; // vanilla 10% dodge
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ItemID.ManaRegenerationBand).AddIngredient(ItemID.BlackBelt).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
