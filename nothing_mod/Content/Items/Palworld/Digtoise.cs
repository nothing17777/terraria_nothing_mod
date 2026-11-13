using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Projectiles.Palworld;
using nothing_mod.Content.Buffs.Palworld;

namespace nothing_mod.Content.Items.Palworld
{
	public class Digtoise : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.useTime = 15; // wiki: use time 15
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.buffType = ModContent.BuffType<DigtoiseBuff>();
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(gold: 1);
		}

		public override bool AltFunctionUse(Player player) => true;

		// Left click: summon, then order it to mine (toward the cursor) or stop. Right click: pet it when the cursor is on it, otherwise unsummon.
		public override bool? UseItem(Player player)
		{
			if (player.whoAmI != Main.myPlayer)
				return true;
			int pet = ModContent.ProjectileType<DigtoisePet>();
			bool present = player.ownedProjectileCounts[pet] > 0;
			if (player.altFunctionUse == 2)
			{
				if (PalPetting.TryPet(player, pet))
					return true;
				player.ClearBuff(Item.buffType);
				foreach (Projectile p in Main.ActiveProjectiles)
					if (p.owner == player.whoAmI && p.type == pet)
						p.Kill();
				return true;
			}
			if (!present)
				player.AddBuff(Item.buffType, 3600);
			else
				DigtoiseAI.Order(player, pet);
			return true;
		}
		// Acquisition not implemented (wiki: found as a Sleeping Digtoise in the Underground Desert; world-gen change).
	}
}
