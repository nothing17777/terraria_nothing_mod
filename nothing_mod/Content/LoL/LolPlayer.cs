using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.LoL.Special;

namespace nothing_mod.Content.LoL
{
	public class LolKeybinds : ModSystem
	{
		public static ModKeybind[] Cast = new ModKeybind[4];
		public static ModKeybind MoveMode; // toggles right-click movement vs plain WASD
		static readonly string[] Names = { "SpellQ", "SpellW", "SpellE", "SpellR" };
		static readonly Keys[] Defaults = { Keys.Q, Keys.W, Keys.E, Keys.R };

		public override void Load() {
			for (int i = 0; i < 4; i++) Cast[i] = KeybindLoader.RegisterKeybind(Mod, Names[i], Defaults[i]);
			MoveMode = KeybindLoader.RegisterKeybind(Mod, "MoveMode", Keys.Y);
		}

		public override void Unload() { Cast = new ModKeybind[4]; MoveMode = null; }
	}

	public class LolPlayer : ModPlayer
	{
		public float[] Cooldown = new float[4];
		public static float RangePx(SpellDef sp) => MathHelper.Clamp(sp.Range / 1.5f, 200f, 1600f);
		public float[] CooldownMax = new float[4] { 1, 1, 1, 1 };
		public float Haste;
		public float Lifesteal;
		// The champion weapon that currently transforms the player: held one first, else first in inventory.
		public LolChampionItem Active;
		public const float SpellCooldown = 2f;
		public const int BonusMinionSlots = 10;

		// Carried champions retain their spells and bonuses, but other held items keep normal controls.
		bool UseChampionControls => Active != null &&
			(Player.HeldItem.IsAir || Player.HeldItem.ModItem is LolChampionItem);

		// Animation state read by LolModelLayer: sheet row (0 idle, 1 run, 2 attack, 3-6 spells) and frame.
		public int AnimRow, AnimFrame;
		float animClock, castTime;
		int castRow = -1;

		// Point-and-click: right-click sets a ground target; the player walks to it (League style).
		public Vector2? MoveTarget;
		int airTicks;
		// Dash state (all dashes): velocity is forced for a few ticks so it is not eaten by friction.
		int dashTicks; Vector2 dashVel;
		// Ahri R: recasts left and the time window to use them.
		public int RCharges; public float RWindow;

		public void StartDash(Vector2 dir, float speed, int ticks) {
			dashVel = dir * speed; dashTicks = ticks;
			Player.SetImmuneTimeForAllTypes(ticks + 4);
		}

		public override void PreUpdateMovement() {
			if (dashTicks > 0) { Player.velocity = dashVel; dashTicks--; }
		}
		public bool ClickMove = true; // true: right-click to move (A/D disabled); false: normal WASD
		// Slot (0-3) whose key is currently held -> HUD draws its range indicator; -1 = none.
		public int AimSlot = -1;

		// damage multiplier per slot Q W E R (UNVERIFIED balance: real spell numbers are not in the client JSON)
		static readonly float[] Tier = { 1.5f, 1.2f, 1.2f, 3f };

		public override void ResetEffects() { Haste = 0; Lifesteal = 0; }

		public override void PostUpdateEquips() {
			Active = Player.HeldItem.ModItem as LolChampionItem;
			if (Active == null)
				for (int n = 0; n < 50; n++)
					if (Player.inventory[n].ModItem is LolChampionItem c) { Active = c; break; }
			// Apply once for the active champion; inventory copies do not stack the bonus.
			if (Active != null) Player.maxMinions += BonusMinionSlots;
		}

		public override void PostUpdate() {
			for (int i = 0; i < 4; i++)
				if (Cooldown[i] > 0) Cooldown[i] = Math.Max(0, Cooldown[i] - 1f / 60f);
			if (RWindow > 0) { RWindow -= 1f / 60f; if (RWindow <= 0 && RCharges > 0) { RCharges = 0; Cooldown[3] = CooldownMax[3] = SpellCooldown; } }
			UpdateAnimation();
			UpdateScriptEffects();
			if (Player.whoAmI == Main.myPlayer && Active?.Name != loggedActive) {
				loggedActive = Active?.Name;
				Mod.Logger.Info($"[LoL] active champion: {loggedActive ?? "none"}");
			}
		}
		string loggedActive;
		// effects driven by spell scripts
		public int BlockTicks, EmpowerTicks, EmpowerFlags; public float EmpowerMult = 1f;
		int recastSlot = -1; float recastTime;
		SpellScript lastRecastScript;

		void UpdateScriptEffects() {
			if (BlockTicks > 0) {
				BlockTicks--;
				foreach (Projectile pr in Main.projectile)
					if (pr.active && pr.hostile && !pr.friendly && Vector2.Distance(pr.Center, Player.Center) < 70f) pr.Kill();
			}
			if (EmpowerTicks > 0) EmpowerTicks--;
			if (recastSlot >= 0 && (recastTime -= 1f / 60f) <= 0) { Cooldown[recastSlot] = CooldownMax[recastSlot] = SpellCooldown; recastSlot = -1; }
		}

		void UpdateAnimation() {
			const float CastLength = 0.55f;
			animClock += 1f / 60f;
			int nf = LolAnimData.Sheets.TryGetValue(Active?.ChampionKey ?? "", out AnimDef ad) ? ad.Frames : 8;
			if (castTime > 0) {
				AnimRow = castRow;
				AnimFrame = Math.Min(nf - 1, (int)((1f - castTime / CastLength) * nf));
				castTime -= 1f / 60f;
			}
			else if (attackAnim > 0) {
				AnimRow = 2;
				AnimFrame = Math.Min(nf - 1, (int)((1f - attackAnim / 0.4f) * nf));
			}
			else if (Math.Abs(Player.velocity.X) > 0.3f) { AnimRow = 1; AnimFrame = (int)(animClock * 12f * nf / 8f) % nf; }
			else { AnimRow = 0; AnimFrame = (int)(animClock * 6f * nf / 8f) % nf; }
		}

		public override void ProcessTriggers(TriggersSet triggersSet) {
			AimSlot = -1;
			if (UseChampionControls && ClickMove) { // Other held items and WASD mode keep vanilla controls
				// vanilla binds that share keys with Q/W/E/R: W = Up, E = Grapple, R = Mount (also Quick Heal/Mana/Buff, Smart Select)
				foreach (string vanilla in new[] { "Up", "Grapple", "QuickMount", "QuickHeal", "QuickMana", "QuickBuff", "SmartSelect" })
					if (triggersSet.KeyStatus.ContainsKey(vanilla)) triggersSet.KeyStatus[vanilla] = false;
			}
			if (Active != null && LolKeybinds.MoveMode != null && LolKeybinds.MoveMode.JustPressed) {
				ClickMove = !ClickMove; MoveTarget = null;
				Main.NewText(ClickMove ? "[LoL] Movement: RIGHT-CLICK to move (A/D disabled)" : "[LoL] Movement: WASD", Color.LightGreen);
			}
			for (int i = 0; i < 4; i++) {
				var key = LolKeybinds.Cast[i];
				if (key == null) continue;
				if (Player.dead || Active is not LolChampionItem champ) {
					if (key.JustPressed && !Player.dead) Main.NewText("[LoL] No champion weapon in your inventory (first 50 slots).", Color.Orange);
					continue;
				}
				if (key.JustPressed) Mod.Logger.Info($"[LoL] key {i} pressed, cooldown {Cooldown[i]:0.0}, champion {champ.Name}");
				if (Cooldown[i] > 0) continue;
				if (key.Current) AimSlot = i;            // hold = show range
				if (key.JustReleased) {                  // release = cast at the cursor
					SpellDef sp = champ.Def.Spells[i + 1];
					Main.NewText($"[LoL] {sp.Key} ({LolKeyLabel.Of(i)}): {sp.Name}", champ.Def.EffectColor);
					Cast(champ, sp, i);
				}
			}
		}

		// ---- basic attack (League style): left-click attacks toward the cursor, right-click on an enemy walks into range and keeps attacking it
		int attackCd; float attackAnim;
		NPC attackTarget;
		const int AttackTicks = 36;

		void AutoAttack(Vector2 at) {
			if (attackCd > 0 || Active == null) return;
			attackCd = AttackTicks; attackAnim = 0.4f;
			Vector2 dir = at - Player.Center;
			if (dir == Vector2.Zero) dir = new Vector2(Player.direction, 0);
			dir.Normalize();
			Player.direction = dir.X >= 0 ? 1 : -1;
			ChampionDef d = Active.Def;
			int dmg = Player.GetWeaponDamage(Active.Item);
			float flags = 0f;
			if (EmpowerTicks > 0) { dmg = (int)(dmg * EmpowerMult); flags = EmpowerFlags; EmpowerTicks = 0; } // spell-empowered attack
			var src = Player.GetSource_ItemUse(Active.Item);
			int type = ModContent.ProjectileType<LolAbilityProjectile>();
			if (d.Magic == 1 || d.Ranged == 1) {
				Projectile.NewProjectile(src, Player.Center, dir * 12f + Player.velocity, type, dmg, Active.Item.knockBack, Player.whoAmI, 0f, d.Hue * 8f + flags, 50);
				Terraria.Audio.SoundEngine.PlaySound(SoundID.Item8, Player.Center);
			}
			else { // melee: short sweeping cone in front
				Projectile.NewProjectile(src, Player.Center, dir * 4f + Player.velocity, type, dmg, Active.Item.knockBack, Player.whoAmI, 3f, d.Hue * 8f + flags, 100 * 1000);
				Terraria.Audio.SoundEngine.PlaySound(SoundID.Item1, Player.Center);
			}
		}

		float AttackRangePx() => Active != null && (Active.Def.Magic == 1 || Active.Def.Ranged == 1) ? 520f : 90f;

		void BasicAttackControls() {
			if (attackCd > 0) attackCd--;
			if (attackAnim > 0) attackAnim -= 1f / 60f;
			if (Main.playerInventory || Player.mouseInterface) return;
			if (Main.mouseLeft) { // we handle attacking ourselves: stop the held vanilla item from also firing
				Player.controlUseItem = false;
				attackTarget = null;
				AutoAttack(Main.MouseWorld);
			}
			if (Main.mouseRight && ClickMove) {
				NPC best = null; float bd = 48f;
				foreach (NPC n in Main.npc) {
					if (!n.active || n.friendly || n.dontTakeDamage || n.life <= 0) continue;
					float dist = n.Hitbox.Distance(Main.MouseWorld);
					if (dist < bd) { bd = dist; best = n; }
				}
				if (best != null) attackTarget = best;
			}
			if (attackTarget != null) {
				if (!attackTarget.active || attackTarget.life <= 0 || attackTarget.friendly) attackTarget = null;
				else if (Vector2.Distance(attackTarget.Center, Player.Center) <= AttackRangePx()) { MoveTarget = null; AutoAttack(attackTarget.Center); }
				else MoveTarget = attackTarget.Center;
			}
		}

		public override void SetControls() {
			if (Player.whoAmI == Main.myPlayer && UseChampionControls && !Player.dead) BasicAttackControls(); else attackTarget = null;
			if (Player.whoAmI != Main.myPlayer || !UseChampionControls || Player.dead) { MoveTarget = null; return; }
			// W = up (flight), E = grapple, R = mount in vanilla: force them off after the engine has read the keyboard
			if (!ClickMove) { MoveTarget = null; return; } // WASD mode: normal Terraria movement and flight
			Player.controlUp = false; Player.controlHook = false; Player.controlMount = false;
			Player.controlLeft = Player.controlRight = false; // click mode: A/D off, right-click walks
			if (Main.mouseRight && attackTarget == null && !Player.mouseInterface && !Main.playerInventory) MoveTarget = Main.MouseWorld;
			if (MoveTarget is not Vector2 t) return;
			float dx = t.X - Player.Center.X, dy = t.Y - Player.Center.Y;
			bool canFly = Player.wingsLogic > 0 && Player.wingTime > 0; // has wings with flight time left
			bool grounded = Player.velocity.Y == 0;
			airTicks = grounded ? 0 : airTicks + 1;
			// arrival: walkers only care about x, flyers about x and y
			if (Math.Abs(dx) < 12f && (!canFly || Math.Abs(dy) < 28f)) { MoveTarget = null; return; }
			if (Math.Abs(dx) >= 12f) { if (dx > 0) Player.controlRight = true; else Player.controlLeft = true; }
			if (canFly) {
				// fly straight at the target: climb while it is above, hold altitude near it, glide down when below
				bool wantUp = dy < -28f || (Math.Abs(dy) <= 28f && Player.velocity.Y > 0.5f);
				if (wantUp) {
					// Terraria only starts wing flight on a fresh jump press while airborne, so release jump for a few ticks after leaving the ground
					Player.controlJump = !(airTicks >= 1 && airTicks <= 4);
				}
				else if (dy > 28f) Player.controlDown = true;
			}
			else {
				bool stuck = grounded && Math.Abs(Player.velocity.X) < 0.4f;
				bool targetAbove = dy < -40f && Math.Abs(dx) < 60f;
				if (stuck || targetAbove) Player.controlJump = true;
			}
		}

		static RealSpell Real(LolChampionItem champ, int slot) =>
			LolSpellData.Spells.TryGetValue(champ.ChampionKey, out RealSpell[] a) && slot < a.Length ? a[slot] : null;

		void Cast(LolChampionItem champ, SpellDef sp, int slot) {
			Mod.Logger.Info($"[LoL] CAST {sp.Key} {sp.Name} arch={sp.Arch} range={sp.Range}");
			RealSpell rs = Real(champ, slot);
			int ri = slot == 3 ? 1 : 2; // representative rank: Q/W/E rank 3, R rank 2 (the mod has no levelling)
			float cd = SpellCooldown; // every spell: flat 2 s cooldown (user request)
			Cooldown[slot] = CooldownMax[slot] = cd;
			castRow = 3 + slot; castTime = 0.55f;

			Vector2 mouse = Main.MouseWorld;
			Vector2 dir = mouse - Player.Center;
			if (dir == Vector2.Zero) dir = new Vector2(Player.direction, 0);
			dir.Normalize();
			float rangePx = RangePx(sp);
			// real base damage + ratio*100 (a typical stat), relative to a 110 reference spell: keeps every spell's real strength ordering
			float mult = rs != null && rs.Dmg[ri] + rs.Ratio * 100f > 0 ? MathHelper.Clamp((rs.Dmg[ri] + rs.Ratio * 100f) / 110f, 0.5f, 6f) : Tier[slot];
			int dmg = (int)(Player.GetWeaponDamage(champ.Item) * mult);
			float kb = champ.Item.knockBack;
			float aiFlags = sp.Flags + champ.Def.Hue * 8f;
			var src = Player.GetSource_ItemUse(champ.Item);
			int type = ModContent.ProjectileType<LolAbilityProjectile>();
			int owner = Player.whoAmI;

			if (LolSpecial.TryCast(this, champ, sp, slot, dir, mouse, dmg, kb, src, out float specialCd)) {
				Cooldown[slot] = CooldownMax[slot] = specialCd;
				LolSound.Play(champ.ChampionKey, slot, "cast", Player.Center); // Ahri's real cast sounds
				return;
			}

			SpellScript script = LolScripts.Get(champ.ChampionKey, slot);
			if (script != null) { // per-champion script (Content/LoL/Scripts): shape from the script, numbers from the real spell data
				SpellScript use = script;
				if (script.RecastShape != null) {
					if (recastSlot == slot && recastTime > 0) { // second press: play the recast shape, then the normal cooldown
						use = (SpellScript)script.GetType().GetMethod("MemberwiseClone", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(script, null);
						use.Shape = script.RecastShape.Value; use.RecastShape = null;
						recastSlot = -1;
						Cooldown[slot] = CooldownMax[slot] = SpellCooldown;
					}
					else { recastSlot = slot; recastTime = 4f; Cooldown[slot] = CooldownMax[slot] = 0.35f; }
				}
				LolScriptCaster.Cast(this, champ, use, rs, slot, dir, mouse, dmg, kb, src, rangePx);
				return;
			}

			if (sp.Heal == 1) {
				int heal = (int)(Player.statLifeMax2 * (slot == 3 ? 0.15f : 0.06f));
				Player.Heal(heal);
				Player.AddBuff(BuffID.Ironskin, 60 * 4);
			}

			// real data decides the shape unless the text says dash/heal: missile -> skillshot, range+radius -> area at cursor, cone, else self burst
			int arch = sp.Arch;
			if (rs != null && arch != 1 && arch != 5) {
				if (rs.Speed > 0 && rs.Range > 0 && (rs.Width > 0 || rs.CastType != 3)) arch = 0;
				else if (rs.ConeAngle > 0 && rs.Cone > 100) arch = 2;
				else if (rs.Range > 250 && rs.Radius > 0 && rs.Speed == 0) arch = 4;
				else if (rs.Range <= 250 && rs.Radius > 0) arch = 3;
			}
			int aoe = rs != null && rs.Radius > 0 ? (int)MathHelper.Clamp(rs.Radius * 2f / 1.5f, 90f, 560f) : 0; // real area diameter in px
			int coneSize = rs != null && rs.Cone > 100 ? (int)MathHelper.Clamp(rs.Cone / 1.5f, 90f, 420f) : 0;
			if (rs != null && rs.CcDur > 0) aiFlags += 1000f * (int)Math.Min(90f, rs.CcDur * 10f); // real CC duration in tenths of a second
			switch (arch) {
				case 1: // dash
					StartDash(dir, Math.Min(16f, rangePx / 12f + 8f), 10);
					Projectile.NewProjectile(src, Player.Center, Vector2.Zero, type, dmg, kb, owner, 1f + 16f * 0, aiFlags + 0f);
					break;
				case 2: // cone sweep
					Projectile.NewProjectile(src, Player.Center, dir * 6f + Player.velocity, type, dmg, kb, owner, 3f, aiFlags, coneSize * 1000);
					break;
				case 3: // burst around self
					Projectile.NewProjectile(src, Player.Center, Vector2.Zero, type, dmg, kb, owner, 1f, aiFlags, aoe * 1000);
					break;
				case 4: { // burst at cursor
					Vector2 off = mouse - Player.Center;
					if (off.Length() > rangePx) off = dir * rangePx;
					Projectile.NewProjectile(src, Player.Center + off, Vector2.Zero, type, dmg, kb, owner, 2f, aiFlags, aoe * 1000);
					break;
				}
				case 5: // pure heal/shield: already applied above (Heal flag is always set for these)
					if (sp.Heal == 0) Player.AddBuff(BuffID.Regeneration, 60 * 6);
					break;
				default: { // skillshot: real missile speed and width, lifetime = range / speed
					float v = rs != null && rs.Speed > 0 ? MathHelper.Clamp(rs.Speed / 150f, 5f, 36f) : Math.Max(4f, rangePx / 60f);
					int life = Math.Min(999, (int)Math.Ceiling(rangePx / v));
					int size = rs != null && rs.Width > 0 ? (int)MathHelper.Clamp(rs.Width / 4f, 16f, 120f) : 0;
					Projectile.NewProjectile(src, Player.Center, dir * v + Player.velocity, type, dmg, kb, owner, 0f, aiFlags, size * 1000 + life); // inherits the caster's motion so range holds while moving
					break;
				}
			}
			if (!LolSound.Play(champ.ChampionKey, slot, "cast", Player.Center))
				Terraria.Audio.SoundEngine.PlaySound(SoundID.Item20, Player.Center);
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => Steal(damageDone);
		public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone) => Steal(damageDone);

		void Steal(int damageDone) {
			if (Lifesteal <= 0) return;
			int heal = (int)(damageDone * Lifesteal * 0.2f);
			if (heal > 0) Player.Heal(Math.Min(heal, 20));
		}
	}
}
