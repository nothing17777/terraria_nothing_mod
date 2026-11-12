using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
		// TODO: real mount behavior (coasting/retained velocity, minecart tracks); wiki lists 4 mount variants, only one item here
	// Texture is a placeholder (vanilla sprite).
	public class RollerSkates : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(gold: 2);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.maxRunSpeed += 1f; // ref vanilla-mounts RollerSkates runSpeed 1 (additive)
			player.runAcceleration += 0.1f; // ref vanilla-mounts RollerSkates acceleration 0.1 (additive)
			player.jumpSpeedBoost += 1f;
			Player.jumpHeight += 2;
		}
	}
}
