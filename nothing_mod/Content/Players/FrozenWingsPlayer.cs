using Terraria;
using Terraria.ModLoader;

namespace nothing_mod.Content.Players
{
	// Applies the flight time of the equipped Frozen Wings tier.
	// The tier items clone the vanilla Frozen Wings (same wing slot, so same look, speed and particles),
	// and only the flight time differs. Set by FrozenWingsTier.UpdateAccessory each frame.
	public class FrozenWingsPlayer : ModPlayer
	{
		// Flight time in ticks (60 ticks = 1 second). 0 means no tier wings equipped.
		public int FlightTicks;

		public override void ResetEffects()
		{
			FlightTicks = 0;
		}

		public override void PostUpdateEquips()
		{
			// UNVERIFIED: whether vanilla recalculates Player.wingTimeMax after this hook runs.
			// If flight time does not change in game, this is the place to fix.
			if (FlightTicks > 0)
			{
				Player.wingTimeMax = FlightTicks;
			}
		}
	}
}
