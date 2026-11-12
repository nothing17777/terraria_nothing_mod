using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Slingshot that fires Acorns. Sprite is a placeholder (Blowpipe).
	public class AcornSlingshot : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.DamageType = DamageClass.Ranged; // UNVERIFIED class
			Item.width = 30;
			Item.height = 26;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4f;
			Item.value = Item.sellPrice(silver: 20);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<AcornShot>();
			Item.shootSpeed = 10f;
			Item.useAmmo = ItemID.Acorn;
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			type = ModContent.ProjectileType<AcornShot>(); // Acorn has no vanilla shoot value
		}
		// TODO: obtained from Living Wood Chests / shaking trees (no drop hook added).
	}
}
