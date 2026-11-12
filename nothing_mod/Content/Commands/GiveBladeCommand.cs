using nothing_mod.Content.Items;
using Terraria.ModLoader;

namespace nothing_mod.Content.Commands
{
	// Type /voidblade in chat to get a Void Blade (handy for testing).
	public class GiveBladeCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "voidblade";
		public override string Description => "Gives you a Void Blade";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			caller.Player.QuickSpawnItem(caller.Player.GetSource_Misc("VoidBladeCommand"), ModContent.ItemType<VoidBlade>());
		}
	}
}
