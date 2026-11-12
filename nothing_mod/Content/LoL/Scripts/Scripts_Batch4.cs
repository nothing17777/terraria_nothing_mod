using System.Collections.Generic;

namespace nothing_mod.Content.LoL
{
	public static class Scripts_Batch4
	{
		public static void Register(Dictionary<string, SpellScript[]> t) {
			t["Karthus"] = new[] {
				new SpellScript(SpellShape.Zone) { DelaySec = 0.9f, DurationSec = 0.2f, Note = "Q Lay Waste: delayed blast at target area" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.2f, DurationSec = 5f, HitIntervalSec = 1f, Flags = 1, DmgMult = 0.1f, Note = "W Wall of Pain: lingering slowing wall (approximated as slow zone)" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 4f, HitIntervalSec = 0.5f, DmgMult = 0.6f, Note = "E Defile: toggled damage aura around caster" },
				new SpellScript(SpellShape.Zone) { DelaySec = 3f, DurationSec = 0.3f, Note = "R Requiem: 3s channel then global damage (large area after delay)" },
			};
			t["Kassadin"] = new[] {
				new SpellScript(SpellShape.Homing) { Pierce = false, Note = "Q Null Sphere: targeted bolt + shield" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, DmgMult = 0.6f, Note = "W Nether Blade: empowered next attack, approximated as short burst around caster" },
				new SpellScript(SpellShape.Cone) { Flags = 1, Note = "E Force Pulse: cone that slows" },
				new SpellScript(SpellShape.Dash) { DashTiles = 31f, Note = "R Riftwalk: short-range teleport then AoE burst" },
			};
			t["Katarina"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Note = "Q Bouncing Blade: dagger thrown, bounces and returns" },
				new SpellScript(SpellShape.SelfBurst) { SpeedBuff = true, DurationSec = 0.5f, DmgMult = 0.2f, Note = "W Preparation: toss dagger up, speed boost" },
				new SpellScript(SpellShape.Dash) { DashTiles = 40f, Note = "E Shunpo: blink toward target, dagger damage" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2.5f, HitIntervalSec = 0.2f, DmgMult = 0.25f, Note = "R Death Lotus: channeled blades hitting nearby enemies" },
			};
			t["Kayle"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 1, Note = "Q Radiant Blast: piercing slowing wave" },
				new SpellScript(SpellShape.Buff) { HealFrac = 0.15f, SpeedBuff = true, Note = "W Celestial Blessings: heal and speed" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 5f, HitIntervalSec = 0.5f, DmgMult = 0.4f, Note = "E Starfire Spellblade: passive on-hit magic damage, approximated as aura" },
				new SpellScript(SpellShape.Buff) { Shield = true, HealFrac = 0.1f, Note = "R Divine Judgment: invulnerability (shield) for caster; ally part and wave not modelled" },
			};
			t["Kayn"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 15f, Note = "Q Reaping Slash: dash and slash around self" },
				new SpellScript(SpellShape.Beam) { Flags = 1 | 2, Note = "W Blade's Reach: delayed line slash that knocks up/slows" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "E Shadow Step: walk through walls with speed" },
				new SpellScript(SpellShape.Dash) { DashTiles = 20f, DmgMult = 1.5f, Note = "R Umbral Trespass: leap onto target, big damage" },
			};
			t["Kennen"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Note = "Q Thundering Shuriken: piercing shuriken" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, DmgMult = 0.8f, Note = "W Electrical Surge: damages marked enemies in range, approximated as burst" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, SpeedBuff = true, Note = "E Lightning Rush: electric dash, burst on caster" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 3f, HitIntervalSec = 0.5f, Flags = 2, DmgMult = 0.5f, Note = "R Slicing Maelstrom: storm around caster for 3s, stuns on repeat hits" },
			};
			t["Khazix"] = new[] {
				new SpellScript(SpellShape.Cone) { Note = "Q Taste Their Fear: melee slash in front" },
				new SpellScript(SpellShape.Skillshot) { Count = 3, SpreadDeg = 40f, Flags = 1, Note = "W Void Spike: three spikes in a cone, slow" },
				new SpellScript(SpellShape.Dash) { DashTiles = 28f, Note = "E Leap: jump to target area, damage on landing" },
				new SpellScript(SpellShape.Buff) { SpeedBuff = true, Note = "R Void Assault: stealth + speed, approximated as speed buff" },
			};
			t["Kindred"] = new[] {
				new SpellScript(SpellShape.Dash) { DashTiles = 14f, Note = "Q Dance of Arrows: short dash, fires arrows" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 8f, HitIntervalSec = 0.7f, DmgMult = 0.4f, Note = "W Wolf's Frenzy: wolf zone around caster damaging enemies" },
				new SpellScript(SpellShape.Homing) { Flags = 1, Note = "E Mounting Dread: targeted slowing shot" },
				new SpellScript(SpellShape.Buff) { Shield = true, HealFrac = 0.3f, Note = "R Lamb's Respite: zone where allies cannot die, approximated as shield + heal" },
			};
			t["Kled"] = new[] {
				new SpellScript(SpellShape.Boomerang) { Flags = 2, Note = "Q Beartrap on a Rope: thrown trap, pulls target back" },
				new SpellScript(SpellShape.Cone) { DmgMult = 1.2f, Note = "W Violent Tendencies: empowered attacks, approximated as cone slashes" },
				new SpellScript(SpellShape.Dash) { DashTiles = 22f, Note = "E Jousting: dash through enemies, recast to dash again" },
				new SpellScript(SpellShape.Dash) { DashTiles = 70f, SpeedBuff = true, Shield = true, Note = "R Chaaaaaaaarge!!!: very long mounted charge toward cursor" },
			};
			t["KogMaw"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Note = "Q Caustic Spittle: acid missile that shreds armor" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 8f, HitIntervalSec = 0.4f, DmgMult = 0.5f, Note = "W Bio-Arcane Barrage: range boost with %HP on-hit, approximated as aura" },
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 1, Note = "E Void Ooze: piercing ooze leaving slowing trail (slow only)" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.8f, DurationSec = 0.2f, Note = "R Living Artillery: long-range delayed artillery blast" },
			};
			t["Leblanc"] = new[] {
				new SpellScript(SpellShape.Homing) { Note = "Q Sigil of Malice: targeted bolt marks and detonates" },
				new SpellScript(SpellShape.Dash) { DashTiles = 28f, Note = "W Distortion: blink toward target dealing damage on arrival" },
				new SpellScript(SpellShape.Skillshot) { Flags = 2 | 4, Note = "E Ethereal Chains: chain missile that roots" },
				new SpellScript(SpellShape.Dash) { DashTiles = 28f, DmgMult = 1.5f, Note = "R Mimic: repeats last spell, approximated as stronger blink" },
			};
			t["LeeSin"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Note = "Q Sonic Wave: skillshot, recast dashes to target" },
				new SpellScript(SpellShape.Dash) { DashTiles = 28f, Shield = true, Note = "W Safeguard: dash to ally/ward with shield" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Flags = 1, Note = "E Tempest: AoE burst around caster that reveals/slows" },
				new SpellScript(SpellShape.Dash) { DashTiles = 10f, Flags = 2, DmgMult = 1.2f, Note = "R Dragon's Rage: roundhouse kick knocking enemy back" },
			};
			t["Leona"] = new[] {
				new SpellScript(SpellShape.Cone) { Flags = 2, Note = "Q Shield of Daybreak: empowered strike that stuns" },
				new SpellScript(SpellShape.SelfBurst) { Shield = true, DurationSec = 3f, HitIntervalSec = 3f, Note = "W Eclipse: damage reduction, explodes after 3s" },
				new SpellScript(SpellShape.Dash) { DashTiles = 25f, Flags = 2, Note = "E Zenith Blade: dash to target in line, root" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.9f, DurationSec = 0.2f, Flags = 1 | 2, Note = "R Solar Flare: delayed area, stuns center, slows edge" },
			};
			t["Lillia"] = new[] {
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Flags = 1, Note = "Q Blooming Blows: spin damage around caster, outer edge true damage" },
				new SpellScript(SpellShape.Zone) { DelaySec = 1f, DurationSec = 0.3f, Flags = 1 | 4, Note = "W Watch Out! Eep!: delayed big blast at target, slow/sleep center" },
				new SpellScript(SpellShape.Skillshot) { Pierce = false, Flags = 1, Note = "E Swirlseed: thrown seed that rolls and bursts" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 2.5f, HitIntervalSec = 2.5f, Flags = 4, DmgMult = 0.8f, Note = "R Lilting Lullaby: sleeps nearby enemies after delay, approximated as delayed-style aura" },
			};
			t["Lissandra"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 1, Note = "Q Ice Shard: piercing shard, slows on hit" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.4f, Flags = 2, Note = "W Ring of Frost: AoE root around caster" },
				new SpellScript(SpellShape.Dash) { DashTiles = 30f, Note = "E Glacial Path: claw flies out, recast teleports to it" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 1.5f, HitIntervalSec = 1.5f, Flags = 2, Shield = true, Note = "R Frozen Tomb: ice around target/self then AoE damage and stun" },
			};
			t["Locke"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Flags = 2, Note = "Q: marking missile that roots" },
				new SpellScript(SpellShape.Buff) { Shield = true, HealFrac = 0.1f, Note = "W: restore/defensive ability, approximated as shield + heal" },
				new SpellScript(SpellShape.Cone) { Note = "E: forward cone strike" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 0.2f, Flags = 2, Note = "R: large area burst with stun" },
			};
			t["Lucian"] = new[] {
				new SpellScript(SpellShape.Beam) { Note = "Q Piercing Light: instant piercing line" },
				new SpellScript(SpellShape.Skillshot) { Pierce = false, Flags = 1, Note = "W Ardent Blaze: shot that marks, explodes and grants speed" },
				new SpellScript(SpellShape.Dash) { DashTiles = 17f, Note = "E Relentless Pursuit: short dash toward cursor" },
				new SpellScript(SpellShape.Skillshot) { Count = 6, SpreadDeg = 8f, Pierce = true, DmgMult = 0.5f, Note = "R The Culling: rapid volley of piercing shots" },
			};
			t["Lulu"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Count = 2, SpreadDeg = 10f, Pierce = true, Flags = 1, Note = "Q Glitterlance: two bolts in a line, slow" },
				new SpellScript(SpellShape.Homing) { Flags = 4, SpeedBuff = true, Note = "W Whimsy: polymorph an enemy / speed an ally" },
				new SpellScript(SpellShape.Buff) { Shield = true, Note = "E Help, Pix!: shield an ally, damage enemy" },
				new SpellScript(SpellShape.SelfBurst) { DurationSec = 0.5f, HealFrac = 0.2f, Flags = 2, DmgMult = 0.1f, Note = "R Wild Growth: knock-up nearby enemies, bonus health" },
			};
			t["Lux"] = new[] {
				new SpellScript(SpellShape.Skillshot) { Pierce = true, Flags = 2, Note = "Q Light Binding: root bolt hitting up to two enemies" },
				new SpellScript(SpellShape.Boomerang) { Shield = true, Note = "W Prismatic Barrier: barrier thrown out and back, shields allies" },
				new SpellScript(SpellShape.Zone) { DelaySec = 0.5f, DurationSec = 5f, HitIntervalSec = 5f, Flags = 1, Note = "E Lucent Singularity: orb slows, detonates on recast or after 5s" },
				new SpellScript(SpellShape.Beam) { Note = "R Final Spark: huge delayed laser" },
			};
		}
	}
}
