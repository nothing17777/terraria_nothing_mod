using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Projectiles.Palworld;
using nothing_mod.Content.Buffs.Palworld;

namespace nothing_mod.Content.Items.Palworld
{
	public class Cattiva : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 11;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 10; // UNVERIFIED
			Item.width = 24;
			Item.height = 24;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(gold: 1); // UNVERIFIED
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item44;
			Item.buffType = ModContent.BuffType<CattivaBuff>();
			Item.shoot = ModContent.ProjectileType<CattivaMinion>();
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.AddBuff(Item.buffType, 2);
			Projectile p = Projectile.NewProjectileDirect(source, player.Center, Vector2.Zero, type, damage, knockback, player.whoAmI);
			p.originalDamage = Item.damage;
			return false;
		}
		// TODO: obtained via distressed-pal rescue (not implemented); helps mining. Sell value UNVERIFIED.
	}
}
