using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Shared base for the modded Vampire Knives tiers (-2, -1, +1, +2).
	// The middle tier is the vanilla Vampire Knives itself (29 damage), see VampireKnivesRecipes.
	// Everything except damage and rarity is cloned from the vanilla item, so the knife spread and
	// lifesteal come from the vanilla projectiles. Abstract, so tModLoader does not load this class as content.
	public abstract class VampireKnivesTier : ModItem
	{
		protected abstract int TierDamage { get; }
		protected abstract int TierRarity { get; }

		// Placeholder sprite: the vanilla Vampire Knives icon.
		public override string Texture => "Terraria/Images/Item_" + ItemID.VampireKnives;

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.VampireKnives);
			Item.damage = TierDamage;
			Item.rare = TierRarity; // UNVERIFIED: rarity per tier is a design choice, not from the wiki
		}
	}
}
