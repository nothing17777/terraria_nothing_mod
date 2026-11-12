using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Spear that slimes enemies. Sprite is a placeholder (Spear).
	public class SlimeSpear : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Melee;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.knockBack = 6.5f;
			Item.value = Item.sellPrice(copper: 50);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<SlimeSpearProj>();
			Item.shootSpeed = 5.5f;
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ItemID.Gel, 50).AddTile(TileID.Solidifier).Register();
		}
	}
}
