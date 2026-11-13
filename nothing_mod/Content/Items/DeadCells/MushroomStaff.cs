using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Projectiles.DeadCells;
using nothing_mod.Content.Buffs.DeadCells;

namespace nothing_mod.Content.Items.DeadCells
{
	public class MushroomStaff : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.SlimeStaff;

		public override void SetDefaults()
		{
			Item.damage = 8;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 10; // UNVERIFIED (wiki shows no mana)
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item44;
			Item.buffType = ModContent.BuffType<MushroomBoiBuff>();
			Item.shoot = ModContent.ProjectileType<MushroomBoiMinion>();
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
			// Quantities from the wiki recipe table (300 Glowing Mushroom as parsed; UNVERIFIED).
			CreateRecipe().AddIngredient(ItemID.GlowingMushroom, 300).AddIngredient(ItemID.MushroomGrassSeeds, 5).AddIngredient(ItemID.ShadowScale, 5).AddTile(TileID.Anvils).Register();
			CreateRecipe().AddIngredient(ItemID.GlowingMushroom, 300).AddIngredient(ItemID.MushroomGrassSeeds, 5).AddIngredient(ItemID.TissueSample, 5).AddTile(TileID.Anvils).Register();
		}
	}
}
