using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch9
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Vladimir"] = new[] {
				new SpellScript(SpellShape.Homing) { Note = "Q Transfusion: targeted bolt that heals" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2f, HitIntervalSec = 0.4f, Flags = 1, Note = "W Sanguine Pool: untargetable pool slowing around caster" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.6f, Flags = 1, Note = "E Tides of Blood: charged burst of blood around caster" },
				new SpellScript(SpellShape.Zone) { DelaySec = 1f, DurationSec = 0.5f, Note = "R Hemoplague: delayed area at cursor" },
			};
			t["Volibear"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Flags = 2, Note = "Q Thundering Smash: dash, stun first target" },
				new SpellScript(SpellShape.Cone) { Note = "W Frenzied Maul: claw swipe" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.6f, DurationSec = 1f, Flags = 2, Note = "E Sky Splitter: lightning strike at cursor" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, DmgMult = 1.5f, Flags = 1, Note = "R Stormbringer: leap and crash" },
			};
			t["Warwick"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, HealFrac = 0.1f, Note = "Q Jaws of the Beast: lunge and bite, heals" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Blood Hunt: speed boost (tracking not implemented)" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "E Primal Howl: damage reduction then fear (fear not implemented)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 15f, Flags = 2, HealFrac = 0.1f, Note = "R Infinite Duress: long leap suppressing target" },
			};
			t["Xayah"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Count = 2, SpreadDeg = 12f, Pierce = true, Note = "Q Double Daggers: two piercing daggers" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Deadly Plumage: attack speed and move speed" },
				new SpellScript(SpellShape.Beam) { Flags = 2, Note = "E Bladecaller: recalls feathers, rooting hit enemies" },
				new SpellScript(SpellShape.Cone) { Flags = 2, DmgMult = 1.2f, Note = "R Featherstorm: leap away, volley of feathers roots" },
			};
			t["Xerath"] = new[] {
				new SpellScript(SpellShape.Beam) { Note = "Q Arcanopulse: charged long beam" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.8f, DurationSec = 0.4f, Flags = 1, Note = "W Eye of Destruction: delayed blast, slows" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "E Shocking Orb: stunning missile" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.6f, DurationSec = 0.3f, DmgMult = 1.2f, Note = "R Rite of the Arcane: long range artillery shot at cursor (charges not implemented)" },
			};
			t["XinZhao"] = new[] {
				new SpellScript(SpellShape.Cone) { Flags = 2, Note = "Q Three Talon Strike: thrust, third hit knocks up" },
				new SpellScript(SpellShape.Beam) { Flags = 1, Note = "W Wind Becomes Lightning: slash then thrust, slows" },
				new SpellScript(SpellShape.Dash) { DashTiles = 7f, Flags = 1, Note = "E Audacious Charge: dash to enemy, slows" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Flags = 2, Note = "R Crescent Guard: knockback burst around caster" },
			};
			t["Yasuo"] = new[] {
				new SpellScript(SpellShape.Beam) { Flags = 2, Note = "Q Steel Tempest: short thrust, third cast knocks up" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "W Wind Wall: blocks projectiles; nearest is damage reduction" },
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Note = "E Sweeping Blade: dash through enemy" },
				new SpellScript(SpellShape.Dash) { DashTiles = 18f, Flags = 2, DmgMult = 1.3f, Note = "R Last Breath: blink to airborne enemy" },
			};
			t["Yone"] = new[] {
				new SpellScript(SpellShape.Beam) { Flags = 1, Note = "Q Mortal Steel: thrust, third cast knocks up" },
				new SpellScript(SpellShape.Cone) { Shield = true, Note = "W Spirit Cleave: cone slash with shield" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, SpeedBuff = true, Note = "E Soul Unbound: spirit form dash (damage replay not implemented)" },
				new SpellScript(SpellShape.Beam) { Flags = 2, WidthMult = 1.5f, Note = "R Fate Sealed: blink and wide piercing slash, pulls enemies" },
			};
			t["Yorick"] = new[] {
				new SpellScript(SpellShape.Cone) { Note = "Q Last Rites: spade strike, heals on Mist Walker" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 3f, Flags = 1, Note = "W Dark Procession: ring of mist (cage) at cursor" },
				new SpellScript(SpellShape.Cone) { Flags = 1, Note = "E Mourning Mist: wave of mist, slows" },
				new SpellScript(SpellShape.Orbit) { Count = 1, DurationSec = 8f, Note = "R Eulogy of the Isles: summons Maiden of the Mist" },
			};
			t["Yunara"] = new[] {
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "Q Arc of Radiance: empowers attacks (approximated as speed)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 1, Note = "W Spirit Beam: piercing shot" },
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, SpeedBuff = true, Note = "E Aspect of Light: dash with speed buff" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2f, Flags = 1, Note = "R Radiant Barrage: channeled area around caster" },
			};
			t["Yuumi"] = new[] {
				new SpellScript(SpellShape.Skillshot) { SpeedMult = 3f, Flags = 1, Note = "Q Prowling Projectile: slow-growing missile" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "W You and Me!: attach to ally, approximated as short dash" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.15f, Shield = true, SpeedBuff = true, Note = "E Zoomies: heal, shield and speed to ally" },
				new SpellScript(SpellShape.Beam) { Flags = 2, Note = "R Final Chapter: channeled waves that root" },
			};
			t["Zaahen"] = new[] {
				new SpellScript(SpellShape.Beam) { Flags = 2, Note = "Q UNVERIFIED shape: long thrust, third cast knocks up" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "W UNVERIFIED shape: slash wave" },
				new SpellScript(SpellShape.Dash) { DashTiles = 7f, Note = "E UNVERIFIED shape: dash" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, DmgMult = 1.3f, Note = "R UNVERIFIED shape: leap strike" },
			};
			t["Zac"] = new[] {
				new SpellScript(SpellShape.Beam) { Flags = 2, Note = "Q Stretching Strikes: elastic whip" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.3f, Note = "W Unstable Matter: burst around caster" },
				new SpellScript(SpellShape.Dash) { DashTiles = 12f, Flags = 2, Note = "E Elastic Slingshot: charged leap, knocks up" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2f, Flags = 2, HitIntervalSec = 0.5f, Note = "R Let's Bounce!: bounces knocking back" },
			};
			t["Zed"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Count = 3, SpreadDeg = 8f, Pierce = true, DmgMult = 0.5f, Note = "Q Razor Shuriken: shuriken by Zed and shadows" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "W Living Shadow: shadow dash/swap" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.3f, Flags = 1, Note = "E Shadow Slash: spin slash, slows" },
				new SpellScript(SpellShape.Dash) { DashTiles = 12f, DmgMult = 2f, Note = "R Death Mark: blink onto target (delayed damage simplified)" },
			};
			t["Zeri"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Count = 3, SpreadDeg = 4f, DmgMult = 0.4f, Note = "Q Burst Fire: rapid bullets" },
				new SpellScript(SpellShape.Beam) { Flags = 1, Note = "W Ultrashock Laser: bolt then laser through terrain" },
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, Note = "E Spark Surge: dash" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 4f, Flags = 1, SpeedBuff = true, Note = "R Lightning Crash: electric burst, overcharged for several seconds" },
			};
			t["Ziggs"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 0.3f, Note = "Q Bouncing Bomb: lobbed bomb" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.8f, DurationSec = 0.3f, Flags = 2, Note = "W Satchel Charge: knockback blast" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 4f, Flags = 1, Note = "E Hexplosive Minefield: mines that slow" },
				new SpellScript(SpellShape.Zone) { DelaySec = 1f, DurationSec = 0.4f, DmgMult = 1.2f, Note = "R Mega Inferno Bomb: huge explosion at cursor" },
			};
			t["Zilean"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 1.5f, DurationSec = 0.3f, Flags = 2, Note = "Q Time Bomb: delayed explosion, stuns" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Rewind: cooldown reset, approximated as speed" },
				new SpellScript(SpellShape.Zone) { DurationSec = 2.5f, Flags = 1, Note = "E Time Warp: slow area / speed up ally" },
				new SpellScript(SpellShape.Buff) { Shield = true, HealFrac = 0.3f, Note = "R Chronoshift: rewinds death, approximated as heal and shield" },
			};
			t["Zoe"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Note = "Q Paddle Star: bounces back, damage grows with distance" },
				new SpellScript(SpellShape.Homing) { SpeedMult = 1.3f, Note = "W Spell Thief: spell shards (approximated as homing bolt)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 4, Note = "E Sleepy Trouble Bubble: bubble puts target to sleep" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "R Portal Jump: blink and return" },
			};
			t["Zyra"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.6f, DurationSec = 0.4f, Note = "Q Deadly Spines: spikes erupt at cursor" },
				new SpellScript(SpellShape.Orbit) { Count = 2, DurationSec = 5f, Note = "W Rampant Growth: plants (approximated as orbiting)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "E Grasping Roots: vine roots enemies" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 2f, Flags = 2, Note = "R Stranglethorns: thorns knock up" },
			};
		}
	}
}
