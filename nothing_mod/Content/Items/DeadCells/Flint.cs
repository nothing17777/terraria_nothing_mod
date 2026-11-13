using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Projectiles.DeadCells;

namespace nothing_mod.Content.Items.DeadCells
{
	public class Flint : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 70;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5f;
			Item.value = Item.sellPrice(gold: 4);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false; // swing is triggered on release (FlintPlayer.PreItemCheck)
			Item.shoot = ModContent.ProjectileType<FlintShot>();
			Item.shootSpeed = 1f;
		}

		// Wiki: the swing deals 178% of the weapon's damage (124 base); the wave of fire deals the weapon's damage.
		public const float SwingMultiplier = 1.78f;

		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
		{
			damage *= SwingMultiplier;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.whoAmI != Main.myPlayer)
				return false;
			bool full = player.GetModPlayer<FlintPlayer>().ReleasedFullCharge;
			float aura = full ? 1.3f : 1f; // wiki: aura becomes bigger as the weapon is charged
			int dir = player.direction;
			Vector2 aim = Main.MouseWorld - player.Center;
			Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<FlintSlash>(), 0, 0f, player.whoAmI, aim.ToRotation(), aura);
			if (full)
			{
				// Wave: first pillar 2 tiles in front of the player (wiki). Each pillar spawns the next one (see FlintShot).
				int waveDamage = (int)(damage / SwingMultiplier);
				Projectile.NewProjectile(source, new Vector2(player.Center.X + dir * 32f, player.Center.Y), Vector2.Zero, type, waveDamage, knockback, player.whoAmI, dir, -1f);
			}
			return false;
		}
		public override void AddRecipes()
		{
			Recipe r = CreateRecipe();
			if (RecipeGroup.recipeGroupIDs.TryGetValue("Adamantite", out int g)) // vanilla "Any Adamantite Bar" group name UNVERIFIED
				r.AddRecipeGroup(g, 24);
			else
				r.AddIngredient(ItemID.AdamantiteBar, 24);
			r.AddIngredient(ItemID.HellstoneBar, 20).AddTile(TileID.AdamantiteForge).Register(); // Titanium Forge counts via adjacent-tile rules
		}
	}
}
