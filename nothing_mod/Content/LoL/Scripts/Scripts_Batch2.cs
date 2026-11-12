using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch2
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Camille"] = new[] {
				new SpellScript(SpellShape.Cone) { SpeedBuff = true, DmgMult = 1.2f, Note = "Q Precision Protocol: next attack deals bonus damage + move speed; recast within window for big bonus after a delay (recast/timing not implemented)" },
				new SpellScript(SpellShape.Cone) { Flags = 1, Lifesteal = 0.3f, WidthMult = 1.4f, DmgMult = 1.2f, Note = "W Tactical Sweep: delayed cone; outer half slows, deals extra damage and heals Camille (delay and inner/outer split not implemented, heal applied to all hits)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 20f, Flags = 2, Note = "E Hookshot: grapple to a wall, leap off, knock up champions on landing (wall hook not implemented, 2 casts)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, Flags = 2, DmgMult = 0.8f, Note = "R Hextech Ultimatum: dash to target champion, anchors them in a zone (zone approximated by root on dash hit); bonus magic damage on attacks not implemented" },
			};
			t["Cassiopeia"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.4f, DurationSec = 0.3f, PoisonSec = 3f, SpeedBuff = true, Note = "Q Noxious Blast: poison blast after brief delay, move speed if it hits a champion (speed given on cast)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.4f, DurationSec = 5f, HitIntervalSec = 0.5f, Flags = 1, DmgMult = 0.5f, Note = "W Miasma: several poison clouds that slow, ground (cannot dash) and lightly damage; grounded not implemented, clouds merged into one" },
				new SpellScript(SpellShape.Homing) { DmgMult = 1.4f, Lifesteal = 0.3f, Note = "E Twin Fang: targeted bolt, bonus damage vs poisoned, heals her; mana refund on kill not implemented" },
				new SpellScript(SpellShape.Cone) { Flags = 3, WidthMult = 1.3f, Note = "R Petrifying Gaze: cone, stuns enemies facing her and slows those with back turned (facing check not implemented: all get stun+slow)" },
			};
			t["Chogath"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.6f, DurationSec = 0.4f, Flags = 3, Note = "Q Rupture: ground erupts after delay, knocks up and slows" },
				new SpellScript(SpellShape.Cone) { Flags = 4, DmgMult = 1.1f, Note = "W Feral Scream: cone magic damage + silence" },
				new SpellScript(SpellShape.Cone) { Flags = 1, DmgMult = 0.7f, Note = "E Vorpal Spikes: next attacks fire spikes in a cone in front, damage + slow (on-attack trigger not implemented, cast as a cone)" },
				new SpellScript(SpellShape.Homing) { DmgMult = 2.5f, Note = "R Feast: devour an enemy for high true damage; grows on kill (max health growth not implemented)" },
			};
			t["Corki"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 0.3f, Note = "Q Phosphorus Bomb: bomb at target location, magic damage in area, reveals units (reveal not implemented)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, BurnSec = 2.5f, DmgMult = 0.6f, Note = "W Valkyrie: fly forward dropping a fire trail that burns those standing in it (trail approximated by burn on dash hit; 2nd carpet-bomb part not implemented)" },
				new SpellScript(SpellShape.Cone) { DmgMult = 0.8f, WidthMult = 1.3f, Note = "E Gatling Gun: channelled cone of rapid bullets, shreds armor and magic resist (channel/shred not implemented)" },
				new SpellScript(SpellShape.Skillshot) { WidthMult = 3f, DmgMult = 1f, Note = "R Missile Barrage: missile exploding in area on impact; every 3rd is a Big One with extra damage/radius (ammo and Big One not implemented)" },
			};
			t["Darius"] = new[] {
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, DmgMult = 1.3f, Lifesteal = 0.2f, BleedSec = 5f, Note = "Q Decimate: wide axe circle, blade edge deals more than handle, heals from champions hit (edge vs handle not implemented)" },
				new SpellScript(SpellShape.Cone) { Flags = 1, BleedSec = 5f, SlowSec = 1f, Note = "W Crippling Strike: next attack hits an artery, bonus damage and slow (empowered attack approximated as short cone)" },
				new SpellScript(SpellShape.Cone) { Flags = 2, Knockback = -9f, DmgMult = 0.6f, BleedSec = 5f, Note = "E Apprehend: axe hook sweeps enemies in a cone and pulls them to him; armor penetration passive not implemented" },
				new SpellScript(SpellShape.Dash) { DashTiles = 16f, DmgMult = 2f, BleedSec = 5f, Note = "R Noxian Guillotine: leap to a champion for true damage scaling with Hemorrhage stacks; cooldown reset on kill not implemented" },
			};
			t["Diana"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, WidthMult = 1.8f, Note = "Q Crescent Strike: arc-shaped bolt, marks enemies with Moonlight (arc curve and Moonlight reveal not implemented, wide straight bolt)" },
				new SpellScript(SpellShape.Orbit) { Count = 3, DurationSec = 5f, Shield = true, DmgMult = 0.8f, Note = "W Pale Cascade: shield + three orbiting spheres that detonate on contact; shield bonus when 3rd detonates not implemented" },
				new SpellScript(SpellShape.Dash) { DashTiles = 20f, Note = "E Lunar Rush: dash to an enemy dealing magic damage; no cooldown on Moonlight targets (not implemented)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.8f, Knockback = -8f, Flags = 1, DmgMult = 1.2f, Note = "R Moonfall: reveals and pulls in all nearby enemies and slows; if a champion was pulled, moonlight crashes after a delay for bonus area damage (delayed crash not implemented)" },
			};
			t["DrMundo"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 1, DmgMult = 1.2f, Note = "Q Infected Bonesaw: cleaver hits first enemy, damage based on current health, slows (percent-health damage and Mundo health cost not implemented)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 4f, HitIntervalSec = 0.5f, DmgMult = 0.4f, Lifesteal = 0.2f, Note = "W Heart Zapper: electrocutes himself for persistent damage, stores damage taken, burst at end or recast and heals from stored damage (burst/store not implemented, drain approximated)" },
				new SpellScript(SpellShape.Cone) { DmgMult = 1.3f, Knockback = 6f, Note = "E Blunt Force Trauma: slam bag into an enemy, damage from missing health; killed enemy is swatted away hitting others (passive AD and kill-swat not implemented)" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.4f, SpeedBuff = true, Note = "R Maximum Dosage: instantly heals percent of missing health, move speed, regenerates max health over long duration (regen over time not implemented)" },
			};
			t["Draven"] = new[] {
				new SpellScript(SpellShape.Boomerang) { DmgMult = 1.2f, RangeMult = 0.7f, Note = "Q Spinning Axe: next attack bonus damage, axe ricochets up; catching it readies another (axe catching/stacks not implemented)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Blood Rush: move speed (decaying) and attack speed; refreshed by catching an axe (attack speed/refresh not implemented)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Count = 2, SpreadDeg = 8f, WidthMult = 1.3f, Flags = 3, Knockback = 8f, Note = "E Stand Aside: two axes that damage, knock aside and slow everything hit" },
				new SpellScript(SpellShape.Boomerang) { Pierce = true, DmgMult = 1.5f, WidthMult = 1.3f, RangeMult = 2f, Note = "R Whirling Death: two huge axes, damage falls per unit hit, reverses after hitting a champion or on recast, executes by Adoration (global range, per-hit falloff, early recall, execute not implemented)" },
			};
			t["Ekko"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Flags = 1, WidthMult = 1.5f, Note = "Q Timewinder: grenade expands into a slowing time field on hitting a champion, then after a delay rewinds back dealing damage (field/delay not implemented; boomerang approximates return)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 2.5f, DurationSec = 2f, Flags = 3, DmgMult = 0.3f, Note = "W Parallel Convergence: anomaly appears after a delay, slows enemies inside; if Ekko enters it he gains shield and enemies are stunned (shield on entering and stun-only-if-entered not implemented)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 13f, DmgMult = 1.2f, Note = "E Phase Dive: roll, next attack deals bonus damage and teleports him to the target (empowered attack/teleport not implemented)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.6f, DmgMult = 1.5f, Shield = true, HealFrac = 0.15f, Note = "R Chronobreak: untargetable, rewinds to position/health a few seconds ago, heals for damage taken, damages enemies at arrival zone (rewind not implemented; burst at current position)" },
			};
			t["Elise"] = new[] {
				new SpellScript(SpellShape.Homing) { PoisonSec = 2f, Note = "Q Neurotoxin: targeted bolt, damage based on target current health (spider form Venomous Bite lunge not implemented)" },
				new SpellScript(SpellShape.Homing) { DmgMult = 1.2f, SpeedMult = 0.7f, Note = "W Volatile Spiderling: spiderling that explodes when it nears a target (homing orb approximates the spiderling; spider form attack speed not implemented)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "E Cocoon: stuns first enemy hit and reveals them (reveal and spider form Rappel not implemented)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "R Spider Form: transforms for move speed and new abilities with spiderling swarm (form swap and spiderlings not implemented)" },
			};
			t["Evelynn"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Count = 3, SpreadDeg = 24f, DmgMult = 0.7f, Note = "Q Hate Spike: lasher hits first unit, then a line of spikes shot a few times at nearby foes (sequential spikes approximated as a fan)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 4, SpeedMult = 1.5f, Note = "W Allure: curses target; next attack/spell after a delay charms and lowers magic resist (delay and MR shred not implemented)" },
				new SpellScript(SpellShape.Cone) { DmgMult = 1.2f, SpeedBuff = true, Note = "E Whiplash: lashes target for damage then gains move speed" },
				new SpellScript(SpellShape.Cone) { DmgMult = 1.8f, WidthMult = 1.3f, Shield = true, Note = "R Last Caress: untargetable, decimates area in front (execute below threshold), then warps backward a long distance (execute and warp back not implemented)" },
			};
			t["Ezreal"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Note = "Q Mystic Shot: bolt stops on first unit, reduces all cooldowns on hit (cooldown reduction not implemented)" },
				new SpellScript(SpellShape.Skillshot) { DmgMult = 1.2f, Note = "W Essence Flux: orb sticks to first champion/objective and detonates (sticking/delayed detonation not implemented; direct hit)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 20f, Note = "E Arcane Shift: teleport to a nearby location, then homing bolt hits nearest enemy (bolt not implemented)" },
				new SpellScript(SpellShape.Beam) { RangeMult = 2f, WidthMult = 1.5f, DmgMult = 1.5f, Note = "R Trueshot Barrage: wind-up then global piercing barrage, reduced vs minions (wind-up and minion reduction not implemented)" },
			};
			t["FiddleSticks"] = new[] {
				new SpellScript(SpellShape.Homing) { Flags = 4, Note = "Q Terrify: targeted fear, flees in terror" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 5f, HitIntervalSec = 0.5f, DmgMult = 0.4f, Lifesteal = 0.5f, Note = "W Bountiful Harvest: channelled drain around him, execute burst damage at the end (end burst not implemented)" },
				new SpellScript(SpellShape.Cone) { Flags = 5, WidthMult = 1.5f, Note = "E Reap: scythe slash area after short delay, slows all hit, silences those in the center (center-only silence and delay not implemented)" },
				new SpellScript(SpellShape.Zone) { DurationSec = 5f, HitIntervalSec = 0.5f, DmgMult = 0.5f, RangeMult = 1.5f, Note = "R Crowstorm: channelled blink to location then crows deal damage per second to all enemies in area (blink not implemented, storm only)" },
			};
			t["Fiora"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "Q Lunge: lunge in a direction and stab a nearby enemy, on-hit effects (vitals not implemented)" },
				new SpellScript(SpellShape.Beam) { Flags = 1, Shield = true, Note = "W Riposte: parry all damage/disables briefly, then stab slows the first champion hit (stun if she blocked an immobilize) (parry window and conditional stun not implemented)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Bladework: bonus attack speed for 2 attacks, 1st slows, 2nd crits (attack speed/crit/slow-on-attack not implemented)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, HealFrac = 0.1f, Note = "R Grand Challenge: reveals four Vitals on an enemy champion, move speed near them, heals her and allies if vitals/kill (vitals and conditional heal not implemented)" },
			};
			t["Fizz"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 12f, Note = "Q Urchin Strike: dash through target dealing magic damage and on-hit effects" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Seastone Trident: attacks bleed enemies for magic damage over time; empowered next attack deals bonus damage (bleed on attacks not implemented, speed buff placeholder)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Flags = 1, Shield = true, Note = "E Playful/Trickster: hop untargetable then slam (slows) or hop again and slam (re-hop and area-at-landing not implemented)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 3, WidthMult = 1.5f, Knockback = 6f, DmgMult = 1.4f, Note = "R Chum the Waters: fish attaches to first champion and slows, then a shark erupts knocking them up and others aside (attach/delay not implemented)" },
			};
			t["Galio"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Count = 2, SpreadDeg = 14f, Pierce = true, DmgMult = 0.7f, Note = "Q Winds of War: two windblasts converge into a large tornado dealing damage over time (convergence and lingering tornado not implemented)" },
				new SpellScript(SpellShape.SelfBurst) { Shield = true, Flags = 4, DurationSec = 0.5f, Note = "W Shield of Durand: charged defensive stance (slowed) then release taunts and damages nearby enemies (charge not implemented, shielded release burst)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 16f, Flags = 2, Note = "E Justice Punch: step back then charge, knocks up the first champion met" },
				new SpellScript(SpellShape.Zone) { DelaySec = 2f, DurationSec = 0.4f, Flags = 2, Shield = true, RangeMult = 3f, Note = "R Hero's Entrance: lands at ally position after a delay, shielding allies and knocking up nearby enemies (ally targeting not implemented, lands at cursor)" },
			};
			t["Gangplank"] = new[] {
				new SpellScript(SpellShape.Homing) { Note = "Q Parrrley: shoots target, gold per kill (gold and crit not implemented)" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.2f, Note = "W Remove Scurvy: cures crowd control and restores health (cleanse not implemented)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.6f, DurationSec = 0.4f, Flags = 1, DmgMult = 1.3f, Note = "E Powder Keg: places a keg; attacking it explodes it, spreading damage and slowing (keg object not implemented, delayed explosion)" },
				new SpellScript(SpellShape.Zone) { DurationSec = 6f, HitIntervalSec = 1.3f, Flags = 1, DmgMult = 0.6f, RangeMult = 2f, Note = "R Cannon Barrage: global bombardment area with waves of cannonballs, damage and slow (global range, wave pattern not implemented)" },
			};
			t["Garen"] = new[] {
				new SpellScript(SpellShape.Cone) { SpeedBuff = true, Flags = 4, DmgMult = 1.2f, Note = "Q Decisive Strike: move speed burst breaking slows, next attack bonus damage and silence (slow break and next-attack trigger not implemented)" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "W Courage: shield and tenacity, then lesser damage reduction for longer (passive armor/MR not implemented)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 3f, HitIntervalSec = 0.5f, DmgMult = 0.6f, Note = "E Judgment: spin sword around body dealing damage repeatedly (armor reduction not implemented)" },
				new SpellScript(SpellShape.Homing) { DmgMult = 2.5f, Note = "R Demacian Justice: execute an enemy champion (true damage by missing health not implemented)" },
			};
			t["Gnar"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Flags = 1, Note = "Q Boomerang Throw: damages and slows, returns to him, cooldown cut if caught (Mega boulder/catch not implemented)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Hyper: attacks/spells give bonus damage and move speed; Mega Wallop smashes ahead stunning (Mega form not implemented)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, Note = "E Hop: leap to a location, bounce off units landed on for extra distance; Mega Crunch area damage (bounce/Mega not implemented)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, Flags = 3, Knockback = 10f, DmgMult = 1.3f, Note = "R GNAR!: Mega throws everything around in a direction, damage and slow, wall hit stuns for bonus damage (directional throw and wall stun not implemented; knockback burst)" },
			};
		}
	}
}
