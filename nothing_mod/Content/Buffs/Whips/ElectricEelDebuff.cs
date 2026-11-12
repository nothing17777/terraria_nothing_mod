using Terraria;

namespace nothing_mod.Content.Buffs.Whips
{
	public class ElectricEelDebuff : WhipTagBuff
	{
		public override int TagDamage => 12;
		public override int TagCritChance => 5;

		// 1.4.5: "Damage over time is now always 50 DPS" (game: lifeRegen -100, reference NPC DoT table).
		public override void Update(NPC npc, ref int buffIndex) {
			if (npc.lifeRegen > 0)
				npc.lifeRegen = 0;
			npc.lifeRegen -= 100;
		}
	}
}
