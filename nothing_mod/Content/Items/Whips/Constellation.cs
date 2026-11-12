using nothing_mod.Content.Projectiles.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Whips
{
	public class Constellation : ModItem
	{

		public override void SetDefaults() {
			Item.DefaultToWhip(ModContent.ProjectileType<ConstellationProjectile>(), 130, 4f, 4f, 30);  // UNVERIFIED shootSpeed (wiki lists 'velocity' only)
			Item.autoReuse = true; // hold to keep swinging, as in the real game
			Item.rare = ItemRarityID.Red;
			Item.value = Item.sellPrice(0, 10, 0, 0);
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.FragmentStardust, 18)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

		public override bool MeleePrefix() {
			return true;
		}
	}
}
