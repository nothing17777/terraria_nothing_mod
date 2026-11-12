using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Accessory. Sprite is a placeholder (vanilla EyeoftheGolem).
	public class TemplarsSling : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.sellPrice(gold: 3, silver: 75);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetCritChance(DamageClass.Summon) += 10f; // approximation: wiki says sentry crit only
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ModContent.ItemType<HeavySling>()).AddIngredient(ItemID.EyeoftheGolem).AddTile(TileID.TinkerersWorkbench).Register();
		}
		// TODO: sentry-only crit and sentry carrying not implemented.
	}
}
