using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Players
{
	// 1.4.5.x Invisibility Potion: getting hit while invisible removes 1 minute of the buff instead of all of it.
	// The final 1.4.5.5+ state fully hides the player again, which is already vanilla 1.4.4.9 behaviour, so nothing to change there.
	public class InvisibilityHit : ModPlayer
	{
		private int timeBeforeHit = -1;

		public override void OnHurt(Player.HurtInfo info) {
			int i = Player.FindBuffIndex(BuffID.Invisibility);
			timeBeforeHit = i >= 0 ? Player.buffTime[i] : -1;
		}

		public override void PostHurt(Player.HurtInfo info) {
			if (timeBeforeHit < 0)
				return;
			int remaining = timeBeforeHit - 3600;
			timeBeforeHit = -1;
			int i = Player.FindBuffIndex(BuffID.Invisibility);
			if (i >= 0)
				Player.DelBuff(i);
			if (remaining > 0)
				Player.AddBuff(BuffID.Invisibility, remaining);
		}
	}
}
