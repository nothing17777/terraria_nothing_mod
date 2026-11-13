using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using nothing_mod.Content.Items.Accessories;
using nothing_mod.Content.Items.Armor;
using nothing_mod.Content.Items.DeadCells;
using nothing_mod.Content.Items.NewWeapons;
using nothing_mod.Content.Items.Palworld;

namespace nothing_mod.Content.Systems
{
	// Acquisition (chests, crates, bags, recipes, Shimmer, tile drops, fishing) for backported 1.4.5 items.
	// Rates from the terraria.wiki.gg Drops/Recipes Cargo tables. Anything not stated on the wiki is marked UNVERIFIED.
	public class Sources145World : ModSystem
	{
		private bool chestsRolled;

		public override void OnWorldLoad() => chestsRolled = false;
		public override void OnWorldUnload() => chestsRolled = false;
		public override void SaveWorldData(TagCompound tag) { if (chestsRolled) tag["sources145Chests"] = true; }
		public override void LoadWorldData(TagCompound tag) => chestsRolled = tag.ContainsKey("sources145Chests");

		// Chest loot is normally decided at world generation, so every chest is rolled once per world (new or existing).
		public override void PostUpdateWorld()
		{
			if (chestsRolled || Main.netMode == NetmodeID.MultiplayerClient)
				return;
			chestsRolled = true;
			for (int i = 0; i < Main.maxChests; i++)
			{
				Chest c = Main.chest[i];
				if (c == null || c.x < 0 || c.y < 0)
					continue;
				Tile t = Main.tile[c.x, c.y];
				if (!t.HasTile || t.TileType != TileID.Containers)
					continue;
				int style = t.TileFrameX / 36;          // 0 wood, 2 locked gold (dungeon), 11 frozen, 12 living wood
				switch (style)
				{
					case 0 when c.y < Main.worldSurface:  // surface chests
						Roll(c, ModContent.ItemType<PoisonBarb>(), 11);
						break;
					case 2:                               // Locked Gold Chest (Dungeon)
						Roll(c, ModContent.ItemType<SilverBracer>(), 4);
						Roll(c, ModContent.ItemType<RamRune>(), 8);
						break;
					case 11:                              // Frozen Chest
						Roll(c, ModContent.ItemType<GlacierFang>(), 1, 0.1357f);
						break;
					case 12:                              // Living Wood Chest
						Roll(c, ModContent.ItemType<AcornSlingshot>(), 3);
						break;
				}
			}
		}

		private static void Roll(Chest c, int type, int denom, float exact = 0f)
		{
			bool hit = exact > 0f ? Main.rand.NextFloat() < exact : Main.rand.NextBool(denom);
			if (!hit)
				return;
			for (int s = 0; s < Chest.maxItems; s++)
				if (c.item[s].IsAir)
				{
					c.item[s].SetDefaults(type);
					return;
				}
		}

		public override void PostSetupContent()
		{
			// Shimmer transmutes (wiki Recipes table: Alchemy Table -> Alchemy Flask; Shimmer Gun page: Slime Gun).
			ItemID.Sets.ShimmerTransformToItem[ItemID.AlchemyTable] = ModContent.ItemType<AlchemyFlask>();
			ItemID.Sets.ShimmerTransformToItem[ItemID.SlimeGun] = ModContent.ItemType<ShimmerGun>();
		}

		public override void AddRecipes()
		{
			Recipe.Create(ModContent.ItemType<LittleKinshipPeach>())
				.AddRecipeGroup(RecipeGroupID.Fruit).AddIngredient(ItemID.SoulofLight, 12).AddIngredient(ItemID.SoulofNight, 12)
				.AddTile(TileID.CrystalBall).Register();
			Recipe.Create(ModContent.ItemType<PhoenixQuiver>())
				.AddIngredient(ModContent.ItemType<HarpyCharm>()).AddIngredient(ItemID.MoltenQuiver)
				.AddTile(TileID.TinkerersWorkbench).Register();
			Recipe.Create(ModContent.ItemType<SweetBarb>())
				.AddIngredient(ItemID.HoneyComb).AddIngredient(ModContent.ItemType<PoisonBarb>())
				.AddTile(TileID.TinkerersWorkbench).Register();
			foreach (int bar in new[] { ItemID.AdamantiteBar, ItemID.TitaniumBar })
				Recipe.Create(ModContent.ItemType<LightningStrike>())
					.AddIngredient(bar, 24).AddIngredient(ItemID.SoulofNight, 20).AddIngredient(ItemID.ThunderStaff)
					.AddTile(TileID.MythrilAnvil).Register();
		}
	}

	// Chest-like containers and boss bags.
	public class Sources145Items : GlobalItem
	{
		private static readonly int[] HardmodeBags = {
			ItemID.WallOfFleshBossBag, ItemID.TwinsBossBag, ItemID.DestroyerBossBag, ItemID.SkeletronPrimeBossBag, ItemID.PlanteraBossBag,
			ItemID.GolemBossBag, ItemID.FishronBossBag, ItemID.FairyQueenBossBag, ItemID.CultistBossBag, ItemID.BossBagBetsy, ItemID.MoonLordBossBag };

		public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
		{
			int t = item.type;
			if (t == ItemID.LockBox)             // Golden Lock Box
			{
				itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SilverBracer>(), 4));
				itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RamRune>(), 8));
			}
			else if (t == ItemID.WoodenCrate || t == ItemID.WoodenCrateHard)  // Wooden / Pearlwood Crate
				itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<PoisonBarb>(), 120));
			else if (t == ItemID.FrozenCrate || t == ItemID.FrozenCrateHard)  // Boreal / Frozen Crate
				itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<GlacierFang>(), 7));

			// Expert+ boss bags carry the Classic boss drops.
			if (t == ItemID.GolemBossBag) itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<MobiusStrip>(), 6));
			else if (t == ItemID.PlanteraBossBag) itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Whips.VulgarDisplayOfFlower>(), 8));
			else if (t == ItemID.FishronBossBag) itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Whips.ElectricEel>(), 7));
			else if (t == ItemID.MoonLordBossBag) itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Whips.Possession>(), 5));

			// Hardmode boss bags (not Queen Slime): 1/16 for one random developer set of 21 (wiki Treasure Bag + Developer items).
			// Heroicis' set is not in the pool (wishing-well item, below). Classic-mode developer drops: UNVERIFIED, not added.
			if (System.Array.IndexOf(HardmodeBags, t) >= 0)
			{
				AddSet(itemLoot, ModContent.ItemType<ChickenBonesVisor>(), ModContent.ItemType<ChickenBonesVest>(), ModContent.ItemType<ChickenBonesPants>());
				AddSet(itemLoot, ModContent.ItemType<KazzymodusHood>(), ModContent.ItemType<KazzymodusChestpiece>(), ModContent.ItemType<KazzymodusLeggings>());
				AddSet(itemLoot, ModContent.ItemType<LunasRunicPixieHood>(), ModContent.ItemType<LunasRunicPixieShirt>(), ModContent.ItemType<LunasRunicPixiePants>());
			}
		}

		private static void AddSet(ItemLoot loot, int head, int body, int legs)
		{
			IItemDropRule r = ItemDropRule.Common(head, 16 * 21);
			r.OnSuccess(ItemDropRule.Common(body));
			r.OnSuccess(ItemDropRule.Common(legs));
			loot.Add(r);
		}

		// Heroicis' set: a Platinum Coin thrown into water, lava or honey in a Desert (wiki, Heroicis' set).
		public override void PostUpdate(Item item)
		{
			if (item.type != ItemID.PlatinumCoin || Main.netMode == NetmodeID.MultiplayerClient)
				return;
			if (!(item.wet || item.lavaWet || item.honeyWet))
				return;
			Player p = Main.player[Player.FindClosest(item.position, item.width, item.height)];
			if (!p.active || !p.ZoneDesert)
				return;
			var src = new EntitySource_Misc("nothing_mod:HeroicisWish");
			foreach (int piece in new[] { ModContent.ItemType<HeroicisHat>(), ModContent.ItemType<HeroicisCoat>(), ModContent.ItemType<HeroicisPants>() })
				Item.NewItem(src, item.Center, piece);
			if (item.stack > 1) item.stack--; else item.TurnToAir();
			if (Main.netMode == NetmodeID.Server)
				NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item.whoAmI);
		}
	}

	// Lucky Clover: 1/1000 from cutting tall grass (wiki). UNVERIFIED: wiki says "without flowers"; flower frames are not filtered here.
	public class Sources145Tiles : GlobalTile
	{
		public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
			if (fail || effectOnly || Main.netMode == NetmodeID.MultiplayerClient)
				return;
			if ((type == TileID.Plants || type == TileID.CorruptPlants || type == TileID.CrimsonPlants || type == TileID.HallowedPlants) && Main.rand.NextBool(1000))
				Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<LuckyClover>());
		}
	}

	// Barnacle Staff: rare Hardmode Jungle catch. UNVERIFIED rate: wiki says 33% more common than Crystal Serpent but gives no number.
	public class Sources145Fishing : ModPlayer
	{
		public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Microsoft.Xna.Framework.Vector2 sonarPosition)
		{
			if (Main.hardMode && Player.ZoneJungle && attempt.rare && !attempt.crate && !attempt.inLava && !attempt.inHoney && Main.rand.NextBool(4))
				itemDrop = ModContent.ItemType<BarnacleStaff>();
		}
	}
}
