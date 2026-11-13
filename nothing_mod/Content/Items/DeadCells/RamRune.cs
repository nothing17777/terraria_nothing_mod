using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Items.DeadCells;

namespace nothing_mod.Content.Items.DeadCells
{
	public class RamRune : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Orange; // UNVERIFIED
			Item.value = Item.sellPrice(gold: 1, silver: 75);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<RamRunePlayer>().Equipped = true;
		}
		// TODO: obtained from Locked Gold Chests / Golden Lock Boxes in the Dungeon (post-Skeletron). Needs chest/crate loot hooks.
	}
}
