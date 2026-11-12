using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
	// Texture is a placeholder (vanilla sprite).
	public class PoisonBarb : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 75);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<AccessoryEffectsPlayer>().PoisonArrows = true;
		}
	}
}
