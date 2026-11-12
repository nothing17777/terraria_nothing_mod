using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Accessories
{
	// +0.03 luck while in the inventory (applied in AccessoryEffectsPlayer.ModifyLuck).
	// Also counts inside the Void Vault while a Void Lens is carried (ref Player.ts luckEquipmentBonus).
	public class LuckyClover : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 50);
		}
	}
}
