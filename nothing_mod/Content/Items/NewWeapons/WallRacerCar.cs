using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Summons a drivable RC car. Sprite is a placeholder (Minecart).
	public class WallRacerCar : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.width = 24;
			Item.height = 16;
			Item.useTime = 32;
			Item.useAnimation = 32;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.noMelee = true;
			Item.value = Item.buyPrice(gold: 10);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<WallRacerCarProj>();
			Item.shootSpeed = 1f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			bool removed = false;
			foreach (Projectile p in Main.ActiveProjectiles)
			{
				if (p.owner == player.whoAmI && p.type == type)
				{
					p.Kill();
					removed = true;
				}
			}
			if (!removed)
				Projectile.NewProjectile(source, player.Center, Vector2.Zero, type, 0, 0f, player.whoAmI);
			return false;
		}
		// TODO: Cyborg shop entry (Hardmode, post-Plantera) not added.
	}
}
