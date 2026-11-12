using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Lets the vanilla Acorn item act as ammo for the Acorn Slingshot.
	public class AcornAmmoGlobalItem : GlobalItem
	{
		public override void SetDefaults(Item item)
		{
			if (item.type == ItemID.Acorn)
				item.ammo = ItemID.Acorn;
		}
	}
}
