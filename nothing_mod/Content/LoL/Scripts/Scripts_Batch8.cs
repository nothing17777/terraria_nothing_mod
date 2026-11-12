using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch8
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Talon"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Note = "Q Noxian Diplomacy: single-target stab; leaps to the target if outside melee range, crit when in melee; refunds health/cooldown on kill (kill refund and targeting not expressible)" },
				new SpellScript(SpellShape.Boomerang) { Count = 3, SpreadDeg = 26f, Pierce = true, Flags = 1, SlowSec = 1.5f, BleedSec = 2f, Note = "W Rake: cone volley of daggers out and back, pierces; returning blades deal bonus damage and slow" },
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, DmgMult = 0.1f, Note = "E Assassin's Path: vault over terrain/structures (no damage); terrain cooldown not expressible" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2.5f, HitIntervalSec = 0.6f, InvisibleSec = 2.5f, SpeedBuff = true, Pierce = true, BleedSec = 3f, Note = "R Shadow Assault: ring of blades, Talon invisible with move speed; blades converge back on him dealing damage each move (inward converge not expressible)" },
			};
			t["Taric"] = new[] {
				new SpellScript(SpellShape.Buff) { HealFrac = 0.15f, Note = "Q Starlight's Touch: heals nearby allied champions based on stored charges (allies/charges not expressible, heals caster)" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "W Bastion: shields an ally and links them (ally link not expressible, shields caster)" },
				new SpellScript(SpellShape.Beam) { Flags = 2, CastDelaySec = 1f, Note = "E Dazzle: starlight beam from Taric (and Bastion ally) that stuns after a brief delay (Bastion ally origin not expressible)" },
				new SpellScript(SpellShape.Buff) { Shield = true, InvulnSec = 2.5f, Note = "R Cosmic Radiance: after a delay makes nearby allies invulnerable briefly (delay and allies not expressible, invulnerability on caster only)" },
			};
			t["Teemo"] = new[] {
				new SpellScript(SpellShape.Homing) { Flags = 1, SlowSec = 2.5f, Note = "Q Blinding Dart: targeted dart, damages and blinds target (blind approximated as slow)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Move Quick: passive move speed until struck; active sprint (passive/sprint split not expressible)" },
				new SpellScript(SpellShape.Homing) { PoisonSec = 4f, DmgMult = 0.6f, Note = "E Toxic Shot: on-hit passive poisoning for 4 s (passive on-hit not expressible, shown as a poison dart)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.4f, DurationSec = 4f, HitIntervalSec = 1f, Flags = 1, SlowSec = 2f, PoisonSec = 4f, Note = "R Noxious Trap: thrown mushroom, armed trap that bursts into poison cloud on contact, slows and damages over time (stored mushrooms/bounce not expressible)" },
			};
			t["Thresh"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Knockback = -9f, RecastShape = SpellShape.Dash, Note = "Q Death Sentence: chain hook, pulls first enemy hit toward Thresh; recast dashes Thresh toward the cursor (not tied to the hooked enemy)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.4f, DurationSec = 6f, HitIntervalSec = 1f, Shield = true, DmgMult = 0.1f, Note = "W Dark Passage: lantern at target location shields nearby allies; allies can click it to dash to Thresh (ally dash not expressible)" },
				new SpellScript(SpellShape.Cone) { Flags = 1, SlowSec = 1f, Knockback = 8f, Note = "E Flay: sweeping chain knocks enemies in direction of the blow (Thresh can pull instead; pull/push choice not expressible)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2f, HitIntervalSec = 0.5f, Flags = 1, SlowSec = 2f, Note = "R The Box: pentagon prison of walls around Thresh, slows, damages if broken (walls not expressible)" },
			};
			t["Tristana"] = new[] {
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "Q Rapid Fire: attack speed buff (attack speed not expressible, move speed used)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 9f, Flags = 1, SlowSec = 1.5f, Note = "W Rocket Jump: leap to location, damages and slows enemies where she lands" },
				new SpellScript(SpellShape.Homing) { DmgMult = 1.3f, Note = "E Explosive Charge: bomb planted on a target that explodes after a short delay, damaging surrounding units (delayed detonation/stacks not expressible)" },
				new SpellScript(SpellShape.Homing) { Flags = 2, Knockback = 12f, DmgMult = 1.2f, Note = "R Buster Shot: magic cannonball at target, knocks it back (bomb radius doubling not expressible)" },
			};
			t["Trundle"] = new[] {
				new SpellScript(SpellShape.Cone) { Flags = 1, SlowSec = 1.5f, EmpowerNextAttack = true, Note = "Q Chomp: bite empowered attack, slows and saps attack damage (AD drain not expressible)" },
				new SpellScript(SpellShape.Zone) { DurationSec = 8f, HitIntervalSec = 1f, SpeedBuff = true, HealFrac = 0.05f, DmgMult = 0.2f, Note = "W Frozen Domain: target-location domain granting attack/move speed and heal bonus while on it (no real damage; buff only at cast)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.25f, DurationSec = 3f, HitIntervalSec = 1f, Flags = 1, SlowSec = 2f, Knockback = 8f, Note = "E Pillar of Ice: ice pillar at target location, knocks nearby enemies away and slows; impassable terrain not expressible" },
				new SpellScript(SpellShape.Homing) { Lifesteal = 0.6f, ArmorShredSec = 4f, DmgMult = 1.2f, Note = "R Subjugate: steals percent of target's health, armor and MR, doubled over 4 s (steal approximated by lifesteal and armor shred)" },
			};
			t["Tryndamere"] = new[] {
				new SpellScript(SpellShape.Buff) { HealFrac = 0.2f, Note = "Q Bloodlust: consumes Fury to heal (Fury resource not expressible)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Flags = 1, SlowSec = 3f, DmgMult = 0.2f, Note = "W Mocking Shout: surrounding champions lose attack damage; enemies facing away are slowed (AD reduction/facing check not expressible)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "E Spinning Slash: slices toward the target dealing damage to enemies along the path" },
				new SpellScript(SpellShape.Buff) { Shield = true, HealFrac = 0.3f, InvulnSec = 5f, Note = "R Undying Rage: cannot die for a few seconds, life stays above a minimum (minimum-life floor approximated by invulnerability)" },
			};
			t["TwistedFate"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Count = 3, SpreadDeg = 28f, Pierce = true, Note = "Q Wild Cards: three cards in a fan, each pierces and damages every enemy passed" },
				new SpellScript(SpellShape.Homing) { Flags = 2, DmgMult = 0.8f, EmpowerNextAttack = true, Note = "W Pick a Card: chosen card empowers next attack (gold stun, blue mana, red slow area); only gold stun shown (card choice not expressible)" },
				null,
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, DmgMult = 0.1f, Note = "R Destiny: reveals champions; Gate teleports him to any target location after 1.5 s (teleport/delay not expressible, shown as a dash)" },
			};
			t["Twitch"] = new[] {
				new SpellScript(SpellShape.Buff) { InvisibleSec = 4f, SpeedBuff = true, Note = "Q Ambush: camouflage with move speed; attack speed when leaving it (attack speed/reset on venom kill not expressible)" },
				new SpellScript(SpellShape.Zone) { DurationSec = 0.6f, HitIntervalSec = 0.6f, Flags = 1, SlowSec = 3f, PoisonSec = 6f, Note = "W Venom Cask: hurled cask exploding in an area, slows and applies deadly venom" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, DmgMult = 1.3f, Note = "E Contaminate: blast on all poisoned enemies in range scaling with stacks (poison-stack check not expressible)" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, RangeMult = 1.3f, SpeedMult = 1.5f, DmgMult = 1.2f, Note = "R Spray and Pray: bolts fired a great distance pierce all enemies; attack range up for several seconds (timed window not expressible)" },
			};
			t["Udyr"] = new[] {
				new SpellScript(SpellShape.Cone) { SpeedBuff = true, DmgMult = 1.2f, EmpowerNextAttack = true, Note = "Q Wilding Claw: attack speed and next two attacks bonus damage; recast calls lightning (recast and second attack not expressible)" },
				new SpellScript(SpellShape.Buff) { Shield = true, HealFrac = 0.1f, EmpowerNextAttack = true, Note = "W Iron Mantle: shield and next two attacks heal; recast bigger shield and heal over time (recast and second attack not expressible)" },
				new SpellScript(SpellShape.Cone) { SpeedBuff = true, Flags = 2, DmgMult = 0.8f, EmpowerNextAttack = true, Note = "E Blazing Stampede: move speed and first attack on each target stuns; recast grants immobilize immunity (immunity and per-target tracking not expressible)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 3f, HitIntervalSec = 0.5f, Flags = 1, SlowSec = 1.5f, Note = "R Wingborne Storm: glacial storm around Udyr damaging and slowing; recast empowers and tracks enemies (tracking not expressible)" },
			};
			t["Urgot"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 0.3f, Flags = 1, SlowSec = 1.25f, Note = "Q Corrosive Charge: explosive charge at target location, damages and slows enemies in explosion" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 4f, HitIntervalSec = 0.4f, Note = "W Purge: Urgot slows himself while autofiring at nearby enemies, prioritizing recently struck champions (self-slow/priority not expressible)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, Shield = true, Flags = 2, Knockback = 8f, Note = "E Disdain: charge with shield, tramples non-champions; stops and hurls an enemy champion out of the way" },
				new SpellScript(SpellShape.Skillshot) { Flags = 4, DmgMult = 1.3f, ExecuteFrac = 0.25f, Note = "R Fear Beyond Death: chem-drill impales first champion hit; execute them when below health threshold, then fear (impale and recast not expressible)" },
			};
			t["Varus"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, RangeMult = 1.2f, DmgMult = 1.2f, Note = "Q Piercing Arrow: charged shot, more range and damage the longer he charges (charging not expressible, fixed long shot)" },
				null,
				new SpellScript(SpellShape.Zone) { DelaySec = 0.25f, DurationSec = 4f, HitIntervalSec = 1f, Flags = 1, SlowSec = 4f, Note = "E Hail of Arrows: hail of arrows desecrates ground, slows and reduces healing (healing reduction not expressible)" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Chain = 2, DmgMult = 1.2f, Note = "R Chain of Corruption: tendril immobilizes first champion hit and spreads to nearby uninfected champions" },
			};
			t["Vayne"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 4f, DmgMult = 0.1f, EmpowerNextAttack = true, Note = "Q Tumble: tumble toward cursor; next attack deals bonus damage" },
				null,
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Knockback = 10f, DmgMult = 1.1f, Note = "E Condemn: heavy bolt knocks target back; impaled and stunned if it hits terrain (terrain collision not expressible)" },
				new SpellScript(SpellShape.Buff) { InvisibleSec = 3f, SpeedBuff = true, Note = "R Final Hour: attack damage, invisibility during Tumble, reduced Tumble cooldown, more move speed" },
			};
			t["Veigar"] = new[] {
				new SpellScript(SpellShape.Skillshot) { PierceLimit = 2, Note = "Q Baleful Strike: dark bolt hits the first two enemies only; kills grant permanent AP (AP not expressible)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 1.2f, DurationSec = 0.4f, Note = "W Dark Matter: mass falls from sky at target location after delay" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 3f, HitIntervalSec = 3f, Flags = 2, DmgMult = 0.3f, Note = "E Event Horizon: cage ring that stuns enemies who pass through its edge (edge-only trigger not expressible, stuns inside)" },
				new SpellScript(SpellShape.Homing) { DmgMult = 1.6f, LowHealthBonus = 1f, Note = "R Primordial Burst: targeted champion nuke, increased by missing health" },
			};
			t["Velkoz"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 1, SlowSec = 1.8f, Note = "Q Plasma Fission: plasma bolt slows; splits in two on recast or on hitting an enemy (split not expressible)" },
				new SpellScript(SpellShape.Beam) { Note = "W Void Rift: line rift deals initial burst then explodes again after a delay (second burst not expressible)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.8f, DurationSec = 0.3f, Flags = 2, Knockback = 3f, Note = "E Tectonic Disruption: area explodes after delay, knocking up enemies and pushing close ones slightly" },
				new SpellScript(SpellShape.Beam) { DmgMult = 1.8f, Note = "R Life Form Disintegration Ray: 2.5 s channeled beam that follows the cursor, true damage to researched champions (channel/cursor follow not expressible)" },
			};
			t["Vex"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, SpeedMult = 1.3f, Note = "Q Mistral Bolt: missile that accelerates mid-flight (acceleration not expressible)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Shield = true, Note = "W Personal Space: gain shield and damage nearby enemies" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 2f, HitIntervalSec = 0.5f, Flags = 1, SlowSec = 2f, Note = "E Looming Darkness: damaging slowing zone that applies Gloom" },
				new SpellScript(SpellShape.Skillshot) { DmgMult = 1.4f, RangeMult = 1.5f, RecastShape = SpellShape.Dash, Note = "R Shadow Surge: long missile marks first champion hit; recast dashes to them and damages (dash goes toward cursor, mark not expressible)" },
			};
			t["Vi"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Knockback = 8f, Note = "Q Vault Breaker: charged punch carries her forward, knocks enemies back and applies Denting Blows (charge time not expressible)" },
				null,
				new SpellScript(SpellShape.Cone) { DmgMult = 1.2f, EmpowerNextAttack = true, Note = "E Relentless Force: next attack blasts through target damaging enemies behind it" },
				new SpellScript(SpellShape.Dash) { DashTiles = 9f, Flags = 2, DmgMult = 1.3f, Note = "R Cease and Desist: runs down target knocking aside others, knocks it up, jumps and slams it down (multi-phase not expressible)" },
			};
			t["Viego"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Lifesteal = 0.1f, Note = "Q Blade of the Ruined King: zweihander thrust impales enemies in front; passive current-health on-hit with second strike heal" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Flags = 2, Note = "W Spectral Maw: charges then dashes forward, releasing Black Mist ball that stuns first enemy hit (charge time not expressible)" },
				new SpellScript(SpellShape.Buff) { InvisibleSec = 2.5f, SpeedBuff = true, Note = "E Harrowed Path: Black Mist around terrain; hiding in it grants camouflage, move and attack speed (terrain zone not expressible)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 7f, DmgMult = 1.5f, Flags = 2, Knockback = 8f, Note = "R Heartbreaker: teleports nearby and executes a champion, shockwave knocks away their allies" },
			};
			t["Viktor"] = new[] {
				new SpellScript(SpellShape.Homing) { Shield = true, EmpowerNextAttack = true, Note = "Q Siphon Power: blasts an enemy, grants shield and empowers next basic attack" },
				new SpellScript(SpellShape.Zone) { DurationSec = 4f, HitIntervalSec = 1f, Flags = 1, SlowSec = 2f, Note = "W Gravity Field: slows enemies in field, stuns those who stay too long (stay-time stun not expressible)" },
				new SpellScript(SpellShape.Beam) { Note = "E Hextech Ray: biomechanical arm fires a ray across the field damaging all in its path (augment explosion not expressible)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.25f, DurationSec = 7f, HitIntervalSec = 0.5f, Note = "R Arcane Storm: storm at location, initial damage, interrupts channels, periodic damage, can be redirected (redirect not expressible)" },
			};
		}
	}
}
