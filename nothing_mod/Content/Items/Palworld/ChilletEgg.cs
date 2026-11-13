using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Palworld
{
	public class ChilletEgg : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = 9999;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(gold: 1);
		}

		public override bool CanRightClick() => true;

		public override void ModifyItemLoot(ItemLoot itemLoot)
		{
			// Contents UNVERIFIED (not read from wiki): one of Chillet / Chillet Ignis.
			itemLoot.Add(ItemDropRule.OneFromOptions(1, ModContent.ItemType<Chillet>(), ModContent.ItemType<ChilletIgnis>()));
		}
		// TODO: placeable 2x2 tile, acquisition.
	}
}
