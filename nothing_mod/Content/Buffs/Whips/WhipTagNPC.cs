using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Buffs.Whips
{
	public class WhipTagNPC : GlobalNPC
	{
		public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers) {
			if (projectile.npcProj || projectile.trap || !projectile.IsMinionOrSentryRelated)
				return;

			float mult = ProjectileID.Sets.SummonTagDamageMultiplier[projectile.type];
			for (int i = 0; i < NPC.maxBuffs; i++) {
				if (npc.buffTime[i] > 0 && ModContent.GetModBuff(npc.buffType[i]) is WhipTagBuff tag)
				{
					modifiers.FlatBonusDamage += tag.TagDamage * mult;
					if (tag.TagCritChance > 0 && Main.rand.Next(100) < tag.TagCritChance)
						modifiers.SetCrit();
				}
			}
		}
	}
}
