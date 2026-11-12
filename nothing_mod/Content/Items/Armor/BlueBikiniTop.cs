using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class BlueBikiniTop : ModItem
	{
		public override void SetDefaults() {
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Blue;
			Item.value = 20000;
			Item.vanity = true;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.Silk, 20)
				.AddTile(TileID.Loom)
				.Register();
		}
	}
}
