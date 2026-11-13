using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.DeadCells
{
	// Placeable flask. Tile art placeholder (Pots 2x2). TODO: ability to place an item inside (display-jar behaviour).
	public class ItemFlaskTile : ModTile
	{
		public override string Texture => "Terraria/Images/Tile_" + TileID.Pots;

		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(Type);
			DustType = DustID.Glass;
			AddMapEntry(new Color(120, 180, 200));
		}
	}
}
