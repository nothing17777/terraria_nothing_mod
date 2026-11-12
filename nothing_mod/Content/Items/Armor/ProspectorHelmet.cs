using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class ProspectorHelmet : ModItem
	{
		public override void SetDefaults() {
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 125000;
			Item.defense = 10;
		}

		public override void UpdateEquip(Player player) {
			Lighting.AddLight(player.Center, 1f, 0.88f, 0.73f); // ref ArmorLights.ts slot 285 (1,0.88,0.73)
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) {
			return UpgradedArmorSets.IsMining(head, body, legs);
		}

		public override void UpdateArmorSet(Player player) {
			if (UpgradedArmorSets.IsFirstModPiece(player, Type)) UpgradedArmorSets.ApplyMining(player);
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.MiningHelmet)
				.AddRecipeGroup(ArmorRecipeGroups.MythrilGroup, 10)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
