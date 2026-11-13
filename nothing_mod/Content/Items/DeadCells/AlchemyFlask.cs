using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.DeadCells
{
	public class AlchemyFlask : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.AlchemyTable;

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<AlchemyFlaskTile>());
			Item.width = 28;
			Item.height = 28;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(gold: 2);
		}
		// TODO: obtained by throwing an Alchemy Table into Shimmer (Shimmer transmutation not hooked). Alchemic Enhancement buff ("Fresh buffs last longer") not implemented.
	}
}
