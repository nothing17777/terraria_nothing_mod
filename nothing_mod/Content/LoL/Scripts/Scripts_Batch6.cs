using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch6
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Olaf"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 1, SlowSec = 2f, ArmorShredSec = 3f, Note = "Q Undertow: axe thrown to target location, damages along the way, reduces armor and move speed; picking it up resets cooldown (not modelled)" },
				new SpellScript(SpellShape.Buff) { Shield = true, SpeedBuff = true, Note = "W Tough It Out: attack speed and a shield" },
				new SpellScript(SpellShape.Cone) { DmgMult = 1.3f, Note = "E Reckless Swing: true damage melee strike, costs Olaf health, refunds it if target dies" },
				new SpellScript(SpellShape.Buff) { Shield = true, Cleanse = true, Note = "R Ragnarok: passive armor/MR, active: immune to disables while attacking" },
			};
			t["Orianna"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "Q Command: Attack: ball thrown to location, damages along the way (less to later targets), stays there" },
				new SpellScript(SpellShape.Zone) { DurationSec = 3f, HitIntervalSec = 0.5f, Flags = 1, SpeedBuff = true, Note = "W Command: Dissonance: pulse around ball, then field that speeds allies and slows enemies" },
				new SpellScript(SpellShape.Homing) { Shield = true, Pierce = true, Note = "E Command: Protect: ball flies to ally, shields them, damages enemies on the way" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 0.2f, Flags = 2, Knockback = -8f, Note = "R Command: Shockwave: after short delay damages and launches nearby enemies toward the ball" },
			};
			t["Ornn"] = new[] {
				new SpellScript(SpellShape.Beam) { Flags = 1, Note = "Q Volcanic Rupture: fissure damages and slows, magma pillar forms at the end after a delay (pillar not modelled)" },
				new SpellScript(SpellShape.Cone) { DurationSec = 0.8f, HitIntervalSec = 0.2f, Note = "W Bellows Breath: Ornn advances breathing fire, final gout makes enemies Brittle (advance/brittle not modelled)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, Flags = 2, Note = "E Searing Charge: charge damages enemies passed; terrain impact makes shockwave that damages and knocks up" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 3, WidthMult = 2f, Note = "R Call of the Forge God: elemental travels toward Ornn with rising speed, slows and makes Brittle; recast to redirect (not modelled)" },
			};
			t["Pantheon"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "Q Comet Spear: tap thrusts spear, hold throws it in a direction" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Flags = 2, Note = "W Shield Vault: dashes to a target, damaging and stunning them" },
				new SpellScript(SpellShape.Cone) { DurationSec = 1f, HitIntervalSec = 0.25f, Shield = true, BlockSec = 1f, Note = "E Aegis Assault: invulnerable to front damage, strikes repeatedly with spear" },
				new SpellScript(SpellShape.Zone) { DelaySec = 1f, DurationSec = 0.3f, Flags = 2, Note = "R Grand Starfall: leaps into the air, lands as comet at chosen location (caster leap not modelled)" },
			};
			t["Poppy"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 1, Note = "Q Hammer Shock: swing leaves a zone that slows and explodes after a delay (delayed second hit not modelled)" },
				new SpellScript(SpellShape.Buff) { Shield = true, SpeedBuff = true, Note = "W Steadfast Presence: armor/MR, activate for move speed and to stop nearby enemy dashes (dash block not modelled)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Flags = 2, Knockback = 5f, Note = "E Heroic Charge: dash to target and push it back, stun if it hits a wall" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Knockback = 14f, DmgMult = 1.2f, Note = "R Keeper's Verdict: channeled hammer strike that knocks enemies very far away" },
			};
			t["Pyke"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Knockback = -8f, Note = "Q Bone Skewer: tap stabs enemy in front, hold hooks and pulls enemy toward him" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, InvisibleSec = 4f, Note = "W Ghostwater Dive: camouflage and big move speed that decays" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Flags = 2, Note = "E Phantom Undertow: dash, phantom returns to him stunning champions on its path (backward dash/return not modelled)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 0.2f, DmgMult = 1.3f, ExecuteFrac = 0.2f, Note = "R Death From Below: blinks to and executes low-health enemies in X-shaped zone, recastable (blink/X shape not modelled)" },
			};
			t["Qiyana"] = new[] {
				new SpellScript(SpellShape.Cone) { Flags = 1, Note = "Q Edge of Ixtal: swing with bonus effect from element (river slow, brush stealth, rock stun)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 12f, SpeedBuff = true, EmpowerNextAttack = true, Note = "W Terrashape: dash to a location, enchant weapon with element for bonus damage" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Note = "E Audacity: dash to an enemy, damaging them" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Knockback = 5f, WidthMult = 2f, Note = "R Supreme Display of Talent: shockwave pushes enemies, detonates elements, stuns near terrain" },
			};
			t["Quinn"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 4, Note = "Q Blinding Assault: Valor marks an enemy, blinds it, damages everything in the area" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Heightened Senses: passive attack/move speed, active: Valor reveals large area (reveal not modelled)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 7f, Flags = 1, Knockback = 4f, Note = "E Vault: dash to enemy, damage and slow, leaps off it interrupting, lands at max attack range" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "R Behind Enemy Lines: fly at great speed, ending casts Skystrike AoE and marks champions (AoE not modelled)" },
			};
			t["Rakan"] = new[] {
				new SpellScript(SpellShape.Skillshot) { HealFrac = 0.05f, Note = "Q Gleaming Quill: feather deals magic damage, hitting a champion heals allies (ally heal approximated on self)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Flags = 2, Note = "W Grand Entrance: dash to a location, knocks up nearby enemies on arrival" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Shield = true, RecastShape = SpellShape.Dash, Note = "E Battle Dance: flies to ally, shields them, free recast for short time (recast approximated as a second dash)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Flags = 4, SpeedBuff = true, Note = "R The Quickness: move speed, charms and damages enemies touched" },
			};
			t["Rammus"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, Flags = 1, Note = "Q Powerball: accelerating ball toward enemies, damage and slow on impact" },
				new SpellScript(SpellShape.Buff) { Shield = true, BlockSec = 3f, Note = "W Defensive Ball Curl: huge armor and MR, returns damage to attackers (damage reflect not modelled)" },
				new SpellScript(SpellShape.Homing) { Flags = 4, Note = "E Frenzying Taunt: taunts one enemy champion or monster" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.4f, DurationSec = 0.3f, Flags = 1, Note = "R Soaring Slam: hops and slams target area, damage and slow; knock-up near center if cast from Powerball (not modelled)" },
			};
			t["RekSai"] = new[] {
				new SpellScript(SpellShape.Cone) { Count = 3, SpreadDeg = 40f, EmpowerNextAttack = true, Note = "Q Queen's Wrath / Prey Seeker: next 3 attacks deal bonus AoE damage; burrowed: void burst reveals enemies" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.3f, Flags = 2, Note = "W Burrow / Un-burrow: burrow for move speed; unburrow knocks up and damages nearby enemies" },
				new SpellScript(SpellShape.Cone) { DmgMult = 1.2f, Note = "E Furious Bite / Tunnel: bite, true damage at max fury; burrowed: reusable tunnel (tunnel not modelled)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 20f, DmgMult = 1.3f, Shield = true, InvulnSec = 0.5f, Note = "R Void Rush: briefly untargetable, lunges at marked target for max-health damage" },
			};
			t["Rell"] = new[] {
				new SpellScript(SpellShape.Beam) { Flags = 2, Note = "Q Shattering Strike: magic damage in a line, breaks shields and stuns" },
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, Flags = 2, Shield = true, Note = "W Ferromancy: Crash Down: dismount crash knocks enemies up and gives large shield; dismounted: armor/MR/attack speed, slowed" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Full Tilt: ramping move speed for Rell and an ally, next attack explodes (ally link/explosion not modelled)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2f, HitIntervalSec = 0.4f, Knockback = -6f, Note = "R Magnet Storm: violently pulls nearby enemies, then keeps dragging them while dealing damage over time" },
			};
			t["Renata"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 2, RecastShape = SpellShape.Skillshot, Note = "Q Handshake: roots first enemy hit; recast throws the unit in a direction (recast approximated as a second missile)" },
				new SpellScript(SpellShape.Buff) { Shield = true, SpeedBuff = true, InvulnSec = 1.5f, Note = "W Bailout: ally delays death and revives on takedown (ally targeting/revive not modelled; death prevention applied to caster)" },
				new SpellScript(SpellShape.Skillshot) { Count = 2, SpreadDeg = 12f, Pierce = true, Flags = 1, Shield = true, Note = "E Loyalty Program: pair of missiles shield allies, damage and slow enemies" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, WidthMult = 3f, Flags = 4, Note = "R Hostile Takeover: wave of chemicals makes enemies Berserk (approximated as charm)" },
			};
			t["Renekton"] = new[] {
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.2f, Lifesteal = 0.15f, Note = "Q Cull the Meek: sweep damages all around and heals; stronger above 50 fury" },
				new SpellScript(SpellShape.Cone) { Flags = 2, DmgMult = 1.3f, Note = "W Ruthless Predator: double slash stuns 0.75s; above 50 fury triple slash, 1.5s stun, breaks shields" },
				new SpellScript(SpellShape.Dash) { DashTiles = 7f, RecastShape = SpellShape.Dash, ArmorShredSec = 3f, Note = "E Slice and Dice: dash damaging units along the way; recast dashes back, empowered reduces armor (fury empowerment not modelled)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 4f, HitIntervalSec = 0.5f, HealFrac = 0.2f, Note = "R Dominus: Tyrant form, bonus health, damages enemies around him, gains fury" },
			};
			t["Rengar"] = new[] {
				new SpellScript(SpellShape.Cone) { DmgMult = 1.3f, SpeedBuff = true, EmpowerNextAttack = true, Note = "Q Savagery: next attack stabs for bonus damage; ferocity: more damage and attack speed" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.2f, HealFrac = 0.1f, Note = "W Battle Roar: damages enemies, heals part of recent damage; ferocity: breaks CC" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Note = "E Bola Strike: bola slows first target; ferocity: roots (root not modelled)" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, InvisibleSec = 5f, Note = "R Thrill of the Hunt: camouflage, move speed, reveals nearest champion, leap to it reducing armor (reveal/leap not modelled)" },
			};
			t["Riven"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Knockback = 5f, RecastShape = SpellShape.Dash, Note = "Q Broken Wings: three strikes (recast); third hit knocks back nearby enemies (recast is one more dash, not three)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.2f, Flags = 2, Note = "W Ki Burst: damages and stuns nearby enemies" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Shield = true, Note = "E Valor: short step forward, blocks incoming damage" },
				new SpellScript(SpellShape.Skillshot) { DmgMult = 1.3f, Count = 3, SpreadDeg = 50f, Pierce = true, Note = "R Blade of the Exile: AD and range buff, then one powerful ranged Wind Slash (buff not modelled)" },
			};
			t["Rumble"] = new[] {
				new SpellScript(SpellShape.Cone) { DurationSec = 3f, HitIntervalSec = 0.25f, Note = "Q Flamespitter: flames in a cone for 3 seconds, more damage in Danger Zone" },
				new SpellScript(SpellShape.Buff) { Shield = true, SpeedBuff = true, Note = "W Scrap Shield: shield and quick burst of speed, boosted in Danger Zone" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, ArmorShredSec = 3f, RecastShape = SpellShape.Skillshot, Note = "E Electro Harpoon: harpoon damage, slow and magic resist reduction; holds 2 harpoons (second harpoon as recast)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 5f, HitIntervalSec = 0.5f, Flags = 1, BurnSec = 3f, Note = "R The Equalizer: rockets create a wall of flames that damages and slows (line zone approximated as circle)" },
			};
			t["Ryze"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Note = "Q Overload: energy in a line hits first enemy, bonus damage on Flux target; runes give speed burst" },
				new SpellScript(SpellShape.Homing) { Flags = 1, Note = "W Rune Prison: cage damages and slows; roots if target has Flux (root not modelled)" },
				new SpellScript(SpellShape.Homing) { Chain = 3, Note = "E Spell Flux: orb damages enemy and spreads Flux to nearby enemies" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "R Realm Warp: portal that teleports allies to a target location after a few seconds (teleport not modelled)" },
			};
			t["Samira"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Note = "Q Flair: gun shot, or sword swing at melee range; during Wild Rush hits all enemies in path" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.3f, HitIntervalSec = 0.15f, BlockSec = 0.8f, Note = "W Blade Whirl: slashes around her, destroys enemy missiles" },
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, SpeedBuff = true, Note = "E Wild Rush: dashes through an enemy slashing others on the path, attack speed, kill refreshes cooldown" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2f, HitIntervalSec = 0.2f, Note = "R Inferno Trigger: torrent of shots at all surrounding enemies" },
			};
		}
	}
}
