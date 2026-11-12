using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	// Framework smoke test: Annie (Disintegrate = homing bolt, Incinerate = cone, Molten Shield = shield, Summon: Tibbers = zone).
	public static class Scripts_Examples
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Annie"] = new[] {
				new SpellScript(SpellShape.Homing) { Note = "Q Disintegrate: targeted bolt" },
				new SpellScript(SpellShape.Cone) { Note = "W Incinerate: cone" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "E Molten Shield" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 1.5f, Note = "R Summon: Tibbers (area at cursor)" },
			};
		}
	}
}
