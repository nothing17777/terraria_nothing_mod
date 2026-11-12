using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Huge bomb. Sprite is a placeholder (Bomb).
	public class PowerBomb : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 200;
			Item.DamageType = DamageClass.Default;
			Item.width = 20;
			Item.height = 20;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
			Item.knockBack = 9f;
			Item.value = Item.buyPrice(silver: 10); // sells for 2 silver
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<PowerBombProj>();
			Item.shootSpeed = 6f;
		}

		public override void AddRecipes()
		{
			// UNVERIFIED: bar quantity and output amount are not on the wiki page.
			CreateRecipe().AddIngredient(ItemID.Bomb, 5).AddIngredient(ItemID.CobaltBar, 1).AddTile(TileID.Anvils).Register();
			CreateRecipe().AddIngredient(ItemID.Bomb, 5).AddIngredient(ItemID.PalladiumBar, 1).AddTile(TileID.Anvils).Register();
		}
	}
}
