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
	public class KillingDeck : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 22;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 8;
			Item.width = 28;
			Item.height = 32;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 5f;
			Item.value = Item.sellPrice(gold: 4);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item8; // placeholder
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<KillingCard>();
			Item.shootSpeed = 24f; // wiki lists "24 (effective)"
		}
		// Wiki: cycles four attacks per use: 1) one card, 2) one card forward and one backward, 3) spread of six, 4) recall all cards.
		private int step;

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int cardType = ModContent.ProjectileType<KillingCard>();
			if (step == 3)
			{
				bool any = false;
				foreach (Projectile p in Main.ActiveProjectiles)
				{
					if (p.owner == player.whoAmI && p.type == cardType && p.ai[1] != KillingCard.Returning)
					{
						p.ai[1] = KillingCard.Returning;
						p.ai[2] = 0f;
						p.tileCollide = false;
						p.friendly = true;
						p.ResetLocalNPCHitImmunity();
						p.netUpdate = true;
						any = true;
					}
				}
				step = 0;
				if (any)
					return false; // wiki: if no cards are out, the recall is skipped and attack 1 is used instead
			}
			switch (step)
			{
				case 0:
					Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
					break;
				case 1:
					Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
					Projectile.NewProjectile(source, position, -velocity, type, damage, knockback, player.whoAmI);
					break;
				default:
					for (int i = 0; i < 6; i++) // spread angle UNVERIFIED
						Projectile.NewProjectile(source, position, velocity.RotatedBy(MathHelper.ToRadians(-25f + 10f * i)), type, damage, knockback, player.whoAmI);
					break;
			}
			step++;
			return false;
		}

		// TODO: sold by the Wizard for 20 gold during a Blood Moon (shop edit needed).
	}
}
