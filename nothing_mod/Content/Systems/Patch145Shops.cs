using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems
{
	// Backports 1.4.5.x shop stock changes (final state after 1.4.5.8, cross-checked with the reference port's shop table).
	// Plain buy price changes are done through Item.value in Patch145Balance.
	public class Patch145Shops : GlobalNPC
	{
		public override void ModifyShop(NPCShop shop)
		{
			switch (shop.NpcType)
			{
				case NPCID.SkeletonMerchant:
					Remove(shop, ItemID.FormatC);                                            // 1.4.5.7 no longer sold
					break;

				case NPCID.Steampunker:
					Remove(shop, ItemID.LivingLoom);                                          // 1.4.5.0 no longer sold
					shop.Add(ItemID.LihzahrdFurnace, Condition.InJungle, Condition.DownedGolem); // 1.4.5.0
					break;

				case NPCID.Mechanic:
					shop.Add(ItemID.SpectreGoggles, Condition.InGraveyard);                  // 1.4.5.7
					break;

				case NPCID.Painter:
					// Echo Coating: Graveyard and (Plantera defeated or a Mechanic is present) (1.4.5.7).
					Remove(shop, ItemID.EchoCoating);
					shop.Add(ItemID.EchoCoating, new Condition(
						Language.GetOrRegister("Mods.nothing_mod.Conditions.EchoCoatingStock", () => "In the Graveyard, after Plantera or with a Mechanic in town"),
						() => Main.LocalPlayer.ZoneGraveyard && (NPC.downedPlantBoss || NPC.AnyNPCs(NPCID.Mechanic))));
					break;

				case NPCID.Dryad:
					// Planter boxes without boss requirements; Forest/Jungle potted trees available before Hardmode (1.4.5.0).
					foreach (int box in new[] { ItemID.DayBloomPlanterBox, ItemID.MoonglowPlanterBox, ItemID.BlinkrootPlanterBox, ItemID.WaterleafPlanterBox, ItemID.ShiverthornPlanterBox, ItemID.FireBlossomPlanterBox })
						Unlock(shop, box);
					Unlock(shop, ItemID.CorruptPlanterBox, Condition.CorruptWorld);
					Unlock(shop, ItemID.CrimsonPlanterBox, Condition.CrimsonWorld);
					foreach (int tree in new[] { ItemID.PottedForestCedar, ItemID.PottedJungleCedar, ItemID.PottedForestTree, ItemID.PottedJungleTree,
						ItemID.PottedForestPalm, ItemID.PottedJunglePalm, ItemID.PottedForestBamboo, ItemID.PottedJungleBamboo })
						Unlock(shop, tree);
					break;

				case NPCID.GoblinTinkerer:
					Unlock(shop, ItemID.RubblemakerSmall);                                   // 1.4.5.0 sold before Hardmode
					break;

				case NPCID.BestiaryGirl:
					// Lowered / added Bestiary completion requirements (1.4.5.0); only touches entries that already exist.
					Rethreshold(shop, ItemID.BlandWhip, 3);                                  // Leather Whip 10% -> 3%
					Rethreshold(shop, ItemID.SquirrelHook, 10);
					Rethreshold(shop, ItemID.DiggingMoleMinecart, 25);                       // Mole Minecart 35% -> 25%
					break;
			}
		}

		private static void Remove(NPCShop shop, int type)
		{
			if (shop.TryGetEntry(type, out NPCShop.Entry entry))
				entry.Disable();
		}

		private static void Unlock(NPCShop shop, int type, params Condition[] conditions)
		{
			Remove(shop, type);
			shop.Add(type, conditions);
		}

		private static void Rethreshold(NPCShop shop, int type, int percent)
		{
			if (shop.TryGetEntry(type, out NPCShop.Entry entry))
			{
				entry.Disable();
				shop.Add(type, Condition.BestiaryFilledPercent(percent));
			}
		}
	}
}
