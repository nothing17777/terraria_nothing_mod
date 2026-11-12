using Terraria.ModLoader;

namespace nothing_mod.Content.LoL
{
	// Overwrites each spell's range with the real League value from the champion .bin (LolSpellData), so the HUD ring and casts use it.
	public class LolSpellPatch : ModSystem
	{
		public override void PostSetupContent() {
			foreach (var kv in LolData.Champions) {
				if (!LolSpellData.Spells.TryGetValue(kv.Key, out RealSpell[] real)) continue;
				for (int i = 0; i < 4 && i < real.Length; i++)
					if (real[i] != null && real[i].Range > 0 && kv.Value.Spells.Length > i + 1) kv.Value.Spells[i + 1].Range = (int)real[i].Range;
			}
		}
	}
}
