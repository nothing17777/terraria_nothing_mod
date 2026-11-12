using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class CaptainPants : ModItem
	{
		public override void SetDefaults() {
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 125000;
			Item.defense = 11;
		}

		public override void UpdateEquip(Player player) {
			player.fishingSkill += 5;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) {
			return UpgradedArmorSets.IsAngler(head, body, legs);
		}

		public override void UpdateArmorSet(Player player) {
			if (UpgradedArmorSets.IsFirstModPiece(player, Type)) UpgradedArmorSets.ApplyAngler(player);
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.AnglerPants)
				.AddRecipeGroup(ArmorRecipeGroups.MythrilGroup, 10)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
