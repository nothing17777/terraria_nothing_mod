TASK (second pass): make each assigned champion's spell scripts match the REAL League spell effects exactly, using real sources.

The scripts in nothing_mod/Content/LoL/Scripts/Scripts_Batch<N>.cs were first written mostly from memory. Re-do them from evidence.

SOURCES (use in this order)
1. tools/spell_data/spell_text.json: Riot's own description of every spell (key -> [Q,W,E,R,P] with name and text). Read every assigned champion's entry. Every effect mentioned (slow, stun, knock-up, pull, dash, shield, heal, burn/poison/bleed over time, chain, stealth, lifesteal, area, cone, line, delay, orbiting, returning, piercing...) must be represented if the framework can express it.
2. tools/spell_data/spells.json: real numbers (castType, range, speed, width, radius, cone, cc seconds, toggle, channel).
3. The League wiki (https://wiki.leagueoflegends.com/en-us/<Champion>), if you have a web fetch/search tool available: read the ability sections for exact behaviour (missile shape, pierce, return, delay, recast). Do NOT use the browser pane tools (mcp__Claude_Browser__*): they are shared with other agents. If no web tool works, rely on sources 1-2 and say so in your final line.

FRAMEWORK (read nothing_mod/Content/LoL/Scripts/SpellScript.cs, LolScriptCaster.cs, nothing_mod/Content/LoL/LolScriptProjectile.cs)
Shapes: Skillshot, Homing, Zone, SelfBurst, Cone, Beam, Boomerang, Dash, Buff, Orbit.
Fields: Count/SpreadDeg, Pierce, DelaySec, DurationSec, HitIntervalSec, DashTiles, HealFrac, Shield, SpeedBuff, Flags (1 slow, 2 stun/root/knock-up/airborne, 4 fear/charm/silence/taunt), DmgMult, SpeedMult/WidthMult/RangeMult, Knockback (negative = pull), BurnSec, PoisonSec, BleedSec, Lifesteal, Chain, InvisibleSec, SlowSec, Note.
If a spell has two parts (for example a dash followed by an area, or a missile then a recast), pick the shape that conveys the main hit and use the other fields (Knockback, Flags, Chain...) for the rest. Put in Note the exact real behaviour in one line, including anything the framework cannot do (so we know what is missing).
You may NOT edit the framework files. If a needed effect is missing from the framework, list it in your final reply as `MISSING: <effect> (<champions>)`.

OUTPUT
- Edit ONLY nothing_mod/Content/LoL/Scripts/Scripts_Batch<N>.cs (your batch). Keep the same class name and Register signature, 4 entries per champion (Q, W, E, R), null only if the generic spell is better.
- Do NOT run dotnet build. Re-read your file for C# syntax: commas, `f` suffixes, braces, valid enum names, field names.
- No markdown files. Final reply: one short line: batch number, champions revised, and any MISSING: lines.
