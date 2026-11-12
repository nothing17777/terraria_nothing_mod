using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems
{
	// Backports 1.4.5.x equipment and consumable numbers that are not plain stat fields.
	// Final values after 1.4.5.8 (changelog.txt shipped with the game).
	public class Patch145Items : GlobalItem
	{
		// Potion Sickness: Bottled Water 60s -> 45s, Eggnog 60s -> 40s (1.4.5.0). Delay is in ticks.
		public override void ModifyPotionDelay(Item item, Player player, ref int potionDelay)
		{
			if (item.type == ItemID.BottledWater)
				potionDelay = 45 * 60;
			else if (item.type == ItemID.Eggnog)
				potionDelay = 40 * 60;
		}

		// 1.4.5.7: Band of Starpower max mana bonus 20 -> 40, inherited by the tinkers built from it.
		public override void UpdateAccessory(Item item, Player player, bool hideVisual)
		{
			switch (item.type)
			{
				case ItemID.BandofStarpower:
				case ItemID.MagicCuffs:
				case ItemID.CelestialCuffs:
					player.statManaMax2 += 20;
					break;
			}
		}
	}
}
