using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Accessory. Sprite is a placeholder (vanilla EndlessQuiver).
	public class ChaosCylinder : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.sellPrice(gold: 8);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Ranged) += 0.08f;
		}
		// TODO: ammo cycling modes (Default/Full/Random/Simple) not implemented; Hallowed Mimic drop not added.
	}
}
