using nothing_mod.Content.Projectiles.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Whips
{
	public class VulgarDisplayOfFlower : ModItem
	{

		public override void SetDefaults() {
			Item.DefaultToWhip(ModContent.ProjectileType<VulgarDisplayOfFlowerProjectile>(), 50, 2f, 4f, 30);  // UNVERIFIED shootSpeed (wiki lists 'velocity' only)
			Item.autoReuse = true; // hold to keep swinging, as in the real game
			Item.rare = ItemRarityID.Lime;
			Item.value = Item.sellPrice(0, 6, 0, 0);
		}

		public override bool MeleePrefix() {
			return true;
		}
	}
}
