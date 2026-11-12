using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems
{
	// Backports 1.4.5.x data-driven NPC changes: villager stats, spawn weights and drop rates.
	// Source: changelog.txt shipped with the game (1.4.5 sections H, I, M; final values after 1.4.5.8).
	// Drop edits only touch a rule whose current denominator equals the expected 1.4.4.9 value, so a mismatch is a safe no-op.
	public class Patch145Npcs : GlobalNPC
	{
		public override void SetDefaults(NPC npc)
		{
			switch (npc.type)
			{
				case NPCID.Guide:
					if (npc.defense == 15) npc.defense = 30;                       // 1.4.5.0
					break;
				case NPCID.Cyborg:
					if (npc.defense == 15) npc.defense = 30;                       // 1.4.5.0
					if (npc.lifeMax == 250) { npc.lifeMax = 500; npc.life = 500; } // 1.4.5.0
					if (npc.knockBackResist == 0.5f) npc.knockBackResist = 0.25f;  // 1.4.5.0 50% -> 75% resistance (UNVERIFIED mapping to knockBackResist)
					break;
			}
		}

		// Guide arrow damage 8 -> 12 (1.4.5.0), applied as a x1.5 ratio so villager boss bonuses are kept.
		public override void TownNPCAttackStrength(NPC npc, ref int damage, ref float knockback)
		{
			if (npc.type == NPCID.Guide)
				damage = damage * 3 / 2;
		}

		// 1.4.5.0 spawn rate changes (relative to the current pool weight).
		public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
		{
			Scale(pool, NPCID.RainbowSlime, 1.17f);       // +~17%
			Scale(pool, NPCID.RockGolem, 1.17f);          // +~17%
			Scale(pool, NPCID.TombCrawlerHead, 0.75f);    // -~25%
			Scale(pool, NPCID.DuneSplicerHead, 0.75f);    // -~25%
			Scale(pool, NPCID.ToxicSludge, 0.67f);        // -~33%
		}

		private static void Scale(IDictionary<int, float> pool, int type, float factor)
		{
			if (pool.TryGetValue(type, out float w))
				pool[type] = w * factor;
		}

		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			switch (npc.type)
			{
				case NPCID.CaveBat:
				case NPCID.GiantBat:
				case NPCID.JungleBat:
				case NPCID.IlluminantBat:
				case NPCID.Hellbat:
				case NPCID.Lavabat:
				case NPCID.GiantFlyingFox:
				case NPCID.IceBat:
				case NPCID.Vampire:
					Retune(npcLoot, ItemID.BatBat, 250, 300);            // 0.4% -> ~0.33% (1.4.5.0)
					break;
				case NPCID.SnowFlinx:
					Retune(npcLoot, ItemID.SnowballLauncher, 50, 25);    // 2% -> 4%
					break;
				case NPCID.RockGolem:
					Retune(npcLoot, ItemID.RockGolemHead, 10, 3);        // 10% -> ~33.33%
					break;
				case NPCID.Crawdad:
				case NPCID.Crawdad2:
				case NPCID.GiantShelly:
				case NPCID.GiantShelly2:
				case NPCID.Salamander:
				case NPCID.Salamander2:
				case NPCID.Salamander3:
				case NPCID.Salamander4:
				case NPCID.Salamander5:
				case NPCID.Salamander6:
				case NPCID.Salamander7:
				case NPCID.Salamander8:
				case NPCID.Salamander9:
					Retune(npcLoot, ItemID.Rally, 25, 15);               // 4% -> ~6.67%
					break;
				case NPCID.HallowBoss:
					SetAmount(npcLoot, ItemID.HallowBossDye, 3);         // Prismatic Dye stack of 3 (1.4.5.0)
					break;
				case NPCID.KingSlime:
					// ~3.33% Slime Staff in Classic mode (1.4.5.0); Expert+ is handled by the Treasure Bag.
					LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
					notExpert.OnSuccess(ItemDropRule.Common(ItemID.SlimeStaff, 30));
					npcLoot.Add(notExpert);
					break;
			}
		}

		// Treasure Bag of the Empress of Light: Prismatic Dye stack of 3 (1.4.5.0).
		private static void Visit(IItemDropRule rule, System.Action<CommonDrop> action)
		{
			if (rule is CommonDrop cd)
				action(cd);
			foreach (IItemDropRuleChainAttempt chain in rule.ChainedRules)
				Visit(chain.RuleToChain, action);
		}

		private static void Retune(NPCLoot loot, int itemId, int oldDenominator, int newDenominator)
		{
			foreach (IItemDropRule rule in loot.Get(false))
				Visit(rule, cd =>
				{
					if (cd.itemId == itemId && cd.chanceDenominator == oldDenominator)
						cd.chanceDenominator = newDenominator;
				});
		}

		private static void SetAmount(NPCLoot loot, int itemId, int amount)
		{
			foreach (IItemDropRule rule in loot.Get(false))
				Visit(rule, cd =>
				{
					if (cd.itemId == itemId)
					{
						cd.amountDroppedMinimum = amount;
						cd.amountDroppedMaximum = amount;
					}
				});
		}
	}
}
