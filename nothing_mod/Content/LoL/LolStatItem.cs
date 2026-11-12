using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace nothing_mod.Content.LoL
{
	// Base for every generated League item. Stats parsed from the client description are mapped onto Terraria stats.
	// Active/passive item effects are TODO; their text is shown in the tooltip.
	public abstract class LolStatItem : ModItem
	{
		protected abstract int LolId { get; }
		public ItemDef Def => LolData.Items[LolId];

		public override LocalizedText DisplayName => this.GetLocalization("DisplayName", () => Def.Name);

		public override void SetDefaults() {
			Item.accessory = true;
			Item.width = 32;
			Item.height = 32;
			Item.value = Def.PriceTotal * 50;
			Item.rare = Def.PriceTotal < 500 ? ItemRarityID.White : Def.PriceTotal < 1300 ? ItemRarityID.Green :
				Def.PriceTotal < 2500 ? ItemRarityID.Blue : Def.PriceTotal < 3200 ? ItemRarityID.Pink : ItemRarityID.Red;
		}

		float S(string k) => Def.Stats.TryGetValue(k, out float v) ? v : 0f;

		public override void UpdateAccessory(Player player, bool hideVisual) {
			var lp = player.GetModPlayer<LolPlayer>();
			float ad = S("ad"), ap = S("ap");
			if (ad > 0) { player.GetDamage(DamageClass.Melee) += ad / 400f; player.GetDamage(DamageClass.Ranged) += ad / 400f; }
			if (ap > 0) player.GetDamage(DamageClass.Magic) += ap / 500f;
			player.statDefense += (int)(S("armor") / 10f);
			player.endurance += S("mr") / 1000f;
			player.statLifeMax2 += (int)(S("hp") / 5f);
			player.statManaMax2 += (int)(S("mana") / 8f);
			player.GetAttackSpeed(DamageClass.Generic) += S("as") / 200f;
			player.GetCritChance(DamageClass.Generic) += S("crit") / 2f;
			player.moveSpeed += S("ms") / 300f;
			if (S("hpregen") > 0) player.lifeRegen += System.Math.Max(1, (int)(S("hpregen") / 50f));
			player.manaRegenBonus += (int)(S("mpregen") / 2f);
			lp.Haste += S("haste");
			lp.Lifesteal += S("lifesteal") / 100f;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) {
			foreach (string l in LolText.Wrap(Def.Desc))
				tooltips.Add(new TooltipLine(Mod, "LolDesc", l) { OverrideColor = Color.LightGray });
			tooltips.Add(new TooltipLine(Mod, "LolPrice", $"League price: {Def.PriceTotal}g") { OverrideColor = Color.Gold });
		}

		public override void AddRecipes() {
			Recipe r = CreateRecipe();
			int combine = Def.PriceTotal;
			foreach (int id in Def.From) {
				if (Mod.TryFind($"Lol{id}", out ModItem comp)) {
					r.AddIngredient(comp.Type);
					if (LolData.Items.TryGetValue(id, out var cd)) combine -= cd.PriceTotal;
				}
			}
			r.AddIngredient(ItemID.GoldCoin, System.Math.Max(1, combine / 200));
			r.AddTile(TileID.TinkerersWorkbench);
			r.Register();
		}
	}
}
