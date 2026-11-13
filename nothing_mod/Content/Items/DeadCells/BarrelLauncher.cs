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
	public class BarrelLauncher : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.GrenadeLauncher;

		public override void SetDefaults()
		{
			Item.damage = 100;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 20;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 8f;
			Item.value = Item.sellPrice(gold: 4);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item61;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<BarrelProjectile>();
			Item.shootSpeed = 11f;
		}
		// TODO: acquisition (drops from Flying Dutchman / Pirate Captain / pirates per wiki; rates not recorded). Sound is a placeholder.
	}
}
