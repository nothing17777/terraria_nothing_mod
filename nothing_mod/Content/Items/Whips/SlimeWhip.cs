using nothing_mod.Content.Projectiles.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Whips
{
	public class SlimeWhip : ModItem
	{

		public override void SetDefaults() {
			Item.DefaultToWhip(ModContent.ProjectileType<SlimeWhipProjectile>(), 15, 1f, 4f, 30);  // UNVERIFIED shootSpeed (wiki lists 'velocity' only)
			Item.autoReuse = true; // hold to keep swinging, as in the real game
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(0, 0, 0, 50);
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.Gel, 50)
				.AddTile(TileID.Solidifier)
				.Register();
		}

		public override bool MeleePrefix() {
			return true;
		}
	}
}
