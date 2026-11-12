using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class ChlorophyteVisor : ModItem
	{
		public override void SetDefaults() {
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Lime;
			Item.value = 300000;
			Item.defense = 2;
		}

		public override void UpdateEquip(Player player) {
			player.GetDamage(DamageClass.Summon) += 0.16f;
			player.maxMinions += 1; // ref vanillaSummonStats 5524 {slot:1, dmg:0.16}
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) {
			return body.type == ItemID.ChlorophytePlateMail && legs.type == ItemID.ChlorophyteGreaves;
		}

		public override void UpdateArmorSet(Player player) {
			player.setBonus = "Summons a powerful leaf crystal to shoot at nearby enemies";
			player.AddBuff(BuffID.LeafCrystal, 5); // ref vanillaArmorSets: AddBuff(60,5) every tick
			player.maxMinions += 2; // ref vanillaArmorSets '283|51|47' slot:2 (+2 minion slots)
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.ChlorophyteBar, 12)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
