using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Players;

namespace nothing_mod
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class nothing_mod : Mod
	{
		public enum PacketType : byte
		{
			BannerSync,
		}

		public override void HandlePacket(BinaryReader reader, int whoAmI) {
			switch ((PacketType)reader.ReadByte()) {
				case PacketType.BannerSync:
					if (Main.netMode == NetmodeID.Server) // clients only report to the server
						BannerMenuPlayer.ReceiveStored(reader, whoAmI);
					break;
			}
		}
	}
}
