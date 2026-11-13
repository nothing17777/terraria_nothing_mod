using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Projectiles.Palworld;

namespace nothing_mod.Content.Buffs.Palworld
{
	// Minion upkeep buff. Icon is a placeholder (Imp minion buff).
	public class CattivaBuff : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + BuffID.ImpMinion;

		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<CattivaMinion>()] > 0)
				player.buffTime[buffIndex] = 18000;
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
