using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch2
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Camille"] = new[] {
				new SpellScript(SpellShape.Cone) { Note = "Q Precision Protocol: empowered blade strike in front" },
				new SpellScript(SpellShape.Cone) { Flags = 1, Note = "W Tactical Sweep: wide cone, outer edge slows" },
				new SpellScript(SpellShape.Dash) { DashTiles = 20f, Flags = 2, Note = "E Hookshot/Wall Dive: grapple and dash, stun on arrival" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 4f, Flags = 1, DmgMult = 0.5f, Note = "R Hextech Ultimatum: arena around the caster that traps enemies" },
			};
			t["Cassiopeia"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.4f, DurationSec = 0.5f, Note = "Q Noxious Blast: delayed poison burst at target area" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 4f, Flags = 1, DmgMult = 1.5f, Note = "W Miasma: lingering poison cloud, slows" },
				new SpellScript(SpellShape.Homing) { Note = "E Twin Fang: targeted bolt" },
				new SpellScript(SpellShape.Cone) { Flags = 2, Note = "R Petrifying Gaze: wide cone, stuns those facing her" },
			};
			t["Chogath"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.6f, DurationSec = 0.5f, Flags = 2, Note = "Q Rupture: delayed knock-up at target area" },
				new SpellScript(SpellShape.Cone) { Flags = 4, Note = "W Feral Scream: cone damage and silence" },
				new SpellScript(SpellShape.Skillshot) { Count = 5, SpreadDeg = 40f, Flags = 1, DmgMult = 0.4f, Note = "E Vorpal Spikes: fan of spikes (approximation of on-hit spike cone)" },
				new SpellScript(SpellShape.Homing) { DmgMult = 2f, Note = "R Feast: targeted true damage, grows Cho'Gath (growth not implemented)" },
			};
			t["Corki"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.6f, DurationSec = 0.3f, Note = "Q Phosphorus Bomb: lobbed bomb with delay at target area" },
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, DmgMult = 0.6f, Note = "W Valkyrie: dash dropping a fire trail (trail approximated by dash damage)" },
				new SpellScript(SpellShape.Cone) { Note = "E Gatling Gun: channelled cone of bullets" },
				new SpellScript(SpellShape.Skillshot) { DmgMult = 0.8f, Note = "R Missile Barrage: explosive missile, big ones every third cast" },
			};
			t["Darius"] = new[] {
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, DmgMult = 1.2f, Note = "Q Decimate: axe sweep around the caster, blade edge hits harder" },
				new SpellScript(SpellShape.Cone) { Flags = 1, Note = "W Crippling Strike: empowered strike that slows" },
				new SpellScript(SpellShape.Cone) { Flags = 2, DmgMult = 0.5f, Note = "E Apprehend: cone that pulls enemies in" },
				new SpellScript(SpellShape.Homing) { DmgMult = 1.5f, Note = "R Noxian Guillotine: targeted execute, damage scales with bleed" },
			};
			t["Diana"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, WidthMult = 1.3f, Note = "Q Crescent Strike: arc-shaped missile, marks enemies" },
				new SpellScript(SpellShape.Orbit) { Count = 3, DurationSec = 5f, Shield = true, Note = "W Pale Cascade: shield and three orbiting orbs" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.25f, DurationSec = 0.4f, Flags = 1, Note = "E Lunar Rush: pull enemies in around target (teleport not implemented)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, Flags = 2, Note = "R Moonfall: dash to target, then pull in nearby enemies" },
			};
			t["DrMundo"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Note = "Q Infected Bonesaw: thrown cleaver, damage as percent of current health" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 4f, HitIntervalSec = 0.5f, DmgMult = 0.4f, Note = "W Heart Zapper: damaging aura around caster" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Blunt Force Trauma: empowered attack and bonus speed (approximated as speed buff)" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.4f, SpeedBuff = true, Note = "R Maximum Dosage: heals a percent of health and speeds up" },
			};
			t["Draven"] = new[] {
				new SpellScript(SpellShape.Boomerang) { DmgMult = 1.2f, Note = "Q Spinning Axe: empowered attack that bounces back (axe catching not implemented)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Blood Rush: attack speed and move speed burst" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "E Stand Aside: piercing axes that knock aside" },
				new SpellScript(SpellShape.Boomerang) { Pierce = true, Note = "R Whirling Death: global missile that returns" },
			};
			t["Ekko"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Flags = 1, Note = "Q Timewinder: out-and-back time device, slows" },
				new SpellScript(SpellShape.Zone) { DelaySec = 1.5f, DurationSec = 1.5f, Flags = 2, DmgMult = 0.3f, Note = "W Parallel Convergence: delayed bubble that slows then stuns" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "E Phase Dive: short blink with strike" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.6f, DmgMult = 1.2f, Note = "R Chronobreak: rewinds position, damages enemies around (rewind not implemented)" },
			};
			t["Elise"] = new[] {
				new SpellScript(SpellShape.Homing) { Note = "Q Neurotoxin: targeted bolt" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 2f, DmgMult = 0.5f, Note = "W Volatile Spiderling: spiderlings burst at target (summons not implemented)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "E Cocoon: line that stuns" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "R Spider Form: transform with speed (form swap not implemented)" },
			};
			t["Evelynn"] = new[] {
				new SpellScript(SpellShape.Homing) { Note = "Q Hate Spike: homing spike" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.8f, DurationSec = 0.5f, Flags = 4, Note = "W Allure: marks area then charms" },
				new SpellScript(SpellShape.Cone) { DmgMult = 1.2f, Note = "E Whiplash: whip lash in front" },
				new SpellScript(SpellShape.Cone) { DmgMult = 1.5f, Flags = 1, Note = "R Last Caress: wide cone, executes low health" },
			};
			t["Ezreal"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Note = "Q Mystic Shot: single-target skillshot" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "W Essence Flux: piercing bolt, marks enemies" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "E Arcane Shift: blink then bolt homes in" },
				new SpellScript(SpellShape.Beam) { Note = "R Trueshot Barrage: global piercing beam" },
			};
			t["FiddleSticks"] = new[] {
				new SpellScript(SpellShape.Homing) { Flags = 4, Note = "Q Terrify: targeted fear" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2f, HitIntervalSec = 0.5f, DmgMult = 0.4f, Note = "W Bountiful Harvest: drain channel around the caster" },
				new SpellScript(SpellShape.Cone) { Flags = 1, Note = "E Reap: scythe cone, slows" },
				new SpellScript(SpellShape.Zone) { DelaySec = 1.5f, DurationSec = 1.5f, DmgMult = 1.2f, Note = "R Crowstorm: channelled blink then storm at target area" },
			};
			t["Fiora"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, Note = "Q Lunge: dash to the target, strike a vital" },
				new SpellScript(SpellShape.Beam) { Flags = 2, Note = "W Riposte: parry then thrust line, stuns (parry window not implemented)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Bladework: bonus attack speed, slow on second hit (approximated as speed)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 4f, DmgMult = 0.6f, Note = "R Grand Challenge: dueling area around the target; vitals not implemented" },
			};
			t["Fizz"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "Q Urchin Strike: dash through the target" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Seastone Trident: on-hit bleed and attack buff (approximated as speed)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, Flags = 1, Note = "E Playful/Trickster: untargetable hop with area slam, slows" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, DmgMult = 1.3f, Note = "R Chum the Waters: shark missile, bursts on hit" },
			};
			t["Galio"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Count = 2, SpreadDeg = 12f, DmgMult = 0.6f, Note = "Q Winds of War: two gusts that merge into a cyclone" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "W Shield of Durand: damage reduction stance, taunts on release" },
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, Flags = 2, Note = "E Justice Punch: dash and knock up" },
				new SpellScript(SpellShape.Zone) { DelaySec = 1.5f, DurationSec = 0.5f, Flags = 2, Note = "R Hero's Entrance: ally teleport landing area (shown as delayed slam at cursor)" },
			};
			t["Gangplank"] = new[] {
				new SpellScript(SpellShape.Homing) { Note = "Q Parrrley: targeted pistol shot" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.2f, Note = "W Remove Scurvy: heal and cleanse" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 1f, Flags = 1, DmgMult = 1.3f, Note = "E Powder Keg: barrel explosion area (barrels not implemented)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0f, DurationSec = 4f, Flags = 1, HitIntervalSec = 0.5f, DmgMult = 0.4f, Note = "R Cannon Barrage: global area bombardment that slows" },
			};
			t["Garen"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, Flags = 4, Note = "Q Decisive Strike: speed burst then silencing strike" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "W Courage: damage reduction and shield" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 3f, HitIntervalSec = 0.5f, DmgMult = 0.8f, Note = "E Judgment: spinning blade around the caster" },
				new SpellScript(SpellShape.Homing) { DmgMult = 2f, Note = "R Demacian Justice: targeted true damage execute" },
			};
			t["Gnar"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Flags = 1, Note = "Q Boomerang Throw / Boulder Toss: out-and-back, slows (mega form not implemented)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Hyper / Wallop: speed bonus (mega wallop not implemented)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 7f, Flags = 1, Note = "E Hop / Crunch: leap with area landing, slows" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, Flags = 2, DmgMult = 1.3f, Note = "R GNAR!: shockwave around the caster that knocks back and stuns" },
			};
		}
	}
}
