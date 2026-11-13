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

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<AlchemyFlaskTile>());
			Item.width = 28;
			Item.height = 28;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(gold: 2);
		}
		public override void SetStaticDefaults()
		{
			// Wiki: obtained by throwing an Alchemy Table or Bewitching Table into Shimmer. (The wiki's post-Skeletron gating is not applied; UNVERIFIED.)
			ItemID.Sets.ShimmerTransformToItem[ItemID.AlchemyTable] = Type;
			ItemID.Sets.ShimmerTransformToItem[ItemID.BewitchingTable] = Type;
		}
	}
}
