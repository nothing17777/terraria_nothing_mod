using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Face)]
	public class WeldingMask : ModItem
	{
		public override void SetDefaults() {
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 50000;
			Item.accessory = true;
			Item.vanity = true;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddRecipeGroup("IronBar", 10)
				.AddRecipeGroup(ArmorRecipeGroups.MythrilGroup, 5)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
