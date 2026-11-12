using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Chargeable icicle lance. Sprite is a placeholder (Ice Rod).
	public class GlacierFang : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 11;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 10;
			Item.crit = 11; // 15% total
			Item.width = 36;
			Item.height = 36;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(silver: 40);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item28;
			Item.shoot = ModContent.ProjectileType<GlacierIcicle>();
			Item.shootSpeed = 4f;
		}
		// TODO: drops (Boreal/Frozen Crate, Frozen Chest, Ice Mimic) and Shimmer transmutation not added.
	}
}
