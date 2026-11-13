using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Projectiles.DeadCells;

namespace nothing_mod.Content.Buffs.DeadCells
{
	// Minion upkeep buff. 
	public class MushroomBoiBuff : ModBuff
	{

		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<MushroomBoiMinion>()] > 0)
				player.buffTime[buffIndex] = 18000;
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
