using System;
using System.Collections.Generic;
using System.Reflection;

namespace nothing_mod.Content.LoL
{
	public enum SpellShape
	{
		Skillshot,  // straight missile (real speed/width/range from LolSpellData)
		Homing,     // missile that steers to the nearest enemy
		Zone,       // area at the cursor; optional DelaySec warning before it detonates, then lingers DurationSec
		SelfBurst,  // area around the caster, lingers DurationSec (follows the caster)
		Cone,       // sweeping cone in front of the caster
		Beam,       // instant line of the spell's range and width
		Boomerang,  // flies out, then returns to the caster (hits both ways)
		Dash,       // caster dashes DashTiles toward the cursor, damaging along the way
		Buff,       // no projectile: heal / shield / speed on the caster
		Orbit,      // projectiles circling the caster for DurationSec
	}

	// Declarative description of how one champion spell plays. Numbers that exist in the game data (range, speed, width, radius,
	// damage, cooldown, CC duration) come from LolSpellData automatically; a script only adds the SHAPE and behaviour.
	public class SpellScript
	{
		public SpellShape Shape;
		public int Count = 1;            // projectiles per cast (fan / orbit count)
		public float SpreadDeg;          // total fan angle for Count > 1
		public bool Pierce;              // missile passes through enemies
		public float DelaySec;           // Zone: warning time before detonation
		public float DurationSec;        // Zone / SelfBurst / Orbit: how long it lingers
		public float HitIntervalSec = 0.5f; // lingering effects: seconds between hits on the same enemy
		public float DashTiles;          // Dash: distance in tiles (1 tile = 16 px)
		public float HealFrac;           // fraction of max life healed
		public bool Shield;              // grants damage reduction for ~3 s
		public bool SpeedBuff;           // grants Swiftness for ~3 s
		public int Flags;                // 1 slow, 2 stun/root/knock-up, 4 fear/charm/silence
		public float DmgMult = 1f;       // multiplier on the computed spell damage
		public float SpeedMult = 1f, WidthMult = 1f, RangeMult = 1f;
		// extra effects (apply to enemies hit unless noted)
		public float Knockback;          // px/tick pushed away from the caster (negative = pulled toward the caster)
		public float BurnSec, PoisonSec, BleedSec; // damage over time (OnFire / Venom / Bleeding)
		public float Lifesteal;          // fraction of damage dealt healed to the caster (omnivamp / drain)
		public int Chain;                // extra enemies the hit jumps to (60% damage each)
		public float InvisibleSec;       // caster turns invisible (stealth)
		public float SlowSec;            // slow duration override when Flags has 1
		// more effects
		public float CastDelaySec;       // Skillshot / Homing / Boomerang / Beam: wind-up before the missile or beam fires (shown as a warning)
		public int PierceLimit;          // Skillshot: stops after this many enemies (0 = use Pierce true/false)
		public float LowHealthBonus;     // extra damage fraction at 0% target health (scales linearly): 1 = up to +100% on a nearly dead enemy
		public float ExecuteFrac;        // enemies below this life fraction die (non-bosses), e.g. 0.2
		public float ArmorShredSec;      // armor / resistance reduction on enemies hit (Ichor)
		public float InvulnSec;          // caster cannot be hurt (untargetable / stasis / death prevention)
		public float BlockSec;           // caster destroys hostile projectiles that touch them (spell shield / wind wall / reflect)
		public bool Cleanse;             // removes the caster's debuffs
		public float TrailSec;           // Skillshot: leaves a damaging zone behind that lingers this long
		public SpellShape? RecastShape;  // pressing the key again within 4 s plays this shape instead (no extra cooldown); null = no recast
		public bool EmpowerNextAttack;   // the next basic attack within 6 s is stronger and carries this spell's Flags
		public float EmpowerDmgMult = 1.6f;
		public string Note;              // one line: what the real spell does, for future work

		public SpellScript(SpellShape shape) { Shape = shape; }
	}

	// Registry: every static class named Scripts_* in this assembly with `public static void Register(Dictionary<string, SpellScript[]> table)`
	// is called once. Keys are champion aliases (e.g. "Annie"); the array has 4 entries Q, W, E, R (null = keep the generic spell).
	public static class LolScripts
	{
		static readonly Dictionary<string, SpellScript[]> Table = Build();

		static Dictionary<string, SpellScript[]> Build() {
			var t = new Dictionary<string, SpellScript[]>();
			foreach (Type ty in typeof(LolScripts).Assembly.GetTypes()) {
				if (!ty.Name.StartsWith("Scripts_") || !ty.IsAbstract || !ty.IsSealed) continue;
				MethodInfo m = ty.GetMethod("Register", BindingFlags.Public | BindingFlags.Static);
				m?.Invoke(null, new object[] { t });
			}
			return t;
		}

		public static SpellScript Get(string alias, int slot) =>
			Table.TryGetValue(alias, out SpellScript[] a) && slot < a.Length ? a[slot] : null;
	}
}
