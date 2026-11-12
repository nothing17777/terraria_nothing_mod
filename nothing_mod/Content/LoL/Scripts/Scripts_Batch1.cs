using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch1
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Aatrox"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 4f, Flags = 2, Note = "Q Darkin Blade: lunging sweep that knocks up" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Note = "W Infernal Chains: slow skillshot that pulls" },
				new SpellScript(SpellShape.Dash) { DashTiles = 7f, DmgMult = 0.2f, Note = "E Umbral Dash: long dash" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2f, Flags = 4, SpeedBuff = true, Note = "R World Ender: fear aura + move speed" },
			};
			t["Akali"] = new[] {
				new SpellScript(SpellShape.Cone) { Note = "Q Five Point Strike: kunai fan" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Twilight Shroud: stealth zone, approximated as speed buff" },
				new SpellScript(SpellShape.Boomerang) { Note = "E Shuriken Flip: shuriken out and back" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "R Perfect Execution: dash through enemies" },
			};
			t["Akshan"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Note = "Q Avengerang: boomerang" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Going Rogue: stealth + speed, approximated as speed buff" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, DmgMult = 0.5f, Note = "E Heroic Swing: grapple swing" },
				new SpellScript(SpellShape.Beam) { Note = "R Comeuppance: long-range line shots" },
			};
			t["Alistar"] = new[] {
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Flags = 2, Note = "Q Pulverize: knock-up burst" },
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Flags = 2, Note = "W Headbutt: dash and knock back" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 4f, HitIntervalSec = 0.5f, Note = "E Trample: stomping aura" },
				new SpellScript(SpellShape.Buff) { Shield = true, HealFrac = 0.1f, Note = "R Unbreakable Will: damage reduction" },
			};
			t["Ambessa"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Note = "Q Cunning Sweep: dashing sweeps" },
				new SpellScript(SpellShape.Cone) { Note = "W Repudiation: wide cleave" },
				new SpellScript(SpellShape.Cone) { Flags = 1, Note = "E Lunge: strike with slow" },
				new SpellScript(SpellShape.Dash) { DashTiles = 9f, Flags = 2, Note = "R Public Execution: leap and slam" },
			};
			t["Amumu"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "Q Bandage Toss: stun skillshot + pull" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 4f, HitIntervalSec = 0.5f, Note = "W Despair: toggle aura" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Note = "E Tantrum: area burst" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, Flags = 2, Note = "R Curse of the Sad Mummy: area stun" },
			};
			t["Anivia"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "Q Flash Frost: piercing orb, stun on burst" },
				new SpellScript(SpellShape.Zone) { DurationSec = 5f, Flags = 1, Note = "W Crystallize: ice wall at cursor" },
				new SpellScript(SpellShape.Homing) { Note = "E Frostbite: targeted nuke" },
				new SpellScript(SpellShape.Zone) { DurationSec = 4f, HitIntervalSec = 0.4f, Flags = 1, Note = "R Glacial Storm: lingering slow field" },
			};
			t["Aphelios"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Note = "Q: weapon-dependent ability, generic skillshot" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Phase: weapon swap, approximated as speed buff" },
				new SpellScript(SpellShape.Orbit) { Count = 3, DurationSec = 3f, Note = "E: weapon-dependent, orbiting projectiles" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.6f, DurationSec = 0.4f, Flags = 2, Note = "R Moonlight Vigil: delayed area" },
			};
			t["Ashe"] = new[] {
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "Q Ranger's Focus: attack-speed stance, approximated as speed buff" },
				new SpellScript(SpellShape.Skillshot) { Count = 7, SpreadDeg = 40f, Flags = 1, DmgMult = 0.4f, Note = "W Volley: cone of arrows, slow" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, DmgMult = 0.1f, Note = "E Hawkshot: scouting hawk" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "R Enchanted Crystal Arrow: global stun arrow" },
			};
			t["AurelionSol"] = new[] {
				new SpellScript(SpellShape.Beam) { Note = "Q Breath of Light: channeled beam" },
				new SpellScript(SpellShape.Dash) { DashTiles = 12f, DmgMult = 0.1f, Note = "W Astral Flight: long flight" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 4f, HitIntervalSec = 0.5f, Note = "E Singularity: lingering black hole" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 0.4f, Flags = 2, Note = "R Falling Star: delayed shockwave + stun" },
			};
			t["Aurora"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Note = "Q Twofold Hex: bolt out and back" },
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, DmgMult = 0.1f, Note = "W Across the Veil: blink" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "E The Weirding: root skillshot" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.6f, Flags = 2, Note = "R Between Worlds: pulls enemies in and stuns" },
			};
			t["Azir"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Note = "Q Conquering Sands: soldiers dash out and back" },
				new SpellScript(SpellShape.Orbit) { Count = 2, DurationSec = 5f, Note = "W Arise!: soldiers" },
				new SpellScript(SpellShape.Dash) { DashTiles = 9f, Note = "E Shifting Sands: dash to soldier" },
				new SpellScript(SpellShape.Cone) { Flags = 2, WidthMult = 2f, Note = "R Emperor's Divide: pushing soldier wall" },
			};
			t["Bard"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "Q Cosmic Binding: piercing stun" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.1f, SpeedBuff = true, Note = "W Caretaker's Shrine: healing shrine, approximated" },
				new SpellScript(SpellShape.Dash) { DashTiles = 9f, DmgMult = 0.1f, Note = "E Magical Journey: portal" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 2.5f, Flags = 2, Note = "R Tempered Fate: stasis area" },
			};
			t["Belveth"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Note = "Q Void Surge: dash" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 0.3f, Flags = 2, Note = "W Above and Below: delayed knock-up" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 1.5f, HitIntervalSec = 0.25f, Note = "E Royal Maelstrom: channeled spin" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, HealFrac = 0.15f, Note = "R Endless Banquet: empowered form" },
			};
			t["Blitzcrank"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "Q Rocket Grab: grab skillshot" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Overdrive: speed boost" },
				new SpellScript(SpellShape.Cone) { Flags = 2, DmgMult = 0.5f, Note = "E Power Fist: knock-up punch" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, Flags = 4, Note = "R Static Field: area blast + silence" },
			};
			t["Brand"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "Q Sear: fireball, stun if ablaze" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.8f, DurationSec = 0.4f, Note = "W Pillar of Flame: delayed fire pillar" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Note = "E Conflagration: area burst" },
				new SpellScript(SpellShape.Homing) { Count = 3, SpreadDeg = 30f, DmgMult = 0.5f, Note = "R Pyroclasm: bouncing fireballs" },
			};
			t["Braum"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Note = "Q Winter's Bite: slowing skillshot" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Shield = true, DmgMult = 0.1f, Note = "W Stand Behind Me: leap to ally" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "E Unbreakable: shield facing a direction" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, WidthMult = 1.5f, Note = "R Glacial Fissure: knock-up line" },
			};
			t["Briar"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, Flags = 2, Note = "Q Head Rush: leap and stun" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Blood Frenzy: berserk, approximated as speed buff" },
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Flags = 2, Note = "E Chilling Scream: charge/wall slam" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 4, Note = "R Certain Death: fear skillshot" },
			};
			t["Caitlyn"] = new[] {
				new SpellScript(SpellShape.Beam) { Note = "Q Piltover Peacemaker: piercing line shot" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.8f, DurationSec = 4f, Flags = 2, Note = "W Yordle Snap Trap: trap area" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Note = "E 90 Caliber Net: slowing shot" },
				new SpellScript(SpellShape.Homing) { Note = "R Ace in the Hole: homing long-range shot" },
			};
		}
	}
}
