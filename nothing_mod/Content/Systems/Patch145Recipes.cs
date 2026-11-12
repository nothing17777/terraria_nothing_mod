using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems
{
	// Backports 1.4.5.x recipe changes to existing vanilla items.
	// Source: changelog.txt shipped with the game (sections "Recipe changes" in 1.4.5 and Balance in 1.4.5.7),
	// final values after 1.4.5.8. Every block is wrapped so a failure only logs a warning instead of breaking mod load.
	public class Patch145Recipes : ModSystem
	{
		// ---------- new / duplicated recipes (vanilla recipes already exist when this runs) ----------
		public override void AddRecipes()
		{
			Guard("alternate stations", AddAlternateStations);
			Guard("extra recipes", AddExtraRecipes);
			Guard("Format:C", ReplaceFormatC);
		}

		// All Loom recipes can also be crafted at Living Looms; all Water recipes can also be crafted at Fountains (1.4.5.0).
		private void AddAlternateStations()
		{
			int count = Recipe.numRecipes;
			for (int i = 0; i < count; i++)
			{
				Recipe r = Main.recipe[i];
				if (r == null || r.Mod != null || r.Disabled || r.createItem.type <= 0)
					continue;
				if (r.requiredTile.Contains(TileID.Loom))
					Duplicate(r, TileID.Loom, TileID.LivingLoom, false);
				if (r.HasCondition(Condition.NearWater))
					Duplicate(r, -1, TileID.WaterFountain, true);
			}
		}

		private void Duplicate(Recipe src, int dropTile, int addTile, bool dropWater)
		{
			Recipe c = Recipe.Create(src.createItem.type, src.createItem.stack);
			foreach (Item ing in src.requiredItem)
				c.AddIngredient(ing.type, ing.stack);
			foreach (int g in src.acceptedGroups)
				if (!c.acceptedGroups.Contains(g))
					c.acceptedGroups.Add(g);
			foreach (int t in src.requiredTile)
				if (t != dropTile)
					c.AddTile(t);
			c.AddTile(addTile);
			foreach (Condition cond in src.Conditions)
				if (!dropWater || cond != Condition.NearWater)
					c.AddCondition(cond);
			c.Register();
		}

		private void AddExtraRecipes()
		{
			// Mushroom Torch: also from Glowing Mushrooms and Gel.
			Recipe.Create(ItemID.MushroomTorch, 3).AddIngredient(ItemID.Gel).AddIngredient(ItemID.GlowingMushroom).Register();
			// Golden Delight also from Golden Carp, Roasted Bird also from Owl.
			Recipe.Create(ItemID.GoldenDelight).AddIngredient(ItemID.GoldenCarp).AddTile(TileID.CookingPots).Register();
			Recipe.Create(ItemID.RoastedBird).AddIngredient(ItemID.Owl).AddTile(TileID.CookingPots).Register();
			// Furnace also from Ebonstone, Crimstone and Pearlstone.
			foreach (int stone in new[] { ItemID.EbonstoneBlock, ItemID.CrimstoneBlock, ItemID.PearlstoneBlock })
				Recipe.Create(ItemID.Furnace).AddIngredient(stone, 20).AddRecipeGroup(RecipeGroupID.Wood, 4).AddIngredient(ItemID.Torch, 3).AddTile(TileID.WorkBenches).Register();
			// Lihzahrd Work Bench can be crafted at a Lihzahrd Furnace.
			Recipe.Create(ItemID.LihzahrdWorkBench).AddIngredient(ItemID.LihzahrdBrick, 10).AddTile(TileID.LihzahrdFurnace).Register();
		}

		// 1.4.5.7: Format:C is crafted from Gold/Platinum + Adamantite/Titanium Bars, Souls of Night and Souls of Fright (Mythril Anvil).
		private void ReplaceFormatC()
		{
			for (int i = 0; i < Recipe.numRecipes; i++)
			{
				Recipe r = Main.recipe[i];
				if (r.Mod == null && r.createItem.type == ItemID.FormatC)
					r.DisableRecipe();
			}
			foreach (int top in new[] { ItemID.GoldBar, ItemID.PlatinumBar })
				foreach (int mech in new[] { ItemID.AdamantiteBar, ItemID.TitaniumBar })
					Recipe.Create(ItemID.FormatC)
						.AddIngredient(top, 12).AddIngredient(mech, 6)
						.AddIngredient(ItemID.SoulofNight, 8).AddIngredient(ItemID.SoulofFright, 15)
						.AddTile(TileID.MythrilAnvil).Register();
		}

		// ---------- edits to existing recipes ----------
		public override void PostAddRecipes()
		{
			Guard("recipe edits", EditRecipes);
		}

		private void EditRecipes()
		{
			// Watches, Goggles and Sunglasses: Work Bench instead of table + chair (1.4.5.0).
			ForAll(r =>
			{
				if (r.requiredTile.Contains(TileID.Tables) && r.requiredTile.Contains(TileID.Chairs))
				{
					r.RemoveTile(TileID.Tables);
					r.RemoveTile(TileID.Chairs);
					r.AddTile(TileID.WorkBenches);
				}
			});

			// Phaseblades: 15 gems, up from 10 (1.4.5.0).
			int[] phaseblades = { ItemID.BluePhaseblade, ItemID.RedPhaseblade, ItemID.GreenPhaseblade, ItemID.PurplePhaseblade, ItemID.WhitePhaseblade, ItemID.YellowPhaseblade };
			int[] gems = { ItemID.Sapphire, ItemID.Ruby, ItemID.Emerald, ItemID.Amethyst, ItemID.Diamond, ItemID.Topaz };
			foreach (int blade in phaseblades)
				For(blade, r => { foreach (Item ing in r.requiredItem) if (gems.Contains(ing.type) && ing.stack == 10) ing.stack = 15; });

			// Endless Quiver / Endless Musket Pouch: 9999 ammo, up from 3996 (1.4.5.0).
			For(ItemID.EndlessQuiver, r => SetStack(r, ItemID.WoodenArrow, 9999));
			For(ItemID.EndlessMusketPouch, r => SetStack(r, ItemID.MusketBall, 9999));

			// Unholy Arrow: 1 Worm Tooth makes 20, 1 Vertebra makes 10 (1.4.5.0).
			For(ItemID.UnholyArrow, r =>
			{
				if (r.HasIngredient(ItemID.WormTooth)) { r.createItem.stack = 20; SetStack(r, ItemID.WoodenArrow, 20); }
				else if (r.HasIngredient(ItemID.Vertebrae)) { r.createItem.stack = 10; SetStack(r, ItemID.WoodenArrow, 10); }
			});

			// Poison Dart needs an Anvil (1.4.5.0).
			For(ItemID.PoisonDart, r => { if (!r.requiredTile.Contains(TileID.Anvils)) r.AddTile(TileID.Anvils); });

			// Mythril/Orichalcum Anvil requirement (1.4.5.0).
			foreach (int item in new[] { ItemID.CursedDart, ItemID.IchorDart, ItemID.CrystalDart, ItemID.SpiderStaff, ItemID.QueenSpiderStaff,
				ItemID.SpiderMask, ItemID.SpiderBreastplate, ItemID.SpiderGreaves, ItemID.VenomDartTrap })
				For(item, r => { r.RemoveTile(TileID.Anvils); if (!r.requiredTile.Contains(TileID.MythrilAnvil)) r.AddTile(TileID.MythrilAnvil); });

			// Stink Potion makes 2, Pumpkin Pie costs 15 Pumpkins (1.4.5.0).
			For(ItemID.StinkPotion, r => r.createItem.stack = 2);
			For(ItemID.PumpkinPie, r => SetStack(r, ItemID.Pumpkin, 15));

			// 1.4.5.7: Super Mana Potion = Greater Mana Potions + Fallen Stars + Ectoplasm.
			For(ItemID.SuperManaPotion, r =>
			{
				foreach (int t in r.requiredItem.Select(i => i.type).ToList())
					r.RemoveIngredient(t);
				r.AddIngredient(ItemID.GreaterManaPotion, 8).AddIngredient(ItemID.FallenStar, 2).AddIngredient(ItemID.Ectoplasm);
				r.createItem.stack = 8;
			});

			// 1.4.5.7: Light Disc, Malaise, Artery cost less; Magic Cuffs use Band of Starpower.
			For(ItemID.LightDisc, r => { SetStack(r, ItemID.HallowedBar, 12); SetStack(r, ItemID.SoulofMight, 15); });
			For(ItemID.CorruptYoyo, r => SetStack(r, ItemID.DemoniteBar, 10));
			For(ItemID.CrimsonYoyo, r => SetStack(r, ItemID.CrimtaneBar, 10));
			For(ItemID.MagicCuffs, r => { if (r.HasIngredient(ItemID.ManaRegenerationBand)) { r.RemoveIngredient(ItemID.ManaRegenerationBand); r.AddIngredient(ItemID.BandofStarpower); } });
		}

		// ---------- helpers ----------
		private static void ForAll(Action<Recipe> edit)
		{
			for (int i = 0; i < Recipe.numRecipes; i++)
			{
				Recipe r = Main.recipe[i];
				if (r != null && r.Mod == null && !r.Disabled)
					edit(r);
			}
		}

		private static void For(int resultType, Action<Recipe> edit)
		{
			ForAll(r => { if (r.createItem.type == resultType) edit(r); });
		}

		private static void SetStack(Recipe r, int ingredientType, int stack)
		{
			foreach (Item ing in r.requiredItem)
				if (ing.type == ingredientType)
					ing.stack = stack;
		}

		private void Guard(string what, Action action)
		{
			try { action(); }
			catch (Exception e) { Mod.Logger.Warn("Patch145Recipes: " + what + " failed: " + e.Message); }
		}
	}
}
