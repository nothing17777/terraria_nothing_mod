using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace nothing_mod.Content.UI
{
	// 1.4.5 crafting search bar: filters the vanilla recipe list by result or ingredient name.
	// Filtering is done by detouring Recipe.FindRecipes and trimming Main.availableRecipe afterwards.
	public class CraftSearchSystem : ModSystem
	{
		// UNVERIFIED screen placement of the box (left of the inventory, above the recipe list).
		public const int BoxX = 94, BoxY = 262, BoxW = 200;

		private static string text = "";
		private static bool focused;
		private static bool filtering;

		public override void Load()
		{
			On_Recipe.FindRecipes += FilterRecipes; // UNVERIFIED: On_ detour name/signature (compile-checked)
		}

		public override void Unload()
		{
			On_Recipe.FindRecipes -= FilterRecipes;
			text = "";
			focused = false;
		}

		private static void FilterRecipes(On_Recipe.orig_FindRecipes orig, bool canDelayCheck)
		{
			orig(canDelayCheck);
			if (filtering || string.IsNullOrWhiteSpace(text) || Main.dedServ)
				return;

			string q = text.Trim();
			int keepFocus = Main.focusRecipe >= 0 && Main.focusRecipe < Main.numAvailableRecipes
				? Main.availableRecipe[Main.focusRecipe] : -1;

			int n = 0;
			for (int i = 0; i < Main.numAvailableRecipes; i++)
			{
				int r = Main.availableRecipe[i];
				if (!Matches(Main.recipe[r], q))
					continue;
				Main.availableRecipe[n] = r;
				Main.availableRecipeY[n] = 65f * n; // UNVERIFIED: vanilla row spacing of 65
				n++;
			}
			Main.numAvailableRecipes = n;

			int f = 0;
			for (int i = 0; i < n; i++)
				if (Main.availableRecipe[i] == keepFocus) { f = i; break; }
			Main.focusRecipe = f;
		}

		private static bool Matches(Recipe recipe, string q)
		{
			if (recipe.createItem.Name.Contains(q, StringComparison.OrdinalIgnoreCase))
				return true;
			foreach (Item ing in recipe.requiredItem)
				if (!ing.IsAir && ing.Name.Contains(q, StringComparison.OrdinalIgnoreCase))
					return true;
			return false;
		}

		private static void SetText(string value)
		{
			if (value == text)
				return;
			text = value;
			Recipe.FindRecipes();
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int index = layers.FindIndex(layer => layer.Name == "Vanilla: Mouse Text");
			if (index == -1)
				return;

			layers.Insert(index, new LegacyGameInterfaceLayer("nothing_mod: Craft Search", () =>
			{
				if (Main.playerInventory && !Main.InReforgeMenu && !Main.dedServ)
					DrawBox();
				else
					focused = false;
				return true;
			}, InterfaceScaleType.UI));
		}

		private static void DrawBox()
		{
			var rect = new Rectangle(BoxX, BoxY, BoxW, 28);
			bool hover = rect.Contains(Main.mouseX, Main.mouseY);
			if (hover)
				Main.LocalPlayer.mouseInterface = true;

			if (Main.mouseLeft && Main.mouseLeftRelease)
			{
				if (hover)
				{
					if (!focused)
						Main.clrInput(); // drop keys pressed before focusing
					focused = true;
				}
				else
					focused = false; // click outside clears focus
			}
			if (hover && Main.mouseRight && Main.mouseRightRelease)
			{
				SetText(""); // right click clears
				focused = false;
			}

			if (focused)
			{
				PlayerInput.WritingText = true;
				Main.instance.HandleIME();
				SetText(Main.GetInputText(text));
				if (Main.inputTextEscape || Main.inputTextEnter)
				{
					Main.inputTextEscape = false;
					Main.inputTextEnter = false;
					focused = false;
				}
			}

			Utils.DrawInvBG(Main.spriteBatch, rect, focused ? new Color(90, 110, 190, 230) : new Color(63, 82, 151, 190));
			string shown = text.Length > 0
				? text
				: Language.GetTextValue("Mods.nothing_mod.UI.CraftSearch.Hint");
			if (focused && (int)(Main.GlobalTimeWrappedHourly * 2) % 2 == 0)
				shown += "|";
			Utils.DrawBorderString(Main.spriteBatch, shown, new Vector2(rect.X + 8, rect.Y + 4),
				text.Length > 0 || focused ? Color.White : Color.Gray, 0.8f);
		}
	}
}
