using Terraria.ModLoader;
using nothing_mod.Content.Buffs.Palworld;

namespace nothing_mod.Content.Mounts
{
	public class ChilletMount : ChilletMountBase
	{
		protected override int MountBuffType => ModContent.BuffType<ChilletMountBuff>();
		public override bool Trusty => false;
	}
}
