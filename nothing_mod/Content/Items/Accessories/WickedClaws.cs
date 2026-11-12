using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
		// TODO: +1 tag slot
	// Texture is a placeholder (vanilla sprite).
	public class WickedClaws : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.sellPrice(gold: 1, silver: 50);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<AccessoryEffectsPlayer>().WhipKnockback = true;
			player.GetAttackSpeed(DamageClass.Melee) += 0.12f;
			player.autoReuseGlove = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ModContent.ItemType<WickedArmlet>()).AddIngredient(ItemID.FeralClaws).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
