using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Calls lightning at the cursor. Sprite is a placeholder (Thunder Staff).
	public class LightningStrike : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 150;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 30;
			Item.crit = 10; // 14% total
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 55;
			Item.useAnimation = 55;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 10f;
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.LightPurple;
			Item.UseSound = SoundID.Item122;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<LightningStrikeSpark>();
			Item.shootSpeed = 8f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, type, damage, knockback, player.whoAmI);
			return false;
		}
		// TODO: recipe (Thunder Zapper + 24 Adamantite/Titanium Bar + 20 Soul of Night at Mythril Anvil) omitted: Thunder Zapper does not exist in 1.4.4.9.
	}
}
