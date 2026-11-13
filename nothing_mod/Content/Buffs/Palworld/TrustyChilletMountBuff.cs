using Terraria;
using Terraria.ModLoader;
using nothing_mod.Content.Mounts;

namespace nothing_mod.Content.Buffs.Palworld
{
	public class TrustyChilletMountBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(ModContent.MountType<TrustyChilletMount>(), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
