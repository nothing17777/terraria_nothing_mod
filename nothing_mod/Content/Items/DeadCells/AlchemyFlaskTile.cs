using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using nothing_mod.Content.Buffs.DeadCells;

namespace nothing_mod.Content.Items.DeadCells
{
	// Alchemy Flask station: 4x4 (wiki), counts as an Alchemy Table (1/3 chance to save each potion ingredient), emits light,
	// right-click opens the crafting menu and grants Alchemic Enhancement (fresh buffs last 20% longer).
	public class AlchemyFlaskTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			Main.tileTable[Type] = true; // wiki: fulfills a house's table requirement
			Main.tileLighted[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Origin = new Point16(2, 3);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16 };
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type);
			AdjTiles = new int[] { TileID.AlchemyTable, TileID.Bottles };
			DustType = DustID.Glass;
			AddMapEntry(new Color(120, 200, 160));
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0.15f; g = 0.35f; b = 0.25f; // wiki: emits some light (colour UNVERIFIED)
		}

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.AddBuff(ModContent.BuffType<AlchemicEnhancement>(), 2);
			Main.playerInventory = true; // opens the inventory/crafting menu
			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = ModContent.ItemType<AlchemyFlask>();
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 64, ModContent.ItemType<AlchemyFlask>());
		}
	}
}
