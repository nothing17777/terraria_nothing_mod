using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
		// TODO: extra whip swing every 3 attacks (30% dmg, 25% KB)
	// Texture is a placeholder (vanilla sprite).
	public class SnakeBand : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.sellPrice(gold: 2);
		}
	}
}
