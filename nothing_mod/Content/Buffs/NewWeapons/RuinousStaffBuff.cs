using nothing_mod.Content.Projectiles.NewWeapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Buffs.NewWeapons
{
	// Minion upkeep buff. 
	public class RuinousStaffBuff : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + BuffID.ImpMinion;

		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<ForbiddenRuinMinion>()] > 0)
			{
				player.buffTime[buffIndex] = 18000;
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
