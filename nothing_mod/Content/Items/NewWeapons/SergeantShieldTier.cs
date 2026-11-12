using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Shared base for the modded Sergeant United Shield tiers (-2, -1, +1, +2).
	// The middle tier is the vanilla Sergeant United Shield itself (80 damage), see SergeantShieldRecipes.
	// Everything except damage, rarity and value is cloned from the vanilla item, so the throw/homing/pierce
	// behavior comes from the vanilla projectile (ProjectileID.SergeantUnitedShield).
	// Abstract, so tModLoader does not load this class as content.
	public abstract class SergeantShieldTier : ModItem
	{
		protected abstract int TierDamage { get; }
		protected abstract int TierRarity { get; }

		// Placeholder sprite: the vanilla shield icon.
		public override string Texture => "Terraria/Images/Item_" + ItemID.SergeantUnitedShield;

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.SergeantUnitedShield);
			Item.damage = TierDamage;
			Item.rare = TierRarity; // UNVERIFIED: rarity per tier is a design choice, not from the wiki
		}

		// TODO: parry (200% contact damage, Striking Moment buff) is vanilla behavior of the held shield and is not
		// implemented for the modded tiers yet. UNVERIFIED how vanilla ties it to the item, check ExampleMod/wiki.
	}
}
