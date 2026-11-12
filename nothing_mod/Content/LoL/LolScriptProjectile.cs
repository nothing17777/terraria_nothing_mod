using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.LoL
{
	// One projectile type that plays every SpellScript shape. ai0 = SpellShape, ai1 = slot, ai2 = size px * 1000 + lifetime ticks.
	public class LolScriptProjectile : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.AmethystBolt;

		SpellShape Shape => (SpellShape)(int)Projectile.ai[0];
		int Slot => (int)Projectile.ai[1];
		int Size => (int)Projectile.ai[2] / 1000;
		Player Owner => Main.player[Projectile.owner];
		LolChampionItem Champ => Owner.GetModPlayer<LolPlayer>().Active;
		string Alias => Champ?.Name.Replace("Champion", "");
		SpellScript Script => Alias == null ? null : LolScripts.Get(Alias, Slot);
		RealSpell Real => Alias != null && LolSpellData.Spells.TryGetValue(Alias, out RealSpell[] a) && Slot < a.Length ? a[Slot] : null;
		Color Col => Champ?.Def.EffectColor ?? Color.White;
		int timer;
		float outDist;
		int windup; Vector2 launchVel;

		public override void SetDefaults() {
			Projectile.width = Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 60;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override void OnSpawn(Terraria.DataStructures.IEntitySource source) {
			Projectile.DamageType = (Champ?.Item ?? Owner.HeldItem).DamageType;
			int life = (int)Projectile.ai[2] % 1000;
			if (life > 0) Projectile.timeLeft = life;
			SpellScript sc = Script;
			int size = Math.Max(16, Size);
			Vector2 c = Projectile.Center;
			Projectile.width = Projectile.height = size;
			Projectile.Center = c;
			if (sc != null) Projectile.localNPCHitCooldown = Math.Max(6, (int)(sc.HitIntervalSec * 60f));
			if (sc != null && sc.CastDelaySec > 0 && Shape is SpellShape.Skillshot or SpellShape.Homing or SpellShape.Boomerang or SpellShape.Beam) {
				windup = (int)(sc.CastDelaySec * 60f);
				launchVel = Projectile.velocity;
				if (Shape != SpellShape.Beam) Projectile.velocity = Vector2.Zero;
				Projectile.timeLeft += windup;
				Projectile.friendly = false;
			}
			switch (Shape) {
				case SpellShape.Skillshot:
				case SpellShape.Homing:
					Projectile.penetrate = sc != null && sc.PierceLimit > 0 ? sc.PierceLimit : sc != null && sc.Pierce ? -1 : 1;
					Projectile.tileCollide = true;
					break;
				case SpellShape.Boomerang: Projectile.localNPCHitCooldown = 10000; break; // every enemy once per leg (reset on return)
				case SpellShape.Zone:
					Projectile.friendly = sc == null || sc.DelaySec <= 0;
					break;
				case SpellShape.Cone: Projectile.localNPCHitCooldown = 20; break;
			}
		}

		public override void AI() {
			timer++;
			SpellScript sc = Script;
			Player o = Owner;
			if (windup > 0) { // wind-up: the missile waits at the caster, then launches
				if (timer < windup) {
					if (Shape != SpellShape.Beam) Projectile.Center = o.Center;
					return;
				}
				if (timer == windup) { Projectile.friendly = true; if (Shape != SpellShape.Beam) Projectile.velocity = launchVel; }
				timer -= windup; windup = -1; // continue as if just spawned
			}
			if (windup < 0 && Projectile.friendly == false) Projectile.friendly = true;
			if (sc != null && sc.TrailSec > 0 && Shape == SpellShape.Skillshot && Projectile.localAI[0] == 0 && timer % 8 == 0 && Main.myPlayer == Projectile.owner) {
				int zi = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, Projectile.type, Math.Max(1, Projectile.damage / 3), 0f, Projectile.owner,
					(float)SpellShape.Zone, Slot, Math.Max(40, Size * 2) * 1000 + Math.Min(999, (int)(sc.TrailSec * 60f)));
				Main.projectile[zi].localAI[0] = 2; // trail piece: no on-hit extras
			}
			switch (Shape) {
				case SpellShape.Skillshot:
					Projectile.rotation = Projectile.velocity.ToRotation();
					Glow(1);
					break;
				case SpellShape.Homing: {
					float spd = Math.Max(6f, Projectile.velocity.Length());
					NPC t = Nearest(Projectile.Center, 700f);
					if (t != null) Projectile.velocity = Vector2.Lerp(Projectile.velocity, (t.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * spd, 0.12f);
					Projectile.rotation = Projectile.velocity.ToRotation();
					Glow(1);
					break;
				}
				case SpellShape.Boomerang: {
					int outTicks = Math.Max(1, (int)Projectile.ai[2] % 1000);
					if (timer == 1) outDist = Projectile.velocity.Length();
					if (timer <= outTicks) { Projectile.timeLeft = outTicks * 2 + 60; }
					else {
						if (timer == outTicks + 1) Projectile.ResetLocalNPCHitImmunity();
						Vector2 to = o.Center - Projectile.Center;
						Projectile.velocity = to.SafeNormalize(Vector2.Zero) * Math.Min(26f, 8f + (timer - outTicks) * 0.8f);
						if (to.Length() < 28f) Projectile.Kill();
					}
					Projectile.rotation += 0.3f;
					Glow(1);
					break;
				}
				case SpellShape.Zone:
					if (sc != null && sc.DelaySec > 0 && !Projectile.friendly && timer >= (int)(sc.DelaySec * 60f)) Projectile.friendly = true;
					if (Projectile.friendly) Glow(3);
					break;
				case SpellShape.SelfBurst:
				case SpellShape.Dash:
					Projectile.Center = o.Center;
					Glow(3);
					break;
				case SpellShape.Cone:
					Projectile.velocity *= 0.96f;
					Glow(3);
					break;
				case SpellShape.Beam:
					Projectile.Center = o.Center;
					break;
				case SpellShape.Orbit: {
					int idx = (int)Projectile.velocity.X, n = Math.Max(1, (int)Projectile.velocity.Y);
					if (timer == 1) { Projectile.ai[0] = (float)SpellShape.Orbit; }
					float ang = idx * MathHelper.TwoPi / n + timer * 0.12f;
					float r = Math.Max(60f, (Real?.Radius ?? 300f) / 4f);
					Projectile.Center = o.Center + new Vector2((float)Math.Cos(ang), (float)Math.Sin(ang)) * r;
					Projectile.velocity = Vector2.Zero;
					Glow(1);
					break;
				}
			}
		}

		static NPC Nearest(Vector2 pos, float range) {
			NPC best = null; float bd = range;
			foreach (NPC n in Main.npc) {
				if (!n.active || n.friendly || n.dontTakeDamage || n.life <= 0) continue;
				float d = Vector2.Distance(n.Center, pos);
				if (d < bd) { bd = d; best = n; }
			}
			return best;
		}

		void Glow(int n) {
			for (int i = 0; i < n; i++) {
				Vector2 pos = Shape is SpellShape.Skillshot or SpellShape.Homing or SpellShape.Boomerang or SpellShape.Orbit
					? Projectile.Center : Projectile.Center + Main.rand.NextVector2Circular(Projectile.width / 2f, Projectile.height / 2f);
				var d = Dust.NewDustPerfect(pos, DustID.RainbowMk2, Main.rand.NextVector2Circular(1.2f, 1.2f), 0, Col, 1.3f);
				d.noGravity = true;
			}
		}

		// Beam is a long thin box from the caster along the aim direction; everything else uses the normal hitbox.
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
			if (Shape != SpellShape.Beam) return null;
			float len = (Real?.Range > 0 ? Real.Range / 1.5f : 600f) * (Script?.RangeMult ?? 1f);
			Vector2 end = Owner.Center + Projectile.velocity.SafeNormalize(Vector2.UnitX) * len;
			float w = 0f;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.Center, end, Math.Max(16, Size), ref w);
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
			SpellScript sc = Script;
			if (sc == null || Projectile.localAI[0] == 2) return;
			if (sc.LowHealthBonus > 0 && target.lifeMax > 0) modifiers.SourceDamage *= 1f + sc.LowHealthBonus * (1f - Math.Clamp(target.life / (float)target.lifeMax, 0f, 1f));
			if (sc.ExecuteFrac > 0 && !target.boss && target.life <= target.lifeMax * sc.ExecuteFrac) modifiers.SetInstantKill();
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			if (Alias != null && Main.myPlayer == Projectile.owner) LolSound.Play(Alias, Slot, "hit", target.Center, 0.7f);
			SpellScript sc = Script;
			int flags = sc?.Flags ?? 0;
			float cc = Real?.CcDur ?? 0f;
			if (sc != null && Projectile.localAI[0] != 2) {
				if (sc.ArmorShredSec > 0) target.AddBuff(BuffID.Ichor, (int)(sc.ArmorShredSec * 60));
				if (sc.Knockback != 0 && !target.boss) {
					Vector2 away = (target.Center - Owner.Center).SafeNormalize(Vector2.UnitX);
					target.velocity = away * sc.Knockback * (target.knockBackResist > 0 ? 1f : 0f) + new Vector2(0, -1.5f * Math.Abs(sc.Knockback) * 0.15f);
					target.netUpdate = true;
				}
				if (sc.BurnSec > 0) target.AddBuff(BuffID.OnFire3, (int)(sc.BurnSec * 60));
				if (sc.PoisonSec > 0) target.AddBuff(BuffID.Venom, (int)(sc.PoisonSec * 60));
				if (sc.BleedSec > 0) target.AddBuff(BuffID.Bleeding, (int)(sc.BleedSec * 60));
				if (sc.Lifesteal > 0) Owner.Heal(Math.Max(1, (int)(damageDone * sc.Lifesteal)));
				if (sc.Chain > 0 && Projectile.localAI[0] == 0) {
					int made = 0;
					foreach (NPC n in Main.npc) {
						if (made >= sc.Chain) break;
						if (!n.active || n.friendly || n.dontTakeDamage || n.whoAmI == target.whoAmI || Vector2.Distance(n.Center, target.Center) > 450f) continue;
						int i = Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, (n.Center - target.Center).SafeNormalize(Vector2.UnitX) * 14f,
							Projectile.type, (int)(Projectile.damage * 0.6f), Projectile.knockBack, Projectile.owner, (float)SpellShape.Homing, Slot, 20 * 1000 + 40);
						Main.projectile[i].localAI[0] = 1; // marks a chain bolt: it does not chain again
						made++;
					}
				}
			}
			if ((flags & 1) != 0) target.AddBuff(BuffID.Slow, sc != null && sc.SlowSec > 0 ? (int)(sc.SlowSec * 60) : cc > 0 ? (int)(cc * 60) : 120);
			if ((flags & 2) != 0) target.AddBuff(BuffID.Frozen, cc > 0 ? (int)Math.Min(180f, cc * 60) : 45);
			if ((flags & 4) != 0) target.AddBuff(BuffID.Confused, cc > 0 ? (int)(cc * 60) : 120);
		}

		static void Line(Vector2 a, Vector2 b, Color c, float w) {
			Vector2 d = b - a;
			Main.spriteBatch.Draw(Terraria.GameContent.TextureAssets.MagicPixel.Value, a - Main.screenPosition, new Rectangle(0, 0, 1, 1), c, d.ToRotation(), Vector2.Zero, new Vector2(d.Length(), w), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
		}

		static void Ring(Vector2 center, float r, Color c, float w) {
			Vector2 prev = center + new Vector2(r, 0);
			for (int k = 1; k <= 40; k++) {
				float ang = k * MathHelper.TwoPi / 40f;
				Vector2 cur = center + new Vector2((float)Math.Cos(ang), (float)Math.Sin(ang)) * r;
				Line(prev, cur, c, w);
				prev = cur;
			}
		}

		public override bool PreDraw(ref Color lightColor) {
			SpellScript sc = Script;
			switch (Shape) {
				case SpellShape.Beam: {
					float len = (Real?.Range > 0 ? Real.Range / 1.5f : 600f) * (sc?.RangeMult ?? 1f);
					float fade = Projectile.timeLeft / 14f;
					Vector2 end = Owner.Center + Projectile.velocity.SafeNormalize(Vector2.UnitX) * len;
					Line(Owner.Center, end, Col * fade, Math.Max(8, Size));
					Line(Owner.Center, end, Color.White * fade, Math.Max(3, Size / 3f));
					return false;
				}
				case SpellShape.Zone when sc != null && !Projectile.friendly: {
					float k = timer / Math.Max(1f, sc.DelaySec * 60f); // warning ring closes in
					Ring(Projectile.Center, Projectile.width / 2f, Col * 0.8f, 3f);
					Ring(Projectile.Center, Projectile.width / 2f * k, Color.White * 0.6f, 2f);
					return false;
				}
				case SpellShape.Skillshot:
				case SpellShape.Homing:
				case SpellShape.Boomerang:
				case SpellShape.Orbit: {
					float r = Math.Max(8f, Projectile.width / 2f);
					Ring(Projectile.Center, r, Col, 4f);
					Ring(Projectile.Center, r * 0.55f, Color.White * 0.8f, 3f);
					return false;
				}
				default: {
					float life = 1f - Projectile.timeLeft / Math.Max(1f, (int)Projectile.ai[2] % 1000);
					float r = Projectile.width / 2f * (0.35f + 0.65f * Math.Min(1f, life * 2f));
					float fade = 1f - life * 0.6f;
					Ring(Projectile.Center, r, Col * fade, 4f);
					Ring(Projectile.Center, r * 0.7f, Color.White * fade * 0.6f, 2f);
					return false;
				}
			}
		}
	}
}
