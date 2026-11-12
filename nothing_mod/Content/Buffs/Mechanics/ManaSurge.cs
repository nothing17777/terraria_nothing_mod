using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Buffs.Mechanics
{
	// 1.4.5.7: mana stars grant Mana Surge: +20% magic damage, decaying linearly over 2 seconds.
	public class ManaSurge : ModBuff
	{
		public const int DurationTicks = 120;
		public const float MaxBonus = 0.2f;

		// Placeholder icon borrowed from the vanilla Magic Power buff.
		public override string Texture => "Terraria/Images/Buff_" + BuffID.MagicPower;

		public override void SetStaticDefaults() {
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			float fraction = player.buffTime[buffIndex] / (float)DurationTicks;
			player.GetDamage(DamageClass.Magic) += MaxBonus * fraction;
		}
	}
}
