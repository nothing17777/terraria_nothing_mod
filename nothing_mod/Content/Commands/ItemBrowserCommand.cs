using nothing_mod.Content.UI;
using Terraria.ModLoader;

namespace nothing_mod.Content.Commands
{
	// Type /items in chat to open or close the item browser.
	public class ItemBrowserCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "items";
		public override string Description => "Opens or closes the item browser";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			ModContent.GetInstance<ItemBrowserSystem>().Toggle();
		}
	}
}
