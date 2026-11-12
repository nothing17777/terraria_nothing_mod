using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Accessory. Sprite is a placeholder (vanilla PutridScent).
	public class ScoutsSling : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Lime;
			Item.value = Item.sellPrice(gold: 4, silver: 25);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Generic) += 0.05f;
			player.aggro -= 400;
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ModContent.ItemType<HeavySling>()).AddIngredient(ItemID.PutridScent).AddTile(TileID.TinkerersWorkbench).Register();
		}
		// TODO: sentry carrying not implemented.
	}
}
