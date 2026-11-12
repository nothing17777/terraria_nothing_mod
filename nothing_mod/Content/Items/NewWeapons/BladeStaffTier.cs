using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Shared base for the modded Blade Staff tiers (-2, -1, +1, +2).
	// The middle tier is the vanilla Blade Staff itself (6 damage), see BladeStaffRecipes.
	// Everything except damage and rarity is cloned from the vanilla item, so the Enchanted Dagger minion
	// (buff and projectile) is the vanilla one.
	// Abstract, so tModLoader does not load this class as content.
	public abstract class BladeStaffTier : ModItem
	{
		protected abstract int TierDamage { get; }
		protected abstract int TierRarity { get; }

		// Placeholder sprite: the vanilla Blade Staff icon.
		// UNVERIFIED: ItemID.Smolstar is the internal name of the Blade Staff (item 4758).
		public override string Texture => "Terraria/Images/Item_" + ItemID.Smolstar;

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.Smolstar);
			Item.damage = TierDamage;
			Item.rare = TierRarity; // UNVERIFIED: rarity per tier is a design choice, not from the wiki
		}

		// UNVERIFIED: all tiers share the vanilla Enchanted Daggers buff, so summoning two different tiers
		// at once may behave oddly. Test in game.
	}
}
