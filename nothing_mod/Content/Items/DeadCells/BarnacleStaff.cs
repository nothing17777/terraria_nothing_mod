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
	public class BarnacleStaff : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 33;
			Item.DamageType = DamageClass.Summon;
			Item.sentry = true;
			Item.mana = 10; // UNVERIFIED
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.knockBack = 7.5f;
			Item.value = Item.sellPrice(gold: 4);
			Item.rare = ItemRarityID.LightRed; // UNVERIFIED
			Item.UseSound = SoundID.Item44;
			Item.shoot = ModContent.ProjectileType<BarnacleSentry>();
			Item.shootSpeed = 12f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.FindSentryRestingSpot(type, out int worldX, out int worldY, out int pushYUp);
			Projectile.NewProjectile(source, worldX, worldY - pushYUp, 0f, 0f, type, damage, knockback, player.whoAmI);
			player.UpdateMaxTurrets();
			return false;
		}
		// TODO: obtained rarely by fishing in the Hardmode Jungle (needs a fishing-loot hook).
	}
}
