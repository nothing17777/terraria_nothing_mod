using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Projectiles.Palworld;
using nothing_mod.Content.Buffs.Palworld;

namespace nothing_mod.Content.Items.Palworld
{
	public class Foxparks : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Summon;
			Item.width = 24;
			Item.height = 24;
			Item.useTime = 4; // flamethrower rate (wiki: one flame per ~0.07 s); the summon only happens when no Foxparks is out
			Item.useAnimation = 4;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.knockBack = 3f;
			Item.value = Item.sellPrice(gold: 1); // UNVERIFIED
			Item.rare = ItemRarityID.LightRed;
			Item.shootSpeed = 5f;
			Item.buffType = ModContent.BuffType<FoxparksBuff>();
			Item.shoot = ModContent.ProjectileType<FoxparksMinion>();
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int mine = ModContent.ProjectileType<FoxparksMinion>();
			if (player.ownedProjectileCounts[mine] <= 0)
			{
				// Summon. Foxparks and Trusty Foxparks replace each other (wiki).
				foreach (Projectile p in Main.ActiveProjectiles)
					if (p.owner == player.whoAmI && p.type == ModContent.ProjectileType<TrustyFoxparksMinion>())
						p.Kill();
				player.AddBuff(Item.buffType, 2);
				SoundEngine.PlaySound(SoundID.Item44, player.Center);
				Projectile m = Projectile.NewProjectileDirect(source, player.Center, Vector2.Zero, type, damage, knockback, player.whoAmI);
				m.originalDamage = Item.damage;
				return false;
			}
			// Huggy Fire: held flamethrower, no ammo (wiki: 100% damage and knockback).
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<FoxparksFlames>(), damage, knockback, player.whoAmI, 0);
			return false;
		}
		// Distressed-pal rescue acquisition not implemented (needs NPC spawn logic outside this folder). Sell value UNVERIFIED.
	}
}
