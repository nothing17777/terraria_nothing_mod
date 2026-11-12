using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Accessory. Sprite is a placeholder (vanilla MoltenQuiver).
	public class PhoenixQuiver : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.LightPurple;
			Item.value = Item.sellPrice(gold: 4, silver: 50);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<NewWeaponsPlayer>().phoenixQuiver = true;
			player.ammoCost80 = true; // 20% chance not to consume ammo
		}
		// TODO: recipe (Harpy Charm + Molten Quiver) omitted, Harpy Charm is not in 1.4.4.9; periodic arrow redirection with Hellfire not implemented.
	}
}
