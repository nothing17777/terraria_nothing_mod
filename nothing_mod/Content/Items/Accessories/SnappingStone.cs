using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
	// Texture is a placeholder (vanilla sprite).
	public class SnappingStone : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(gold: 1, silver: 50);
		}

		// Ranged hits discharge a pulse (shared 6s cooldown): see Content/Players/AccessoryPulse.cs.
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<AccessoryEffectsPlayer>().SnappingStone = true;
		}
	}
}
