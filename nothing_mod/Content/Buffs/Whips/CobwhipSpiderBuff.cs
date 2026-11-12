using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Buffs.Whips
{
	// Player buff granted on every Cobwhip hit (game: buff 365, 180 ticks). While it lasts the Cobweb Spider
	// stays alive; it is refreshed by hitting again. UNVERIFIED display name/icon.
	public class CobwhipSpiderBuff : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + BuffID.Webbed;

		public override void SetStaticDefaults() {
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}
	}
}
