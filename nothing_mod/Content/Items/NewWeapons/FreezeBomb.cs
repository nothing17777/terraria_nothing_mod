using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Bomb that freezes water. Sprite is a placeholder (Bomb).
	public class FreezeBomb : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.DamageType = DamageClass.Default;
			Item.width = 20;
			Item.height = 20;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
			Item.knockBack = 12f;
			Item.value = Item.buyPrice(silver: 10); // sells for 2 silver
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<FreezeBombProj>();
			Item.shootSpeed = 5f;
		}
		// TODO: Demolitionist shop entry (night, after EoC/King Slime) not added; no crafting recipe on the wiki.
	}
}
