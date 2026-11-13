using System.ComponentModel;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace nothing_mod.Content.Players
{
	// 1.4.5.5: Dash keybinding (default unbound) and a "Double Tap Dashing" toggle.
	public class DashKeybindSystem : ModSystem
	{
		public static ModKeybind Dash { get; private set; }

		public override void Load() {
			Dash = KeybindLoader.RegisterKeybind(Mod, "Dash", Keys.None);
		}

		public override void Unload() {
			Dash = null;
		}
	}

	public class NothingClientConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[DefaultValue(true)]
		public bool DoubleTapDashing;
	}

	// Fakes the double tap that vanilla's Player.DoCommonDashHandle looks for, so every vanilla dash
	// (Shield of Cthulhu, Tabi, Master Ninja Gear, Solar Flare armor, Ram/Crystal Ninja...) works from the key.
	// UNVERIFIED: that PostUpdateEquips runs before vanilla's DashMovement in Player.Update, and the exact
	// double-tap logic of DoCommonDashHandle (dashTime > 0 on a fresh left/right press).
	public class DashKeybindPlayer : ModPlayer
	{
		private bool forced;
		private bool oldControlLeft, oldControlRight, oldReleaseLeft, oldReleaseRight;
		private bool dashRequested;

		public override void ProcessTriggers(TriggersSet triggersSet) {
			if (DashKeybindSystem.Dash != null && DashKeybindSystem.Dash.JustPressed)
				dashRequested = true;
		}

		public override void PostUpdateEquips() {
			if (Player.whoAmI != Main.myPlayer)
				return;

			if (dashRequested) {
				dashRequested = false;
				if (Player.dashType != 0 && Player.dashDelay >= 0 && !Player.mount.Active) {
					int dir = Player.controlLeft != Player.controlRight ? (Player.controlRight ? 1 : -1) : Player.direction;
					oldControlLeft = Player.controlLeft;
					oldControlRight = Player.controlRight;
					oldReleaseLeft = Player.releaseLeft;
					oldReleaseRight = Player.releaseRight;
					forced = true;
					Player.controlLeft = dir < 0;
					Player.controlRight = dir > 0;
					Player.releaseLeft = dir < 0;
					Player.releaseRight = dir > 0;
					Player.dashTime = 15;
					return;
				}
			}

			// Double Tap Dashing off: never let the first tap arm the dash.
			var config = ModContent.GetInstance<NothingClientConfig>();
			if (config != null && !config.DoubleTapDashing)
				Player.dashTime = 0;
		}

		public override void PostUpdate() {
			if (!forced)
				return;
			forced = false;
			Player.controlLeft = oldControlLeft;
			Player.controlRight = oldControlRight;
			Player.releaseLeft = oldReleaseLeft;
			Player.releaseRight = oldReleaseRight;
		}
	}
}
