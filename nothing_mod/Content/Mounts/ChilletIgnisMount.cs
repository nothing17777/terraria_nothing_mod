using Terraria.ModLoader;
using nothing_mod.Content.Buffs.Palworld;

namespace nothing_mod.Content.Mounts
{
	public class ChilletIgnisMount : ChilletMountBase
	{
		protected override int MountBuffType => ModContent.BuffType<ChilletIgnisMountBuff>();
		public override bool Trusty => false;
	}
}
