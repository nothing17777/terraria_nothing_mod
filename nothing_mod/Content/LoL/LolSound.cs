using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;

namespace nothing_mod.Content.LoL
{
	// Plays the champion's own spell sounds, decoded from the game's Wwise banks (tools/lol_sound_extract.py).
	public static class LolSound
	{
		static readonly System.Collections.Generic.Dictionary<string, uint> lastHit = new();

		public static bool Has(string alias, string key) =>
			LolSoundData.Sounds.TryGetValue(alias, out string[] a) && Array.IndexOf(a, key) >= 0;

		// key = "Q_cast", "E_hit", ...
		public static bool Play(string alias, int slot, string kind, Vector2 pos, float volume = 0.9f) {
			string key = "QWER"[slot] + "_" + kind;
			if (!Has(alias, key)) return false;
			if (kind == "hit") { // multi-hit spells would spam: at most one hit sound per spell every 12 ticks
				string k = alias + key;
				if (lastHit.TryGetValue(k, out uint t) && Main.GameUpdateCount - t < 12) return true;
				lastHit[k] = Main.GameUpdateCount;
			}
			SoundEngine.PlaySound(new SoundStyle($"nothing_mod/Content/LoL/Sounds/{alias}_{key}") { Volume = volume, MaxInstances = 6 }, pos);
			return true;
		}
	}
}
