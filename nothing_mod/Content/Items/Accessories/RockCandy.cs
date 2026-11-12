using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Accessories
{
	// Recipe ingredient quantity UNVERIFIED (1 gem critter).
	public class RockCandy : ModItem
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
			Item.buffTime = 10 * 60 * 60;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 20);
		}

		public override void AddRecipes()
		{
			foreach (int critter in new[] {
				ItemID.GemSquirrelAmethyst, ItemID.GemSquirrelTopaz, ItemID.GemSquirrelSapphire, ItemID.GemSquirrelEmerald,
				ItemID.GemSquirrelRuby, ItemID.GemSquirrelDiamond, ItemID.GemSquirrelAmber,
				ItemID.GemBunnyAmethyst, ItemID.GemBunnyTopaz, ItemID.GemBunnySapphire, ItemID.GemBunnyEmerald,
				ItemID.GemBunnyRuby, ItemID.GemBunnyDiamond, ItemID.GemBunnyAmber })
				CreateRecipe().AddIngredient(critter).AddTile(TileID.CookingPots).Register();
		}
	}
}
