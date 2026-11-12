using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
	// Grants the Torch Blessing buff (8 min, ref itemfunc buffTime 28800); the buff converts nearby biome torches
	// (see Content/Players/AccessoryBuffs.cs). Recipe verified against the reference recipe table (tile Bottles).
	// TODO: the in-game unlock condition (Torch God's Favor consumed) is not enforced.
	public class TorchGodsFlavor : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 30;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useTurn = true;
			Item.UseSound = SoundID.Item3;
			Item.buffType = ModContent.BuffType<TorchBlessing>();
			Item.buffTime = 28800;
			Item.rare = ItemRarityID.Green;
			Item.value = Item.sellPrice(silver: 2);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.BottledWater)
				.AddIngredient(ItemID.Torch)
				.AddIngredient(ItemID.Daybloom)
				.AddIngredient(ItemID.Moonglow)
				.AddIngredient(ItemID.Fireblossom)
				.AddTile(TileID.Bottles)
				.Register();
		}
	}
}
