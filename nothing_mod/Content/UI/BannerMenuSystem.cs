using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using nothing_mod.Content.Players;

namespace nothing_mod.Content.UI
{
	// Banner Menu: a button next to the crafting search bar opens the panel.
	public class BannerMenuSystem : ModSystem
	{
		private UserInterface ui;
		private BannerMenuState state;
		private GameTime lastUpdate;

		public void Toggle()
		{
			if (Main.dedServ)
				return;
			if (ui == null)
			{
				state = new BannerMenuState();
				state.Activate();
				ui = new UserInterface();
			}
			ui.SetState(ui.CurrentState == null ? state : null);
		}

		public override void Unload()
		{
			ui = null;
			state = null;
		}

		public override void UpdateUI(GameTime gameTime)
		{
			lastUpdate = gameTime;
			if (ui?.CurrentState != null)
			{
				if (!Main.playerInventory)
					ui.SetState(null); // close with the inventory
				else
					ui.Update(gameTime);
			}
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int index = layers.FindIndex(layer => layer.Name == "Vanilla: Mouse Text");
			if (index == -1)
				return;

			layers.Insert(index, new LegacyGameInterfaceLayer("nothing_mod: Banner Menu", () =>
			{
				if (Main.playerInventory && !Main.dedServ)
					DrawButton();
				if (ui?.CurrentState != null && lastUpdate != null)
					ui.Draw(Main.spriteBatch, lastUpdate);
				return true;
			}, InterfaceScaleType.UI));
		}

		private void DrawButton()
		{
			// UNVERIFIED placement: sits just right of the crafting search bar (see CraftSearchSystem).
			var rect = new Rectangle(CraftSearchSystem.BoxX + CraftSearchSystem.BoxW + 6, CraftSearchSystem.BoxY, 90, 28);
			bool hover = rect.Contains(Main.mouseX, Main.mouseY);
			Utils.DrawInvBG(Main.spriteBatch, rect, hover ? new Color(90, 110, 190, 220) : new Color(63, 82, 151, 190));
			int n = 0;
			foreach (int c in Main.LocalPlayer.GetModPlayer<BannerMenuPlayer>().Stored.Values)
				n += c;
			string label = Language.GetTextValue("Mods.nothing_mod.UI.BannerMenu.Button") + (n > 0 ? $" ({n})" : "");
			Utils.DrawBorderString(Main.spriteBatch, label, new Vector2(rect.X + 8, rect.Y + 4), Color.White, 0.75f);
			if (hover)
			{
				Main.LocalPlayer.mouseInterface = true;
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					Main.mouseLeftRelease = false;
					Toggle();
				}
			}
		}
	}
}
