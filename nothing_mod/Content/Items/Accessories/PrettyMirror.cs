using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.Items.Accessories
{
	// Stat portion only: using it applies the -0.25 luck curse (7 min) to the user.
	// TODO: thrown projectile that shatters and curses all players within ~19.9 tiles (13 dmg, 3 KB);
	// Stylist graveyard shop (1 gold) not wired.
	public class PrettyMirror : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 28;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useAnimation = 25;
			Item.useTime = 25;
			Item.UseSound = SoundID.Item1;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.buyPrice(gold: 1);
		}

		public override bool? UseItem(Player player)
		{
			player.GetModPlayer<AccessoryEffectsPlayer>().PrettyMirrorTime = 7 * 60 * 60;
			return true;
		}
	}
}
