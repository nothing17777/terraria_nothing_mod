using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Thrown light source that cycles through rainbow colors. Sprite is a placeholder (Bouncy Glowstick).
	public class RainbowGlowstick : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.BouncyGlowstick;

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.width = 14;
			Item.height = 28;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.buyPrice(copper: 20); // sells for 4 copper
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<RainbowGlowstickProj>();
			Item.shootSpeed = 6f;
		}
		// TODO: Merchant party shop entry and Celebration Mk 10 chest loot not added.
	}
}
