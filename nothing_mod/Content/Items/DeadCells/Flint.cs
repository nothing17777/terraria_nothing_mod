using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.DeadCells
{
	public class Flint : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.Muramasa;

		public override void SetDefaults()
		{
			Item.damage = 70;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5f;
			Item.value = Item.sellPrice(gold: 4);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		// TODO: charge-up on hold, wide arc swing with aura, wave of fire projectiles (FlintShot/FlintSlash) and pillar eruptions. Currently a plain sword.
		public override void AddRecipes()
		{
			Recipe r = CreateRecipe();
			if (RecipeGroup.recipeGroupIDs.TryGetValue("Adamantite", out int g)) // vanilla "Any Adamantite Bar" group name UNVERIFIED
				r.AddRecipeGroup(g, 24);
			else
				r.AddIngredient(ItemID.AdamantiteBar, 24);
			r.AddIngredient(ItemID.HellstoneBar, 20).AddTile(TileID.AdamantiteForge).Register(); // Titanium Forge counts via adjacent-tile rules
		}
	}
}
