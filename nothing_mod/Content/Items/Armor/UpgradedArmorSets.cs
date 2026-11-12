using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Armor
{
	/// <summary>
	/// Prospector / Captain pieces mix freely with the vanilla Mining / Angler pieces
	/// (ref vanillaArmorSets.ts 'Mining' triples [88|5588|4008]x[410|5589]x[411|5590],
	/// 'Angler' [2367|5591]x[2368|5592]x[2369|5593]).
	/// </summary>
	public static class UpgradedArmorSets
	{
		static bool IsMiningHead(Item i) => i.type == ItemID.MiningHelmet || i.type == ItemID.UltrabrightHelmet || i.type == ModContent.ItemType<ProspectorHelmet>();
		static bool IsMiningBody(Item i) => i.type == ItemID.MiningShirt || i.type == ModContent.ItemType<ProspectorShirt>();
		static bool IsMiningLegs(Item i) => i.type == ItemID.MiningPants || i.type == ModContent.ItemType<ProspectorPants>();
		static bool IsAnglerHead(Item i) => i.type == ItemID.AnglerHat || i.type == ModContent.ItemType<CaptainHat>();
		static bool IsAnglerBody(Item i) => i.type == ItemID.AnglerVest || i.type == ModContent.ItemType<CaptainVest>();
		static bool IsAnglerLegs(Item i) => i.type == ItemID.AnglerPants || i.type == ModContent.ItemType<CaptainPants>();

		public static bool IsMining(Item head, Item body, Item legs) => IsMiningHead(head) && IsMiningBody(body) && IsMiningLegs(legs);
		public static bool IsAngler(Item head, Item body, Item legs) => IsAnglerHead(head) && IsAnglerBody(body) && IsAnglerLegs(legs);

		/// <summary>tML calls UpdateArmorSet on every mod piece in the set; only the first one (head, body, legs) may apply the bonus.</summary>
		public static bool IsFirstModPiece(Player player, int myType) {
			for (int s = 0; s < 3; s++) {
				Item it = player.armor[s];
				if (it.ModItem is ProspectorHelmet || it.ModItem is ProspectorShirt || it.ModItem is ProspectorPants
					|| it.ModItem is CaptainHat || it.ModItem is CaptainVest || it.ModItem is CaptainPants)
					return it.type == myType;
			}
			return true;
		}

		public static void ApplyMining(Player player) {
			player.setBonus = "10% increased mining speed"; // ref/game lang ArmorSetBonus.Mining
			player.pickSpeed -= 0.10f; // ref vanillaArmorSets 'Mining' pickSpeed 0.1
		}

		public static void ApplyAngler(Player player) {
			player.setBonus = "Decreased enemy spawn rate"; // game lang ArmorSetBonus.Angler
			player.calmed = true; // ref VanillaSpawner anglerSetSpawnReduction: spawn rate x1.3, max spawns x0.7 = Calm values
		}
	}

	public class ArmorRecipeGroups : ModSystem
	{
		public const string MythrilGroup = "nothing_mod:MythrilOrOrichalcumBar";

		public override void AddRecipeGroups() {
			RecipeGroup.RegisterGroup(MythrilGroup, new RecipeGroup(
				() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.MythrilBar),
				ItemID.MythrilBar, ItemID.OrichalcumBar));
		}
	}
}
