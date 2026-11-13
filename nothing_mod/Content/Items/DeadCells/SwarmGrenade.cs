using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Projectiles.DeadCells;
using nothing_mod.Content.Buffs.DeadCells;

namespace nothing_mod.Content.Items.DeadCells
{
	public class SwarmGrenade : ModItem
	{

		public override void SetDefaults()
		{
			Item.DefaultToVanitypet(ModContent.ProjectileType<SwarmBiterPet>(), ModContent.BuffType<SwarmBiterBuff>());
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(gold: 1);
		}

		public override bool? UseItem(Player player)
		{
			if (player.whoAmI == Main.myPlayer)
				player.AddBuff(Item.buffType, 3600);
			return true;
		}
		// TODO: sold by the Zoologist for 10 gold during a Party (shop edit). Sell price UNVERIFIED.
	}
}
