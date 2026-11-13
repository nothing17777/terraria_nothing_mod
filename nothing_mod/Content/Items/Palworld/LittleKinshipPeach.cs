using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Palworld
{
	public class LittleKinshipPeach : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.Peach;

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 9999;
			Item.rare = ItemRarityID.Orange; // UNVERIFIED
		}
		// TODO: acquisition/recipe not read from the wiki.
	}
}
