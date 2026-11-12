using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class BeheadedHead : ModItem
	{
		public override void SetDefaults() {
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 50000;
			Item.vanity = true;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.SoulofNight, 10)
				.AddIngredient(ItemID.ShadowCandle)
				.AddIngredient(ItemID.RottenChunk, 10)
				.AddTile(TileID.DemonAltar)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.SoulofNight, 10)
				.AddIngredient(ItemID.ShadowCandle)
				.AddIngredient(ItemID.Vertebrae, 10)
				.AddTile(TileID.DemonAltar)
				.Register();
		}
	}
}
