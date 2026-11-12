using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch5
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Malphite"] = new[] {
				new SpellScript(SpellShape.Homing) { Flags = 1, Note = "Q Seismic Shard: targeted shard, slows" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Note = "W Thunderclap: empowered strike area around caster" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, DmgMult = 0.8f, Flags = 1, Note = "E Ground Slam: area around caster, slows" },
				new SpellScript(SpellShape.Dash) { DashTiles = 30f, Flags = 2, Note = "R Unstoppable Force: dash with knock-up impact" },
			};
			t["Malzahar"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 4, Note = "Q Call of the Void: two portals' wave, silences" },
				new SpellScript(SpellShape.Zone) { DurationSec = 3f, Note = "W Void Swarm: spawns voidlings (lingering area at cursor)" },
				new SpellScript(SpellShape.Homing) { DmgMult = 0.8f, Note = "E Malefic Visions: targeted DoT" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 2.5f, HitIntervalSec = 0.4f, Flags = 2, Note = "R Nether Grasp: suppress channel at target" },
			};
			t["Maokai"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Flags = 2, Note = "Q Bramble Smash: short dash, knockback" },
				new SpellScript(SpellShape.Dash) { DashTiles = 18f, Flags = 2, Note = "W Twisted Advance: dash to target, roots" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Note = "E Sapling Toss: lobbed sapling, slows (Zone-like projectile)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "R Nature's Grasp: long-range wall of brambles, roots" },
			};
			t["MasterYi"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 15f, Note = "Q Alpha Strike: untargetable dash striking enemies" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.2f, Note = "W Meditate: channel heal and damage reduction" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Wuju Style: bonus damage on attacks (speed stand-in)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "R Highlander: move/attack speed buff" },
			};
			t["Mel"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.4f, DurationSec = 0.6f, Note = "Q Radiant Volley: channelled barrage of missiles at an area" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "W Rebuttal: reflects magic damage (shield stand-in)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "E Solar Snare: snare skillshot" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, Note = "R Golden Eclipse: global execute pulse (area burst stand-in)" },
			};
			t["Milio"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "Q Ultra Mega Fire Kick: kick that knocks back" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.15f, Note = "W Cozy Campfire: healing zone (self heal stand-in)" },
				new SpellScript(SpellShape.Buff) { Shield = true, SpeedBuff = true, Note = "E Warm Hugs: ally shield and speed (self)" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.25f, Note = "R Breath of Life: area heal and cleanse (self heal)" },
			};
			t["MissFortune"] = new[] {
				new SpellScript(SpellShape.Homing) { Note = "Q Double Up: bounce shot to a second target" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Strut: passive attack speed, active move speed" },
				new SpellScript(SpellShape.Zone) { DurationSec = 2f, HitIntervalSec = 0.5f, Flags = 1, Note = "E Make It Rain: area of bullets, slows" },
				new SpellScript(SpellShape.Cone) { DurationSec = 2f, Note = "R Bullet Time: channelled cone of bullets" },
			};
			t["MonkeyKing"] = new[] {
				new SpellScript(SpellShape.Cone) { Note = "Q Crushing Blow: empowered strike" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Decoy: invisible clone and speed" },
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, Note = "E Nimbus Strike: dash to target with clones" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2f, HitIntervalSec = 0.4f, Flags = 2, Note = "R Cyclone: spinning area, knock-up" },
			};
			t["Mordekaiser"] = new[] {
				new SpellScript(SpellShape.Cone) { Note = "Q Obliterate: mace smash in front" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "W Indestructible: shield, re-cast heals" },
				new SpellScript(SpellShape.Cone) { Flags = 1, WidthMult = 1.3f, Note = "E Death's Grasp: pulls enemies in (cone stand-in)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 3f, HitIntervalSec = 0.5f, Flags = 1, Note = "R Realm of Death: drags target to a duel arena" },
			};
			t["Morgana"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "Q Dark Binding: snare skillshot" },
				new SpellScript(SpellShape.Zone) { DurationSec = 5f, HitIntervalSec = 0.5f, Flags = 1, Note = "W Tormented Shadow: damaging pool" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "E Black Shield: spell shield" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, Flags = 2, Note = "R Soul Shackles: chains pull and stun nearby enemies" },
			};
			t["Naafiri"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Note = "Q Darkin Daggers: daggers out and back" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "W Hounds' Pursuit: hounds dash to target" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "E Eviscerate: dash slash" },
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, Flags = 2, Note = "R The Call of the Pack: dash with pack, impact stun" },
			};
			t["Nami"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.8f, DurationSec = 0.3f, Flags = 2, Note = "Q Aqua Prison: delayed bubble, stuns" },
				new SpellScript(SpellShape.Homing) { Count = 3, SpreadDeg = 40f, HealFrac = 0.05f, Note = "W Ebb and Flow: bouncing water heal/damage" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Tidecaller's Blessing: ally attack empower (self speed)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "R Tidal Wave: huge wave, knock-up" },
			};
			t["Nasus"] = new[] {
				new SpellScript(SpellShape.Cone) { Note = "Q Siphoning Strike: empowered strike" },
				new SpellScript(SpellShape.Homing) { Flags = 1, Note = "W Wither: targeted slow" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 3f, HitIntervalSec = 0.5f, Note = "E Spirit Fire: area at cursor, damage over time" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.3f, Note = "R Fury of the Sands: transform, bonus health" },
			};
			t["Nautilus"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "Q Dredge Line: hook, pulls and stuns" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "W Titan's Wrath: shield and empowered attacks" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Flags = 1, Note = "E Riptide: shockwaves around caster, slow" },
				new SpellScript(SpellShape.Homing) { Flags = 2, Note = "R Depth Charge: targeted charge, knock-up" },
			};
			t["Neeko"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 0.3f, Flags = 1, Note = "Q Blooming Burst: seed blooms at area" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Shapesplitter: clone and speed" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "E Tangle-Barbs: piercing vine, roots" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, Flags = 2, Note = "R Pop Blossom: leap then burst, stun" },
			};
			t["Nidalee"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Note = "Q Javelin Toss: long spear" },
				new SpellScript(SpellShape.Zone) { DurationSec = 4f, HitIntervalSec = 1f, Flags = 1, Note = "W Bushwhack: trap at cursor" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.15f, Note = "E Primal Surge: heal" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "R Aspect of the Cougar: transform, pounce (dash)" },
			};
			t["Nilah"] = new[] {
				new SpellScript(SpellShape.Beam) { Note = "Q Formless Blade: whip line" },
				new SpellScript(SpellShape.Buff) { Shield = true, SpeedBuff = true, Note = "W Jubilant Veil: damage reduction and speed" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Note = "E Slipstream: dash through target" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 1f, HitIntervalSec = 0.3f, Flags = 2, Note = "R Apotheosis: spinning area, pulls enemies" },
			};
			t["Nocturne"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "Q Duskbringer: shadow trail missile" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "W Shroud of Darkness: spell shield" },
				new SpellScript(SpellShape.Homing) { Flags = 4, Note = "E Unspeakable Horror: tether then fear" },
				new SpellScript(SpellShape.Dash) { DashTiles = 25f, Note = "R Paranoia: blind and dash to target" },
			};
			t["Nunu"] = new[] {
				new SpellScript(SpellShape.Homing) { Note = "Q Consume: bite, heals" , HealFrac = 0.05f },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "W Biggest Snowball Ever!: rolling snowball" },
				new SpellScript(SpellShape.Skillshot) { Count = 3, SpreadDeg = 10f, Flags = 1, Note = "E Snowball Barrage: snowballs, root" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2.5f, HitIntervalSec = 0.4f, Flags = 1, Note = "R Absolute Zero: channelled blizzard" },
			};
		}
	}
}
