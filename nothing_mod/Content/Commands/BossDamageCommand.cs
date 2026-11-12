using System.Linq;
using Microsoft.Xna.Framework;
using nothing_mod.Content.Systems.Mechanics;
using Terraria.ModLoader;

namespace nothing_mod.Content.Commands
{
	// 1.4.5: /bossdamage shows how much damage each player dealt in the last boss fight(s).
	public class BossDamageCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "bossdamage";
		public override string Description => "Shows the damage each player dealt in the last boss fights";

		public override void Action(CommandCaller caller, string input, string[] args) {
			var history = BossDamageTracker.History;
			if (history.Count == 0) {
				caller.Reply("No boss fight recorded yet.", Color.Orange);
				return;
			}

			foreach (var fight in history) {
				long total = fight.Damage.Values.Sum();
				caller.Reply($"{fight.Boss} ({fight.DurationTicks / 60}s, {total} total)", Color.Gold);
				foreach (var kv in fight.Damage.OrderByDescending(k => k.Value))
					caller.Reply($"  {kv.Key}: {kv.Value} ({kv.Value * 100 / total}%)", Color.White);
			}
		}
	}
}
