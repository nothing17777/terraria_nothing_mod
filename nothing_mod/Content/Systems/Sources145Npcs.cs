using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Items.Accessories;
using nothing_mod.Content.Items.DeadCells;
using nothing_mod.Content.Items.NewWeapons;
using nothing_mod.Content.Items.Whips;

namespace nothing_mod.Content.Systems
{
	// Acquisition (NPC drops) for backported 1.4.5 items. Source: terraria.wiki.gg Drops table (Cargo) per item, fetched via Chrome.
	// Rates are the wiki's Classic-mode rates. Boss items are also added to the matching Treasure Bag in Sources145Items.
	public class Sources145Npcs : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			switch (npc.type)
			{
				case NPCID.Raven:
					npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RavenFeather>(), 10));          // 10%
					break;
				case NPCID.GreekSkeleton:
					npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StressBall>(), 100));           // Hoplite 1%
					break;
				case NPCID.GraniteGolem:
				case NPCID.GraniteFlyer:
					npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SnappingStone>(), 80));         // 1.25%
					break;
				case NPCID.CursedHammer:
				case NPCID.CrimsonAxe:
					npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WickedArmlet>(), 25));          // 4%
					break;
				case NPCID.MartianSaucerCore:
					npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ArcSurge>(), 50));              // 2%
					break;
				case NPCID.BigMimicHallow:
					npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChaosCylinder>(), 5));          // 20%
					break;
				case NPCID.IceMimic:
					// Wiki: 23.75% (Hardmode) and 11.88% (Pre-Hardmode variant) for Heavy Sling; 11.88% Glacier Fang (Pre-Hardmode variant).
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ModContent.ItemType<HeavySling>(), 80, 1, 1, 19));
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ModContent.ItemType<HeavySling>(), 160, 1, 1, 19));
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ModContent.ItemType<GlacierFang>(), 160, 1, 1, 19));
					break;
				case NPCID.PirateShip:
					npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BarrelLauncher>(), 10));        // Flying Dutchman 10%
					break;
				case NPCID.PirateCaptain:
					npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BarrelLauncher>(), 50));        // 2%
					break;
				case NPCID.PirateCorsair:
				case NPCID.PirateCrossbower:
				case NPCID.PirateDeadeye:
				case NPCID.PirateDeckhand:
					npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BarrelLauncher>(), 200));       // 0.5%
					break;
				case NPCID.Plantera:
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<VulgarDisplayOfFlower>(), 8));   // 12.5%
					break;
				case NPCID.Golem:
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<MobiusStrip>(), 6));             // 16.67%
					break;
				case NPCID.DukeFishron:
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ElectricEel>(), 7));             // 14.29%
					break;
				case NPCID.MoonLordCore:
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Possession>(), 5));              // 20%
					break;
			}
			// Snake Band: 2.5% from Lamias (Light and Dark Lamia).
			if (npc.type == NPCID.DesertLamiaDark || npc.type == NPCID.DesertLamiaLight)
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SnakeBand>(), 40));
		}
	}
}
