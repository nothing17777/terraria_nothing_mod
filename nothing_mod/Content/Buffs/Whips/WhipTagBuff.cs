using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Buffs.Whips
{
	// Base for whip tag debuffs (abstract, so it is not autoloaded). Pattern from ExampleMod ExampleWhipDebuff.
	public abstract class WhipTagBuff : ModBuff
	{
		public abstract int TagDamage { get; }
		// Percent chance a tagged minion hit becomes a crit (game: WhipTagEffect.CritChance).
		public virtual int TagCritChance => 0;

		// Placeholder icon (vanilla Leather Whip tag debuff).
		public override string Texture => "Terraria/Images/Buff_" + BuffID.CoolWhipNPCDebuff;

		public override void SetStaticDefaults() {
			BuffID.Sets.IsATagBuff[Type] = true;
		}
	}
}
