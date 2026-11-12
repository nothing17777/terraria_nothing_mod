TASK: per-champion spell scripts for the tModLoader mod in /Volumes/T7 Shield/terraria_mod/nothing_mod

Goal: make each assigned champion's Q, W, E, R play as close to the real League spell as the framework allows.

READ FIRST
- nothing_mod/Content/LoL/Scripts/SpellScript.cs (SpellShape enum + SpellScript fields) and Scripts_Examples.cs (format).
- nothing_mod/Content/LoL/Scripts/LolScriptCaster.cs and nothing_mod/Content/LoL/LolScriptProjectile.cs (what each shape does).
- Real numbers (range, speed, width, radius, damage, cooldown, CC duration) are applied AUTOMATICALLY from tools/spell_data/spells.json; you only choose shape and behaviour.

FOR EACH CHAMPION
1. Read what the spell really does: client JSON under /Volumes/T7 Shield/league_of_legends_cilent/extracted_assets/rcp-be-lol-game-data/default-assets2/data/*.json (find the champion's file by grepping its name; spells have names and descriptions) and tools/spell_data/spells.json (castType, range, speed, width, radius, cone, cc, dmg, ratio, toggle, channel).
2. Choose the closest SpellShape and parameters (Count/SpreadDeg for fans, Pierce for passing through, Boomerang for out-and-back, Zone with DelaySec for delayed areas, SelfBurst/Orbit with DurationSec for auras or orbiting things, Dash with DashTiles for gap closers (damage burst on the caster), Beam for instant lines, Cone for sweeps, Buff with HealFrac/Shield/SpeedBuff for self buffs, Flags for CC: 1 slow, 2 stun/root/knock-up/airborne, 4 fear/charm/silence/taunt). Use DmgMult to keep weak/strong spells in proportion, and a one-line Note stating the real behaviour.
3. Spells that are only passive toggles, stances or summons that the framework cannot do: pick the nearest shape (for example a Buff with SpeedBuff, or a SelfBurst) and say so in Note. Use null for a slot only if the generic system already does it better.

OUTPUT
- Write ONE file: nothing_mod/Content/LoL/Scripts/Scripts_Batch<N>.cs, with exactly this shape:

using System.Collections.Generic;
namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch<N>
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Alias"] = new[] { new SpellScript(SpellShape.X) { ... }, /* W */, /* E */, /* R */ };
		}
	}
}

- Aliases are the folder names given in your assignment (they match LolSpellData keys). Each array has exactly 4 entries (Q, W, E, R); use `null` for a slot to keep the generic spell.
- Use only the public fields in SpellScript.cs. Do NOT edit any other file. Do NOT run dotnet build (builds in a shared folder collide; the lead compiles afterwards). Double-check your C# by reading it: object initializers, commas, `f` suffix on floats, closing braces.
- Do not create any markdown file. Finish by replying with one line: batch number, number of champions written, and any champion you could not handle.
