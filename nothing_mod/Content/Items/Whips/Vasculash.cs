using nothing_mod.Content.Projectiles.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Whips
{
	public class Vasculash : ModItem
	{

		public override void SetDefaults() {
			Item.DefaultToWhip(ModContent.ProjectileType<VasculashProjectile>(), 19, 1.25f, 4f, 30);  // UNVERIFIED shootSpeed (wiki lists 'velocity' only)
			Item.autoReuse = true; // hold to keep swinging, as in the real game
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(0, 1, 0, 0);
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.CrimtaneBar, 12)
				.AddTile(TileID.Anvils)
				.Register();
		}

		public override bool MeleePrefix() {
			return true;
		}
	}
}
