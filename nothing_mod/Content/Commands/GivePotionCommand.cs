using nothing_mod.Content.Items;
using Terraria.ModLoader;

namespace nothing_mod.Content.Commands
{
	// Type /allbuffs in chat to get 5 All-Buffs Potions (handy for testing).
	public class GivePotionCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "allbuffs";
		public override string Description => "Gives you 5 All-Buffs Potions";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			caller.Player.QuickSpawnItem(caller.Player.GetSource_Misc("AllBuffsCommand"), ModContent.ItemType<AllBuffsPotion>(), 5);
		}
	}
}
