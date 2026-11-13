using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Players
{
	// 1.4.5.x player mechanics for existing vanilla gear and buffs (changelog.txt shipped with the game).
	public class Patch145ExtraPlayer : ModPlayer
	{
		// Falcon Blade / Exotic Scimitar / Cutlass (1.4.5.0): each hit +12% damage, max 50%, decays 30% per second.
		private float swordRamp;

		private static bool IsRampSword(Item item) =>
			item.type == ItemID.FalconBlade || item.type == ItemID.DyeTradersScimitar || item.type == ItemID.Cutlass;

		public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (IsRampSword(item))
				swordRamp = System.Math.Min(0.5f, swordRamp + 0.12f);
		}

		public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
		{
			if (IsRampSword(item) && swordRamp > 0f)
				damage += swordRamp;
		}

		public override void ResetEffects()
		{
			if (swordRamp > 0f)
				swordRamp = System.Math.Max(0f, swordRamp - 0.30f / 60f);
		}

		public override void PostUpdateEquips()
		{
			// 1.4.5.0 Molten armor: immune to burning blocks (fireWalk, as the Obsidian Skull).
			if (Player.armor[0].type == ItemID.MoltenHelmet && Player.armor[1].type == ItemID.MoltenBreastplate && Player.armor[2].type == ItemID.MoltenGreaves)
				Player.fireWalk = true;

			if (Player.mount.Active)
			{
				if (Player.mount.Type == MountID.LavaShark)
					Player.fireWalk = true;                 // 1.4.5.0 Superheated Blood (lava vision / jump / no water walking not ported)
				if (Player.mount.Type == MountID.Slime)
					Player.extraFall += 10;                 // 1.4.5.0 Slimy Saddle: +10 blocks safe fall
				if (Player.mount.Type == MountID.PogoStick)
					Player.extraFall += 20;                 // 1.4.5.0 Pogo Stick: +20 blocks safe fall
			}

			// 1.4.5.7 Flask of Nanites: +5% total melee and whip damage.
			if (Player.HasBuff(BuffID.WeaponImbueNanites))
			{
				Player.GetDamage(DamageClass.Melee) += 0.05f;
				Player.GetDamage(DamageClass.SummonMeleeSpeed) += 0.05f;
			}

			// 1.4.5.0 Skeletron / Skeletron Prime enraged by daylight can no longer be dodged by accessories.
			if (Main.IsItDay() && (NPC.AnyNPCs(NPCID.SkeletronHead) || NPC.AnyNPCs(NPCID.SkeletronPrime)))
			{
				Player.blackBelt = false;
				Player.shadowDodge = false;
			}
		}

		public override void PostUpdateBuffs()
		{
			// 1.4.5.0 Wet (Water Gun) grants immunity to On Fire!, Hellfire and Burning.
			if (Player.HasBuff(BuffID.Wet))
			{
				Player.ClearBuff(BuffID.OnFire);
				Player.ClearBuff(BuffID.OnFire3);
				Player.ClearBuff(BuffID.Burning);
			}

			// 1.4.5.7 Warmth Potion: Chilled duration -75%, Frozen -50% (extra decay on top of the normal 1 tick per tick).
			if (Player.HasBuff(BuffID.Warmth))
			{
				Shorten(BuffID.Chilled, 3);
				Shorten(BuffID.Frozen, 1);
			}
		}

		private void Shorten(int buffType, int extraTicks)
		{
			int i = Player.FindBuffIndex(buffType);
			if (i < 0)
				return;
			if (Player.buffTime[i] > extraTicks)
				Player.buffTime[i] -= extraTicks;
			else
				Player.DelBuff(i);
		}
	}
}
