using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch6
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Olaf"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Flags = 1, Note = "Q Undertow: thrown axe, slows, lands at target" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.1f, SpeedBuff = true, Note = "W Berserker Rage: attack speed and lifesteal" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.1f, DurationSec = 0.3f, Note = "E Reckless Swing: true damage strike" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "R Ragnarok: CC immunity and bonus resistances" },
			};
			t["Orianna"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "Q Command: Attack, ball thrown to target, damages along the line" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.3f, SpeedBuff = true, Note = "W Command: Dissonance, field around ball, speeds allies" },
				new SpellScript(SpellShape.Homing) { Shield = true, Note = "E Command: Protect, ball flies to ally with shield" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 0.2f, Flags = 2, Note = "R Command: Shockwave, shockwave pulls enemies in" },
			};
			t["Ornn"] = new[] {
				new SpellScript(SpellShape.Beam) { Flags = 1, Note = "Q Volcanic Rupture: line of rock, slows, leaves pillar" },
				new SpellScript(SpellShape.Cone) { Note = "W Bellows Breath: cone of flame" },
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, Flags = 2, Note = "E Searing Charge: charge, knock up on wall hit" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "R Call of the Forge God: wave of fire, slows then knocks up" },
			};
			t["Pantheon"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "Q Spear Shot: spear, tap or charged" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Flags = 2, Note = "W Shield Vault: leap to target and stun" },
				new SpellScript(SpellShape.Cone) { DurationSec = 0.3f, Note = "E Aegis Assault: blocks damage, strikes in cone" },
				new SpellScript(SpellShape.Zone) { DelaySec = 1f, DurationSec = 0.3f, Note = "R Grand Starfall: leaps to far area and lands" },
			};
			t["Poppy"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "Q Hammer Shock: hammer slam line, delayed" },
				new SpellScript(SpellShape.Buff) { Shield = true, SpeedBuff = true, Note = "W Steadfast Presence: speed, blocks dashes" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Flags = 2, Note = "E Heroic Charge: dash and pin enemy to wall" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, DmgMult = 1.2f, Flags = 2, Note = "R Keeper's Verdict: charged hammer, knock up" },
			};
			t["Pyke"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Note = "Q Bone Skewer: hook pulls enemy, or stab" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Ghostwater Dive: stealth and speed" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Flags = 2, Note = "E Phantom Undertow: dash back, stun on return" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 0.2f, DmgMult = 1.3f, Note = "R Death From Below: execute X mark" },
			};
			t["Qiyana"] = new[] {
				new SpellScript(SpellShape.Cone) { Flags = 1, Note = "Q Edge of Ixtal: cone strike, element empowered" },
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, SpeedBuff = true, Note = "W Terrashape: dash to terrain, empower weapon" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "E Audacity: dash through target" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "R Supreme Display of Talent: shockwave stuns near terrain" },
			};
			t["Quinn"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 4, Note = "Q Blinding Assault: bird blinds and damages" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Heightened Senses: attack speed, reveals" },
				new SpellScript(SpellShape.Dash) { DashTiles = 7f, Flags = 1, Note = "E Vault: leap, knock back and slow" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "R Behind Enemy Lines: speed, then Skystrike" },
			};
			t["Rakan"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Note = "Q Gleaming Quill: feather bolt, heals allies" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Flags = 2, Note = "W Grand Entrance: dash and knock up" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Shield = true, Note = "E Battle Dance: leap to ally, shield" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Flags = 4, SpeedBuff = true, Note = "R The Quickness: sprint, charm enemies touched" },
			};
			t["Rammus"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, Flags = 2, Note = "Q Powerball: roll, knock up on impact" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "W Defensive Ball Curl: armor, reflects damage" },
				new SpellScript(SpellShape.Homing) { Flags = 4, Note = "E Puncturing Taunt: targeted taunt" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 4f, HitIntervalSec = 0.5f, Flags = 4, Note = "R Soaring Slam: area of taunting tremors around Rammus" },
			};
			t["RekSai"] = new[] {
				new SpellScript(SpellShape.Cone) { Note = "Q Queen's Wrath: three-hit melee, burrowed: Prey Seeker line" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.3f, Flags = 2, Note = "W Burrow: knock up on unburrow" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "E Furious Bite: bite, tunnel dash when burrowed" },
				new SpellScript(SpellShape.Dash) { DashTiles = 20f, DmgMult = 1.3f, Note = "R Void Rush: tunnel to marked target, true damage" },
			};
			t["Rell"] = new[] {
				new SpellScript(SpellShape.Cone) { Flags = 1, Note = "Q Shattering Strike: lance thrust, shred" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Flags = 2, Note = "W Ferromancy: Crash Down, mounted knock up" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Flags = 2, Note = "E Attract and Repel: pulls enemies together then repels" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, Flags = 2, Note = "R Magnet Storm: pulls enemies in around her" },
			};
			t["Renata"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "Q Hand Shake: grabs first enemy, root and pull" },
				new SpellScript(SpellShape.Homing) { Shield = true, Note = "W Bailout: ally revive, speed" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 1, Note = "E Loyalty Program: bolt shields allies, damages enemies" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 0.2f, Flags = 2, Note = "R Hostile Takeover: wave, crazed enemies" },
			};
			t["Renekton"] = new[] {
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.2f, HealFrac = 0.05f, Note = "Q Cull the Meek: sweep around, heals" },
				new SpellScript(SpellShape.Buff) { Flags = 2, Note = "W Ruthless Predator: empowered stunning strike" },
				new SpellScript(SpellShape.Dash) { DashTiles = 7f, Note = "E Slice and Dice: dash through enemies" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 4f, HitIntervalSec = 0.5f, HealFrac = 0.2f, Note = "R Dominus: transforms, damaging aura" },
			};
			t["Rengar"] = new[] {
				new SpellScript(SpellShape.Cone) { Note = "Q Savagery: empowered strike" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.2f, HealFrac = 0.1f, Flags = 1, Note = "W Battle Roar: AoE damage and heal" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "E Bola Strike: bola, root when empowered" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "R Thrill of the Hunt: stealth and speed" },
			};
			t["Riven"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Flags = 2, Note = "Q Broken Wings: three dashes, last knocks up" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.2f, Flags = 2, Note = "W Ki Burst: stuns adjacent enemies" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Shield = true, Note = "E Valor: dash with shield" },
				new SpellScript(SpellShape.Cone) { DmgMult = 1.3f, Count = 3, SpreadDeg = 50f, Pierce = true, Note = "R Wind Slash: fan of energy" },
			};
			t["Rumble"] = new[] {
				new SpellScript(SpellShape.Cone) { DurationSec = 3f, Note = "Q Flamespitter: cone of flame" },
				new SpellScript(SpellShape.Buff) { Shield = true, SpeedBuff = true, Note = "W Scrap Shield: shield and speed" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Note = "E Electro Harpoon: harpoon, slows" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 5f, HitIntervalSec = 0.5f, Flags = 1, Note = "R The Equalizer: line of rockets, burning zone" },
			};
			t["Ryze"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Note = "Q Overload: spell flux bolt" },
				new SpellScript(SpellShape.Homing) { Flags = 2, Note = "W Rune Prison: rooting bolt" },
				new SpellScript(SpellShape.Homing) { Note = "E Spell Flux: bouncing bolt that spreads flux" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "R Realm Warp: portal teleport, plus speed" },
			};
			t["Samira"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Note = "Q Flair: gun shot, or sword slash when close" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 1f, HitIntervalSec = 0.25f, Note = "W Blade Whirl: spinning blades block missiles" },
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, Note = "E Wild Rush: dash through enemies" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2f, HitIntervalSec = 0.2f, Note = "R Inferno Trigger: bullets at all nearby enemies" },
			};
		}
	}
}
