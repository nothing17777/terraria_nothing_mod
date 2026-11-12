using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Red lightning arcs. Sprite is a placeholder (Laser Rifle).
	public class ArcSurge : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 180;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 18;
			Item.width = 40;
			Item.height = 20;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4f;
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item122;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<ArcSurgeArc>();
			Item.shootSpeed = 8f;
		}

		public override bool CanUseItem(Player player) => !player.wet; // unusable while in any liquid

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (int i = 0; i < 3; i++)
				Projectile.NewProjectile(source, position, velocity.RotatedByRandom(0.02f), type, damage, knockback, player.whoAmI);
			return false;
		}
		// TODO: Martian Saucer drop (2%) not added.
	}
}
