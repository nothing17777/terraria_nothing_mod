using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch1
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Aatrox"] = new[] {
				new SpellScript(SpellShape.Cone) { Flags = 2, DmgMult = 1.2f, Note = "Q Darkin Blade: 3 casts, each a different area (line, wide cone, ring sweet-spot) that knocks up; framework: single cone knock-up" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, Knockback = -4f, Note = "W Infernal Chains: hits first enemy, then drags them to the centre unless they leave the area (slow + pull); no delayed 2nd hit" },
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Lifesteal = 0.1f, Note = "E Umbral Dash: dash in a direction (can be used during other casts); passive heal on champion damage" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 3f, HitIntervalSec = 1f, Flags = 4, SpeedBuff = true, HealFrac = 0.1f, Lifesteal = 0.2f, Note = "R World Ender: fears nearby minions, bonus AD, heal and move speed, extends on takedown; no AD buff/extension" },
			};
			t["Akali"] = new[] {
				new SpellScript(SpellShape.Cone) { Flags = 1, SlowSec = 0.5f, Note = "Q Five Point Strike: 5 kunai in a cone, slow, outer edge hits harder" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, InvisibleSec = 4f, Note = "W Twilight Shroud: smoke zone, invisible + move speed inside, revealed on attack; no zone" },
				new SpellScript(SpellShape.Skillshot) { RecastShape = SpellShape.Dash, Note = "E Shuriken Flip: flip back and fire shuriken, marks first hit; recast = dash to the marked target; no backflip, no mark tracking" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, DmgMult = 1.3f, RecastShape = SpellShape.Dash, LowHealthBonus = 1f, Note = "R Perfect Execution: leap, recast dash through enemies (damage rises with missing health); no true execute on the recast" },
			};
			t["Akshan"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Note = "Q Avengerang: boomerang hits out and back, range extends per enemy hit; no range extension" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, InvisibleSec = 3f, Note = "W Going Rogue: camouflage near terrain/brush + move speed toward Scoundrels; no terrain condition" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, DmgMult = 0.5f, Note = "E Heroic Swing: grapple into terrain and swing, auto-firing at nearest enemy; no grapple/arc" },
				new SpellScript(SpellShape.Beam) { DmgMult = 1.5f, CastDelaySec = 1f, LowHealthBonus = 1f, Note = "R Comeuppance: channel locks target, stores bullets, fires them all (missing-health damage, hits first unit); no lock-on, no bullet storing" },
			};
			t["Alistar"] = new[] {
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Flags = 2, Note = "Q Pulverize: smash the ground, damage and toss nearby enemies up" },
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Flags = 2, Knockback = 8f, Note = "W Headbutt: ram a target, damage and knock back (then stun-free); targeted, approximated as dash" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 5f, HitIntervalSec = 0.5f, Flags = 2, Note = "E Trample: stomps nearby enemies for 5s ignoring collision, stacks then stunning empowered attack; stun applied directly" },
				new SpellScript(SpellShape.Buff) { Shield = true, Cleanse = true, Note = "R Unbreakable Will: removes CC on self, reduces incoming damage" },
			};
			t["Ambessa"] = new[] {
				new SpellScript(SpellShape.Cone) { RecastShape = SpellShape.Skillshot, Note = "Q Cunning Sweep: semicircle sweep, bonus damage on blade; recast = Sundering Slam line (not conditional on a hit)" },
				new SpellScript(SpellShape.SelfBurst) { Shield = true, DurationSec = 0.5f, DmgMult = 1.3f, Note = "W Repudiation: gain shield, brace, then slam nearby enemies, more damage if damage blocked; no block bonus" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Flags = 1, RecastShape = SpellShape.Dash, Note = "E Lacerate: whip drakehounds around her, damage + slow nearby; recast = dash (Drakehound's Step), no second strike at dash end" },
				new SpellScript(SpellShape.Dash) { DashTiles = 12f, Flags = 2, Note = "R Public Execution: blink to farthest champion in a line, suppress, slam and stun; suppress approximated" },
			};
			t["Amumu"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Knockback = -5f, Note = "Q Bandage Toss: sticky bandage stuns the target and pulls Amumu to them (approximated as pulling target)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 5f, HitIntervalSec = 1f, Note = "W Despair: toggle aura, nearby enemies lose % max health per second" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Note = "E Tantrum: rage burst damaging surrounding enemies (passive armor)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, Flags = 2, Note = "R Curse of the Sad Mummy: bandages entangle surrounding enemies, damage + stun" },
			};
			t["Anivia"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 1, SlowSec = 1f, RecastShape = SpellShape.Zone, Note = "Q Flash Frost: orb chills and damages in path; recast = burst/stun area at the cursor (not at the orb), no auto-burst on arrival" },
				new SpellScript(SpellShape.Zone) { DurationSec = 5f, Note = "W Crystallize: impassable ice wall for a short time; framework cannot block movement" },
				new SpellScript(SpellShape.Homing) { DmgMult = 1.5f, Note = "E Frostbite: targeted blast, double damage if chilled by Flash Frost or Glacial Storm; no double-damage condition" },
				new SpellScript(SpellShape.Zone) { DurationSec = 4f, HitIntervalSec = 0.4f, Flags = 1, Note = "R Glacial Storm: toggle hail field damaging and slowing, full formation boosts damage" },
			};
			t["Aphelios"] = new[] {
				new SpellScript(SpellShape.Skillshot) { DmgMult = 1.2f, Flags = 1, Note = "Q: 5 weapon-dependent abilities (Calibrum mark shot, Severum heal dash, Gravitum root, Infernum cone, Crescendum chakrams); generic long-range shot, weapon system missing" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Phase: swap main/off-hand gun; no weapon system, speed buff placeholder" },
				null,
				new SpellScript(SpellShape.Skillshot) { Pierce = true, WidthMult = 1.6f, Flags = 2, Note = "R Moonlight Vigil: moonlight blast explodes on first champion hit (wide area) and applies main-hand gun effect, also spawns turret" },
			};
			t["Ashe"] = new[] {
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "Q Ranger's Focus: at full Focus gain attack speed and flurry basic attacks; no focus stacks" },
				new SpellScript(SpellShape.Skillshot) { Count = 9, SpreadDeg = 57f, Flags = 1, DmgMult = 0.4f, Note = "W Volley: 9 arrows in a cone, applies Frost Shot slow" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Hawkshot: send scouting hawk anywhere on the map, reveals area; no vision mechanic" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2, SpeedMult = 1.5f, RangeMult = 4f, Chain = 3, Note = "R Enchanted Crystal Arrow: global arrow, stuns champion (longer with distance), splash damages and slows nearby; stun length not scaled" },
			};
			t["AurelionSol"] = new[] {
				new SpellScript(SpellShape.Beam) { DmgMult = 1.5f, Note = "Q Breath of Light: channeled dragon breath, damages first enemy plus splash, ramps up per second" },
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, DmgMult = 0.1f, Note = "W Astral Flight: fly over terrain in a direction, can cast abilities while flying" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.3f, DurationSec = 5f, HitIntervalSec = 0.5f, Flags = 1, Knockback = -1.5f, ExecuteFrac = 0.1f, Note = "E Singularity: black hole damages and slowly pulls enemies in, executes low health at centre; execute threshold 0.1 is UNVERIFIED and applies to the whole zone, not only the centre" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 0.4f, Flags = 2, WidthMult = 1.3f, Note = "R Falling Star: star crash damages and stuns (The Skies Descend: larger, knock-up); no Stardust upgrade" },
			};
			t["Aurora"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Note = "Q Twofold Hex: missile curses enemies hit, recast draws curses back to her damaging along the way" },
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, DmgMult = 0.1f, InvisibleSec = 2f, SpeedBuff = true, Note = "W Across the Veil: leap, enter spirit realm on landing, invisible + move speed" },
				new SpellScript(SpellShape.Beam) { Flags = 1, Knockback = 0f, Note = "E The Weirding: blast damages and slows enemies, then Aurora hops backward; no backward hop" },
				new SpellScript(SpellShape.Dash) { DashTiles = 4f, DmgMult = 1.2f, Flags = 1, Note = "R Between Worlds: leap with shockwave (damage + slow), then slowing zone she can teleport across; no zone/teleport" },
			};
			t["Azir"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Pierce = true, Count = 2, SpreadDeg = 10f, Flags = 1, SlowSec = 1f, Note = "Q Conquering Sands: all Sand Soldiers dash to location damaging and slowing 1s as they pass" },
				new SpellScript(SpellShape.Orbit) { Count = 2, DurationSec = 8f, Note = "W Arise!: summon Sand Soldier that attacks nearby targets in a line for him (up to 2 charges)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 9f, Shield = true, Note = "E Shifting Sands: shield and dash to a Sand Soldier damaging enemies; hit champion halts dash and readies a soldier" },
				new SpellScript(SpellShape.Cone) { Flags = 2, Knockback = 8f, WidthMult = 2f, Note = "R Emperor's Divide: wall of soldiers charges forward, damage and knockback (soldiers persist briefly)" },
			};
			t["Bard"] = new[] {
				new SpellScript(SpellShape.Skillshot) { PierceLimit = 2, Flags = 1, SlowSec = 1.4f, Note = "Q Cosmic Binding: slows first enemy, continues; stuns initial target if it hits a wall or another enemy; wall/second-hit stun missing" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.15f, SpeedBuff = true, Note = "W Caretaker's Shrine: shrine powers up, heals and speeds first ally touching it; approximated on self" },
				new SpellScript(SpellShape.Dash) { DashTiles = 12f, DmgMult = 0.1f, Note = "E Magical Journey: opens one-way portal through terrain for allies and enemies" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 2.5f, Flags = 2, Note = "R Tempered Fate: arcs to location, puts all units hit into stasis briefly (stun approximated)" },
			};
			t["Belveth"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 6f, Note = "Q Void Surge: dash in a direction, damages enemies passed through" },
				new SpellScript(SpellShape.Cone) { Flags = 3, SlowSec = 1.5f, Knockback = 0f, Note = "W Above and Below: tail slam in a line, damage, knock-up and slow" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2f, HitIntervalSec = 0.2f, Lifesteal = 0.25f, Shield = true, Note = "E Royal Maelstrom: roots herself and channels slashes on lowest-health enemy, lifesteal + damage reduction" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, HealFrac = 0.2f, Note = "R Endless Banquet: true form, bonus max health, attack range and attack speed" },
			};
			t["Blitzcrank"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Knockback = -10f, Note = "Q Rocket Grab: fires hand, grabs first opponent and drags them to him" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "W Overdrive: big move and attack speed, slowed afterward; no slow after" },
				new SpellScript(SpellShape.Buff) { Flags = 2, EmpowerNextAttack = true, EmpowerDmgMult = 2f, Note = "E Power Fist: next attack deals double damage and pops target up" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, Flags = 4, Note = "R Static Field: removes shields, damages and silences nearby enemies (passive lightning on attacked)" },
			};
			t["Brand"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 2, BurnSec = 4f, Note = "Q Sear: fireball, stuns if target ablaze, applies Blaze" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.8f, DurationSec = 0.4f, BurnSec = 4f, DmgMult = 1.2f, Note = "W Pillar of Flame: delayed pillar, +25% damage to ablaze targets" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, BurnSec = 4f, Chain = 4, Note = "E Conflagration: blast spreads to nearby enemies (doubled if ablaze)" },
				new SpellScript(SpellShape.Homing) { Count = 3, SpreadDeg = 30f, Chain = 4, Flags = 1, BurnSec = 4f, DmgMult = 0.5f, Note = "R Pyroclasm: fire bounces up to 5 times off nearby enemies, slows ablaze targets" },
			};
			t["Braum"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 1, SlowSec = 2f, Note = "Q Winter's Bite: ice from shield, slows and damages, applies Concussive Blows stack" },
				new SpellScript(SpellShape.Dash) { DashTiles = 8f, Shield = true, DmgMult = 0.1f, Note = "W Stand Behind Me: leap to ally, both gain armor and magic resist" },
				new SpellScript(SpellShape.Buff) { Shield = true, BlockSec = 3f, Note = "E Unbreakable: raises shield in a direction for several seconds, blocks projectiles (all directions here), first attack negated" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 3, SlowSec = 2f, WidthMult = 1.5f, TrailSec = 3f, Note = "R Glacial Fissure: slams ground knocking up nearby and in line, fissure slows afterward; no knock-up around Braum" },
			};
			t["Briar"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, Flags = 2, ArmorShredSec = 4f, Note = "Q Head Rush: leap to a unit and hit enemies, stun and break armor (shred duration 4s UNVERIFIED)" },
				new SpellScript(SpellShape.Dash) { DashTiles = 5f, SpeedBuff = true, Lifesteal = 0.1f, DmgMult = 0.5f, Note = "W Blood Frenzy: leap forward, frenzy pursuing nearest enemy with attack and move speed, area attacks; recast Snack Attack" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.6f, Flags = 1, Knockback = 8f, HealFrac = 0.1f, Shield = true, Note = "E Chilling Scream: channels with damage reduction and healing, then scream damages, slows and knocks back, wall collision stuns" },
				new SpellScript(SpellShape.Skillshot) { Flags = 4, Pierce = true, Note = "R Certain Death: kicks gem marking first champion hit, beelines to them, fears surrounding enemies, then hemomania" },
			};
			t["Caitlyn"] = new[] {
				new SpellScript(SpellShape.Beam) { Pierce = true, CastDelaySec = 1f, Note = "Q Piltover Peacemaker: revs 1s, penetrating shot, less damage to subsequent targets; no damage falloff" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 4f, Flags = 2, Note = "W Yordle Snap Trap: trap springs on enemy champion, reveals and roots 1.5s, empowers headshot" },
				new SpellScript(SpellShape.Skillshot) { Flags = 1, SlowSec = 1f, Note = "E 90 Caliber Net: net slows target, recoil knocks Caitlyn back; no recoil (framework cannot push caster)" },
				new SpellScript(SpellShape.Homing) { DmgMult = 3f, SpeedMult = 2f, RangeMult = 2f, CastDelaySec = 1f, Note = "R Ace in the Hole: channel then massive damage to a single target (1s delay UNVERIFIED), enemy champions can intercept; no interception" },
			};
		}
	}
}
