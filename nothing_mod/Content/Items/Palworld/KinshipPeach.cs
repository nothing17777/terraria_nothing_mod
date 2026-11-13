using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Palworld
{
	public class KinshipPeach : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.LifeFruit;

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.maxStack = 9999;
			Item.rare = ItemRarityID.Lime;
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ItemID.LifeFruit).AddIngredient(ItemID.SoulofLight, 12).AddIngredient(ItemID.SoulofNight, 12).AddTile(TileID.CrystalBall).Register();
		}
	}
}
