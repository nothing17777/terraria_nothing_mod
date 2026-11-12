using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Pink Phaseblade. Sprite is a placeholder (Purple Phaseblade).
	public class PinkPhaseblade : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 26;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3f;
			Item.value = Item.sellPrice(silver: 54);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item15;
			Item.autoReuse = true;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			Lighting.AddLight(hitbox.Center.ToVector2(), 1f, 0.45f, 0.75f);
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ItemID.MeteoriteBar, 15).AddIngredient(ItemID.PinkPearl, 15).AddTile(TileID.Anvils).Register();
		}
		// TODO: throw on release / recall mechanic.
	}
}
