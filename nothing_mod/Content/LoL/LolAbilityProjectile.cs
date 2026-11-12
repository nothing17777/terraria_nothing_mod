using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.LoL
{
	// One projectile type for every champion spell. ai0 = style, ai1 = CC flags + 8 * hue index.
	// ai0: 0 bolt, 1 burst following owner, 2 static burst, 3 cone sweep.
	public class LolAbilityProjectile : ModProjectile
	{
		// Borrowed vanilla sprite (placeholder); League spell particles were not converted.
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.AmethystBolt;

		int Style => (int)Projectile.ai[0];
		int Flags => (int)Projectile.ai[1] % 8;
		float CcSeconds => (int)Projectile.ai[1] / 1000 / 10f; // real CC duration (0 = default)
		int HueIndex => (int)Projectile.ai[1] % 1000 / 8;
		Color Col => Main.hslToRgb(HueIndex * 10f / 360f, 0.9f, 0.6f);

		public override void SetDefaults() {
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 60;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override void OnSpawn(Terraria.DataStructures.IEntitySource source) {
			Projectile.DamageType = (Main.player[Projectile.owner].GetModPlayer<LolPlayer>().Active?.Item ?? Main.player[Projectile.owner].HeldItem).DamageType;
			int packed = (int)Projectile.ai[2];  // sizePx * 1000 + lifetime ticks (0 = defaults), from the real spell data
			switch (Style) {
				case 0: Projectile.scale = 1.8f; Projectile.tileCollide = true; Projectile.penetrate = 3; Projectile.timeLeft = 60; break; // 60 ticks * speed (range/60) = exactly the spell range
				case 1: ResizeCentered(150); Projectile.timeLeft = 18; break;
				case 2: ResizeCentered(170); Projectile.timeLeft = 20; break;
				case 3: ResizeCentered(110); Projectile.timeLeft = 20; break;
			}
			if (Style != 0 && packed >= 1000) ResizeCentered(packed / 1000); // real area size
			if (Style == 0 && packed >= 1000) { ResizeCentered(packed / 1000); Projectile.scale = Math.Max(1f, packed / 1000 / 11f); }
			if (Style == 0 && packed % 1000 > 0) Projectile.timeLeft = packed % 1000;
		}

		void ResizeCentered(int size) {
			Vector2 c = Projectile.Center;
			Projectile.width = Projectile.height = size;
			Projectile.Center = c;
		}

		public override void AI() {
			switch (Style) {
				case 0:
					Projectile.rotation = Projectile.velocity.ToRotation();
					Dust(Projectile.Center, 1);
					break;
				case 1:
					Projectile.Center = Main.player[Projectile.owner].Center;
					Burst();
					break;
				case 2:
					Burst();
					break;
				case 3:
					Projectile.velocity *= 0.96f;
					Dust(Projectile.Center, 3);
					break;
			}
		}

		void Burst() {
			int r = Projectile.width / 2;
			for (int i = 0; i < 4; i++) {
				Vector2 off = Main.rand.NextVector2CircularEdge(r, r) * Main.rand.NextFloat(0.5f, 1f);
				Dust(Projectile.Center + off, 1);
			}
		}

		void Dust(Vector2 pos, int n) {
			for (int i = 0; i < n; i++) {
				var d = Terraria.Dust.NewDustPerfect(pos, DustID.RainbowMk2, Main.rand.NextVector2Circular(1.5f, 1.5f), 0, Col, 1.3f);
				d.noGravity = true;
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			if ((Flags & 1) != 0) target.AddBuff(BuffID.Slow, CcSeconds > 0 ? (int)(CcSeconds * 60) : 120);
			if ((Flags & 2) != 0) target.AddBuff(BuffID.Frozen, CcSeconds > 0 ? (int)Math.Min(180f, CcSeconds * 60) : 45);   // stun/root/knock-up stand-in
			if ((Flags & 4) != 0) target.AddBuff(BuffID.Confused, CcSeconds > 0 ? (int)(CcSeconds * 60) : 120); // fear/charm/silence stand-in
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
			if (Style == 0) return true; // the bolt uses its (scaled, tinted) sprite
			// bursts and sweeps: draw an expanding ring plus a filled glow so the effect is clearly visible
			float life = 1f - Projectile.timeLeft / (Style == 1 ? 18f : 20f);
			float r = Projectile.width / 2f * (0.35f + 0.65f * Math.Min(1f, life * 2f));
			float fade = 1f - life * 0.7f;
			Ring(Projectile.Center, r, Col * fade, 4f);
			Ring(Projectile.Center, r * 0.7f, Color.White * fade * 0.6f, 2f);
			for (int i = 0; i < 6; i++) {
				float ang = i * MathHelper.TwoPi / 6f + life * 3f;
				Line(Projectile.Center, Projectile.Center + new Vector2((float)Math.Cos(ang), (float)Math.Sin(ang)) * r, Col * fade * 0.5f, 2f);
			}
			return false;
		}

		public override Color? GetAlpha(Color lightColor) => Col;

		public override void OnKill(int timeLeft) {
			if (Style == 0) Dust(Projectile.Center, 8);
		}
	}
}
