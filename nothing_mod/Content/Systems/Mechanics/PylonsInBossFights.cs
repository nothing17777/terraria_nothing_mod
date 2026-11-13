using Terraria.GameContent;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems.Mechanics
{
	// 1.4.5.x: Pylons can be used during boss fights and invasions (the "AnyDanger" step always passes).
	// Enemy-nearby and biome checks still apply.
	public class PylonsInBossFights : GlobalPylon
	{
		public override bool? ValidTeleportCheck_PreAnyDanger(TeleportPylonInfo pylonInfo) => true;
	}
}
