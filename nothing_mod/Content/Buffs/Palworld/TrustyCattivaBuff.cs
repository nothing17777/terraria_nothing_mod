using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Projectiles.Palworld;

namespace nothing_mod.Content.Buffs.Palworld
{
	// Minion upkeep buff. 
	public class TrustyCattivaBuff : ModBuff
	{

		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<TrustyCattivaMinion>()] > 0)
				player.buffTime[buffIndex] = 18000;
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
