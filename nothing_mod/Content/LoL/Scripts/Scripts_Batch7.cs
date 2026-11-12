using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch7
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Sejuani"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 25f, Flags = 2, Note = "Q Arctic Assault: charge forward, knocks enemies airborne, stops at first champion hit" },
				new SpellScript(SpellShape.Cone) { Flags = 1, DmgMult = 1.2f, Note = "W Winter's Wrath: two mace swings (cone then smash), slows and applies Frost (two hits not expressible)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, SpeedMult = 1.5f, Note = "E Permafrost: real = freezes and stuns champion with max Frost stacks (needs stacks); approximated as stunning bolt" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, WidthMult = 1.4f, Note = "R Glacial Prison: bola stuns first champion hit, then ice storm slows enemies in the area (storm not expressible)" },
			};
			t["Senna"] = new[] {
				new SpellScript(SpellShape.Beam) { Flags = 1, HealFrac = 0.05f, Note = "Q Piercing Darkness: beam through target damaging enemies and healing allies, slows enemies (real: slow comes from Q passive stacks)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, WidthMult = 1.3f, Note = "W Last Embrace: mist wave latches onto first enemy, roots it and nearby enemies after brief delay (delayed root not expressible)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 6f, HitIntervalSec = 1f, InvisibleSec = 6f, SpeedBuff = true, DmgMult = 0.01f, Note = "E Curse of the Black Mist: storm around her; allies inside become unselectable wraiths with move speed (no damage; ally camouflage not expressible)" },
				new SpellScript(SpellShape.Beam) { WidthMult = 1.5f, Shield = true, RangeMult = 3f, Note = "R Dawning Shadow: global beam, shields allies and damages enemies in the center" },
			};
			t["Seraphine"] = new[] {
				new SpellScript(SpellShape.Zone) { DurationSec = 0.2f, Note = "Q High Note: instant area damage at target location (every third basic spell recasts - not expressible)" },
				new SpellScript(SpellShape.SelfBurst) { Shield = true, SpeedBuff = true, HealFrac = 0.1f, DurationSec = 0.3f, DmgMult = 0.01f, Note = "W Surround Sound: shields and hastes nearby allies; heals if already shielded (ally targeting not expressible)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 1, Note = "E Beat Drop: piercing sound wave damaging and slowing enemies in a line (real: roots enemies already slowed/impaired)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 4, WidthMult = 1.3f, Note = "R Encore: piercing wave charming enemies; range refreshes per champion hit (range extension not expressible)" },
			};
			t["Sett"] = new[] {
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "Q Knuckle Down: next two attacks deal max-health damage and he gains move speed toward champions (empowered attacks not expressible)" },
				new SpellScript(SpellShape.Cone) { Shield = true, WidthMult = 1.4f, DmgMult = 1.3f, Note = "W Haymaker: spends stored Grit for a shield, punches area (true damage center, physical sides)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.3f, Flags = 2, Knockback = -8f, Note = "E Facebreaker: pulls in enemies from both sides and stuns (only slows if one side - not expressible)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Flags = 1, Knockback = -4f, DmgMult = 1.3f, Note = "R The Show Stopper: carries an enemy champion through the air, slams them down, slows enemies near landing (carry not expressible)" },
			};
			t["Shaco"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, InvisibleSec = 3f, Note = "Q Deceive: invisible, teleports to target location; next attack empowered, crits from behind (empowered attack not expressible)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 2f, HitIntervalSec = 0.5f, Flags = 4, Note = "W Jack In The Box: hidden box that fears and attacks nearby enemies when triggered (hidden trigger not expressible)" },
				new SpellScript(SpellShape.Homing) { Flags = 1, PoisonSec = 4f, Note = "E Two-Shiv Poison: thrown shiv, poisons and slows, bonus damage below 30% health" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 5f, HitIntervalSec = 1f, InvisibleSec = 1f, Note = "R Hallucinate: clone fights for him, explodes on death into three mini Jack in the Boxes (clone/boxes not expressible)" },
			};
			t["Shen"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Pierce = true, Flags = 1, DmgMult = 1.2f, Note = "Q Twilight Assault: recalls spirit blade which strikes (max-health damage); empowered and slows if it hits a champion" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2f, HitIntervalSec = 1f, Shield = true, DmgMult = 0.01f, Note = "W Spirit's Refuge: zone around spirit blade that blocks auto attacks on allies (blocking not expressible; shield stands in)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 12f, Flags = 4, Note = "E Shadow Dash: dash that taunts enemies in his path" },
				new SpellScript(SpellShape.Buff) { Shield = true, HealFrac = 0.1f, Note = "R Stand United: shields target ally then teleports to them (global ally targeting/teleport not expressible)" },
			};
			t["Shyvana"] = new[] {
				new SpellScript(SpellShape.Cone) { WidthMult = 0.8f, Note = "Q Emberstrike: next attack hits target and surrounding area, recastable (dragon form adds a single-target recast); approximated as cone" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, DelaySec = 0.5f, Shield = true, SpeedBuff = true, Note = "W Inferno Aegis: shield and move speed, then area detonates after short delay (heals in dragon form); delay not expressible on SelfBurst" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, BurnSec = 3f, Note = "E Molten Burst: fireball explodes on large target and slows; in dragon form pierces and leaves fire trail" },
				new SpellScript(SpellShape.Dash) { DashTiles = 15f, Flags = 4, Note = "R Dragon's Descent: leaps forward, enemies along path flee, transforms into dragon (transformation not expressible)" },
			};
			t["Singed"] = new[] {
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 5f, HitIntervalSec = 0.5f, PoisonSec = 2f, Note = "Q Poison Trail: toggle, leaves a poison trail damaging enemies in its path (trail left behind not expressible; aura follows him)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 5f, HitIntervalSec = 0.5f, Flags = 1, Note = "W Mega Adhesive: vial thrown on ground, slows and grounds enemies standing on it" },
				new SpellScript(SpellShape.Cone) { Flags = 2, Knockback = 12f, WidthMult = 0.5f, Note = "E Fling: damages one nearby enemy and flings it into the air behind him (roots if lands in Mega Adhesive)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, HealFrac = 0.1f, Shield = true, Note = "R Insanity Potion: big combat stat boost, Poison Trail applies Grievous Wounds (stats/grievous not expressible)" },
			};
			t["Sion"] = new[] {
				new SpellScript(SpellShape.Cone) { Flags = 2, WidthMult = 1.3f, DmgMult = 1.3f, Note = "Q Decimating Smash: channeled swing, damage and knock-up/stun scale with charge time (charge not expressible)" },
				new SpellScript(SpellShape.SelfBurst) { Shield = true, DurationSec = 0.4f, DelaySec = 0.5f, Note = "W Soul Furnace: shield, recast after 3s to explode for magic damage around him (recast not expressible)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 1, Knockback = 6f, RangeMult = 0.8f, Note = "E Roar of the Slayer: short shockwave damages, slows and shreds armor of first enemy; hitting a minion knocks it back through all enemies" },
				new SpellScript(SpellShape.Dash) { DashTiles = 28f, Flags = 2, Note = "R Unstoppable Onslaught: charges with ramping speed, steerable, knocks up on collision by distance charged (ramp/steer not expressible)" },
			};
			t["Sivir"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Pierce = true, Note = "Q Boomerang Blade: crossblade thrown out and back, damaging each way" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Ricochet: next few attacks gain attack speed and bounce to nearby targets (bouncing attacks not expressible)" },
				new SpellScript(SpellShape.Buff) { Shield = true, SpeedBuff = true, HealFrac = 0.05f, Note = "E Spell Shield: blocks one enemy ability, then grants health and move speed (spell block not expressible; approximated)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "R On The Hunt: team move speed surge, her attacks reduce spell cooldowns (ally buff / cooldown reduction not expressible)" },
			};
			t["Skarner"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Pierce = true, WidthMult = 1.5f, Note = "Q Shattered Earth / Upheaval: rips a boulder that empowers attacks, recast to throw it as a powerful projectile (boulder as thrown shot)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Shield = true, Flags = 1, Note = "W Seismic Bastion: shield and earthquake shockwave damaging and slowing foes" },
				new SpellScript(SpellShape.Dash) { DashTiles = 28f, Pierce = true, Flags = 2, Note = "E Ixtal's Impact: charges through terrain; champion hit is slammed into next wall for damage and stun (wall slam/terrain not expressible)" },
				new SpellScript(SpellShape.Cone) { Flags = 2, WidthMult = 0.7f, Knockback = -3f, Note = "R Impale: tails suppress enemy champions in front and drag them along as he moves (suppress/drag not expressible)" },
			};
			t["Smolder"] = new[] {
				new SpellScript(SpellShape.Homing) { BurnSec = 2f, Note = "Q Super Scorcher Breath: fire breath on an enemy, stronger with stacks, splash on kill" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, WidthMult = 1.2f, BurnSec = 2f, Note = "W Achooo!: flaming sneeze that explodes on hitting champions (area explosion on hit not expressible)" },
				new SpellScript(SpellShape.Orbit) { Count = 3, DurationSec = 2f, HitIntervalSec = 0.5f, SpeedBuff = true, Note = "E Flap, Flap, Flap: flies ignoring terrain and bombards lowest health enemy (flight/bombard approximated as orbiting flames + speed)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.75f, DurationSec = 1.5f, HitIntervalSec = 0.5f, Flags = 1, BurnSec = 2f, Note = "R MMOOOMMMM!: mom breathes fire from above at far target, extra damage and slow in the center" },
			};
			t["Sona"] = new[] {
				new SpellScript(SpellShape.Homing) { Count = 2, SpreadDeg = 30f, Note = "Q Hymn of Valor: bolts of sound hit two nearby enemies (champions prioritized); aura gives allies bonus damage (aura not expressible)" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.1f, Shield = true, Note = "W Aria of Perseverance: heals Sona and a wounded ally; aura shields allies tagged (ally targeting not expressible)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Song of Celerity: move speed for nearby allies; aura gives champions tagged move speed (ally aura not expressible)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, WidthMult = 1.6f, Note = "R Crescendo: ultimate chord stuns enemy champions, forcing them to dance" },
			};
			t["Soraka"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.25f, DurationSec = 0.2f, Flags = 1, HealFrac = 0.05f, Note = "Q Starcall: star falls at target location, damages and slows; heals Soraka if it hits a champion" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.2f, Note = "W Astral Infusion: sacrifices own health to heal another champion (ally target/self cost not expressible; heals self)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0f, DurationSec = 2.5f, HitIntervalSec = 0.5f, Flags = 4, Note = "E Equinox: zone silences enemies inside; when it expires enemies still inside are rooted (final root not expressible)" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.4f, Note = "R Wish: instantly restores health to herself and all allied champions (ally heal not expressible)" },
			};
			t["Swain"] = new[] {
				new SpellScript(SpellShape.Cone) { Count = 3, SpreadDeg = 20f, Pierce = true, Note = "Q Death's Hand: several bolts pierce through enemies; more bolts hitting the same enemy deal more damage" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.9f, DurationSec = 0.3f, Flags = 1, Note = "W Vision of Empire: demon eye after a delay damages and slows, reveals champions and grants Soul Fragment" },
				new SpellScript(SpellShape.Boomerang) { Pierce = true, Flags = 2, Knockback = -3f, Note = "E Nevermove: wave goes out, returns to Swain and roots enemies it hits; recast pulls rooted champions closer" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 8f, HitIntervalSec = 1f, Lifesteal = 0.3f, Flags = 1, Note = "R Demonic Ascension: demon form drains health from nearby enemies; Demonflare recast novas and slows (recast not expressible)" },
			};
			t["Sylas"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.6f, DurationSec = 0.3f, Flags = 1, Note = "Q Chain Lash: chains intersect at target location damaging and slowing, then energy explodes after a delay for more damage" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, HealFrac = 0.1f, Note = "W Kingslayer: lunges at enemy dealing damage, heals himself more against champions (heal scales on missing health)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "E Abscond: dashes to a location; recast Abduct throws chains pulling him to an enemy hit (recast not expressible)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, DmgMult = 1.5f, Note = "R Hijack: steals the targeted enemy champion's ultimate and casts it freely (stolen ultimate not expressible)" },
			};
			t["Syndra"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.4f, DurationSec = 0.2f, Note = "Q Dark Sphere: sphere conjured at target location dealing damage, remains for other spells (persisting sphere not expressible)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Note = "W Force of Will: throws a Dark Sphere or minion dealing damage and slowing enemies (pickup not expressible)" },
				new SpellScript(SpellShape.Cone) { Knockback = 10f, Flags = 1, WidthMult = 1.2f, Note = "E Scatter the Weak: knocks back enemies and spheres with damage; enemies hit by spheres are stunned (sphere stun not expressible)" },
				new SpellScript(SpellShape.Homing) { Count = 5, SpreadDeg = 40f, DmgMult = 0.8f, Note = "R Unleashed Power: bombards an enemy champion with all Dark Spheres (sphere count varies)" },
			};
			t["TahmKench"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Lifesteal = 0.3f, Note = "Q Tongue Lash: tongue hits first unit, damages and slows, heals him on champion hit; third stack stuns (stacks not expressible)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.6f, DurationSec = 0.2f, Flags = 2, Note = "W Abyssal Dive: dives down then reappears at target, damaging and knocking up enemies in area (dive travel not expressible)" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "E Thick Skin: converts stored (gray) health into a temporary shield" },
				new SpellScript(SpellShape.Skillshot) { Pierce = false, Flags = 2, Knockback = -4f, WidthMult = 1.2f, Note = "R Devour: swallows a champion for a few seconds, damaging an enemy or shielding an ally (swallow approximated as pull + stun)" },
			};
			t["Taliyah"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Count = 5, SpreadDeg = 8f, Note = "Q Threaded Volley: volley of rocks in a direction; on Worked Ground throws a stronger slowing boulder (worked ground not expressible)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.7f, DurationSec = 0.3f, Flags = 2, Knockback = 6f, Note = "W Seismic Shove: ground erupts, throws enemies in a chosen direction (knock-up if no direction)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.4f, DurationSec = 4f, HitIntervalSec = 0.5f, Flags = 1, Note = "E Unraveled Earth: slowing minefield; mines explode and stun enemies who dash/get knocked through (mine stun not expressible)" },
				new SpellScript(SpellShape.Beam) { WidthMult = 1.5f, RangeMult = 2f, Flags = 1, Note = "R Weaver's Wall: very long wall of earth she surfs on (wall/surf not expressible)" },
			};
		}
	}
}
