using Terraria.ModLoader;
using nothing_mod.Content.Buffs.Palworld;

namespace nothing_mod.Content.Mounts
{
	public class TrustyChilletMount : ChilletMountBase
	{
		protected override int MountBuffType => ModContent.BuffType<TrustyChilletMountBuff>();
		public override bool Trusty => true;
		protected override float RunSpeed => 8f; // UNVERIFIED (wiki: Trusty has increased max speed)
		protected override int JumpHeight => 8; // UNVERIFIED
		protected override float JumpSpeed => 9f; // UNVERIFIED

	}
}
