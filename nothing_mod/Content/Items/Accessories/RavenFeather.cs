using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Accessories
{
	// -0.1 luck while in the inventory (applied in AccessoryEffectsPlayer.ModifyLuck).
	// Also counts inside the Void Vault while a Void Lens is carried (ref Player.ts). TODO: Raven drop (10%) not wired.
	public class RavenFeather : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.Blue;
			Item.value = 0; // ref itemvalue table has no entry for 5576 (value 0)
		}
	}
}
