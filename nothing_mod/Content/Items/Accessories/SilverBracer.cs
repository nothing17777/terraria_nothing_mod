using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
		// TODO: +1 tag slots and 3x tag duration (no tag-slot system in 1.4.4.9)
	// Texture is a placeholder (vanilla sprite).
	public class SilverBracer : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Green;
			Item.value = Item.sellPrice(gold: 1, silver: 75);
		}
	}
}
