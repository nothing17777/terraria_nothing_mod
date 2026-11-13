using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems
{
	// Backports more 1.4.5.x changes to existing vanilla items (changelog.txt shipped with the game, final values after 1.4.5.8).
	// Items already handled in Patch145Balance / Patch145Items / Patch145Recipes are not repeated here.
	public class Patch145ExtraItems : GlobalItem
	{
		public override void SetDefaults(Item item)
		{
			switch (item.type)
			{
				case ItemID.NanoBullet:
					if (item.damage == 15) item.damage = 10;        // 1.4.5.7 (guarded: only if 1.4.4.9 value is 15). The +25% total damage effect is not ported.
					break;
				case ItemID.SkeletronHand:
					item.shootSpeed *= 1.07f;                        // 1.4.5.7 velocity ~+7% (range scales with it only through speed; UNVERIFIED)
					break;
				case ItemID.SpikyBall:
					item.autoReuse = true;                           // 1.4.5.0 can be auto-reused
					break;
			}
		}

		public override void UpdateAccessory(Item item, Player player, bool hideVisual)
		{
			switch (item.type)
			{
				case ItemID.ArcaneFlower:
					// 1.4.5.7: inherits Putrid Scent's 5% damage and 5% crit. UNVERIFIED: assumes vanilla Arcane Flower does not already give them.
					player.GetDamage(DamageClass.Generic) += 0.05f;
					player.GetCritChance(DamageClass.Generic) += 5f;
					break;
				case ItemID.ManaRegenerationBand:
					// 1.4.5.7: 12.5 -> 30 mana/s. Vanilla adds 25 to manaRegenBonus for 12.5/s, so 30/s is +60 (linear scaling, UNVERIFIED).
					player.manaRegenBonus += 35;
					// 1.4.5.7: inherits Band of Regeneration's life regeneration (+1 regen unit; UNVERIFIED magnitude).
					player.lifeRegen += 1;
					break;
			}
		}

		// 1.4.5.0: Meteor armor also removes the Laser Rifle's mana cost.
		public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
		{
			if (item.type == ItemID.LaserRifle
				&& player.armor[0].type == ItemID.MeteorHelmet
				&& player.armor[1].type == ItemID.MeteorSuit
				&& player.armor[2].type == ItemID.MeteorLeggings)
				mult = 0f;
		}

		// Confusion on hit. Durations are UNVERIFIED (the changelog gives only chances).
		public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			switch (item.type)
			{
				case ItemID.PurpleClubberfish:
					if (Main.rand.NextBool(2)) target.AddBuff(BuffID.Confused, 120);   // 1.4.5.0: 50%
					break;
				case ItemID.BeeKeeper:
					target.AddBuff(BuffID.Confused, 120);                              // 1.4.5.0: 90% -> 100%
					break;
			}
		}
	}
}
