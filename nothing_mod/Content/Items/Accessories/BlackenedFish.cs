using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Accessories
{
	// Recipe ingredient quantity UNVERIFIED (1 fish).
	public class BlackenedFish : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useTurn = true;
			Item.UseSound = SoundID.Item2;
			Item.buffType = BuffID.WellFed;
			Item.buffTime = 5 * 60 * 60;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 3);
		}

		public override void AddRecipes()
		{
			foreach (int fish in new[] { ItemID.Bass, ItemID.NeonTetra, ItemID.Salmon, ItemID.Tuna, ItemID.Flounder, ItemID.Trout, ItemID.AtlanticCod, ItemID.RedSnapper })
				CreateRecipe().AddIngredient(fish).AddTile(TileID.CookingPots).Register();
		}
	}
}
