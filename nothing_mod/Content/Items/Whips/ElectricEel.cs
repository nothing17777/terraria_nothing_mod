using nothing_mod.Content.Projectiles.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Whips
{
	public class ElectricEel : ModItem
	{

		public override void SetDefaults() {
			Item.DefaultToWhip(ModContent.ProjectileType<ElectricEelProjectile>(), 150, 3f, 4f, 30);  // UNVERIFIED shootSpeed (wiki lists 'velocity' only)
			Item.autoReuse = true; // hold to keep swinging, as in the real game
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.sellPrice(0, 5, 0, 0);
		}

		public override bool MeleePrefix() {
			return true;
		}
	}
}
