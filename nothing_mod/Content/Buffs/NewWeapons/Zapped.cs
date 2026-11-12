using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Buffs.NewWeapons
{
	// Purely visual debuff (wiki: deals no damage). Buff icon is a placeholder (Electrified).
	public class Zapped : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + BuffID.Electrified;

		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			if (Main.rand.NextBool(3))
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.Electric);
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (Main.rand.NextBool(3))
				Dust.NewDust(player.position, player.width, player.height, DustID.Electric);
		}
	}
}
