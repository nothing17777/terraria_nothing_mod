using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch3
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Gragas"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 2f, Flags = 1, Note = "Q Barrel Roll: barrel thrown, detonates on recast/after delay, slows" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "W Drunken Rage: damage reduction then empowered hit" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Flags = 2, Note = "E Body Slam: dash, stuns on impact" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.25f, Flags = 2, Note = "R Explosive Cask: thrown barrel, knocks enemies away" },
			};
			t["Graves"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "Q End of the Line: bullet that explodes after a delay" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 4f, Flags = 1, Note = "W Smoke Screen: cloud slows and blinds" },
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Note = "E Quickdraw: short dash, reload and armor" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "R Collateral Damage: piercing shell with cone explosion" },
			};
			t["Gwen"] = new[] {
				new SpellScript(SpellShape.Cone) { Count = 1, Note = "Q Snip Snip!: scissor cuts in a cone, final cut larger" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.2f, DurationSec = 4f, Note = "W Hallowed Mist: protective mist zone" },
				new SpellScript(SpellShape.Dash) { DashTiles = 4f, Note = "E Skip 'n Slash: dash and empowered attack" },
				new SpellScript(SpellShape.Skillshot) { Count = 3, SpreadDeg = 20f, Flags = 1, Note = "R Needlework: up to three needle volleys, slows" },
			};
			t["Hecarim"] = new[] {
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Note = "Q Rampage: sweep around the caster, stacks cooldown refund" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 3f, HealFrac = 0.05f, Note = "W Spirit of Dread: aura damages nearby, heals" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Flags = 2, Note = "E Devastating Charge: speed buff, knockback on hit" },
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, Flags = 4, Note = "R Onslaught of Shadows: long charge, fear then shockwave" },
			};
			t["Heimerdinger"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 6f, Note = "Q H-28G Evolution Turret: turret placed, attacks in zone" },
				new SpellScript(SpellShape.Beam) { Note = "W Hextech Micro-Rockets: three rockets in a line" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "E CH-2 Electron Storm Grenade: grenade stuns/slows at end" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "R UPGRADE!!!: empowers next basic spell (no equivalent, speed buff)" },
			};
			t["Hwei"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "Q Devastating Fire: missile explosion (QQ variant)" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "W Shapes of Mind: shield (WW) or aura (WE variants)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 4, Note = "E Grim Visage: fear/slow skillshot (EQ variant)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.6f, DurationSec = 1.5f, Flags = 1, Note = "R Spiraling Despair: large area at cursor, slows" },
			};
			t["Illaoi"] = new[] {
				new SpellScript(SpellShape.Beam) { Note = "Q Tentacle Smash: tentacle slam in a line" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Harsh Lesson: empowered hit (speed buff stand-in)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Note = "E Test of Spirit: pulls spirit out of enemy" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, DmgMult = 1.2f, Note = "R Leap of Faith: slam around caster, tentacles strike" },
			};
			t["Irelia"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "Q Bladesurge: dash to target, resets on kill" },
				new SpellScript(SpellShape.Cone) { Flags = 1, Note = "W Defiant Dance: charged blade sweep, damage reduction" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.4f, DurationSec = 2f, Flags = 2, Note = "E Flawless Duet: two blades form a stunning line" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.2f, DurationSec = 5f, Flags = 1, Note = "R Vanguard's Edge: blade wall slows" },
			};
			t["Ivern"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "Q Rootcaller: roots first enemy hit" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.2f, DurationSec = 4f, Note = "W Brushmaker: bush at cursor (no stealth, damage zone)" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "E Triggerseed: shield an ally with explosion" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 5f, Note = "R Daisy!: summons Daisy golem (area stand-in)" },
			};
			t["Janna"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "Q Howling Gale: tornado, knock-up grows with charge" },
				new SpellScript(SpellShape.Homing) { Flags = 1, Note = "W Zephyr: homing wind, slows" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "E Eye of the Storm: shield" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 3f, HealFrac = 0.1f, Flags = 2, Note = "R Monsoon: knocks enemies back, heals while channeling" },
			};
			t["JarvanIV"] = new[] {
				new SpellScript(SpellShape.Beam) { Flags = 2, Note = "Q Dragon Strike: line strike, knocks up" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Shield = true, Flags = 1, Note = "W Golden Aegis: shield and slow" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 5f, Note = "E Demacian Standard: flag zone" },
				new SpellScript(SpellShape.Dash) { DashTiles = 9f, Flags = 2, Note = "R Cataclysm: leap, arena traps enemies" },
			};
			t["Jax"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "Q Leap Strike: leap to target" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Empower: empowers next attack (speed buff)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.7f, Flags = 2, Note = "E Counter Strike: dodge, then stunning spin" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "R Grandmaster's Might: bonus resists and empowered hits" },
			};
			t["Jayce"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "Q To the Skies!/Shock Blast: fast orb (ranged form)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 3f, Note = "W Lightning Field/Hyper Charge: aura around caster" },
				new SpellScript(SpellShape.Cone) { Flags = 2, Note = "E Thundering Blow: knockback strike" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "R Mercury Cannon/Hammer: form swap (speed buff stand-in)" },
			};
			t["Jhin"] = new[] {
				new SpellScript(SpellShape.Homing) { Note = "Q Dancing Grenade: bounces between enemies" },
				new SpellScript(SpellShape.Beam) { Flags = 2, Note = "W Deadly Flourish: long line, roots marked targets" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 3f, Flags = 1, Note = "E Captive Audience: trap flower, slows" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, DmgMult = 1.3f, Flags = 1, Note = "R Curtain Call: four long-range shots, slow" },
			};
			t["Jinx"] = new[] {
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "Q Switcheroo!: weapon swap (speed buff stand-in)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Note = "W Zap!: long shock slows" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.4f, DurationSec = 5f, Flags = 2, Note = "E Flame Chompers!: chompers root enemies" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, SpeedMult = 1.5f, Note = "R Super Mega Death Rocket!: global rocket, explodes on first hit" },
			};
			t["KSante"] = new[] {
				new SpellScript(SpellShape.Beam) { Flags = 2, Note = "Q Ntofo Strikes: thrust, pulls enemies" },
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, Flags = 4, Note = "W Path Maker: charge, knockback and slow" },
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Note = "E Footwork: dash to allied/enemy target" },
				new SpellScript(SpellShape.Dash) { DashTiles = 7f, Flags = 2, Note = "R Dream Weaver: knocks enemy toward wall, stun" },
			};
			t["Kaisa"] = new[] {
				new SpellScript(SpellShape.Orbit) { Count = 6, DurationSec = 1f, Note = "Q Icathian Rain: missiles fire at nearby enemies" },
				new SpellScript(SpellShape.Skillshot) { Note = "W Void Seeker: long-range missile marking Plasma" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Supercharge: attack/move speed buff" },
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, Shield = true, Note = "R Killer Instinct: dash to marked enemy, shield" },
			};
			t["Kalista"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "Q Pierce: spear passes through enemies" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Sentinel: summons sentinel (no equivalent)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.3f, DmgMult = 1.2f, Flags = 1, Note = "E Rend: detonates spears on enemies, slows" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 1.5f, Flags = 2, Note = "R Fate's Call: pulls ally and knocks nearby enemies up" },
			};
			t["Karma"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Note = "Q Inner Flame: orb, explodes slowing" },
				new SpellScript(SpellShape.Beam) { Flags = 2, Note = "W Focused Resolve: tether to enemy, roots" },
				new SpellScript(SpellShape.Buff) { Shield = true, SpeedBuff = true, Note = "E Inspire: shield and haste" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "R Mantra: empowers next spell" },
			};
		}
	}
}
