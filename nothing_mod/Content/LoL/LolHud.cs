using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;

namespace nothing_mod.Content.LoL
{
	// Draws the Q/W/E/R icons with cooldown overlays while a champion weapon is held.
	public class LolHud : ModSystem
	{
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
			int i = layers.FindIndex(l => l.Name == "Vanilla: Mouse Text");
			if (i < 0) return;
			layers.Insert(i, new LegacyGameInterfaceLayer("nothing_mod: LoL World", () => { DrawWorld(Main.spriteBatch); return true; }, InterfaceScaleType.Game));
			layers.Insert(i, new LegacyGameInterfaceLayer("nothing_mod: LoL Abilities", () => { Draw(Main.spriteBatch); return true; }, InterfaceScaleType.UI));
		}

		static void Line(SpriteBatch sb, Vector2 a, Vector2 b, Color c, float w = 2f) {
			Vector2 d = b - a;
			sb.Draw(TextureAssets.MagicPixel.Value, a, new Rectangle(0, 0, 1, 1), c, d.ToRotation(), Vector2.Zero, new Vector2(d.Length(), w), SpriteEffects.None, 0f);
		}

		static void Ring(SpriteBatch sb, Vector2 center, float r, Color c) {
			const int N = 72;
			Vector2 prev = center + new Vector2(r, 0);
			for (int k = 1; k <= N; k++) {
				float ang = k * MathHelper.TwoPi / N;
				Vector2 cur = center + new Vector2((float)System.Math.Cos(ang), (float)System.Math.Sin(ang)) * r;
				Line(sb, prev, cur, c);
				prev = cur;
			}
		}

		// World-space overlay: click-move marker and the range indicator of the spell key being held.
		void DrawWorld(SpriteBatch sb) {
			Player p = Main.LocalPlayer;
			var lp = p.GetModPlayer<LolPlayer>();
			if (p.dead || lp.Active is not LolChampionItem champ) return;
			if (lp.MoveTarget is Vector2 t) {
				Vector2 s = t - Main.screenPosition;
				Ring(sb, s, 10f, Color.LimeGreen * 0.9f);
				Ring(sb, s, 4f, Color.LimeGreen * 0.9f);
			}
			if (lp.AimSlot < 0) return;
			SpellDef sp = champ.Def.Spells[lp.AimSlot + 1];
			float range = LolPlayer.RangePx(sp);
			Color col = champ.Def.EffectColor;
			Vector2 c0 = p.Center - Main.screenPosition;
			Ring(sb, c0, range, col * 0.8f);
			Vector2 mouse = Main.MouseWorld - Main.screenPosition;
			Vector2 dir = mouse - c0; if (dir == Vector2.Zero) dir = Vector2.UnitX; dir.Normalize();
			switch (sp.Arch) {
				case 3: Ring(sb, c0, 75f, col * 0.5f); break;                                   // burst around self
				case 4: {                                                                         // burst at cursor
					Vector2 off = mouse - c0; if (off.Length() > range) off = dir * range;
					Ring(sb, c0 + off, 85f, col * 0.6f); break;
				}
				case 5: break;                                                                   // self buff
				default: Line(sb, c0, c0 + dir * range, col * 0.7f, 3f); break;                  // skillshot / dash / sweep line
			}
		}

		void Draw(SpriteBatch sb) {
			Player p = Main.LocalPlayer;
			var lp = p.GetModPlayer<LolPlayer>();
			if (p.dead || lp.Active is not LolChampionItem champ) return;
			float x0 = Main.screenWidth / 2f - 2 * 44, y = Main.screenHeight - 90;
			string mode = lp.ClickMove ? "MOVE: RIGHT-CLICK (A/D off)" : "MOVE: WASD";
			Utils.DrawBorderString(sb, $"{mode}  [{LolKeyLabel.Of(4)}] switch", new Vector2(x0 - 30, y - 22), lp.ClickMove ? Color.LimeGreen : Color.Orange, 0.8f);
			for (int i = 0; i < 4; i++) {
				SpellDef sp = champ.Def.Spells[i + 1];
				var rect = new Rectangle((int)(x0 + i * 44), (int)y, 40, 40);
				sb.Draw(TextureAssets.MagicPixel.Value, rect, Color.Black * 0.6f);
				string path = $"nothing_mod/Content/LoL/Abilities/{champ.ChampionKey}_{sp.Key}";
				if (sp.HasIcon == 1 && ModContent.HasAsset(path)) {
					var tex = ModContent.Request<Texture2D>(path).Value;
					sb.Draw(tex, rect, lp.Cooldown[i] > 0 ? Color.Gray : Color.White);
				}
				if (lp.Cooldown[i] > 0) {
					float f = lp.Cooldown[i] / lp.CooldownMax[i];
					int h = (int)(40 * f);
					sb.Draw(TextureAssets.MagicPixel.Value, new Rectangle(rect.X, rect.Bottom - h, 40, h), Color.Black * 0.55f);
					Utils.DrawBorderString(sb, lp.Cooldown[i].ToString("0.0"), new Vector2(rect.X + 6, rect.Y + 10), Color.White, 0.8f);
				}
				Utils.DrawBorderString(sb, LolKeyLabel.Of(i), new Vector2(rect.X + 2, rect.Y - 2), Color.Yellow, 0.7f);
			}
		}
	}
}
