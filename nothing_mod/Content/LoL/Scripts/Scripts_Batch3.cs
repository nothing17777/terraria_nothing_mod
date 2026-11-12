using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch3
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Gragas"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 2f, DurationSec = 0.3f, Flags = 1, SlowSec = 2f, RecastShape = SpellShape.Zone, Note = "Q Barrel Roll: barrel rolls to location, recast detonates early (modelled as an instant zone at cursor), auto-explodes after 4s; damage growth over time not modelled" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Shield = true, EmpowerNextAttack = true, Note = "W Drunken Rage: 1s drink, then reduced damage taken and next basic attack deals magic damage to all nearby (area part of the empowered attack not modelled)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 12f, Flags = 2, Note = "E Body Slam: charges to location, stops at first enemy, damages and stuns all nearby" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.25f, Knockback = 10f, Note = "R Explosive Cask: cask thrown to location, damages and knocks enemies back from centre" },
			};
			t["Graves"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "Q End of the Line: shell passes through units, detonates after 1s or on terrain, then shrapnel burst" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.15f, DurationSec = 4f, Flags = 1, SlowSec = 1f, Note = "W Smoke Screen: canister damages and briefly slows on impact, cloud reduces sight range (sight not modelled)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 9f, Shield = true, Note = "E Quickdraw: dash forward, armor/MR boost; towards champion gives True Grit stacks; basic attacks lower cooldown" },
				new SpellScript(SpellShape.Skillshot) { DmgMult = 1.2f, WidthMult = 1.5f, Note = "R Collateral Damage: shell hits first champion hard, then explodes in a cone behind the target" },
			};
			t["Gwen"] = new[] {
				new SpellScript(SpellShape.Cone) { DmgMult = 1.5f, Note = "Q Snip Snip!: up to 6 snips in a cone, true damage in the centre, applies passive each snip" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 4f, Shield = true, Note = "W Hallowed Mist: mist around Gwen, only enemies inside can target her" },
				new SpellScript(SpellShape.Dash) { DashTiles = 12f, SpeedBuff = true, Note = "E Skip 'n Slash: short dash then bonus attack speed, range and on-hit magic damage, cooldown partly refunded on hit" },
				new SpellScript(SpellShape.Skillshot) { Count = 3, SpreadDeg = 12f, Flags = 1, SlowSec = 1.5f, RecastShape = SpellShape.Skillshot, Note = "R Needlework: needle slows and marks, recast fires more needles (modelled as one extra cast; second recast not modelled)" },
			};
			t["Hecarim"] = new[] {
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.3f, Note = "Q Rampage: cleave around Hecarim, each hit raises damage and lowers cooldown of next Rampages" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 4f, Shield = true, Lifesteal = 0.3f, Note = "W Spirit of Dread: armor/MR, magic damage aura, heals for % of damage enemies take" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, SpeedBuff = true, Knockback = 8f, EmpowerNextAttack = true, Note = "E Devastating Charge: rising move speed, ghosting; next attack knocks back and deals bonus damage scaling with distance travelled (distance scaling not modelled)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 16f, Flags = 4, Note = "R Onslaught of Shadows: charge forward damaging in a line, shockwave at end fears nearby enemies" },
			};
			t["Heimerdinger"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 8f, HitIntervalSec = 0.4f, Note = "Q H-28G Evolution Turret: rapid-fire turret with secondary pass-through beam" },
				new SpellScript(SpellShape.Skillshot) { Count = 3, SpreadDeg = 8f, Note = "W Hextech Micro-Rockets: three long-range rockets converging on cursor" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.1f, DurationSec = 0.3f, Flags = 3, Note = "E CH-2 Electron Storm Grenade: damages, stuns direct hit, slows surrounding (here both on all)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "R UPGRADE!!!: next spell gets increased effects (no equivalent, speed buff)" },
			};
			t["Hwei"] = new[] {
				new SpellScript(SpellShape.Skillshot) { WidthMult = 1.5f, Note = "Q Subject: Disaster, uses Devastating Fire (QQ): missile exploding on contact; Severing Bolt/Molten Fissure not modelled" },
				new SpellScript(SpellShape.Buff) { Shield = true, SpeedBuff = true, Note = "W Subject: Serenity, Fleeting Current (WQ) shield + speed; Pool of Reflection/Stirring Lights not modelled (UNVERIFIED)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 5, Note = "E Subject: Torment, Grim Visage (EQ) fear/slow visage (UNVERIFIED which CC); Gaze of the Abyss/Crushing Maw not modelled" },
				new SpellScript(SpellShape.Skillshot) { WidthMult = 3f, Flags = 1, Note = "R Spiraling Despair: first champion hit becomes centre of an expanding painting that slows and damages, explodes at max size or on death" },
			};
			t["Illaoi"] = new[] {
				new SpellScript(SpellShape.Beam) { Note = "Q Tentacle Smash: tentacle slams down at target area (passive empowers tentacles)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "W Harsh Lesson: leaps to target, damages, nearby tentacles also strike" },
				new SpellScript(SpellShape.Skillshot) { Knockback = -6f, Note = "E Test of Spirit: rips spirit from foe and pulls it before her; spirit echoes damage, becomes Vessel if killed" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, Note = "R Leap of Faith: idol slam around her, spawns a tentacle per champion hit" },
			};
			t["Irelia"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 12f, HealFrac = 0.04f, Note = "Q Bladesurge: dash to target, damages and heals, cooldown refreshes on Marked target or kill" },
				new SpellScript(SpellShape.Cone) { Shield = true, Note = "W Defiant Dance: charged strike, damage grows with charge, reduced physical damage during charge" },
				new SpellScript(SpellShape.Beam) { Flags = 2, Note = "E Flawless Duet: two blades converge, enemies between are damaged, stunned and Marked" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, SlowSec = 1.5f, TrailSec = 5f, Note = "R Vanguard's Edge: blades explode on first champion (damage, Mark), then form a wall that damages and slows (wall approximated as trail; Mark not modelled)" },
			};
			t["Ivern"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "Q Rootcaller: vine stops at first enemy hit, damages and roots, allies can dash to it" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.2f, DurationSec = 6f, Note = "W Brushmaker: creates brush patch; allies in brush deal bonus magic damage (stealth not modelled)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Shield = true, Flags = 1, Note = "E Triggerseed: shield on ally that explodes after a short time, slowing and damaging; refreshes if it hits nothing" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 8f, HitIntervalSec = 1f, Note = "R Daisy!: summons Daisy to fight alongside, recast to command (summon not modelled, area stand-in)" },
			};
			t["Janna"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, WidthMult = 1.5f, Flags = 2, RecastShape = SpellShape.Skillshot, Note = "Q Howling Gale: storm grows with charge time, recast to release (modelled as a second missile), damages and knocks up in its path; charge growth not modelled" },
				new SpellScript(SpellShape.Homing) { Flags = 1, SpeedBuff = true, SlowSec = 2f, Note = "W Zephyr: passive move speed and ghosting; active deals damage and slows" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "E Eye Of The Storm: shields ally champion, bonus attack damage (AD bonus not modelled)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 3f, HitIntervalSec = 3f, HealFrac = 0.1f, Knockback = 10f, Note = "R Monsoon: storm throws enemies back, then soothing winds heal nearby allies while active" },
			};
			t["JarvanIV"] = new[] {
				new SpellScript(SpellShape.Beam) { ArmorShredSec = 3f, Note = "Q Dragon Strike: lance line damages and lowers armor; pulls JarvanIV to a flag, knocking up enemies in path (E-Q combo not modelled)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Shield = true, Flags = 1, Note = "W Golden Aegis: shield and slow surrounding enemies" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.1f, DurationSec = 8f, SpeedBuff = true, Note = "E Demacian Standard: flag placed at location, magic damage on impact, attack speed to nearby allies" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Note = "R Cataclysm: leaps to target, damages on impact, terraforms arena walls around (arena not modelled)" },
			};
			t["Jax"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Note = "Q Leap Strike: leaps to a unit, strikes it if enemy" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, EmpowerNextAttack = true, Note = "W Empower: next attack deals bonus damage" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, Shield = true, Flags = 2, InvulnSec = 1f, Note = "E Counter Strike: dodges all attacks briefly (InvulnSec 1s UNVERIFIED), then stuns all surrounding enemies" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.3f, Shield = true, Note = "R Grandmaster-at-Arms: every 3rd attack bonus magic damage; active damages around him and boosts armor/MR" },
			};
			t["Jayce"] = new[] {
				new SpellScript(SpellShape.Skillshot) { WidthMult = 1.3f, Note = "Q Shock Blast (cannon): orb detonates on first enemy or end of path; hammer form To the Skies! leaps and slows" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 3f, SpeedBuff = true, Note = "W Lightning Field (hammer): field damages nearby for seconds; cannon Hyper Charge gives max attack speed" },
				new SpellScript(SpellShape.Cone) { Knockback = 8f, Note = "E Thundering Blow (hammer): magic damage and knockback; cannon form deploys Acceleration Gate" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "R Mercury Cannon/Hammer: swaps stance (hammer gains armor/MR); stance swap not modelled" },
			};
			t["Jhin"] = new[] {
				new SpellScript(SpellShape.Homing) { Chain = 3, Note = "Q Dancing Grenade: cartridge hits up to four targets, gains damage on kill" },
				new SpellScript(SpellShape.Beam) { Flags = 2, Note = "W Deadly Flourish: single shot of huge range, pierces minions, stops at first champion, roots recently damaged targets" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.8f, DurationSec = 1f, Flags = 1, Note = "E Captive Audience: invisible lotus trap blooms when walked over, slows then explodes" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, DmgMult = 1.3f, Flags = 1, LowHealthBonus = 1f, Note = "R Curtain Call: channel of 4 super shots, pierce minions, stop on champion, cripple (slow) with execute (modelled as low-health bonus, magnitude UNVERIFIED); 4th crits (single shot only)" },
			};
			t["Jinx"] = new[] {
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "Q Switcheroo!: swaps Pow-Pow (attack speed) and Fishbones (AoE, range) (no equivalent)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, SlowSec = 2f, Note = "W Zap!: shock hits first enemy, damages, slows and reveals" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.4f, DurationSec = 5f, Flags = 2, BurnSec = 2f, Note = "E Flame Chompers!: line of grenades, explode after 5s lighting enemies on fire, root champions that walk over" },
				new SpellScript(SpellShape.Skillshot) { SpeedMult = 1.5f, WidthMult = 1.5f, LowHealthBonus = 1f, Note = "R Super Mega Death Rocket!: global rocket gaining damage with distance (not modelled), explodes on first champion, bonus vs missing health (magnitude UNVERIFIED)" },
			};
			t["KSante"] = new[] {
				new SpellScript(SpellShape.Beam) { Flags = 1, Note = "Q Ntofo Strikes: thrust damages and slows in a short line; 2nd stack fires shockwave that pulls enemies in" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Shield = true, Flags = 2, Knockback = 6f, Note = "W Path Maker: charges with reduced damage, dashes, knocks back and stuns" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Shield = true, Note = "E Footwork: dashes (further to an ally) and gains a shield" },
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Flags = 2, Knockback = 10f, Note = "R All Out: knocks enemy back through walls, then dashes after them; stun on wall hit" },
			};
			t["Kaisa"] = new[] {
				new SpellScript(SpellShape.Homing) { Count = 6, SpreadDeg = 120f, Note = "Q Icathian Rain: swarm of missiles seeking nearby targets (more when evolved)" },
				new SpellScript(SpellShape.Skillshot) { Note = "W Void Seeker: long-range missile stops at first champion, marks with passive" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Supercharge: move speed, then attack speed; evolved grants invisibility" },
				new SpellScript(SpellShape.Dash) { DashTiles = 24f, Shield = true, Note = "R Killer Instinct: dashes near an enemy champion, gains shield" },
			};
			t["Kalista"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "Q Pierce: fast spear passes through enemies it kills" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Sentinel: sends a soul to scout and reveal area; bonus damage with Oathsworn (scout not modelled)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.3f, DmgMult = 1.2f, Flags = 1, Note = "E Rend: rips spears out of impaled enemies, escalating damage and slow" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, Knockback = 10f, Note = "R Fate's Call: Oathsworn teleports to her then dashes, knocking enemy champions back" },
			};
			t["Karma"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 1, SlowSec = 1.5f, Note = "Q Inner Flame: orb explodes on first enemy, slows; Mantra adds delayed cataclysm" },
				new SpellScript(SpellShape.Beam) { Flags = 2, Note = "W Focused Resolve: tether damages and reveals, roots if unbroken (instant root here); Mantra heals and extends root" },
				new SpellScript(SpellShape.Buff) { Shield = true, SpeedBuff = true, Note = "E Inspire: shield and move speed; Mantra spreads to nearby allies" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "R Mantra: empowers next ability with an extra effect (no equivalent, shield stand-in)" },
			};
		}
	}
}
