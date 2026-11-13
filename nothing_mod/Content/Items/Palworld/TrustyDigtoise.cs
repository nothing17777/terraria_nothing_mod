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
	public class TrustyDigtoise : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.CopperPickaxe;

		public override void SetDefaults()
		{
			Item.DefaultToVanitypet(ModContent.ProjectileType<TrustyDigtoisePet>(), ModContent.BuffType<TrustyDigtoiseBuff>());
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(gold: 1);
		}

		public override bool? UseItem(Player player)
		{
			if (player.whoAmI == Main.myPlayer)
				player.AddBuff(Item.buffType, 3600);
			return true;
		}
		// TODO: mining at pickaxe power. Rarity/value UNVERIFIED.

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ModContent.ItemType<Digtoise>()).AddIngredient(ModContent.ItemType<LittleKinshipPeach>()).Register();
		}
	}
}
