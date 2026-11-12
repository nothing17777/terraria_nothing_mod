using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
	// Texture is a placeholder (vanilla sprite).
	public class RestorationShield : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Green;
			Item.value = Item.sellPrice(gold: 1, silver: 50);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense += 1;
			player.noKnockback = true;
			player.lifeRegen += 2;
			player.statManaMax2 += 40;
			player.manaRegenBonus += 60; // changelog: band regen 30 mana/s; ref accfx manaRegenBonus 60
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ItemID.ManaRegenerationBand).AddIngredient(ItemID.CobaltShield).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
