using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items
{
	// Drinking this applies every vanilla buff-potion effect at once.
	// Battle Potion is left out on purpose: it cancels out the Calming Potion.
	public class AllBuffsPotion : ModItem
	{
		// Borrow a vanilla potion sprite until custom art exists.
		public override string Texture => "Terraria/Images/Item_" + ItemID.LifeforcePotion;

		private static readonly int[] Buffs =
		{
			BuffID.AmmoReservation, BuffID.Archery, BuffID.BiomeSight, BuffID.Builder, BuffID.Calm,
			BuffID.Dangersense, BuffID.Endurance, BuffID.Featherfall, BuffID.Gills, BuffID.Gravitation,
			BuffID.Heartreach, BuffID.Hunter, BuffID.Inferno, BuffID.Invisibility, BuffID.Ironskin,
			BuffID.Lifeforce, BuffID.MagicPower, BuffID.ManaRegeneration, BuffID.Mining, BuffID.NightOwl,
			BuffID.ObsidianSkin, BuffID.Rage, BuffID.Regeneration, BuffID.Shine, BuffID.Spelunker,
			BuffID.Summoning, BuffID.Swiftness, BuffID.Thorns, BuffID.Titan, BuffID.Warmth,
			BuffID.WaterWalking, BuffID.Wrath
		};

		private const int DurationTicks = 10 * 60 * 60; // 10 minutes

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 30;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useTurn = true;
			Item.UseSound = SoundID.Item3;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.sellPrice(gold: 1);
		}

		public override bool? UseItem(Player player)
		{
			foreach (int buff in Buffs)
				player.AddBuff(buff, DurationTicks);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(ItemID.FallenStar, 2);
			recipe.AddTile(TileID.Bottles);
			recipe.Register();
		}
	}
}
