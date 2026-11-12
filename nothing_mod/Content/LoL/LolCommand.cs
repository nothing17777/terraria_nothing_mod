using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace nothing_mod.Content.LoL
{
	// /lol champ <name>   /lol item <id or name>   /lol list champs|items   /lol allitems
	public class LolCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "lol";
		public override string Description => "Give League content: /lol champ <name> | /lol item <id|name> | /lol list champs|items | /lol allitems";

		public override void Action(CommandCaller caller, string input, string[] args) {
			if (args.Length == 0) { caller.Reply(Description); return; }
			var src = caller.Player.GetSource_Misc("LolCommand");
			string rest = string.Join(" ", args.Skip(1)).ToLowerInvariant().Replace("'", "").Replace(" ", "");
			switch (args[0].ToLowerInvariant()) {
				case "champ": {
					var hit = LolData.Champions.FirstOrDefault(kv => kv.Key.ToLowerInvariant() == rest || kv.Value.Name.ToLowerInvariant().Replace("'", "").Replace(" ", "") == rest);
					if (hit.Key == null || !Mod.TryFind(hit.Key + "Champion", out ModItem mi)) { caller.Reply("No such champion. Try /lol list champs"); return; }
					caller.Player.QuickSpawnItem(src, mi.Type);
					break;
				}
				case "item": {
					var hit = LolData.Items.FirstOrDefault(kv => kv.Key.ToString() == rest || kv.Value.Name.ToLowerInvariant().Replace("'", "").Replace(" ", "") == rest);
					if (hit.Value == null || !Mod.TryFind("Lol" + hit.Key, out ModItem mi)) { caller.Reply("No such item. Try /lol list items"); return; }
					caller.Player.QuickSpawnItem(src, mi.Type);
					break;
				}
				case "allitems":
					foreach (int id in LolData.Items.Keys)
						if (Mod.TryFind("Lol" + id, out ModItem mi)) caller.Player.QuickSpawnItem(src, mi.Type);
					break;
				case "list":
					caller.Reply(rest.StartsWith("i") ? string.Join(", ", LolData.Items.Select(kv => kv.Key + "=" + kv.Value.Name)) : string.Join(", ", LolData.Champions.Keys));
					break;
				default: caller.Reply(Description); break;
			}
		}
	}
}
