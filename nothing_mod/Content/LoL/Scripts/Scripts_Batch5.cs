using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch5
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Malphite"] = new[] {
				new SpellScript(SpellShape.Homing) { Flags = 1, SlowSec = 3f, Note = "Q Seismic Shard: targeted shard through the ground, damage + steals move speed 3s (slow)" },
				new SpellScript(SpellShape.Cone) { DmgMult = 0.8f, Note = "W Thunderclap: empowered attack; for a few seconds attacks create aftershock area in front (timed empowerment not expressible)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Flags = 1, Note = "E Ground Slam: shockwave around caster, damage scales with armor, reduces attack speed (slow stand-in)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 40f, Flags = 2, Note = "R Unstoppable Force: high-speed launch to location, knocks enemies up on impact" },
			};
			t["Malzahar"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Count = 2, SpreadDeg = 14f, Pierce = true, Flags = 4, Note = "Q Call of the Void: two portals open, short delay, then fire piercing waves that silence" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 4f, HitIntervalSec = 0.5f, Note = "W Void Swarm: summons voidlings (stand-in: lingering damage area at cursor)" },
				new SpellScript(SpellShape.Homing) { PoisonSec = 4f, Chain = 1, Note = "E Malefic Visions: targeted DoT, refreshed by other spells, jumps to nearby enemy on death" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.2f, DurationSec = 2.5f, HitIntervalSec = 0.4f, Flags = 2, Note = "R Nether Grasp: channelled suppress on target within a damaging void zone" },
			};
			t["Maokai"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 1, Knockback = 7f, Note = "Q Bramble Smash: wave knocks back and slows enemies" },
				new SpellScript(SpellShape.Dash) { DashTiles = 22f, Flags = 2, Note = "W Twisted Advance: untargetable root-dash to target, roots on arrival" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 5f, HitIntervalSec = 1f, Note = "E Sapling Toss: lobbed sapling guards area, chases/explodes on enemies, stronger in brush" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, SpeedMult = 0.5f, WidthMult = 1.6f, Note = "R Nature's Grasp: huge slow wall of brambles, damages and roots everything it passes" },
			};
			t["MasterYi"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 25f, Note = "Q Alpha Strike: untargetable blink hitting multiple units along path; can crit; basic attacks reduce cooldown" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.2f, Shield = true, Note = "W Meditate: channel, heals and reduces damage taken" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Wuju Style: bonus true damage on basic attacks (attack-speed stand-in)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "R Highlander: move + attack speed, slow immunity, extended by kills/assists" },
			};
			t["Mel"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.2f, DurationSec = 1.5f, HitIntervalSec = 0.25f, Note = "Q Radiant Volley: barrage of projectiles explode repeatedly around target location" },
				new SpellScript(SpellShape.Buff) { Shield = true, SpeedBuff = true, Note = "W Rebuttal: barrier negates damage, reflects projectiles, grants move speed (reflect not expressible)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 3, SlowSec = 2f, Note = "E Solar Snare: orb roots those at center, slows and damages over time in surrounding area" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, Note = "R Golden Eclipse: hits every enemy marked with Overwhelm at any distance (global, mark-based not expressible)" },
			};
			t["Milio"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Knockback = 4f, Note = "Q Ultra Mega Fire Kick: ball knocks back target, lobs up and lands dealing damage + slow in area" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.1f, Note = "W Cozy Campfire: healing zone that follows nearest ally, +attack range (self heal stand-in)" },
				new SpellScript(SpellShape.Buff) { Shield = true, SpeedBuff = true, Note = "E Warm Hugs: tosses shield + move speed to ally, 2 charges (self stand-in)" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.25f, Note = "R Breath of Life: wave heals allies and cleanses CC (self heal stand-in)" },
			};
			t["MissFortune"] = new[] {
				new SpellScript(SpellShape.Homing) { Chain = 1, Note = "Q Double Up: bullet hits target and a second enemy behind it" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Strut: passive move speed out of combat, active bonus attack speed" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.2f, DurationSec = 2f, HitIntervalSec = 0.5f, Flags = 1, Note = "E Make It Rain: waves of bullets in area, slows" },
				new SpellScript(SpellShape.Cone) { Note = "R Bullet Time: 2s+ channel of bullet waves into a cone, each wave can crit (repeated waves not expressible)" },
			};
			t["MonkeyKing"] = new[] {
				new SpellScript(SpellShape.Cone) { Note = "Q Crushing Blow: next attack gains range, bonus damage, shreds armor" },
				new SpellScript(SpellShape.Dash) { DashTiles = 12f, InvisibleSec = 1.5f, SpeedBuff = true, Note = "W Warrior Trickster: invisible dash leaving a clone that attacks (clone not expressible)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 27f, Chain = 2, Note = "E Nimbus Strike: dash to targeted enemy, images strike enemies near the target" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 4f, HitIntervalSec = 0.4f, Flags = 2, SpeedBuff = true, Note = "R Cyclone: spinning staff with move speed, repeated damage and knock-up" },
			};
			t["Mordekaiser"] = new[] {
				new SpellScript(SpellShape.Cone) { Note = "Q Obliterate: mace smash, extra damage when it hits a single enemy" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "W Indestructible: stores damage dealt/taken as shield, recast consumes it to heal (storing/heal not expressible)" },
				new SpellScript(SpellShape.Cone) { Knockback = -7f, WidthMult = 1.3f, Note = "E Death's Grasp: pulls all enemies in an area toward him" },
				new SpellScript(SpellShape.Homing) { Flags = 2, Note = "R Realm of Death: drags target into a separate duel dimension and steals stats (isolation not expressible)" },
			};
			t["Morgana"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "Q Dark Binding: long snare on first enemy hit, damage" },
				new SpellScript(SpellShape.Zone) { DurationSec = 5f, HitIntervalSec = 0.5f, Note = "W Tormented Shadow: damaging pool, more damage on low-health enemies" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "E Black Shield: ally barrier absorbs magic damage and blocks CC" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, Flags = 3, SpeedBuff = true, Note = "R Soul Shackles: chains slow and damage nearby champions, then stun after delay unless they escape; move speed" },
			};
			t["Naafiri"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Count = 2, SpreadDeg = 12f, BleedSec = 4f, Note = "Q Darkin Daggers: up to two daggers, each bleeds (bonus dmg on bleeding); packmates leap to first champion hit" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W The Call of the Pack: untargetable, spawns extra packmates, bonus move speed and AD" },
				new SpellScript(SpellShape.Dash) { DashTiles = 12f, Note = "E Eviscerate: dash damaging enemies around her, recalls and heals packmates" },
				new SpellScript(SpellShape.Dash) { DashTiles = 25f, Note = "R Hounds' Pursuit: Naafiri and pack dash at a champion; recast once for a shield" },
			};
			t["Nami"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 1f, DurationSec = 0.3f, Flags = 2, Note = "Q Aqua Prison: bubble to target area, short delay, stuns all enemies on impact" },
				new SpellScript(SpellShape.Homing) { Chain = 2, HealFrac = 0.05f, Note = "W Ebb and Flow: stream bounces between allied and enemy champions, heals allies / damages enemies" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Tidecaller's Blessing: ally's attacks/spells deal bonus magic damage + slow (ally buff, self speed stand-in)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 3, WidthMult = 1.4f, SpeedMult = 0.8f, Note = "R Tidal Wave: massive wave knocks up, slows, damages; allies hit gain extra Surging Tides" },
			};
			t["Nasus"] = new[] {
				new SpellScript(SpellShape.Cone) { Note = "Q Siphoning Strike: empowered strike, permanently gains damage stacks on kill (stacking not expressible)" },
				new SpellScript(SpellShape.Homing) { Flags = 1, SlowSec = 5f, Note = "W Wither: targeted, decelerates move speed and attack speed over time" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 5f, HitIntervalSec = 0.5f, Note = "E Spirit Fire: flame at location damages and reduces armor of enemies standing on it" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 8f, HitIntervalSec = 0.5f, Shield = true, HealFrac = 0.3f, Note = "R Fury of the Sands: sandstorm damages nearby enemies; bonus health, range, armor and MR" },
			};
			t["Nautilus"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Knockback = -8f, Note = "Q Dredge Line: anchor pulls first enemy hit (and Nautilus) together; terrain hit pulls Nautilus" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "W Titan's Wrath: shield; while up, attacks deal DoT to target and nearby enemies" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.9f, HitIntervalSec = 0.3f, Flags = 1, Note = "E Riptide: three exploding waves around him, each damages and slows" },
				new SpellScript(SpellShape.Homing) { Flags = 2, Note = "R Depth Charge: shockwave chases target through ground, knocks enemies on path up, then knocks up and stuns target" },
			};
			t["Neeko"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.4f, DurationSec = 0.3f, Note = "Q Blooming Burst: seed thrown to area, blooms again on champion hit or kill (second bloom not expressible)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Shapesplitter: send a clone in a direction (recast redirects); passive bonus damage + move speed every 3rd attack (clone not expressible)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "E Tangle-Barbs: piercing tangle damages and roots all; bigger, faster, longer root after a kill or champion" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, Flags = 2, Note = "R Pop Blossom: short prep, leap knocks up, landing damages and stuns nearby enemies" },
			};
			t["Nidalee"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Note = "Q Javelin Toss: long spear, damage grows with distance flown (cougar form: Takedown not expressible)" },
				new SpellScript(SpellShape.Zone) { DurationSec = 4f, HitIntervalSec = 4f, Note = "W Bushwhack: trap at cursor, damages and reveals target when sprung (cougar: Pounce)" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.15f, SpeedBuff = true, Note = "E Primal Surge: heals ally and grants attack speed (cougar: Swipe)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 12f, Note = "R Aspect of the Cougar: transform into cougar with new abilities (form swap not expressible; pounce stand-in)" },
			};
			t["Nilah"] = new[] {
				new SpellScript(SpellShape.Beam) { Note = "Q Formless Blade: whip line damages all in a straight line, briefly extends attack range" },
				new SpellScript(SpellShape.Buff) { Shield = true, SpeedBuff = true, Note = "W Jubilant Veil: mist grants move speed and dodges all attacks; allies touched gain it too" },
				new SpellScript(SpellShape.Dash) { DashTiles = 23f, Note = "E Slipstream: dash toward target damaging enemies passed through" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.9f, HitIntervalSec = 0.3f, Knockback = -5f, Note = "R Apotheosis: spinning whip damages enemies around her, then pulls them closer" },
			};
			t["Nocturne"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "Q Duskbringer: shadow blade leaves Dusk Trail; on trail he passes through units, +move speed and AD (trail not expressible)" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "W Shroud of Darkness: spell shield blocks one enemy ability, doubles passive attack speed; passive AS" },
				new SpellScript(SpellShape.Homing) { Flags = 4, PoisonSec = 2f, Note = "E Unspeakable Horror: tether damages each second, fears if target stays in range at end" },
				new SpellScript(SpellShape.Dash) { DashTiles = 25f, Note = "R Paranoia: reduces enemy champion sight, then launches at a nearby enemy champion (vision effect not expressible)" },
			};
			t["Nunu"] = new[] {
				new SpellScript(SpellShape.Homing) { HealFrac = 0.05f, Note = "Q Consume: bite on minion, monster or champion, damages and heals" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, WidthMult = 1.3f, SpeedMult = 0.7f, Note = "W Biggest Snowball Ever!: snowball grows in size and speed as rolled, damages and knocks up (growth not expressible)" },
				new SpellScript(SpellShape.Skillshot) { Count = 3, SpreadDeg = 10f, Flags = 2, Note = "E Snowball Barrage: multiple snowballs, then roots champions/large monsters hit (delayed root not expressible)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 3f, HitIntervalSec = 0.5f, Flags = 1, Note = "R Absolute Zero: channelled blizzard around him slows enemies, massive damage burst at end (end burst not expressible)" },
			};
		}
	}
}
