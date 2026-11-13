using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems
{
	// 1.4.5.x NPC changes not covered by Patch145Npcs (changelog.txt shipped with the game).
	public class Patch145ExtraNpcs : GlobalNPC
	{
		// 1.4.5.0 Cyborg: attack rate +200% (cooldown / 3).
		public override void TownNPCAttackCooldown(NPC npc, ref int cooldown, ref int randExtraCooldown)
		{
			if (npc.type == NPCID.Cyborg)
			{
				cooldown = cooldown / 3;
				randExtraCooldown = randExtraCooldown / 3;
			}
		}

		// 1.4.5.0 Flying Dutchman and Martian Saucer: Expert+ health scaling 100% -> 85% of base.
		// Vanilla multiplies by m (Expert 2, Master 3); target is base * (1 + 0.85 * (m - 1)). UNVERIFIED for Master/Legendary.
		public override void ApplyDifficultyAndPlayerScaling(NPC npc, int numPlayers, float balance, float bossAdjustment)
		{
			if (npc.type != NPCID.PirateShip && npc.type != NPCID.MartianSaucerCore)
				return;
			if (!Main.expertMode)
				return;
			float m = Main.GameModeInfo.EnemyMaxLifeMultiplier;
			if (m <= 1f)
				return;
			float factor = (1f + 0.85f * (m - 1f)) / m;
			npc.lifeMax = (int)(npc.lifeMax * factor);
			npc.life = npc.lifeMax;
		}

		// Drop edits only touch a rule whose current denominator equals the expected 1.4.4.9 value (safe no-op otherwise).
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			switch (npc.type)
			{
				case NPCID.MotherSlime:
				case NPCID.SnowFlinx:
				case NPCID.UndeadViking:
				case NPCID.ArmoredViking:
					Retune(npcLoot, ItemID.Compass, 50, 100);          // 1.4.5.0 2% -> 1%
					break;
				case NPCID.CaveBat:
				case NPCID.GiantBat:
				case NPCID.JungleBat:
				case NPCID.IlluminantBat:
				case NPCID.Hellbat:
				case NPCID.Lavabat:
				case NPCID.IceBat:
					Retune(npcLoot, ItemID.DepthMeter, 100, 200);      // 1.4.5.0 1% -> 0.5%
					break;
			}
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

		private static void Visit(IItemDropRule rule, System.Action<CommonDrop> action)
		{
			if (rule is CommonDrop cd)
				action(cd);
			foreach (IItemDropRuleChainAttempt chain in rule.ChainedRules)
				Visit(chain.RuleToChain, action);
		}
	}
}
