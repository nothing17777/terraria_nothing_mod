using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class HallowedCrown : ModItem
	{
		public override void SetDefaults() {
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Pink;
			Item.value = 250000;
			Item.vanity = true;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.HallowedBar, 12)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
