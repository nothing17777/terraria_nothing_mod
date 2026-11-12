using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
	// Texture is a placeholder (vanilla sprite).
	public class StrungCounterweight : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(gold: 1, silver: 3);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.yoyoString = true; // vanilla yoyo AI: +25% radius and longer lifetime (ref Item.stringColor > 0 -> yoyoString)
			player.counterWeight = ProjectileID.BlackCounterweight; // UNVERIFIED: 1.4.5 uses projectile 1079 (Pink), which 1.4.4.9 does not have
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ItemID.WhiteString).AddIngredient(ItemID.BlackCounterweight).AddTile(TileID.TinkerersWorkbench).Register();
			CreateRecipe().AddIngredient(ItemID.WhiteString).AddIngredient(ItemID.PurpleCounterweight).AddTile(TileID.TinkerersWorkbench).Register();
			CreateRecipe().AddIngredient(ItemID.WhiteString).AddIngredient(ItemID.RedCounterweight).AddTile(TileID.TinkerersWorkbench).Register();
			CreateRecipe().AddIngredient(ItemID.WhiteString).AddIngredient(ItemID.GreenCounterweight).AddTile(TileID.TinkerersWorkbench).Register();
			CreateRecipe().AddIngredient(ItemID.WhiteString).AddIngredient(ItemID.BlueCounterweight).AddTile(TileID.TinkerersWorkbench).Register();
			CreateRecipe().AddIngredient(ItemID.WhiteString).AddIngredient(ItemID.YellowCounterweight).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
