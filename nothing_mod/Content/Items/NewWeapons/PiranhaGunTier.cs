using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Shared base for the modded Piranha Gun tiers (-2, -1, +1, +2).
	// The middle tier is the vanilla Piranha Gun itself (40 damage), see PiranhaGunRecipes.
	// Everything except damage and rarity is cloned from the vanilla item, so the latching piranhas
	// and Hemorrhage come from the vanilla projectile. Abstract, so tModLoader does not load this class as content.
	public abstract class PiranhaGunTier : ModItem
	{
		protected abstract int TierDamage { get; }
		protected abstract int TierRarity { get; }

		// Placeholder sprite: the vanilla Piranha Gun icon.
		public override string Texture => "Terraria/Images/Item_" + ItemID.PiranhaGun;

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.PiranhaGun);
			Item.damage = TierDamage;
			Item.rare = TierRarity; // UNVERIFIED: rarity per tier is a design choice, not from the wiki
		}

		// UNVERIFIED: the vanilla piranha projectile may check that the held item is the vanilla Piranha Gun
		// to stay alive while the button is held. If the modded tiers fire piranhas that vanish at once, look there.
	}
}
