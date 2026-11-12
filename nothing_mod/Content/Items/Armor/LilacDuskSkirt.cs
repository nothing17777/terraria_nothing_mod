using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class LilacDuskSkirt : ModItem
	{
		public override void SetDefaults() {
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Green;
			Item.value = 25000;
			Item.vanity = true;
		}
	}
}
