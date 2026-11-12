TASK (third pass): upgrade each assigned champion's spell scripts to use the NEW effect fields.

File to edit: nothing_mod/Content/LoL/Scripts/Scripts_Batch<N>.cs (your batch only). Read nothing_mod/Content/LoL/Scripts/SpellScript.cs: it now has these extra fields (see the section "more effects"):
- CastDelaySec: wind-up before a Skillshot/Homing/Boomerang/Beam fires (Lux R, Kayle Q portal, Kayn W, Lucian Q...).
- PierceLimit: missile stops after N enemies (Lux Q = 2, Veigar Q = 2).
- LowHealthBonus (damage rises as the target's health falls), ExecuteFrac (kills non-boss enemies below that life fraction: Pyke R, Urgot R, Garen R, Darius R, Akali R, Locke R...).
- ArmorShredSec: armor / resistance shred on hit (KogMaw Q, Karthus W, Kayle Q, Wukong Q, Nasus E, Diana Q, Trundle R...).
- InvulnSec: caster untargetable / stasis / death prevention for that many seconds (Vladimir W, Xayah R, Zed R, Tryndamere R, Kayn R, Bard R, Zilean...).
- BlockSec: destroys hostile projectiles near the caster (Sivir E, Yasuo W, Morgana E, Nocturne W, Rammus W, Samira W, Mel W).
- Cleanse: removes the caster's debuffs (Alistar R, Gangplank W, Olaf R...).
- TrailSec: a skillshot leaves a damaging zone behind (KogMaw E, Irelia R wall, Jinx E chompers, Lulu R aura...).
- RecastShape: the key pressed again within 4 s plays this other SpellShape (Lee Sin Q/W/E, Leblanc W return = Dash, Kled E, Lissandra E, Riven Q, Khazix, Ezreal E, Gragas Q detonate = SelfBurst/Zone, Janna Q...). Set RecastShape and describe the second cast in Note.
- EmpowerNextAttack (+ EmpowerDmgMult): next basic attack within 6 s is stronger and carries Flags (Jax W, Vayne Q, Garen Q silence, Camille Q, Darius W, Xin Zhao Q, Yorick Q, Nasus Q, Fiora, Sett W...).
Existing fields still apply (Knockback, BurnSec, Lifesteal, Chain, InvisibleSec, SlowSec, Flags, Shield, HealFrac...).

HOW
1. Read the Notes in your batch file: each one states what the real spell does and often what the framework could not do. Also re-read tools/spell_data/spell_text.json for your champions.
2. For every spell where one of the new fields now expresses a described effect, add it (keep the existing shape and fields). Do not remove working fields. Update the Note so it lists only what is STILL missing.
3. Do NOT edit any other file, do NOT run dotnet build, do NOT create markdown files. Check C# syntax by re-reading (commas, `f` suffixes, braces; RecastShape needs `SpellShape.X`).
4. Final reply: one short line: batch number and champions changed.
