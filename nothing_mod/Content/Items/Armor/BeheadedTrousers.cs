using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class BeheadedTrousers : ModItem
	{
		public override void SetDefaults() {
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 25000;
			Item.vanity = true;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.AncientCloth, 5)
				.AddIngredient(ItemID.TatteredCloth, 5)
				.AddTile(TileID.Loom)
				.Register();
		}
	}
}
