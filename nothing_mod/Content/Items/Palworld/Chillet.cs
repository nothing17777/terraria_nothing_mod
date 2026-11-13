using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Palworld
{
	public class Chillet : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.ReindeerBells;

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.UseSound = SoundID.Item79;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 1);
			Item.mountType = MountID.Bunny; // placeholder mount
		}
		// TODO: real Chillet mount (ModMount), attack empowerment, Rocket Slam double-tap, petting. Mounts as a Bunny for now. Rarity UNVERIFIED.
	}
}
