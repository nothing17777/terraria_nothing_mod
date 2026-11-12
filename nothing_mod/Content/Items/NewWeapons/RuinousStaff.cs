using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Summons forbidden ruins. Sprite is a placeholder (Desert Tiger Staff).
	public class RuinousStaff : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 10; // UNVERIFIED
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.knockBack = 4f;
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item44;
			Item.autoReuse = true;
			Item.buffType = ModContent.BuffType<RuinousStaffBuff>();
			Item.shoot = ModContent.ProjectileType<ForbiddenRuinMinion>();
			Item.shootSpeed = 10f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.AddBuff(Item.buffType, 2);
			Projectile p = Projectile.NewProjectileDirect(source, Main.MouseWorld, Vector2.Zero, type, damage, knockback, player.whoAmI);
			p.originalDamage = Item.damage;
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ItemID.DarkShard).AddIngredient(ItemID.LightShard).AddIngredient(ItemID.AncientBattleArmorMaterial, 2)
				.AddIngredient(ItemID.SoulofNight, 5).AddIngredient(ItemID.SoulofLight, 5).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
