using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Mounts;

namespace nothing_mod.Content.Items.Palworld
{
	public class Chillet : ModItem
	{

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
			Item.mountType = ModContent.MountType<ChilletMount>();
		}
		// Mount: see Content/Mounts (placeholder Bunny art). Rocket Slam / +10% damage: ChilletPlayer. Petting not implemented. Rarity UNVERIFIED.
	}
}
