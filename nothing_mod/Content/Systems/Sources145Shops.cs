using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using nothing_mod.Content.Items.Accessories;
using nothing_mod.Content.Items.Armor;
using nothing_mod.Content.Items.DeadCells;
using nothing_mod.Content.Items.NewWeapons;
using nothing_mod.Content.Items.Palworld;

namespace nothing_mod.Content.Systems
{
	// Acquisition (NPC shops) for backported 1.4.5 items. Conditions from each item's wiki page; prices are the items' own Item.value.
	public class Sources145Shops : GlobalNPC
	{
		public override void ModifyShop(NPCShop shop)
		{
			switch (shop.NpcType)
			{
				case NPCID.SkeletonMerchant:
					shop.Add(ModContent.ItemType<MagicString>(), Condition.Hardmode, Condition.DownedMechBossAny); // wiki: Hardmode, after one mechanical boss
					shop.Add(ModContent.ItemType<RollerSkates>()); // UNVERIFIED: wiki sells 4 colour variants by moon phase; one item here, always stocked
					break;
				case NPCID.Demolitionist:
					// Night, after Eye of Cthulhu or King Slime.
					shop.Add(ModContent.ItemType<FreezeBomb>(), Condition.TimeNight, new Condition(
						Language.GetOrRegister("Mods.nothing_mod.Conditions.EyeOrKingSlime", () => "After defeating the Eye of Cthulhu or King Slime"),
						() => NPC.downedBoss1 || NPC.downedSlimeKing));
					break;
				case NPCID.Cyborg:
					shop.Add(ModContent.ItemType<WallRacerCar>(), Condition.Hardmode, Condition.DownedPlantera);
					break;
				case NPCID.Wizard:
					shop.Add(ModContent.ItemType<KillingDeck>(), Condition.Hardmode, Condition.BloodMoon);
					break;
				case NPCID.BestiaryGirl:
					shop.Add(ModContent.ItemType<SwarmGrenade>(), Condition.BirthdayParty);
					break;
				case NPCID.Merchant:
					shop.Add(ModContent.ItemType<RainbowGlowstick>(), Condition.BirthdayParty);
					break;
				case NPCID.Stylist:
					shop.Add(ModContent.ItemType<PrettyMirror>(), Condition.InGraveyard); // changelog 1.4.5: Stylist sells Pretty Mirrors in the graveyard
					break;
			}
		}

		// Traveling Merchant stock is built outside NPCShop, so it is edited on the live item list.
		public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
		{
			if (npc.type != NPCID.TravellingMerchant)
				return;
			// UNVERIFIED: wiki gives randomised slots; here the sets are always stocked when their condition holds.
			Add(items, ModContent.ItemType<LilacDuskHairclip>());
			Add(items, ModContent.ItemType<LilacDuskDress>());
			Add(items, ModContent.ItemType<LilacDuskSkirt>());
			if (HasPal(Main.LocalPlayer))
			{
				Add(items, ModContent.ItemType<PalMetalChestplate>());
				Add(items, ModContent.ItemType<PalMetalLeggings>());
			}
		}

		private static bool HasPal(Player p)
		{
			int[] pals = {
				ModContent.ItemType<Cattiva>(), ModContent.ItemType<Foxparks>(), ModContent.ItemType<Chillet>(), ModContent.ItemType<ChilletIgnis>(), ModContent.ItemType<Digtoise>(),
				ModContent.ItemType<TrustyCattiva>(), ModContent.ItemType<TrustyFoxparks>(), ModContent.ItemType<TrustyChillet>(), ModContent.ItemType<TrustyChilletIgnis>(), ModContent.ItemType<TrustyDigtoise>() };
			foreach (int t in pals)
				if (p.HasItem(t))
					return true;
			return false;
		}

		private static void Add(Item[] items, int type)
		{
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] != null && items[i].type == type)
					return;
				if (items[i] == null || items[i].IsAir)
				{
					items[i] = new Item(type);
					return;
				}
			}
		}
	}
}
