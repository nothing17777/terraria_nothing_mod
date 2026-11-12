using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using nothing_mod.Content.Buffs.NewWeapons;
using nothing_mod.Content.Projectiles.NewWeapons;

namespace nothing_mod.Content.Items.NewWeapons
{
	// Pink Phasesaber. Sprite is a placeholder (Purple Phasesaber).
	public class PinkPhasesaber : ModItem
	{

		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3f;
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item15;
			Item.autoReuse = true;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			Lighting.AddLight(hitbox.Center.ToVector2(), 1f, 0.45f, 0.75f);
		}

		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ModContent.ItemType<PinkPhaseblade>()).AddIngredient(ItemID.CrystalShard, 25).AddTile(TileID.MythrilAnvil).Register();
		}
		// TODO: throw on release / recall mechanic.
	}
}
