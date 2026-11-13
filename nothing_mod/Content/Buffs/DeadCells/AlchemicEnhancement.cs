using Terraria;
using Terraria.ModLoader;

namespace nothing_mod.Content.Buffs.DeadCells
{
	// Wiki: "Fresh buffs last longer" (+20% to newly received potion buffs). Lasts until death / leaving the world; right-click the icon to cancel.
	public class AlchemicEnhancement : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000; // keep it infinite
		}
	}
}
