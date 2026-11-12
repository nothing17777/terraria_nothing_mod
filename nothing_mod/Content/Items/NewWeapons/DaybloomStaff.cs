using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Short-range burst of 4 pellets. Sprite is a placeholder (Amber Staff).
	public class DaybloomStaff : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 8;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 5;
			Item.width = 36;
			Item.height = 36;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 3f;
			Item.value = Item.sellPrice(copper: 20);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item43;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<DaybloomPellet>();
			Item.shootSpeed = 6f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (int i = 0; i < 4; i++)
			{
				Vector2 v = velocity.RotatedByRandom(0.18f) * Main.rand.NextFloat(0.85f, 1.15f);
				Projectile.NewProjectile(source, position, v, type, damage, knockback, player.whoAmI);
			}
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ItemID.Daybloom, 3).AddIngredient(ItemID.Gel, 5).AddIngredient(ItemID.Wood, 12).Register();
		}
	}
}
