using nothing_mod.Content.Projectiles.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Whips
{
	public class Starcrash : ModItem
	{

		public override void SetDefaults() {
			Item.DefaultToWhip(ModContent.ProjectileType<StarcrashProjectile>(), 18, 1.5f, 4f, 30);  // UNVERIFIED shootSpeed (wiki lists 'velocity' only)
			Item.autoReuse = true; // hold to keep swinging, as in the real game
			Item.rare = ItemRarityID.Green;
			Item.value = Item.sellPrice(0, 2, 50, 0);
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.MeteoriteBar, 20)
				.AddIngredient(ItemID.Chain, 10)
				.AddIngredient(ItemID.FallenStar, 10)
				.AddTile(TileID.Anvils)
				.Register();
		}

		public override bool MeleePrefix() {
			return true;
		}
	}
}
