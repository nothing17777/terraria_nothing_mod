using nothing_mod.Content.Projectiles.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Whips
{
	public class Cobwhip : ModItem
	{

		public override void SetDefaults() {
			Item.DefaultToWhip(ModContent.ProjectileType<CobwhipProjectile>(), 9, 0.7f, 4f, 35);  // UNVERIFIED shootSpeed (wiki lists 'velocity' only)
			Item.autoReuse = true; // hold to keep swinging, as in the real game
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(0, 0, 1, 50);
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.Cobweb, 30)
				.AddRecipeGroup(RecipeGroupID.Wood, 10)
				.AddTile(TileID.Loom)
				.Register();
		}

		public override bool MeleePrefix() {
			return true;
		}
	}
}
