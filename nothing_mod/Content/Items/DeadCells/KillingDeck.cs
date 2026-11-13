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
		public override string Texture => "Terraria/Images/Item_" + ItemID.WaterBolt;

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
		// TODO: sold by the Wizard for 20 gold during a Blood Moon (shop edit needed).
	}
}
