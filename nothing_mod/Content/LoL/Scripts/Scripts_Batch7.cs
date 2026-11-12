using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch7
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Sejuani"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 20f, Flags = 2, Note = "Q Arctic Assault: dash through enemies, knocking up" },
				new SpellScript(SpellShape.Cone) { Flags = 1, Note = "W Flail of the Northern Winds: sweeping flail, then smash" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, Flags = 1, Note = "E Permafrost: frost burst around caster (real: stuns frosted targets)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = false, Flags = 2, WidthMult = 1.6f, Note = "R Glacial Prison: thrown bola that stuns and freezes an area" },
			};
			t["Senna"] = new[] {
				new SpellScript(SpellShape.Beam) { Flags = 1, Note = "Q Piercing Darkness: piercing line shot (real: also heals allies)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "W Last Embrace: slow missile that roots the first target hit" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Curse of the Black Mist: camouflage in mist, approximated as speed buff" },
				new SpellScript(SpellShape.Beam) { WidthMult = 1.2f, Note = "R Dawning Shadow: global piercing beam, shields allies" },
			};
			t["Seraphine"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.4f, DurationSec = 0.3f, Note = "Q High Note: area burst at cursor" },
				new SpellScript(SpellShape.Buff) { Shield = true, HealFrac = 0.1f, Note = "W Surround Sound: shield and heal for self/allies" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 1 | 2, Note = "E Beat Drop: piercing sound wave, slows then roots" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 4, WidthMult = 2f, Note = "R Encore: piercing wave that charms, extends through enemies" },
			};
			t["Sett"] = new[] {
				new SpellScript(SpellShape.Cone) { DmgMult = 0.8f, Note = "Q Knuckle Down: empowered two-hit punches (percent max HP)" },
				new SpellScript(SpellShape.Cone) { Shield = true, DmgMult = 1.3f, Note = "W Haymaker: spends grit for shield, then wide cone punch" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.3f, Flags = 2, Note = "E Facebreaker: pulls enemies from both sides and stuns" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Flags = 2, Note = "R The Show Stopper: grab enemy, slam at target location" },
			};
			t["Shaco"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, Note = "Q Deceive: teleport and become stealthed" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 4f, HitIntervalSec = 0.8f, Flags = 4, Note = "W Jack in the Box: trap that fears nearby enemies" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Note = "E Two-Shiv Poison: targeted shiv with slow, execute bonus" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 5f, HitIntervalSec = 1f, Note = "R Hallucinate: summons clone that explodes on death" },
			};
			t["Shen"] = new[] {
				new SpellScript(SpellShape.Cone) { Flags = 1, Note = "Q Twilight Assault: empowered blade strikes (percent HP damage)" },
				new SpellScript(SpellShape.Buff) { Shield = true, SpeedBuff = true, Note = "W Spirit's Refuge: zone that blocks incoming auto attacks" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Flags = 4, Note = "E Shadow Dash: dash that taunts enemies" },
				new SpellScript(SpellShape.Buff) { Shield = true, HealFrac = 0.15f, Note = "R Stand United: global shield on an ally and teleport" },
			};
			t["Shyvana"] = new[] {
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Note = "Q Twin Bite: two empowered auto attacks" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 3f, HitIntervalSec = 0.5f, SpeedBuff = true, Note = "W Burnout: flame aura around caster plus speed" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "E Flame Breath: fireball that leaves burning ground" },
				new SpellScript(SpellShape.Dash) { DashTiles = 15f, Flags = 2, Note = "R Dragon's Descent: dash forward, knock back, become dragon" },
			};
			t["Singed"] = new[] {
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 4f, HitIntervalSec = 0.4f, Note = "Q Poison Trail: toggle leaving poison behind (aura around caster)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 3f, Flags = 1, Note = "W Mega Adhesive: slowing goo patch at cursor" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Flags = 2, Note = "E Fling: tosses nearby enemy behind (approximated as burst)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, HealFrac = 0.15f, Note = "R Insanity Potion: stat boost for several seconds" },
			};
			t["Sion"] = new[] {
				new SpellScript(SpellShape.Cone) { Flags = 2, WidthMult = 1.3f, Note = "Q Decimating Smash: channeled swing, knock-up scaling with charge" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "W Soul Furnace: shield that detonates for damage (burst folded into generic hit)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2 | 1, Note = "E Roar of the Slayer: shockwave that slows and knocks back, splitting" },
				new SpellScript(SpellShape.Dash) { DashTiles = 24f, Flags = 2, Note = "R Unstoppable Onslaught: long charging dash, knock-up on impact" },
			};
			t["Sivir"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Pierce = true, Note = "Q Boomerang Blade: out-and-back blade" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Ricochet: bouncing auto attacks (approximated as buff)" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "E Spell Shield: blocks the next spell" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "R On the Hunt: team movement speed boost" },
			};
			t["Skarner"] = new[] {
				new SpellScript(SpellShape.Cone) { Flags = 1, Note = "Q Crystal Slash: three-stage tail slash, third throws crystal shard" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Shield = true, Flags = 1, Note = "W Seismic Bastion: shield plus shockwave slowing nearby enemies" },
				new SpellScript(SpellShape.Dash) { DashTiles = 20f, Pierce = true, Flags = 2, Note = "E Ixtal's Impact: charge, pins first champion hit to terrain" },
				new SpellScript(SpellShape.Cone) { Flags = 2 | 4, WidthMult = 0.6f, Note = "R Impale: suppress target and drag them with him" },
			};
			t["Smolder"] = new[] {
				new SpellScript(SpellShape.Homing) { Note = "Q Super Scorcher Breath: targeted fire breath, splash on kill" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Count = 3, SpreadDeg = 24f, Flags = 1, Note = "W Achooooo!: fan of slowing explosive snot" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, SpeedBuff = true, Note = "E Flap, Flap, Flap: flies up, gaining speed and range (approx as dash)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.75f, DurationSec = 0.4f, Flags = 2 | 1, Note = "R MMOOOMMMM!: huge-range area slam on target location" },
			};
			t["Sona"] = new[] {
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.3f, Note = "Q Hymn of Valor: burst of damage to two nearest enemies, aura" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.15f, Shield = true, Note = "W Aria of Perseverance: heal and shield nearby allies" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Song of Celerity: movement speed aura" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, WidthMult = 1.5f, Note = "R Crescendo: wave that forces enemies to dance (stun)" },
			};
			t["Soraka"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.4f, DurationSec = 0.3f, Flags = 1, Note = "Q Starcall: star falls at cursor, slows" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.2f, Note = "W Astral Infusion: heals ally at cost of self health" },
				new SpellScript(SpellShape.Zone) { DelaySec = 1.5f, DurationSec = 1.5f, Flags = 4 | 2, Note = "E Equinox: silence zone, roots after delay" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.4f, Note = "R Wish: global heal on all allies" },
			};
			t["Swain"] = new[] {
				new SpellScript(SpellShape.Cone) { Count = 3, SpreadDeg = 20f, Note = "Q Death's Hand: cone of ravens that chain" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.9f, DurationSec = 0.3f, Flags = 2 | 1, Note = "W Vision of Empire: delayed area, slows and reveals" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "E Nevermove: tendrils pull enemy toward him, then root" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 8f, HitIntervalSec = 1f, HealFrac = 0.1f, Note = "R Demonic Ascension: demon form draining nearby enemies" },
			};
			t["Sylas"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.6f, DurationSec = 0.3f, Flags = 1, Note = "Q Chain Lash: chains lash out then explode, slow" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, HealFrac = 0.1f, Note = "W Kingslayer: dash to target, heal based on missing health" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Flags = 2, Note = "E Abscond / Abduct: dash then pull enemy with a chain" },
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, DmgMult = 1.5f, Note = "R Hijack: dash to champion and steal their ultimate (stolen ult not implemented)" },
			};
			t["Syndra"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.4f, DurationSec = 0.2f, Note = "Q Dark Sphere: orb dropped at cursor" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, DmgMult = 1.1f, Note = "W Force of Will: throws object, slows" },
				new SpellScript(SpellShape.Cone) { Flags = 2, WidthMult = 1.4f, Note = "E Scatter the Weak: wide knockback cone, stuns" },
				new SpellScript(SpellShape.Orbit) { Count = 5, DurationSec = 2f, HitIntervalSec = 0.4f, Note = "R Unleashed Power: spheres orbiting then fired at target" },
			};
			t["TahmKench"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = false, Flags = 1, Note = "Q Tongue Lash: tongue skillshot, slows" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 0.3f, Flags = 2, Note = "W Abyssal Dive: delayed ground slam, knock-up" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "E Thick Skin: gray health converts to shield (approximated)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 30f, Note = "R Devour / Abyssal Voyage: long-distance travel (approximated as dash)" },
			};
			t["Taliyah"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Count = 5, SpreadDeg = 8f, Note = "Q Threaded Volley: volley of rocks" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.7f, DurationSec = 0.3f, Flags = 2, Note = "W Seismic Shove: delayed area, knock-up or knockback" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 4f, HitIntervalSec = 0.5f, Flags = 1, Note = "E Unraveled Earth: mines scattered in cone (approx as lingering area)" },
				new SpellScript(SpellShape.Beam) { WidthMult = 1.3f, Flags = 1, Note = "R Weaver's Wall: long wall of earth (approx as beam)" },
			};
		}
	}
}
