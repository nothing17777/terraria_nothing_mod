using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch4
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Karthus"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.9f, DurationSec = 0.2f, Note = "Q Lay Waste: delayed blast at a location (r160); more damage to isolated enemies (not modelled)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.25f, DurationSec = 5f, HitIntervalSec = 1f, Flags = 1, SlowSec = 1.5f, WidthMult = 2.5f, DmgMult = 0.1f, Note = "W Wall of Pain: passable screen (line) that slows and cuts magic resist; modelled as wide slow zone, MR cut and wall shape missing" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 5f, HitIntervalSec = 0.5f, DmgMult = 0.6f, Note = "E Defile: toggle aura damaging nearby enemies per second, drains mana; mana gain on kill not modelled" },
				new SpellScript(SpellShape.Zone) { DelaySec = 3f, DurationSec = 0.3f, WidthMult = 4f, Note = "R Requiem: 3s channel then damage to ALL enemy champions globally; modelled as huge delayed zone at cursor" },
			};
			t["Kassadin"] = new[] {
				new SpellScript(SpellShape.Homing) { Shield = true, Note = "Q Null Sphere: targeted orb, damage + interrupts channels, caster gains magic shield; interrupt not modelled" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, DmgMult = 0.6f, Note = "W Nether Blade: passive on-hit magic damage; active empowers next attack for big damage and restores mana; modelled as short burst" },
				new SpellScript(SpellShape.Cone) { Flags = 1, SlowSec = 1f, Note = "E Force Pulse: charged by nearby spells, cone damage + slow" },
				new SpellScript(SpellShape.Dash) { DashTiles = 31f, Note = "R Riftwalk: teleport up to 500 range, damages enemies around arrival (r270); stacking cost/damage not modelled" },
			};
			t["Katarina"] = new[] {
				new SpellScript(SpellShape.Homing) { Chain = 3, Note = "Q Bouncing Blade: dagger hits target then bounces to nearby enemies (bounce count UNVERIFIED), then drops on ground; dagger pickup missing" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Preparation: move speed burst and tosses a dagger above herself (dagger pickup slash missing)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 45f, Note = "E Shunpo: blink up to 725 to target/nearest enemy and strike it" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2.5f, HitIntervalSec = 0.2f, DmgMult = 0.25f, WidthMult = 3.5f, Note = "R Death Lotus: channel, blades hit the 3 nearest enemy champions within 550; target limit not modelled" },
			};
			t["Kayle"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 1, SlowSec = 2f, Note = "Q Radiant Blast: portal then celestial sword piercing enemies, slow + resist shred; portal delay and shred missing" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.15f, SpeedBuff = true, Note = "W Celestial Blessing: heal and move speed to self and nearest ally (ally part missing)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 5f, HitIntervalSec = 0.5f, DmgMult = 0.4f, Note = "E Starfire Spellblade: passive on-hit magic damage; active next attack smites for missing-health damage; modelled as aura" },
				new SpellScript(SpellShape.SelfBurst) { Shield = true, DurationSec = 2.5f, HitIntervalSec = 0.5f, DmgMult = 0.4f, WidthMult = 3.5f, Note = "R Divine Judgment: ally invulnerable ~2s, holy sword rain around target (r775); modelled as invulnerability + damage aura on self, ally targeting missing" },
			};
			t["Kayn"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 22f, Note = "Q Reaping Slash: dash then slash around himself, both deal damage" },
				new SpellScript(SpellShape.Beam) { Flags = 1, SlowSec = 1.5f, Note = "W Blade's Reach: delayed line slash (700 x 175) damaging and slowing; cast delay missing" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Shadow Step: toggle, walk through terrain with move speed; terrain pass missing" },
				new SpellScript(SpellShape.Dash) { DashTiles = 34f, DmgMult = 1.5f, Note = "R Umbral Trespass: hide inside enemy champion (untargetable), burst out for massive damage; untargetable/ride missing" },
			};
			t["Kennen"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Note = "Q Thundering Shuriken: fast shuriken stops at first enemy, damage + Mark of the Storm (3 marks stun, missing)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, DmgMult = 0.8f, WidthMult = 3f, Note = "W Electrical Surge: damages enemies already marked within range; mark gating missing" },
				new SpellScript(SpellShape.SelfBurst) { SpeedBuff = true, DurationSec = 2f, HitIntervalSec = 1f, Note = "E Lightning Rush: lightning form 2s, pass through units, damages/marks nearby, move speed (attack speed on exit missing)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 3f, HitIntervalSec = 0.5f, DmgMult = 0.5f, WidthMult = 2f, Note = "R Slicing Maelstrom: storm strikes nearby enemy champions repeatedly; mark stun missing" },
			};
			t["Khazix"] = new[] {
				new SpellScript(SpellShape.Cone) { Note = "Q Taste Their Fear: melee strike on target (range 325), bonus vs isolated (missing)" },
				new SpellScript(SpellShape.Skillshot) { Count = 3, SpreadDeg = 40f, WidthMult = 3f, Flags = 1, SlowSec = 2.25f, Note = "W Void Spike: exploding spike(s) (3 in cone when evolved), heals Kha'Zix if in blast, slow + reveal when evolved; heal/reveal missing" },
				new SpellScript(SpellShape.Dash) { DashTiles = 44f, Note = "E Leap: leap to area (700), damage on landing (r300)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, InvisibleSec = 1.25f, Note = "R Void Assault: invisibility (duration UNVERIFIED) and move speed; evolve system missing" },
			};
			t["Kindred"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 21f, Note = "Q Dance of Arrows: tumble 340 and shoot up to 3 arrows at nearby targets; arrows missing" },
				new SpellScript(SpellShape.Zone) { DurationSec = 8f, HitIntervalSec = 0.7f, DmgMult = 0.4f, WidthMult = 4f, Note = "W Wolf's Frenzy: wolf zone (r800) for 8s, Wolf attacks enemies inside; Lamb heal passive missing" },
				new SpellScript(SpellShape.Homing) { Flags = 1, SlowSec = 1.5f, Note = "E Mounting Dread: shot slows 1.5s; third attack makes Wolf pounce for massive damage (missing)" },
				new SpellScript(SpellShape.Buff) { Shield = true, HealFrac = 0.3f, Note = "R Lamb's Respite: zone r1200, nothing inside can die, heal at end; modelled as shield + heal on caster" },
			};
			t["Kled"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Knockback = -14f, Note = "Q Bear Trap on a Rope: trap damages and hooks, if still shackled target takes extra damage and is yanked to Kled" },
				new SpellScript(SpellShape.Cone) { DmgMult = 1.2f, Note = "W Violent Tendencies: huge attack speed for 4 attacks, 4th deals more damage; modelled as cone slashes" },
				new SpellScript(SpellShape.Dash) { DashTiles = 34f, SpeedBuff = true, Note = "E Jousting: dash 550 damaging, short speed burst; recast dash back missing" },
				new SpellScript(SpellShape.Dash) { DashTiles = 70f, SpeedBuff = true, Shield = true, Note = "R Chaaaaaaaarge!!!: charge to far location (range 4500) leaving speed trail, shield, Skaarl rams first champion; trail/ram missing, distance shortened" },
			};
			t["KogMaw"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Note = "Q Caustic Spittle: missile stops on first enemy, magic damage + armor/MR shred, bonus attack speed; shred missing" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 8f, HitIntervalSec = 0.4f, DmgMult = 0.5f, Note = "W Bio-Arcane Barrage: 8s of extra attack range + % max health on-hit; modelled as aura" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 1, SlowSec = 2f, Note = "E Void Ooze: ooze pierces enemies, leaves slowing trail; trail missing (slow applied on hit)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.8f, DurationSec = 0.2f, Note = "R Living Artillery: long-range shell (1200, r240), bonus damage on low health, reveals; low-hp bonus missing" },
			};
			t["Leblanc"] = new[] {
				new SpellScript(SpellShape.Homing) { Note = "Q Sigil of Malice: targeted bolt, marks 3.5s, ability hit detonates for extra damage; mark/detonate and refund missing" },
				new SpellScript(SpellShape.Dash) { DashTiles = 37f, Note = "W Distortion: dash 600 damaging near destination; recast returns to start for 4s (missing)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "E Ethereal Chains: chain shackles first enemy, roots after 1.5s with extra damage; delayed root approximated as instant" },
				new SpellScript(SpellShape.Dash) { DashTiles = 37f, DmgMult = 1.5f, Note = "R Mimic: casts a mimicked copy of last basic spell; approximated as stronger Distortion" },
			};
			t["LeeSin"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Note = "Q Sonic Wave: skillshot hits first enemy; recast Resonating Strike dashes to it with missing-health damage; recast missing" },
				new SpellScript(SpellShape.Dash) { DashTiles = 44f, Shield = true, Note = "W Safeguard: rush to target ally (700), shield; Iron Will omnivamp recast missing, ally targeting missing" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Flags = 1, WidthMult = 3f, Note = "E Tempest: shockwave (r450) damages and reveals, Cripple recast slows (cc 4s, decaying)" },
				new SpellScript(SpellShape.Cone) { Knockback = 18f, Note = "R Dragon's Rage: roundhouse kick launches target back, collisions deal damage and knock enemies up; collision missing" },
			};
			t["Leona"] = new[] {
				new SpellScript(SpellShape.Cone) { Flags = 2, Note = "Q Shield of Daybreak: empowered next attack, bonus magic damage and 1s stun" },
				new SpellScript(SpellShape.SelfBurst) { Shield = true, DurationSec = 3f, HitIntervalSec = 3f, Note = "W Eclipse: damage reduction/armor/MR, explosion (r500) when duration ends, prolongs; explosion fires at start instead of end" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "E Zenith Blade: sword image damages all in line (875), last champion hit is immobilized 0.5s and Leona dashes to them; dash missing" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.6f, DurationSec = 0.2f, Flags = 1 | 2, WidthMult = 2f, Note = "R Solar Flare: beam of solar energy (range 1200), center stuns, edge slows; delay UNVERIFIED, center/edge split missing" },
			};
			t["Lillia"] = new[] {
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, WidthMult = 3f, Note = "Q Blooming Blows: spin damaging nearby enemies, edge deals extra true damage; edge bonus and passive move speed missing" },
				new SpellScript(SpellShape.Zone) { DelaySec = 1f, DurationSec = 0.3f, WidthMult = 1.5f, Note = "W Watch Out! Eep!: delayed blast nearby (range 500), heavy damage at the center; center bonus missing" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, SlowSec = 3f, Note = "E Swirlseed: seed damages and slows (3s) where it lands, keeps rolling to a wall/target if it misses; roll missing" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2.5f, HitIntervalSec = 2.5f, Flags = 4, WidthMult = 3.5f, Note = "R Lilting Lullaby: enemies with Dream Dust (range 1600) get drowsy then sleep 3s, extra damage on waking; dust gating/delay missing" },
			};
			t["Lissandra"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 1, SlowSec = 1.5f, Note = "Q Ice Shard: spear shatters on first hit, shards pass through for same damage to others, slow" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Flags = 2, Note = "W Ring of Frost: freezes nearby enemies, damage + root 1.45s" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "E Glacial Path: ice claw flies out (1050) damaging enemies in path; recast teleports to the claw (missing)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 1.5f, HitIntervalSec = 1.5f, Flags = 1 | 2, Shield = true, HealFrac = 0.15f, WidthMult = 3f, Note = "R Frozen Tomb: stun enemy target 3s, or self-cast: heal (amount UNVERIFIED), untargetable; dark ice (r690) damages and slows; target/self split missing" },
			};
			t["Locke"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Count = 3, SpreadDeg = 24f, Pierce = true, Note = "Q Ritual Nails: set of Soul Nails thrown (count/spread UNVERIFIED), damage and mark enemies hit; marks consumed by attacks (missing)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, HealFrac = 0.1f, Note = "W Soul Ignition: attack + move speed, Locke damages himself, heals back a portion at end; self-damage missing" },
				new SpellScript(SpellShape.Dash) { DashTiles = 27f, Note = "E Ashen Pursuit: teleports to location then dashes to attack next target, damaging enemies passed through; teleport split missing" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 0.2f, Flags = 2, WidthMult = 1.5f, Note = "R Purgatory: binding artifact thrown (range 1000, r425), damage + execute threshold; execute and seal bonus missing" },
			};
			t["Lucian"] = new[] {
				new SpellScript(SpellShape.Beam) { RangeMult = 2.2f, Note = "Q Piercing Light: bolt through a target, line extends past it to ~1100 (UNVERIFIED); cast delay missing" },
				new SpellScript(SpellShape.Skillshot) { WidthMult = 2f, Note = "W Ardent Blaze: missile explodes in a star on first enemy, marks and reveals; move speed when attacking marked (missing)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 28f, Note = "E Relentless Pursuit: quick dash 445 toward cursor; cooldown reduction on attacks missing" },
				new SpellScript(SpellShape.Skillshot) { Count = 6, SpreadDeg = 4f, Pierce = true, DmgMult = 0.5f, Note = "R The Culling: 3s channel torrent of piercing shots (range 1400), steerable; modelled as one volley" },
			};
			t["Lulu"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Count = 2, SpreadDeg = 10f, Pierce = true, Flags = 1, SlowSec = 2f, Note = "Q Glitterlance: Pix and Lulu each fire a bolt, damage and heavy slow" },
				new SpellScript(SpellShape.Homing) { Flags = 4, SpeedBuff = true, Note = "W Whimsy: polymorph enemy (cannot attack/cast), or ally gets attack + move speed; ally targeting and polymorph duration UNVERIFIED" },
				new SpellScript(SpellShape.Homing) { Shield = true, Note = "E Help, Pix!: shield an ally or damage an enemy and grant vision; ally targeting missing (shield goes to caster)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, HitIntervalSec = 4f, HealFrac = 0.2f, Flags = 1 | 2, DmgMult = 0.1f, Note = "R Wild Growth: enlarge ally, knock enemies up, bonus health, slowing aura for a few seconds; aura and ally targeting missing" },
			};
			t["Lux"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "Q Light Binding: sphere roots and damages up to two enemies; pierce limit of 2 missing" },
				new SpellScript(SpellShape.Boomerang) { Shield = true, Note = "W Prismatic Barrier: wand thrown out and back, shields allies it touches; ally shield missing" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.9f, DurationSec = 5f, HitIntervalSec = 5f, Flags = 1, Note = "E Lucent Singularity: orb flies to area, slows nearby enemies, detonates on recast or after 5s; detonate timing approximated" },
				new SpellScript(SpellShape.Beam) { Note = "R Final Spark: after 1.375s channel, huge beam (range 3340, width 190); delay missing" },
			};
		}
	}
}
