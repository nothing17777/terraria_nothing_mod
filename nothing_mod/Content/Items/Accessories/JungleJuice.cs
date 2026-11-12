using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Accessories
{
	public class JungleJuice : ModItem
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
			Item.healLife = 180;
			Item.potion = true;
			Item.rare = ItemRarityID.Lime;
			Item.value = Item.sellPrice(gold: 2, silver: 10);
		}

		public override void AddRecipes()
		{
			CreateRecipe(3)
				.AddIngredient(ItemID.GreaterHealingPotion, 3)
				.AddIngredient(ItemID.LifeFruit)
				.AddTile(TileID.Bottles)
				.Register();
			CreateRecipe(3)
				.AddIngredient(ItemID.GreaterHealingPotion, 3)
				.AddIngredient(ItemID.LifeFruit)
				.AddTile(TileID.AlchemyTable)
				.Register();
		}
	}
}
