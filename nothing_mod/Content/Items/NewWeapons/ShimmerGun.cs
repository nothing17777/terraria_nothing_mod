using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Sprays Shimmer. Sprite is a placeholder (Slime Gun).
	public class ShimmerGun : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 1; // UNVERIFIED: wiki lists no damage
			Item.DamageType = DamageClass.Default; // UNVERIFIED
			Item.width = 36;
			Item.height = 20;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 0f; // UNVERIFIED
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item34;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<ShimmerStream>();
			Item.shootSpeed = 10f;
		}
		// TODO: obtained by throwing a Slime Gun into Shimmer (shimmer transmutation not hooked).
	}
}
