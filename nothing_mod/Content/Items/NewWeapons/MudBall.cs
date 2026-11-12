using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Throwable that places mud where it lands. Sprite is a placeholder (Snowball).
	public class MudBall : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 8;
			Item.DamageType = DamageClass.Ranged; // UNVERIFIED class
			Item.width = 16;
			Item.height = 16;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.consumable = true;
			Item.autoReuse = true;
			Item.maxStack = Item.CommonMaxStack;
			Item.knockBack = 5.75f;
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<MudBallProj>();
			Item.shootSpeed = 7f;
		}

		public override void AddRecipes()
		{
			// UNVERIFIED: ingredient/output quantities not on the wiki page.
			CreateRecipe(1).AddIngredient(ItemID.MudBlock, 1).Register();
		}
	}
}
