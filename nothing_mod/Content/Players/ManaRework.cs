using Microsoft.Xna.Framework;
using nothing_mod.Content.Buffs.Mechanics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Players
{
	// 1.4.5.7 mana rework (changelog "D. Magic Weapons > Mana"; numbers cross-checked against the
	// reference port's rules, code written from scratch).
	//  - regeneration delay removed
	//  - regen continues while attacking at 5%
	//  - standing still / grappling / Mana Regeneration Potion doubles the base regen
	//  - lower mana regenerates faster (0.5 + 0.5 * mana/max, potion: always 1.0)
	//  - -13% overall (the old x1.15 multiplier is gone)
	//  - mana weapons can be fired without enough mana: it drains the rest and use speed is 40%
	public class ManaRework : ModPlayer
	{
		private const float TrickleFactor = 0.05f;
		private const float EmptyManaSpeed = 0.4f;
		private const int AttackGraceTicks = 4; // reference keeps the "attacking" state ~4 frames after a cast

		private int regenAccumulator;
		private int attackTimer;
		private int manaAtItemCheck;
		private uint missingManaTick = uint.MaxValue;

		public override void PostUpdateEquips() {
			// Run before vanilla's UpdateManaRegen: park vanilla's regen delay high so it regenerates nothing,
			// and zero its accumulators. We do the regeneration ourselves below.
			Player.manaRegenDelay = 1000;
			Player.manaRegen = 0;
			Player.manaRegenCount = 0;

			if (Player.dead)
				return;

			if (Player.itemAnimation > 0 && Player.HeldItem.mana > 0)
				attackTimer = AttackGraceTicks;
			bool attacking = attackTimer > 0;
			if (attackTimer > 0)
				attackTimer--;

			int max = Player.statManaMax2;
			if (max <= 0)
				return;

			bool still = Player.IsStandingStillForSpecialEffects || Player.grappling[0] >= 0;
			int perTick = max / 3 + Player.manaRegenBonus + 1;
			int regen = perTick;
			if (still || Player.manaRegenBuff)
				regen += perTick;
			if (Player.usedArcaneCrystal)
				regen += max / 50;

			float fullness = Player.statMana / (float)max;
			float floor = Player.manaRegenBuff ? 1f : 0.5f;
			float factor = floor + (1f - floor) * fullness;
			if (attacking)
				factor *= TrickleFactor;

			regen = (int)(regen * factor);
			if (regen < 2)
				regen = 2;

			regenAccumulator += regen;
			while (regenAccumulator >= 120) {
				regenAccumulator -= 120;
				if (Player.statMana < max) {
					Player.statMana++;
					if (Player.statMana >= max && Player.whoAmI == Main.myPlayer && !Main.dedServ) {
						SoundEngine.PlaySound(SoundID.MaxMana);
						for (int i = 0; i < 5; i++) {
							Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.ManaRegeneration, 0f, 0f, 255, default, Main.rand.Next(20, 26) * 0.1f);
							d.noLight = true;
							d.noGravity = true;
							d.velocity *= 0.5f;
						}
					}
				}
			}
			if (Player.statMana > max)
				Player.statMana = max;
		}

		public override bool PreItemCheck() {
			manaAtItemCheck = Player.statMana;
			return true;
		}

		// Vanilla refuses to use a mana weapon when mana is short. Topping mana up to the cost here makes
		// vanilla pay it normally, which leaves mana at 0 and lets the cast happen.
		// UNVERIFIED: whether Mana Flower's auto-potion runs before or after this hook.
		public override void OnMissingMana(Item item, int neededMana) {
			Player.statMana = neededMana;
			missingManaTick = Main.GameUpdateCount;
		}

		public override float UseSpeedMultiplier(Item item) {
			if (item.mana <= 0)
				return 1f;
			if (missingManaTick == Main.GameUpdateCount)
				return EmptyManaSpeed;
			// Mana Flower may drink a potion instead, so there only the hook flag above counts.
			if (!Player.manaFlower && Player.manaCost > 0f && manaAtItemCheck < (int)(item.mana * Player.manaCost))
				return EmptyManaSpeed;
			return 1f;
		}

		// TODO: the "squint" facial animation when casting without mana (cosmetic, needs a draw layer).
	}

	// Mana stars (Star, Soul Cake, Sugar Plum) also grant Mana Surge when picked up.
	public class ManaStarSurge : GlobalItem
	{
		public override bool OnPickup(Item item, Player player) {
			if (item.type == ItemID.Star || item.type == ItemID.SoulCake || item.type == ItemID.SugarPlum)
				player.AddBuff(ModContent.BuffType<ManaSurge>(), ManaSurge.DurationTicks);
			return true;
		}
	}
}
