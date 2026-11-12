using System.Collections.Generic;
using System.Linq;

namespace nothing_mod.Content.LoL
{
	public static class LolKeyLabel
	{
		static readonly string[] Defaults = { "Q", "W", "E", "R", "Y" };

		// The key actually assigned to spell slot 0-3 (4 = movement-mode toggle) in Settings > Controls.
		// Shows "unbound" when the game says no key is set, so the HUD never advertises a key that does nothing.
		public static string Of(int slot) {
			var kb = slot == 4 ? LolKeybinds.MoveMode : LolKeybinds.Cast[slot];
			List<string> keys;
			try { keys = kb?.GetAssignedKeys(Terraria.GameInput.InputMode.Keyboard); } catch { return Defaults[slot]; }
			if (keys == null) return Defaults[slot];
			string s = keys.FirstOrDefault();
			return string.IsNullOrEmpty(s) ? "unbound" : s.Replace("OemTilde", "`").ToUpperInvariant();
		}
	}
}
