using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Fires three homing petals. Sprite is a placeholder (Flower of Frost).
	public class MysticBloom : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 20;
			Item.width = 36;
			Item.height = 36;
			Item.useTime = 42;
			Item.useAnimation = 42;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 3.5f;
			Item.value = Item.sellPrice(gold: 1, silver: 50);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item8;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<MysticPetal>();
			Item.shootSpeed = 5f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (int i = -1; i <= 1; i++)
				Projectile.NewProjectile(source, position, velocity.RotatedBy(i * 0.25f), type, damage, knockback, player.whoAmI);
			return false;
		}

		public override void AddRecipes()
		{
			// UNVERIFIED: Nature's Gift quantity not on the wiki page.
			CreateRecipe().AddIngredient(ItemID.NaturesGift, 1).AddIngredient(ItemID.Moonglow, 2).AddIngredient(ItemID.Vine, 3)
				.AddIngredient(ItemID.RichMahogany, 7).AddTile(TileID.Anvils).Register();
		}
	}
}
