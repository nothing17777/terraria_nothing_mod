using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Accessory. Sprite is a placeholder (vanilla HoneyComb).
	public class SweetBarb : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(gold: 1, silver: 50);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<NewWeaponsPlayer>().sweetBarb = true;
		}
		// TODO: recipe (Poison Barb + Honey Comb) omitted, Poison Barb is not in 1.4.4.9; arrow poison + bee-on-hit effect not implemented.
	}
}
