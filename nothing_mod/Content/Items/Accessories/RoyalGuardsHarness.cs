using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
		// TODO: carry sentries; recipe needs Heavy Sling (only added if a HeavySling item exists in the mod)
	// Texture is a placeholder (vanilla sprite).
	public class RoyalGuardsHarness : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.LightPurple;
			Item.value = Item.sellPrice(gold: 1, silver: 50);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense += 1;
			player.noKnockback = true;
		}

		public override void AddRecipes()
		{
			if (ModContent.TryFind<ModItem>("nothing_mod/HeavySling", out ModItem sling))
				CreateRecipe().AddIngredient(sling.Type).AddIngredient(ItemID.CobaltShield).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
