using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;

namespace nothing_mod.Content.UI
{
	// A clickable cell showing one item. Left click = 1, right click = a full stack.
	internal class ItemCell : UIElement
	{
		private readonly int type;

		public ItemCell(int type)
		{
			this.type = type;
			Width.Set(44, 0f);
			Height.Set(44, 0f);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			Rectangle rect = GetDimensions().ToRectangle();
			spriteBatch.Draw(TextureAssets.InventoryBack.Value, rect, Color.White * (IsMouseHovering ? 1f : 0.75f));

			Main.instance.LoadItem(type);
			Texture2D tex = TextureAssets.Item[type].Value;
			Rectangle frame = Main.itemAnimations[type] != null ? Main.itemAnimations[type].GetFrame(tex) : tex.Frame();
			float scale = Math.Min(1f, 32f / Math.Max(frame.Width, frame.Height));
			spriteBatch.Draw(tex, rect.Center.ToVector2(), frame, Color.White, 0f, frame.Size() / 2f, scale, SpriteEffects.None, 0f);

			if (IsMouseHovering)
			{
				Main.HoverItem = ContentSamples.ItemsByType[type].Clone();
				Main.hoverItemName = Main.HoverItem.Name;
			}
		}

		public override void LeftClick(UIMouseEvent evt) => Give(1);

		public override void RightClick(UIMouseEvent evt) => Give(ContentSamples.ItemsByType[type].maxStack);

		private void Give(int stack)
		{
			Player player = Main.LocalPlayer;
			int index = player.QuickSpawnItem(player.GetSource_Misc("ItemBrowser"), type, stack);
			if (index >= 0 && index < Main.maxItems && Main.item[index].maxStack == 1)
			{
				int best = global::nothing_mod.Content.Systems.BestPrefixesOnly.BestPrefix(type);
				if (best > 0)
					Main.item[index].Prefix(best);
			}
		}
	}

	internal class ItemBrowserState : UIState
	{
		private const int MaxResults = 600;

		private static bool IsTool(Item i) => i.pick > 0 || i.axe > 0 || i.hammer > 0;
		private static bool IsWeaponOf(Item i, DamageClass c) => i.damage > 0 && i.ammo == 0 && !IsTool(i) && i.CountsAsClass(c);

		// Category filters. "Bars/Ores" matches by English name since vanilla has no flag for it.
		private static readonly (string Name, Func<Item, bool> Match)[] Filters =
		{
			("All", i => true),
			("Melee", i => IsWeaponOf(i, DamageClass.Melee)),
			("Ranged", i => IsWeaponOf(i, DamageClass.Ranged)),
			("Magic", i => IsWeaponOf(i, DamageClass.Magic)),
			("Summon", i => IsWeaponOf(i, DamageClass.Summon)),
			("Throwing", i => IsWeaponOf(i, DamageClass.Throwing)),
			("Tools", IsTool),
			("Armor", i => !i.vanity && (i.headSlot >= 0 || i.bodySlot >= 0 || i.legSlot >= 0)),
			("Accessories", i => i.accessory && !i.vanity),
			("Potions", i => (i.buffType > 0 && i.buffTime > 0) || i.healLife > 0 || i.healMana > 0),
			("Ammo", i => i.ammo > 0 && i.damage > 0),
			("Materials", i => i.material),
			("Bars/Ores", i => i.Name.EndsWith(" Bar") || i.Name.EndsWith(" Ore")),
			("Placeable", i => i.createTile >= 0 || i.createWall >= 0),
			("Vanity", i => i.vanity),
			("Mounts/Pets", i => i.mountType >= 0 || (i.buffType > 0 && (Main.vanityPet[i.buffType] || Main.lightPet[i.buffType]))),
		};

		private UITextPanel<string>[] filterButtons;
		private int activeFilter;

		private UIPanel panel;
		private UIPanel searchPanel;
		private UIText searchText;
		private UITextPanel<string> modToggle;
		private UIGrid grid;
		private bool typing;
		private bool modOnly;
		private string search = "";

		public override void OnInitialize()
		{
			panel = new UIPanel();
			panel.Width.Set(720, 0f);
			panel.Height.Set(580, 0f);
			panel.HAlign = 0.5f;
			panel.VAlign = 0.5f;
			Append(panel);

			searchPanel = new UIPanel();
			searchPanel.Top.Set(0, 0f);
			searchPanel.Width.Set(370, 0f);
			searchPanel.Height.Set(40, 0f);
			searchPanel.OnLeftClick += (evt, el) => typing = true;
			panel.Append(searchPanel);

			searchText = new UIText("");
			searchPanel.Append(searchText);

			modToggle = new UITextPanel<string>("Mod items: off");
			modToggle.Left.Set(380, 0f);
			modToggle.Width.Set(170, 0f);
			modToggle.Height.Set(40, 0f);
			modToggle.OnLeftClick += (evt, el) =>
			{
				modOnly = !modOnly;
				modToggle.SetText(modOnly ? "Mod items: on" : "Mod items: off");
				Rebuild();
			};
			panel.Append(modToggle);

			UITextPanel<string> closeButton = new UITextPanel<string>("X");
			closeButton.Left.Set(-50, 1f);
			closeButton.Width.Set(44, 0f);
			closeButton.Height.Set(40, 0f);
			closeButton.OnLeftClick += (evt, el) => ModContent.GetInstance<ItemBrowserSystem>().Toggle();
			panel.Append(closeButton);

			// Two rows of 8 category buttons.
			filterButtons = new UITextPanel<string>[Filters.Length];
			for (int n = 0; n < Filters.Length; n++)
			{
				int index = n;
				UITextPanel<string> button = new UITextPanel<string>(Filters[n].Name, 0.7f);
				button.Left.Set(n % 8 * 86, 0f);
				button.Top.Set(48 + n / 8 * 38, 0f);
				button.Width.Set(82, 0f);
				button.Height.Set(34, 0f);
				button.OnLeftClick += (evt, el) =>
				{
					activeFilter = index;
					Rebuild();
				};
				filterButtons[n] = button;
				panel.Append(button);
			}

			UIScrollbar scrollbar = new UIScrollbar();
			scrollbar.Top.Set(130, 0f);
			scrollbar.Height.Set(-150, 1f);
			scrollbar.Left.Set(-20, 1f);
			panel.Append(scrollbar);

			grid = new UIGrid();
			grid.Top.Set(130, 0f);
			grid.Width.Set(-30, 1f);
			grid.Height.Set(-140, 1f);
			grid.ListPadding = 4f;
			grid.SetScrollbar(scrollbar);
			panel.Append(grid);

			Rebuild();
		}

		private void Rebuild()
		{
			grid.Clear();
			for (int n = 0; n < filterButtons.Length; n++)
				filterButtons[n].BackgroundColor = n == activeFilter ? new Color(90, 130, 220) : new Color(63, 82, 151) * 0.7f;
			int count = 0;
			foreach (KeyValuePair<int, Item> pair in ContentSamples.ItemsByType)
			{
				Item item = pair.Value;
				if (pair.Key <= 0 || string.IsNullOrEmpty(item.Name))
					continue;
				if (modOnly && item.ModItem == null)
					continue;
				if (!Filters[activeFilter].Match(item))
					continue;
				if (search.Length > 0 && item.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) < 0)
					continue;
				grid.Add(new ItemCell(pair.Key));
				if (++count >= MaxResults)
					break;
			}
		}

		// Like tModLoader's own text boxes, keyboard text is read while drawing, not in Update.
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (!typing)
				return;

			PlayerInput.WritingText = true;
			Main.instance.HandleIME();
			string newText = Main.GetInputText(search);
			if (newText != search)
			{
				search = newText;
				Rebuild();
			}
			if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter) || Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
				typing = false;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (panel.ContainsPoint(Main.MouseScreen))
				Main.LocalPlayer.mouseInterface = true;

			if (typing && Main.mouseLeft && Main.mouseLeftRelease && !searchPanel.IsMouseHovering)
				typing = false;

			bool showCursor = typing && (int)(Main.GlobalTimeWrappedHourly * 2) % 2 == 0;
			string display = search.Length == 0 && !typing ? "Click here to search..." : search;
			searchText.SetText(display + (showCursor ? "|" : ""));
		}
	}
}
