using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace nothing_mod.Content.UI
{
	public class ReforgeSystem : ModSystem
	{
		private UserInterface ui;
		private ReforgeState state;
		private GameTime lastUpdate;

		public void Toggle()
		{
			if (Main.dedServ)
				return;

			if (ui == null)
			{
				state = new ReforgeState();
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
				ui.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int index = layers.FindIndex(layer => layer.Name == "Vanilla: Mouse Text");
			if (index == -1)
				return;

			layers.Insert(index, new LegacyGameInterfaceLayer("nothing_mod: Reforge", () =>
			{
				if (ui?.CurrentState != null && lastUpdate != null)
					ui.Draw(Main.spriteBatch, lastUpdate);
				return true;
			}, InterfaceScaleType.UI));
		}
	}
}
