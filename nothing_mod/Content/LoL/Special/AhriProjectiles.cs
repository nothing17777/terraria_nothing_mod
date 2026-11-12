using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.LoL.Special
{
	// Ahri's real kit (Data Dragon numbers + wiki behaviour), sprites taken from the client's particle textures:
	// Q orb goes out then returns (magic out, true damage back); W three fox-fires orbit then seek up to 3 enemies;
	// E heart charm; R up to three dashes that each fire bolts at nearby enemies.
	public abstract class AhriProjectile : ModProjectile
	{
		protected abstract string Sprite { get; }
		public override string Texture => "nothing_mod/Content/LoL/Special/" + Sprite;

		protected Player Owner => Main.player[Projectile.owner];

		public override void OnSpawn(IEntitySource source) {
			Projectile.DamageType = (Owner.GetModPlayer<LolPlayer>().Active?.Item ?? Owner.HeldItem).DamageType;
		}

		// nth closest hostile NPC to pos within range (n wraps), or null
		protected NPC Target(Vector2 pos, float range, int n = 0) {
			var list = new System.Collections.Generic.List<NPC>();
			foreach (NPC npc in Main.npc)
				if (npc.active && npc.CanBeChasedBy(Projectile) && Vector2.Distance(npc.Center, pos) < range) list.Add(npc);
			if (list.Count == 0) return null;
			list.Sort((a, b) => Vector2.Distance(a.Center, pos).CompareTo(Vector2.Distance(b.Center, pos)));
			return list[n % list.Count];
		}

		protected void Draw(string sprite, Vector2 pos, Color c, float scale, float rot = 0f) {
			Texture2D t = ModContent.Request<Texture2D>("nothing_mod/Content/LoL/Special/" + sprite).Value;
			Main.EntitySpriteDraw(t, pos - Main.screenPosition, null, c, rot, t.Size() / 2f, scale, SpriteEffects.None, 0);
		}

		// Draws frame t of a League VFX sheet (tools/lol_vfx_export.py); first 10 frames are the spawn-in, the rest loops. Tip points along +x.
		protected static void DrawVfx(string name, Vector2 pos, float rot, int t, Color c, float scale = 1f) {
			if (!AhriVfxData.Sheets.TryGetValue(name, out VfxSheet v)) return;
			Texture2D tex = ModContent.Request<Texture2D>("nothing_mod/Content/LoL/Special/Ahri_vfx_" + name).Value;
			t /= 2; // sheets are 30 fps, the game ticks at 60
			int f = t < v.Frames ? t : 10 + (t - 10) % (v.Frames - 10);
			Main.EntitySpriteDraw(tex, pos - Main.screenPosition, new Rectangle(f * v.Cell, 0, v.Cell, v.Cell), c, rot, new Vector2(v.Cell / 2f), v.Scale * scale, SpriteEffects.None, 0);
		}

		protected static void Sparkle(Vector2 pos, int dust, Color c, int n = 1) {
			for (int i = 0; i < n; i++) {
				var d = Dust.NewDustPerfect(pos, dust, Main.rand.NextVector2Circular(1.2f, 1.2f), 0, c, 1.2f);
				d.noGravity = true;
			}
		}
	}

	// Q - Orb of Deception: travels out, then returns to Ahri; magic damage out, true damage back.
	public class AhriOrb : AhriProjectile
	{
		protected override string Sprite => "Ahri_glow";
		// ai0 = outward distance in px
		int timer;
		bool Returning => timer > 20;

		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Type] = 40; // history for the trail ribbon
			ProjectileID.Sets.TrailingMode[Type] = 2;
		}

		// Ribbon built like the game's VfxPrimitiveCameraTrail in Ahri_Base_Q_mis (Trail2/3/4 emitters):
		// particles live ~0.6s, colour over life (0, 0.1, 0.4, 1) = cyan fade-in -> teal -> clear, width scale 1.5 -> 4
		// (x 40 units), texture scrolls at -0.25 uv/s, world acceleration pushes the tail upward.
		static readonly float[] StopT = { 0f, 0.1f, 0.4f, 1f };
		static readonly Vector4[] StopC = {
			new Vector4(0.31f, 0.91f, 1f, 0f), new Vector4(0.13f, 0.89f, 1f, 1f), new Vector4(0.05f, 0.64f, 1f, 0.32f), new Vector4(0.01f, 0.63f, 0.49f, 0f) };

		static Vector4 Stop(float life) {
			for (int i = 1; i < StopT.Length; i++)
				if (life <= StopT[i]) return Vector4.Lerp(StopC[i - 1], StopC[i], (life - StopT[i - 1]) / (StopT[i] - StopT[i - 1]));
			return StopC[^1];
		}

		void Ribbon(string sprite, float lifeSeconds, float scrollSeed, float alphaMul, Color tint) {
			Texture2D tex = ModContent.Request<Texture2D>("nothing_mod/Content/LoL/Special/" + sprite).Value;
			Vector2 half = Projectile.Size / 2f;
			float dist = 0f;
			for (int i = 0; i < Projectile.oldPos.Length - 1; i++) {
				Vector2 a = Projectile.oldPos[i], b = Projectile.oldPos[i + 1];
				if (a == Vector2.Zero || b == Vector2.Zero) break;
				float life = i / 60f / lifeSeconds;
				if (life >= 1f) break;
				float rise = 0.5f * 62f * (i / 60f) * (i / 60f);             // 250 units/s^2 upward, /4 for pixels
				Vector2 pa = a + half - new Vector2(0, rise) , pb = b + half - new Vector2(0, 0.5f * 62f * ((i + 1) / 60f) * ((i + 1) / 60f));
				Vector2 seg = pa - pb; float len = seg.Length(); if (len < 0.01f) continue;
				Vector4 c = Stop(life);
				float width = 10f * (1.5f + 2.5f * life);                       // 40 units * scale / 4
				float tile = 150f;                                               // 600 units / 4 per texture repeat
				float u = ((dist + scrollSeed - Main.GameUpdateCount * 0.25f * tile / 60f) % tile + tile) % tile;
				int sw = Math.Max(1, (int)(len / tile * tex.Width)); sw = Math.Min(sw, tex.Width / 2);
				int sx = (int)(u / tile * (tex.Width - sw));
				var src = new Rectangle(sx, 0, sw, tex.Height);
				Color col = new Color(c.X * tint.R / 255f, c.Y * tint.G / 255f, c.Z * tint.B / 255f, 1f) * (c.W * alphaMul);
				Main.EntitySpriteDraw(tex, pb - Main.screenPosition, src, col, seg.ToRotation(), new Vector2(0, tex.Height / 2f), new Vector2(len / sw * 1.02f, width / tex.Height * 2f), SpriteEffects.None, 0);
				dist += len;
			}
		}

		public override void SetDefaults() {
			Projectile.width = Projectile.height = 40;
			Projectile.friendly = true; Projectile.penetrate = -1; Projectile.tileCollide = false; Projectile.ignoreWater = true;
			Projectile.timeLeft = 200;
			Projectile.usesLocalNPCImmunity = true; Projectile.localNPCHitCooldown = -1; // each enemy once per direction
		}

		public override void AI() {
			timer++;
			if (timer == 21) { Projectile.ResetLocalNPCHitImmunity(); Projectile.netUpdate = true; }
			if (timer == 1) Projectile.ai[1] = Projectile.velocity.ToRotation(); // fixed aim, so the owner's motion added below cannot bend it
			if (!Returning) Projectile.velocity = Vector2.UnitX.RotatedBy(Projectile.ai[1]) * (Projectile.ai[0] / 20f) + Owner.velocity; // keeps full range while Ahri moves
			else {
				Vector2 to = Owner.Center - Projectile.Center;
				float speed = Math.Min(24f, 6f + (timer - 20) * 0.9f);
				Projectile.velocity = to.SafeNormalize(Vector2.Zero) * speed;
				if (to.Length() < 26f) Projectile.Kill();
			}
			Projectile.rotation += 0.2f;
			Sparkle(Projectile.Center, DustID.BlueTorch, Color.White);
			Lighting.AddLight(Projectile.Center, 0.2f, 0.5f, 0.9f);
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
			if (Returning) modifiers.DefenseEffectiveness *= 0f; // true damage on the way back
		}

		public override bool PreDraw(ref Color lightColor) {
			// real Ahri_Base_Q_mis / Q_return VFX frames rendered from the client's .bin (see tools/lol_vfx_export.py)
			DrawVfx(Returning ? "Qret" : "Q", Projectile.Center, Projectile.velocity.ToRotation(), Returning ? timer - 21 : timer, Color.White);
			return false;
		}
	}

	// W - Fox-Fire: three flames orbit Ahri, then each darts at a nearby enemy (up to 3 different ones).
	public class AhriFoxFire : AhriProjectile
	{
		protected override string Sprite => "Ahri_fox";
		// ai0 = flame index 0-2
		int timer; NPC lock_;

		public override void SetDefaults() {
			Projectile.width = Projectile.height = 22;
			Projectile.friendly = true; Projectile.penetrate = 1; Projectile.tileCollide = false; Projectile.ignoreWater = true;
			Projectile.timeLeft = 300; // fox-fire lasts 5s
		}

		public override void AI() {
			timer++;
			int idx = (int)Projectile.ai[0];
			float range = 150f; // W target radius (700 units / 4, a bit generous)
			if (lock_ != null && (!lock_.active || !lock_.CanBeChasedBy(Projectile))) lock_ = null;
			if (timer > 25 && lock_ == null) lock_ = Target(Owner.Center, range, idx);
			if (lock_ != null) {
				Vector2 to = lock_.Center - Projectile.Center;
				Projectile.velocity = Vector2.Lerp(Projectile.velocity, to.SafeNormalize(Vector2.Zero) * 13f, 0.25f);
				Projectile.Center += Projectile.velocity;
			}
			else {
				float ang = timer * 0.12f + idx * MathHelper.TwoPi / 3f;
				Vector2 goal = Owner.Center + new Vector2((float)Math.Cos(ang), (float)Math.Sin(ang) * 0.8f) * 52f;
				Projectile.velocity = goal - Projectile.Center;
				Projectile.Center = goal;
			}
			Sparkle(Projectile.Center, DustID.PinkTorch, Color.White);
			Lighting.AddLight(Projectile.Center, 0.8f, 0.3f, 0.6f);
		}

		public override bool PreDraw(ref Color lightColor) {
			DrawVfx("W", Projectile.Center, 0f, timer, Color.White);
			return false;
		}

		public override void OnKill(int timeLeft) => Sparkle(Projectile.Center, DustID.PinkTorch, Color.White, 10);
	}

	// E - Charm: heart projectile that charms the first enemy hit.
	public class AhriCharm : AhriProjectile
	{
		protected override string Sprite => "Ahri_heart";

		public override void SetDefaults() {
			Projectile.width = Projectile.height = 26;
			Projectile.friendly = true; Projectile.penetrate = 1; Projectile.tileCollide = true; Projectile.ignoreWater = true;
			Projectile.timeLeft = 40; // travels exactly the spell range at range/38 px per tick
		}

		public override void AI() {
			Projectile.rotation = Projectile.velocity.X * 0.03f;
			Sparkle(Projectile.Center, DustID.PinkTorch, Color.White);
			Lighting.AddLight(Projectile.Center, 0.9f, 0.3f, 0.6f);
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			target.AddBuff(BuffID.Lovestruck, 120); // heart over the head
			target.AddBuff(BuffID.Confused, 120);   // charmed enemies stumble (stand-in for forced walking)
			target.AddBuff(BuffID.Slow, 120);
		}

		public override bool PreDraw(ref Color lightColor) {
			DrawVfx("E", Projectile.Center, Projectile.velocity.ToRotation(), (int)Projectile.localAI[0]++, Color.White);
			return false;
		}

		public override void OnKill(int timeLeft) => Sparkle(Projectile.Center, DustID.PinkTorch, Color.White, 14);
	}

	// R - Spirit Rush bolts: homing bolts fired by each dash.
	public class AhriSpiritBolt : AhriProjectile
	{
		protected override string Sprite => "Ahri_fox";
		NPC lock_;

		public override void SetDefaults() {
			Projectile.width = Projectile.height = 18;
			Projectile.friendly = true; Projectile.penetrate = 1; Projectile.tileCollide = false; Projectile.ignoreWater = true;
			Projectile.timeLeft = 90;
		}

		public override void AI() {
			if (lock_ == null || !lock_.active) lock_ = Target(Projectile.Center, 260f);
			if (lock_ != null) Projectile.velocity = Vector2.Lerp(Projectile.velocity, (lock_.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 16f, 0.2f);
			Sparkle(Projectile.Center, DustID.PinkTorch, Color.White);
		}

		public override bool PreDraw(ref Color lightColor) {
			DrawVfx("R", Projectile.Center, Projectile.velocity.ToRotation(), (int)Projectile.localAI[0]++, Color.White);
			return false;
		}
	}
}
