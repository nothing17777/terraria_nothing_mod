using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
		// TODO: exclusions for wiring/paint tools, Bug Net, food only without Well Fed
	// Texture is a placeholder (vanilla sprite).
	public class StressBall : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(gold: 1);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<AccessoryEffectsPlayer>().StressBall = true;
		}
	}
}
