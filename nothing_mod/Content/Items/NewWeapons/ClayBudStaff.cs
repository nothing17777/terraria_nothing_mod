using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Summons a Clay Bud. Sprite is a placeholder (Slime Staff).
	public class ClayBudStaff : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 10; // UNVERIFIED
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(silver: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item44;
			Item.autoReuse = true;
			Item.buffType = ModContent.BuffType<ClayBudBuff>();
			Item.shoot = ModContent.ProjectileType<ClayBudMinion>();
			Item.shootSpeed = 10f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.AddBuff(Item.buffType, 2);
			Projectile p = Projectile.NewProjectileDirect(source, player.Center, Vector2.Zero, type, damage, knockback, player.whoAmI);
			p.originalDamage = Item.damage;
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ItemID.ClayBlock, 30).AddIngredient(ItemID.LesserHealingPotion).AddIngredient(ItemID.Daybloom)
				.AddIngredient(ItemID.Blinkroot).AddTile(TileID.Furnaces).Register();
		}
	}
}
