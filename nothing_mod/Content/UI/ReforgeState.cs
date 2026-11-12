using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using BestPrefixes = nothing_mod.Content.Systems.BestPrefixesOnly;

namespace nothing_mod.Content.UI
{
	// One clickable row: prefix name. Hover shows the item's tooltip with that prefix applied.
	internal class PrefixRow : UITextPanel<string>
	{
		private readonly Item preview;

		public PrefixRow(string text, Item preview, Color background, Action onPick) : base(text, 0.8f)
		{
			this.preview = preview;
			BackgroundColor = background;
			Width.Set(0, 1f);
			Height.Set(36, 0f);
			OnLeftClick += (evt, el) => onPick();
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			if (IsMouseHovering)
			{
				Main.HoverItem = preview.Clone();
				Main.hoverItemName = preview.AffixName();
			}
		}
	}

	internal class ReforgeState : UIState
	{
		private UIPanel panel;
		private UIText title;
		private UIList list;
		private int shownType = -1;
		private int shownPrefix = -1;
		private int shownSlot = -1;

		private static Item Target() => Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem];

		public override void OnInitialize()
		{
			panel = new UIPanel();
			panel.Width.Set(380, 0f);
			panel.Height.Set(540, 0f);
			panel.HAlign = 0.15f;
			panel.VAlign = 0.5f;
			Append(panel);

			title = new UIText("");
			title.Top.Set(4, 0f);
			panel.Append(title);

			UITextPanel<string> close = new UITextPanel<string>("X");
			close.Left.Set(-50, 1f);
			close.Width.Set(44, 0f);
			close.Height.Set(36, 0f);
			close.OnLeftClick += (evt, el) => ModContent.GetInstance<ReforgeSystem>().Toggle();
			panel.Append(close);

			UIScrollbar scrollbar = new UIScrollbar();
			scrollbar.Top.Set(48, 0f);
			scrollbar.Height.Set(-60, 1f);
			scrollbar.Left.Set(-20, 1f);
			panel.Append(scrollbar);

			list = new UIList();
			list.Top.Set(48, 0f);
			list.Width.Set(-30, 1f);
			list.Height.Set(-60, 1f);
			list.ListPadding = 4f;
			list.SetScrollbar(scrollbar);
			panel.Append(list);
		}

		private static string PrefixName(int pre) =>
			pre < PrefixID.Count ? Lang.prefix[pre].Value : PrefixLoader.GetPrefix(pre).DisplayName.Value;

		private void Rebuild()
		{
			list.Clear();
			Item item = Target();
			shownType = item.type;
			shownPrefix = item.prefix;
			shownSlot = Main.LocalPlayer.selectedItem;

			if (item.IsAir || item.maxStack > 1)
			{
				title.SetText("Hold a weapon or accessory");
				return;
			}
			title.SetText("Reforge: " + item.Name);

			Item plain = new Item();
			plain.SetDefaults(item.type);

			var options = new List<(int Pre, Item Preview)>();
			BestPrefixes.Bypass = true;
			try
			{
				for (int pre = 1; pre < PrefixLoader.PrefixCount; pre++)
				{
					Item test = new Item();
					test.SetDefaults(item.type);
					if (test.Prefix(pre))
						options.Add((pre, test));
				}
			}
			finally
			{
				BestPrefixes.Bypass = false;
			}

			foreach (var (pre, preview) in options.OrderByDescending(o => o.Preview.value))
			{
				Color color = pre == item.prefix ? new Color(70, 100, 200)
					: preview.value >= plain.value ? new Color(40, 110, 60) : new Color(120, 50, 50);
				int chosen = pre;
				list.Add(new PrefixRow(PrefixName(pre), preview, color, () => Apply(chosen)));
			}
		}

		private static void Apply(int pre)
		{
			Player player = Main.LocalPlayer;
			Item item = player.inventory[player.selectedItem];
			if (item.IsAir)
				return;

			BestPrefixes.Bypass = true;
			try
			{
				item.ResetPrefix();
				item.Prefix(pre);
			}
			finally
			{
				BestPrefixes.Bypass = false;
			}

			if (Main.netMode == NetmodeID.MultiplayerClient)
				NetMessage.SendData(MessageID.SyncEquipment, -1, -1, null, Main.myPlayer, player.selectedItem, item.prefix);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (panel.ContainsPoint(Main.MouseScreen))
				Main.LocalPlayer.mouseInterface = true;

			Item item = Target();
			if (item.type != shownType || item.prefix != shownPrefix || Main.LocalPlayer.selectedItem != shownSlot)
				Rebuild();
		}
	}
}
