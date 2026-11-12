using nothing_mod.Content.UI;
using Terraria.ModLoader;

namespace nothing_mod.Content.Commands
{
	// Type /reforge in chat to open or close the reforge chooser for the item in your hand.
	public class ReforgeCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "reforge";
		public override string Description => "Opens or closes the reforge chooser";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			ModContent.GetInstance<ReforgeSystem>().Toggle();
		}
	}
}
