using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.DeadCells
{
	// Placeable alchemy station. Tile art is a placeholder (Alchemy Table, 3x2); wiki size is 4x4 (UNVERIFIED) so TODO real art/size.
	public class AlchemyFlaskTile : ModTile
	{
		public override string Texture => "Terraria/Images/Tile_" + TileID.AlchemyTable;

		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.addTile(Type);
			AdjTiles = new int[] { TileID.AlchemyTable };
			DustType = DustID.Glass;
			AddMapEntry(new Color(120, 200, 160));
		}

		// TODO: right-click opens its own crafting/ingredient-saving behavior ("Usable when placed" + Alchemic Enhancement buff).
	}
}
