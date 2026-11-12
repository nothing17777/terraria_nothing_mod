using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch9
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Vladimir"] = new[] {
				new SpellScript(SpellShape.Homing) { Lifesteal = 0.5f, Note = "Q Transfusion: targeted bolt, steals life (heals Vladimir); empowered at full Crimson Rush (not implemented)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2f, HitIntervalSec = 0.5f, Flags = 1, Lifesteal = 0.3f, InvisibleSec = 2f, Note = "W Sanguine Pool: 2s untargetable pool, slows enemies on it and drains life (untargetable approximated by invisibility)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Flags = 1, Note = "E Tides of Blood: pays health to charge, releases a blood burst around him, blocked by first enemy unit in each direction (charge, health cost, block not implemented)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 3f, DurationSec = 0.5f, HealFrac = 0.1f, PoisonSec = 3f, Note = "R Hemoplague: area at cursor, enemies take increased damage then after 3s burst damage and heal Vladimir (amp not implemented)" },
			};
			t["Volibear"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, SpeedBuff = true, Flags = 2, Note = "Q Thundering Smash: speed toward enemies, first attack deals damage and stuns (speed + dash approximation)" },
				new SpellScript(SpellShape.Cone) { HealFrac = 0.05f, BleedSec = 3f, Note = "W Frenzied Maul: bite with on-hit and mark; recast on same target deals bonus damage and heals (recast not implemented)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.6f, DurationSec = 0.3f, Flags = 1, Shield = true, Note = "E Sky Splitter: lightning bolt at cursor, slows enemies, shields Volibear if inside blast" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Flags = 1, DmgMult = 1.3f, Note = "R Stormbringer: leap to location (targetable only after landing), slows and damages enemies beneath, gains bonus health; disables towers (not implemented)" },
			};
			t["Warwick"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Lifesteal = 1f, Note = "Q Jaws of the Beast: lunges and bites target, %max health damage, heals for damage dealt (hold to continue, not implemented)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Blood Hunt: senses enemies below 50% health, move speed toward and attack speed against them, tripled below 25% (tracking not implemented)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Flags = 4, Shield = true, Note = "E Primal Howl: 2.5s damage reduction, then howl makes nearby enemies flee for 1s (reduction approximated as shield, fear applied at cast)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 15f, Flags = 2, Lifesteal = 1f, Note = "R Infinite Duress: leap scaling with move speed, suppresses first champion hit 1.5s while channeling, heals (suppress approximated as stun)" },
			};
			t["Xayah"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Count = 2, SpreadDeg = 8f, Pierce = true, Note = "Q Double Daggers: two piercing daggers that drop Feathers (feather drop not implemented)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Deadly Plumage: 4s attack speed + damage, move speed when attacking champion (attack speed/damage not implemented)" },
				new SpellScript(SpellShape.Beam) { Flags = 2, Pierce = true, Note = "E Bladecaller: recalls all dropped Feathers, damaging enemies in their path and rooting (dropped feathers not tracked; beam approximation)" },
				new SpellScript(SpellShape.Cone) { Count = 5, SpreadDeg = 60f, Flags = 2, Knockback = -4f, InvisibleSec = 1f, DmgMult = 1.2f, Note = "R Featherstorm: leap into air untargetable, fan of daggers drops Feathers, backward hop (backward leap not implemented)" },
			};
			t["Xerath"] = new[] {
				new SpellScript(SpellShape.Beam) { RangeMult = 2f, Note = "Q Arcanopulse: hold to charge, long-range piercing beam, longer with charge (charge not implemented)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.8f, DurationSec = 0.3f, Flags = 1, Note = "W Eye of Destruction: delayed blast at cursor, slows, center deals extra damage + stronger slow (center bonus not implemented)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "E Shocking Orb: missile stuns first enemy hit, stun longer the farther it travels" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.6f, DurationSec = 0.3f, DmgMult = 1.2f, Note = "R Rite of the Arcane: immobile, 3 long-range delayed barrages at cursor (multi-charge and self-root not implemented)" },
			};
			t["XinZhao"] = new[] {
				new SpellScript(SpellShape.Cone) { Flags = 2, DmgMult = 1.3f, Note = "Q Three Talon Strike: next 3 attacks deal bonus damage, third knocks airborne (3-hit sequence not implemented)" },
				new SpellScript(SpellShape.Beam) { Flags = 1, SlowSec = 1.5f, WidthMult = 1.3f, Note = "W Wind Becomes Lightning: slash then thrust forward, slows and marks Challenged" },
				new SpellScript(SpellShape.Dash) { DashTiles = 7f, Flags = 1, SpeedBuff = true, SlowSec = 0.5f, Note = "E Audacious Charge: charges to an enemy, attack speed, damages and briefly slows nearby; longer range vs Challenged" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Flags = 2, Knockback = 6f, Shield = true, Note = "R Crescent Guard: %current health damage to nearby, knocks non-challenged back, impervious to champions outside circle (approximated as shield)" },
			};
			t["Yasuo"] = new[] {
				new SpellScript(SpellShape.Beam) { Flags = 2, RangeMult = 0.6f, Note = "Q Steel Tempest: short thrust through line; 2 stacks fires whirlwind that knocks airborne (stacks not implemented)" },
				new SpellScript(SpellShape.Zone) { DurationSec = 4f, Shield = true, Note = "W Wind Wall: moving wall blocks enemy projectiles for 4s (projectile blocking not implemented; shield approximation)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Note = "E Sweeping Blade: dashes through target enemy, magic damage, stacks bonus; cannot recast on same enemy" },
				new SpellScript(SpellShape.Dash) { DashTiles = 22f, Flags = 2, DmgMult = 1.3f, Note = "R Last Breath: blinks to airborne champion, damages and suspends all airborne enemies in area" },
			};
			t["Yone"] = new[] {
				new SpellScript(SpellShape.Beam) { Flags = 2, RangeMult = 0.8f, Note = "Q Mortal Steel: thrust through line; 2 stacks dashes Yone with a gust knocking airborne (stacks not implemented)" },
				new SpellScript(SpellShape.Cone) { Shield = true, Note = "W Spirit Cleave: cleaves a cone, shield for Yone increased per champion hit" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, InvisibleSec = 0.1f, Note = "E Soul Unbound: spirit leaves body with move speed, on end snaps back and repeats portion of damage dealt (spirit body/replay not implemented)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, Flags = 2, Knockback = -5f, WidthMult = 1.5f, Note = "R Fate Sealed: blinks behind last champion in line with a slash that pulls all enemies hit toward him (airborne pull)" },
			};
			t["Yorick"] = new[] {
				new SpellScript(SpellShape.Cone) { HealFrac = 0.05f, DmgMult = 1.3f, Note = "Q Last Rites: next attack bonus damage and heals; digs a grave if champion/large/kills (empowered attack + grave not implemented)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 4f, Flags = 1, HitIntervalSec = 1f, Note = "W Dark Procession: destructible ring-wall at cursor blocking movement, (wall collision not implemented; slow approximation)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, SlowSec = 1.5f, Note = "E Mourning Mist: globule of Black Mist, damages, slows, reduces armor and marks enemies (armor reduction not implemented)" },
				new SpellScript(SpellShape.Orbit) { Count = 1, DurationSec = 8f, Note = "R Eulogy of the Isles: summons Maiden of the Mist that attacks target and raises Walkers (summon behavior not implemented)" },
			};
			t["Yunara"] = new[] {
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "Q Cultivation of Spirit: attack speed, on-hit damage, attacks spread to nearby enemies (on-hit/spread not implemented)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = false, Flags = 1, SlowSec = 1.5f, SpeedMult = 0.9f, Note = "W Arc of Judgment: spinning prayer bead damages and slows; Transcendent form is a laser (form not implemented)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, InvisibleSec = 2f, Note = "E Kanmei's Steps: move speed and ghosted; Transcendent form dashes instead (form not implemented)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 1f, Flags = 1, SpeedBuff = true, Note = "R Transcend One's Self: enters Transcendent State upgrading basic abilities (state not implemented; area burst approximation)" },
			};
			t["Yuumi"] = new[] {
				new SpellScript(SpellShape.Skillshot) { SpeedMult = 0.5f, Flags = 1, SlowSec = 2f, Note = "Q Prowling Projectile: slow missile, damage + slow on first target; bonus and stronger slow if flight >= 1.35s; steerable when attached (not implemented)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, InvisibleSec = 0.5f, Note = "W You and Me!: dashes to ally and attaches, untargetable except turrets (attach not implemented)" },
				new SpellScript(SpellShape.Buff) { Shield = true, SpeedBuff = true, Note = "E Zoomies: shields and boosts move + attack speed; passes to ally if attached" },
				new SpellScript(SpellShape.Beam) { Flags = 1, HealFrac = 0.1f, DurationSec = 3f, HitIntervalSec = 0.6f, Note = "R Final Chapter: channels five waves, damaging enemies (slows/roots) and healing allies, steerable when attached" },
			};
			t["Zaahen"] = new[] {
				new SpellScript(SpellShape.Cone) { Count = 2, SpreadDeg = 20f, Lifesteal = 0.3f, Flags = 2, Note = "Q The Darkin Glaive: next attack slashes twice with bonus damage and heals; recast knocks target up (recast not implemented)" },
				new SpellScript(SpellShape.Beam) { Flags = 2, Knockback = -5f, Note = "W Dreaded Return: stabs in a direction, damages and pulls enemies hit toward him" },
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, Note = "E Aureate Rush: dashes to location and slices around himself (final spin not implemented)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.8f, DurationSec = 0.3f, DmgMult = 1.3f, Lifesteal = 0.3f, Note = "R Grim Deliverance: rises then stabs downward, damaging enemies and healing for a portion of damage dealt" },
			};
			t["Zac"] = new[] {
				new SpellScript(SpellShape.Beam) { Flags = 2, Knockback = -4f, Note = "Q Stretching Strikes: elastic arm grabs enemy; attacking different enemy throws both toward each other (throw not implemented, pull approximation)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.3f, Note = "W Unstable Matter: explodes outward, magic damage as % max health to nearby enemies" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Flags = 2, Note = "E Elastic Slingshot: charges then launches forward, knocks up enemies on landing (charge not implemented)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2f, HitIntervalSec = 0.5f, Flags = 2, SpeedBuff = true, Note = "R Let's Bounce!: four bounces, each knocking up and slowing enemies (bounces approximated as repeating burst)" },
			};
			t["Zed"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Count = 3, SpreadDeg = 10f, Note = "Q Razor Shuriken: Zed and shadows throw shurikens, piercing, damage each enemy hit (shadows approximated as fan)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, SpeedMult = 1f, Note = "W Living Shadow: shadow dashes forward and stays; recast swaps positions (shadow not implemented; dash approximation)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.3f, Flags = 1, SlowSec = 1.5f, Note = "E Shadow Slash: Zed and shadows slash nearby enemies, slowing those hit by shadows" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, InvisibleSec = 1f, BleedSec = 3f, DmgMult = 1.5f, Note = "R Death Mark: untargetable dash to champion and mark; after 3s repeats portion of damage dealt (delayed replay not implemented)" },
			};
			t["Zeri"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Count = 3, SpreadDeg = 6f, Note = "Q Burst Fire: burst of 7 rounds hits first enemy, treated as attack (7 rounds approximated as 3)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, SlowSec = 2f, SpeedMult = 1.4f, Note = "W Ultrashock Laser: electric pulse slows and damages first enemy hit; expands into long laser if hits wall (laser not implemented)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, SpeedBuff = true, Note = "E Spark Surge: short dash over terrain, Burst Fire pierces (pierce empowerment not implemented)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, SpeedBuff = true, Chain = 3, Note = "R Lightning Crash: nova of electricity, overcharged: damage, stacking move speed, Q triple shot chains lightning (overcharge state not implemented)" },
			};
			t["Ziggs"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 0.3f, Note = "Q Bouncing Bomb: bomb bounces and explodes, magic damage (bounce path not implemented)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.8f, DurationSec = 0.3f, Knockback = 6f, Note = "W Satchel Charge: charge detonates after delay or on recast, knocks enemies away; Ziggs also knocked, no damage (self knock + recast not implemented)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 6f, HitIntervalSec = 1f, Flags = 1, SlowSec = 1.5f, Note = "E Hexplosive Minefield: scatters proximity mines that detonate on contact, damage + slow, reduced on repeats (individual mines not implemented)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 1f, DurationSec = 0.4f, DmgMult = 1.3f, Note = "R Mega Inferno Bomb: long-range bomb, more damage in center blast zone (center bonus not implemented)" },
			};
			t["Zilean"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 3f, DurationSec = 0.3f, Flags = 2, Note = "Q Time Bomb: bomb sticks to nearby units, detonates after 3s with area damage; stuns if detonated early by another bomb (stick + chain stun not implemented)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Rewind: reduces cooldowns of his other abilities (cooldown reduction not implemented)" },
				new SpellScript(SpellShape.Homing) { Flags = 1, SlowSec = 2.5f, SpeedBuff = true, Note = "E Time Warp: slows enemy move speed or speeds up an ally (ally speed buff approximated as self speed)" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.5f, Shield = true, Note = "R Chronoshift: rune on ally, revives to health on lethal damage (revive not implemented; heal + shield approximation)" },
			};
			t["Zoe"] = new[] {
				new SpellScript(SpellShape.Skillshot) { SpeedMult = 1.2f, DmgMult = 1.3f, Note = "Q Paddle Star!: redirectable missile, more damage the farther it flies straight (redirect and distance scaling not implemented)" },
				new SpellScript(SpellShape.Homing) { Count = 3, SpreadDeg = 30f, Note = "W Spell Thief: picks up enemy summoner/item remnants, 3 missiles at nearest target after casting a summoner spell (pickup not implemented)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 4, DmgMult = 1.5f, SpeedMult = 1.1f, Note = "E Sleepy Trouble Bubble: bubble makes target drowsy then asleep (lowers MR, first damage doubled; sleep approximated as charm/silence flag)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Note = "R Portal Jump: blinks to nearby position for 1s then blinks back (return not implemented)" },
			};
			t["Zyra"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.6f, DurationSec = 0.3f, Note = "Q Deadly Spines: thick vines explode into spines in an area, grows Thorn Spitter near seed (plants not implemented)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.1f, DurationSec = 8f, HitIntervalSec = 1f, Note = "W Rampant Growth: plants a seed (up to 60s, stores multiple) that empowers Q/E into plants (seeds/plants not implemented; lingering zone approximation)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "E Grasping Roots: vines through the ground damage and root enemies they pass, Vine Lasher near seed slows (plant not implemented)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 2f, HitIntervalSec = 0.5f, Flags = 2, Knockback = -2f, Note = "R Stranglethorns: thicket at cursor damages enemies as it expands and knocks airborne as it contracts, enrages plants" },
			};
		}
	}
}
