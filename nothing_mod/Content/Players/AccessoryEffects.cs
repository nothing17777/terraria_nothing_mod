using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Items.Accessories;

namespace nothing_mod.Content.Players
{
	// Shared state for the backported 1.4.5.x accessories.
	public class AccessoryEffectsPlayer : ModPlayer
	{
		public bool WhipKnockback;
		public bool WhipRange;
		public bool PoisonArrows;
		public bool SnappingStone;
		public bool PyroclasticPulse;
		public bool ArmletPulse;
		public bool HarpyCharm;
		public bool SeraphCharm;
		public bool CatalystBand;
		public int PulseCooldown;       // ref accSnappingStoneCooldown: shared by the three pulse accessories, 360 ticks
		public int HarpyCharmCooldown;  // ref accHarpyCharmCooldown
		public bool StressBall;
		public int PrettyMirrorTime; // ticks of -0.25 luck remaining

		public override void ResetEffects()
		{
			WhipKnockback = false;
			WhipRange = false;
			PoisonArrows = false;
			SnappingStone = false;
			PyroclasticPulse = false;
			ArmletPulse = false;
			HarpyCharm = false;
			SeraphCharm = false;
			CatalystBand = false;
			StressBall = false;
		}

		public override void PostUpdateEquips()
		{
			if (WhipRange)
				Player.whipRangeMultiplier += 0.1f;
			if (PrettyMirrorTime > 0)
				PrettyMirrorTime--;
			if (PulseCooldown > 0)
				PulseCooldown--;
			if (HarpyCharmCooldown > 0)
				HarpyCharmCooldown--;
		}

		public override void PostUpdate()
		{
			if (HarpyCharm && Player.whoAmI == Main.myPlayer && HarpyCharmCooldown <= 0)
				HarpyCharmLogic.TryRedirectArrow(Player, this);
		}

		public override void ModifyLuck(ref float luck)
		{
			// ref Player.ts luckEquipmentBonus: inventory always counts; the Void Vault only counts while a Void Lens is carried.
			bool lens = Player.HasItem(ItemID.VoidLens);
			if (HasLuckItem(ModContent.ItemType<LuckyClover>(), lens))
				luck += 0.03f;
			if (HasLuckItem(ModContent.ItemType<RavenFeather>(), lens))
				luck -= 0.1f;
			if (PrettyMirrorTime > 0)
				luck -= 0.25f;
		}

		private bool HasLuckItem(int type, bool includeVoidBag)
		{
			if (Player.HasItem(type))
				return true;
			if (includeVoidBag)
				foreach (Item it in Player.bank4.item)
					if (it.type == type && it.stack > 0)
						return true;
			return false;
		}

		public override void SetControls()
		{
			// TODO: skip wiring/paint tools, Bug Net; food only without Well Fed.
			if (StressBall && !Main.playerInventory && Player.velocity == Vector2.Zero
				&& Player.itemAnimation == 0 && Player.HeldItem.fishingPole == 0)
				Player.controlUseItem = true;
		}

		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
		{
			if (WhipKnockback && ProjectileID.Sets.IsAWhip[proj.type])
				modifiers.Knockback += 1f;
			if (proj.owner == Player.whoAmI && proj.type != ModContent.ProjectileType<AccessoryPulseProjectile>())
				AccessoryPulse.OnHit(Player, this, target, proj, proj.Center, proj.position, proj.width, proj.height, proj.CountsAsClass(DamageClass.Melee), proj.CountsAsClass(DamageClass.Ranged), proj.CountsAsClass(DamageClass.Summon) || proj.minion || ProjectileID.Sets.IsAWhip[proj.type], ref modifiers);
		}

		public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
		{
			if (item.CountsAsClass(DamageClass.Melee))
				AccessoryPulse.OnHit(Player, this, target, null, target.Center, target.Center, 1, 1, true, false, false, ref modifiers);
		}

		public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (proj.owner != Player.whoAmI)
				return;
			if (PoisonArrows && proj.arrow)
				target.AddBuff(BuffID.Poisoned, 90);
			if (CatalystBand)
				target.AddBuff(ModContent.BuffType<AcceleratedAfflictions>(), 300); // ref projStatus: any projectile hit, 300 ticks
		}
	}
}
