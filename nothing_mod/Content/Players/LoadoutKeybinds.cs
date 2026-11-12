using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace nothing_mod.Content.Players
{
	// 1.4.5.5: Next / Previous Loadout keybindings (default unbound), cycling through all loadouts.
	public class LoadoutKeybinds : ModSystem
	{
		public static ModKeybind Next { get; private set; }
		public static ModKeybind Previous { get; private set; }

		public override void Load() {
			Next = KeybindLoader.RegisterKeybind(Mod, "NextLoadout", Keys.None);
			Previous = KeybindLoader.RegisterKeybind(Mod, "PreviousLoadout", Keys.None);
		}

		public override void Unload() {
			Next = null;
			Previous = null;
		}
	}

	public class LoadoutKeybindPlayer : ModPlayer
	{
		public override void ProcessTriggers(TriggersSet triggersSet) {
			if (LoadoutKeybinds.Next == null)
				return;
			int count = Player.Loadouts.Length;
			if (LoadoutKeybinds.Next.JustPressed)
				Player.TrySwitchingLoadout((Player.CurrentLoadoutIndex + 1) % count);
			else if (LoadoutKeybinds.Previous.JustPressed)
				Player.TrySwitchingLoadout((Player.CurrentLoadoutIndex + count - 1) % count);
		}
	}
}
