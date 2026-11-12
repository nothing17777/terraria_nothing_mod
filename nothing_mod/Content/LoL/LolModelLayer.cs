using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace nothing_mod.Content.LoL
{
	// While a champion weapon is in the inventory, replace the player's body with the champion's posed 3D model.
	// Sheets come from tools/lol_anim_render.py: rows idle/run/attack/Q/W/E/R, 8 frames each (right-facing).
	// Falls back to the static render in Models/ when a champion has no sheet.
	public class LolModelLayer : PlayerDrawLayer
	{
		const float Scale = 0.7f; // sheets render the idle character 128px tall -> ~90px in game

		public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.LastVanillaLayer);

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
			Player p = drawInfo.drawPlayer;
			return !p.dead && p.GetModPlayer<LolPlayer>().Active != null;
		}

		protected override void Draw(ref PlayerDrawSet drawInfo) {
			Player p = drawInfo.drawPlayer;
			var lp = p.GetModPlayer<LolPlayer>();
			string alias = lp.Active.Name.Replace("Champion", "");
			Vector2 feet = (p.Bottom + new Vector2(0, p.gfxOffY) - Main.screenPosition).Floor();
			bool left = p.direction == -1;
			var fx = left ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

			string sheet = $"nothing_mod/Content/LoL/Anim/{alias}";
			if (LolAnimData.Sheets.TryGetValue(alias, out AnimDef a) && ModContent.HasAsset(sheet)) {
				Texture2D tex = ModContent.Request<Texture2D>(sheet).Value;
				var src = new Rectangle(lp.AnimFrame * a.W, lp.AnimRow * a.H, a.W, a.H);
				var origin = new Vector2(left ? a.W - a.OriginX : a.OriginX, a.FeetY);
				drawInfo.DrawDataCache.Clear(); // hide the vanilla player
				drawInfo.DrawDataCache.Add(new DrawData(tex, feet, src, drawInfo.colorArmorBody, 0f, origin, Scale, fx, 0));
				return;
			}
			string path = $"nothing_mod/Content/LoL/Models/{alias}";
			if (!ModContent.HasAsset(path)) return;
			Texture2D st = ModContent.Request<Texture2D>(path).Value;
			drawInfo.DrawDataCache.Clear();
			drawInfo.DrawDataCache.Add(new DrawData(st, feet, null, drawInfo.colorArmorBody, 0f, new Vector2(st.Width / 2f, st.Height), 88f / st.Height, fx, 0));
		}
	}
}
