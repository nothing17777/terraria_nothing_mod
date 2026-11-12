using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
	// Texture is a placeholder (vanilla sprite).
	public class HarpyCharm : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Green;
			Item.value = Item.sellPrice(silver: 65);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetCritChance(DamageClass.Ranged) += 5f;
			player.GetModPlayer<AccessoryEffectsPlayer>().HarpyCharm = true; // ref: 90 tick redirect cooldown
		}

		public override void AddRecipes()
		{
			foreach (int bar in new[] { ItemID.SilverBar, ItemID.TungstenBar })
				CreateRecipe().AddIngredient(ItemID.Amber, 3).AddIngredient(ItemID.Feather, 7).AddIngredient(bar, 5).AddTile(TileID.Anvils).Register();
		}
	}
}
