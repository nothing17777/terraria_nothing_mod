using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch8
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Talon"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Note = "Q Noxian Diplomacy: leap to target and strike" },
				new SpellScript(SpellShape.Boomerang) { Count = 3, SpreadDeg = 30f, Flags = 1, Note = "W Rake: fan of blades out and back, slows" },
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, Note = "E Assassin's Path: vault over terrain/walls" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2.5f, Flags = 1, Note = "R Shadow Assault: blade ring around Talon, becomes invisible, blades return" },
			};
			t["Taric"] = new[] {
				new SpellScript(SpellShape.Buff) { HealFrac = 0.15f, Note = "Q Starlight's Touch: heal nearby allies" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "W Bastion: shield self and linked ally" },
				new SpellScript(SpellShape.Beam) { Flags = 2, Note = "E Dazzle: delayed beam stun from Taric's link" },
				new SpellScript(SpellShape.SelfBurst) { DelaySec = 1f, DurationSec = 2.5f, Shield = true, Note = "R Cosmic Radiance: delayed invulnerability aura" },
			};
			t["Teemo"] = new[] {
				new SpellScript(SpellShape.Homing) { Flags = 1, Note = "Q Blinding Dart: targeted dart, blinds (slow)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Move Quick: move speed passive/active" },
				null,
				new SpellScript(SpellShape.Zone) { DurationSec = 4f, HitIntervalSec = 1f, Flags = 1, Note = "R Noxious Trap: mushroom poison zone at cursor, slows" },
			};
			t["Thresh"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "Q Death Sentence: hook, pulls target" },
				new SpellScript(SpellShape.Zone) { DurationSec = 5f, HitIntervalSec = 1f, Shield = true, Note = "W Dark Passage: lantern zone shielding allies" },
				new SpellScript(SpellShape.Cone) { Flags = 2, Note = "E Flay: sweeping knockback" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2f, Flags = 1, Note = "R The Box: ring of walls, slows" },
			};
			t["Tristana"] = new[] {
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "Q Rapid Fire: attack speed buff" },
				new SpellScript(SpellShape.Dash) { DashTiles = 9f, Flags = 1, Note = "W Rocket Jump: leap, damage and slow at landing" },
				new SpellScript(SpellShape.Skillshot) { Note = "E Explosive Charge: bomb on target, explodes" },
				new SpellScript(SpellShape.Homing) { Flags = 2, DmgMult = 1.2f, Note = "R Buster Shot: knockback blast on target" },
			};
			t["Trundle"] = new[] {
				new SpellScript(SpellShape.Cone) { Flags = 1, Note = "Q Chomp: bite empowered strike, slows" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 8f, HitIntervalSec = 1f, SpeedBuff = true, Note = "W Frozen Domain: icy zone, speed boost" },
				new SpellScript(SpellShape.Zone) { DurationSec = 5f, HitIntervalSec = 1f, Flags = 2, Note = "E Pillar of Ice: pillar knock-up, slow zone" },
				new SpellScript(SpellShape.Homing) { HealFrac = 0.15f, Note = "R Subjugate: drain enemy, heal" },
			};
			t["Tryndamere"] = new[] {
				new SpellScript(SpellShape.Buff) { HealFrac = 0.2f, Note = "Q Bloodlust: heal based on fury" },
				new SpellScript(SpellShape.Cone) { Flags = 1, Note = "W Mocking Shout: sweep, reduces damage, slows" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "E Spinning Slash: spin dash" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.5f, Shield = true, Note = "R Undying Rage: cannot die" },
			};
			t["TwistedFate"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Count = 3, SpreadDeg = 28f, Pierce = true, Note = "Q Wild Cards: three piercing cards in a fan" },
				new SpellScript(SpellShape.Homing) { Flags = 2, Note = "W Pick A Card: gold card stuns (picks gold)" },
				null,
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Note = "R Destiny: teleport to cursor" },
			};
			t["Twitch"] = new[] {
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "Q Ambush: stealth with speed" },
				new SpellScript(SpellShape.Zone) { DurationSec = 3f, HitIntervalSec = 0.75f, Flags = 1, Note = "W Venom Cask: poison slow zone" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, DmgMult = 1.3f, Note = "E Contaminate: poison burst on all poisoned enemies" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Count = 3, SpreadDeg = 14f, Note = "R Spray and Pray: piercing bolts, attack range up" },
			};
			t["Udyr"] = new[] {
				new SpellScript(SpellShape.Cone) { Note = "Q Tiger Stance: claw swipes" },
				new SpellScript(SpellShape.Buff) { Shield = true, HealFrac = 0.1f, Note = "W Turtle Stance: shield and heal" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Bear Stance: movement speed, stun on hit" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 3f, HitIntervalSec = 0.5f, Note = "R Phoenix Stance: burning aura and cone" },
			};
			t["Urgot"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.6f, DurationSec = 1f, Flags = 1, Note = "Q Corrosive Charge: delayed volley area, slows" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 4f, HitIntervalSec = 0.4f, Note = "W Purge: autofiring cannon around Urgot" },
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, Flags = 2, Note = "E Disdain: charge, flings an enemy" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 4, DmgMult = 1.3f, Note = "R Fear Beyond Death: harpoon, fear" },
			};
			t["Varus"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "Q Piercing Arrow: charged piercing arrow" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Blighted Quiver: passive on-hit blight" },
				new SpellScript(SpellShape.Zone) { DurationSec = 4f, HitIntervalSec = 1f, Flags = 1, Note = "E Hail of Arrows: area, slows" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Pierce = true, DmgMult = 1.3f, Note = "R Chain of Corruption: tendril roots, spreads" },
			};
			t["Vayne"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Note = "Q Tumble: dash, empowers next attack" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Silver Bolts: passive true damage" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "E Condemn: bolt knocks target back" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "R Final Hour: stealth, bonus damage and tumble" },
			};
			t["Veigar"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Count = 1, Note = "Q Baleful Strike: piercing dark matter missile" },
				new SpellScript(SpellShape.Zone) { DelaySec = 1.2f, DurationSec = 0.4f, Note = "W Dark Matter: delayed meteor at cursor" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.8f, DurationSec = 3f, HitIntervalSec = 1.5f, Flags = 2, Note = "E Event Horizon: cage ring, stuns" },
				new SpellScript(SpellShape.Homing) { DmgMult = 1.5f, Note = "R Primordial Burst: targeted nuke" },
			};
			t["Velkoz"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Note = "Q Plasma Fission: missile splits at end" },
				new SpellScript(SpellShape.Skillshot) { Count = 3, SpreadDeg = 36f, Pierce = true, Note = "W Void Rift: three rift lines, piercing" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.8f, DurationSec = 0.3f, Flags = 2, Note = "E Tectonic Disruption: delayed knock-up zone" },
				new SpellScript(SpellShape.Beam) { DurationSec = 2.5f, DmgMult = 1.5f, Note = "R Life Form Disintegration Ray: channeled beam" },
			};
			t["Vex"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "Q Mistral Bolt: piercing bolt" },
				new SpellScript(SpellShape.SelfBurst) { DelaySec = 0.3f, DurationSec = 0.4f, Flags = 4, Note = "W Personal Space: burst around Vex, fear if shielded" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 2f, HitIntervalSec = 0.5f, Flags = 1, Note = "E Looming Darkness: cursor zone, slows" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, DmgMult = 1.4f, Note = "R Shadow Surge: long-range seeking missile" },
			};
			t["Vi"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Flags = 2, Note = "Q Vault Breaker: charged dash, knocks up" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Denting Blows: passive armor shred" },
				new SpellScript(SpellShape.Cone) { DmgMult = 1.2f, Note = "E Excessive Force: empowered cone strike" },
				new SpellScript(SpellShape.Dash) { DashTiles = 9f, Flags = 2, DmgMult = 1.3f, Note = "R Assault and Battery: chase target, knock up" },
			};
			t["Viego"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "Q Blade of the Ruined King: thrusting line" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Flags = 2, Note = "W Spectral Maw: charged dash, stuns" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Harrowed Path: mist stealth and speed" },
				new SpellScript(SpellShape.Dash) { DashTiles = 7f, DmgMult = 1.5f, Flags = 2, Note = "R Heartbreaker: dash, knocks enemies away" },
			};
			t["Viktor"] = new[] {
				new SpellScript(SpellShape.Homing) { Shield = true, Note = "Q Siphon Power: missile and shield" },
				new SpellScript(SpellShape.Zone) { DurationSec = 4f, HitIntervalSec = 1f, Flags = 1, Note = "W Gravity Field: slow zone then stun" },
				new SpellScript(SpellShape.Beam) { Note = "E Death Ray: laser line with explosion" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.4f, DurationSec = 4f, HitIntervalSec = 0.5f, Note = "R Chaos Storm: moving storm area" },
			};
		}
	}
}
