using Microsoft.Xna.Framework;
using nothing_mod.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items
{
	// A sword that swings and also fires a void bolt.
	public class VoidBlade : ModItem
	{
		// Display name and tooltip live in Localization/en-US_Mods.nothing_mod.hjson
		public override void SetDefaults()
		{
			Item.damage = 35;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 22;
			Item.useAnimation = 22;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<VoidBolt>();
			Item.shootSpeed = 10f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.IronBar, 8);
			recipe.AddIngredient(ItemID.FallenStar, 3);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
