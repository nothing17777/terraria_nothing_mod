using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.LoL
{
	// Turns a SpellScript + the real spell numbers into projectiles / buffs. Called from LolPlayer.Cast.
	public static class LolScriptCaster
	{
		public static void Cast(LolPlayer lp, LolChampionItem champ, SpellScript sc, RealSpell rs, int slot, Vector2 dir, Vector2 mouse, int dmg, float kb, IEntitySource src, float rangePx) {
			Player p = lp.Player;
			int type = ModContent.ProjectileType<LolScriptProjectile>();
			int me = p.whoAmI;
			dmg = (int)(dmg * sc.DmgMult);
			rangePx *= sc.RangeMult;

			if (sc.HealFrac > 0) p.Heal((int)(p.statLifeMax2 * sc.HealFrac));
			if (sc.Shield) p.AddBuff(BuffID.Endurance, 60 * 3);
			if (sc.SpeedBuff) p.AddBuff(BuffID.Swiftness, 60 * 3);

			float speed = (rs != null && rs.Speed > 0 ? MathHelper.Clamp(rs.Speed / 150f, 5f, 36f) : Math.Max(6f, rangePx / 50f)) * sc.SpeedMult;
			int width = (int)MathHelper.Clamp((rs != null && rs.Width > 0 ? rs.Width / 4f : 24f) * sc.WidthMult, 16f, 160f);
			int aoe = (int)MathHelper.Clamp((rs != null && rs.Radius > 0 ? rs.Radius * 2f / 1.5f : 150f) * sc.WidthMult, 90f, 560f);
			int life = Math.Min(999, (int)Math.Ceiling(rangePx / speed));
			int dur = (int)(sc.DurationSec * 60f);
			int delay = (int)(sc.DelaySec * 60f);

			void Spawn(Vector2 pos, Vector2 vel, int size, int lifeTicks, float a2 = 0f) =>
				Projectile.NewProjectile(src, pos, vel, type, dmg, kb, me, (float)sc.Shape, slot, size * 1000 + Math.Min(999, lifeTicks) + a2 * 0f);

			switch (sc.Shape) {
				case SpellShape.Skillshot:
				case SpellShape.Homing:
				case SpellShape.Boomerang: {
					int n = Math.Max(1, sc.Count);
					for (int i = 0; i < n; i++) {
						float a = n == 1 ? 0f : MathHelper.ToRadians(sc.SpreadDeg) * (i / (float)(n - 1) - 0.5f);
						Spawn(p.Center, dir.RotatedBy(a) * speed + p.velocity, width, life);
					}
					break;
				}
				case SpellShape.Beam:
					Spawn(p.Center, dir, width, 14);
					break;
				case SpellShape.Cone:
					Spawn(p.Center, dir * 6f + p.velocity, (int)MathHelper.Clamp(rs != null && rs.Cone > 100 ? rs.Cone / 1.5f : 200f, 90f, 420f), 20);
					break;
				case SpellShape.Zone: {
					Vector2 off = mouse - p.Center;
					if (off.Length() > rangePx) off = dir * rangePx;
					Spawn(p.Center + off, Vector2.Zero, aoe, Math.Max(20, delay + Math.Max(dur, 12)));
					break;
				}
				case SpellShape.SelfBurst:
					Spawn(p.Center, Vector2.Zero, aoe, Math.Max(18, dur));
					break;
				case SpellShape.Orbit: {
					int n = Math.Max(1, sc.Count);
					for (int i = 0; i < n; i++) Spawn(p.Center, new Vector2(i, n), 30, Math.Max(60, dur));
					break;
				}
				case SpellShape.Dash: {
					float tiles = sc.DashTiles > 0 ? sc.DashTiles : rangePx / 16f;
					float px = tiles * 16f * sc.RangeMult / Math.Max(1f, sc.RangeMult);
					int ticks = 10;
					lp.StartDash(dir, px / ticks, ticks);
					Spawn(p.Center, Vector2.Zero, Math.Max(90, width * 2), ticks + 6);
					break;
				}
			}
			if (!LolSound.Play(champ.Name.Replace("Champion", ""), slot, "cast", p.Center))
				Terraria.Audio.SoundEngine.PlaySound(SoundID.Item20, p.Center);
		}
	}
}
