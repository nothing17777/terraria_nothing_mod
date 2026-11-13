using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.UI
{
	// Slot grid of stored banners. Left click withdraws 1, shift+left click withdraws a full stack.
	internal class BannerGrid : UIElement
	{
		private const int Slot = 52, Pad = 4;
		private int scroll; // rows

		private static BannerMenuPlayer P => Main.LocalPlayer.GetModPlayer<BannerMenuPlayer>();

		private int Cols => System.Math.Max(1, (int)(GetDimensions().Width / (Slot + Pad)));

		public override void ScrollWheel(UIScrollWheelEvent evt)
		{
			scroll -= evt.ScrollWheelValue > 0 ? 1 : -1;
		}

		public override void LeftClick(UIMouseEvent evt)
		{
			int type = TypeAt(evt.MousePosition);
			if (type > 0)
				P.Withdraw(type, Main.keyState.PressingShift() ? ContentSamples.ItemsByType[type].maxStack : 1);
		}

		private int TypeAt(Vector2 mouse)
		{
			var d = GetDimensions();
			int col = (int)((mouse.X - d.X) / (Slot + Pad));
			int row = (int)((mouse.Y - d.Y) / (Slot + Pad)) + scroll;
			int idx = row * Cols + col;
			var keys = P.Stored.Keys.OrderBy(k => k).ToList();
			if (col < 0 || col >= Cols || row < 0 || idx < 0 || idx >= keys.Count)
				return -1;
			return keys[idx];
		}

		protected override void DrawSelf(SpriteBatch sb)
		{
			var d = GetDimensions();
			var keys = P.Stored.Keys.OrderBy(k => k).ToList();
			int cols = Cols;
			int rows = (int)(d.Height / (Slot + Pad));
			int totalRows = (keys.Count + cols - 1) / cols;
			scroll = Utils.Clamp(scroll, 0, System.Math.Max(0, totalRows - rows));

			int hover = IsMouseHovering ? TypeAt(Main.MouseScreen) : -1;
			for (int i = scroll * cols; i < keys.Count && i < (scroll + rows) * cols; i++)
			{
				int type = keys[i];
				int r = i / cols - scroll, c = i % cols;
				var rect = new Rectangle((int)d.X + c * (Slot + Pad), (int)d.Y + r * (Slot + Pad), Slot, Slot);
				bool fresh = P.Fresh.Contains(type);
				// Newly obtained banners are highlighted with a gold slot.
				Color bg = fresh ? new Color(190, 150, 40, 200) : new Color(63, 82, 151, 160);
				sb.Draw(TextureAssets.MagicPixel.Value, rect, bg);

				Main.instance.LoadItem(type);
				Texture2D tex = TextureAssets.Item[type].Value;
				float scale = System.Math.Min(1f, (Slot - 12f) / System.Math.Max(tex.Width, tex.Height));
				sb.Draw(tex, rect.Center.ToVector2(), null, Color.White, 0f, tex.Size() / 2f, scale, SpriteEffects.None, 0f);
				Utils.DrawBorderString(sb, P.Stored[type].ToString(), new Vector2(rect.X + 4, rect.Bottom - 20), Color.White, 0.8f);

				if (type == hover)
				{
					Main.LocalPlayer.mouseInterface = true;
					Main.HoverItem = new Item(type);
					Main.hoverItemName = Main.HoverItem.Name;
					P.Fresh.Remove(type);
				}
			}
		}
	}

	internal class BannerMenuState : UIState
	{
		public override void OnInitialize()
		{
			var panel = new UIPanel();
			panel.Width.Set(480, 0f);
			panel.Height.Set(420, 0f);
			panel.HAlign = 0.5f;
			panel.VAlign = 0.5f;
			Append(panel);

			var title = new UIText(Language.GetTextValue("Mods.nothing_mod.UI.BannerMenu.Title"));
			title.Top.Set(2, 0f);
			panel.Append(title);

			var close = new UITextPanel<string>("X");
			close.Left.Set(-50, 1f);
			close.Width.Set(44, 0f);
			close.Height.Set(32, 0f);
			close.OnLeftClick += (evt, el) => ModContent.GetInstance<BannerMenuSystem>().Toggle();
			panel.Append(close);

			var hint = new UIText(Language.GetTextValue("Mods.nothing_mod.UI.BannerMenu.Hint"), 0.7f);
			hint.Top.Set(-26, 1f);
			panel.Append(hint);

			var grid = new BannerGrid();
			grid.Top.Set(44, 0f);
			grid.Width.Set(0, 1f);
			grid.Height.Set(-76, 1f);
			panel.Append(grid);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			var panel = Elements.Count > 0 ? Elements[0] : null;
			if (panel != null && panel.ContainsPoint(Main.MouseScreen))
				Main.LocalPlayer.mouseInterface = true;
		}
	}
}
