using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace nothing_mod.Content.LoL
{
	// Base for every generated champion weapon. Hold it, then use Z/X/C/V (rebindable) to cast Q/W/E/R.
	// Spell damage numbers are NOT in the client JSON, so damage is the weapon's damage times a slot multiplier (UNVERIFIED balance).
	public abstract class LolChampionItem : ModItem
	{
		protected abstract string Alias { get; }
		public string ChampionKey => Alias;
		public virtual int ChampionLevel => 1;
		public ChampionDef Def => LolData.Champions[Alias];

		public override LocalizedText DisplayName => this.GetLocalization("DisplayName", () => ChampionLevel == 1 ? $"{Def.Name}, {Def.Title}" : $"{Def.Name}, {Def.Title} (Level {ChampionLevel})");

		public override void SetDefaults() {
			Item.damage = LolChampionProgression.DamageAt(ChampionLevel);
			Item.DamageType = Def.Magic == 1 ? DamageClass.Magic : (Def.Ranged == 1 ? DamageClass.Ranged : DamageClass.Melee);
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.knockBack = 4f;
			Item.value = Item.buyPrice(gold: 5);
			Item.rare = ItemRarityID.Pink;
			Item.autoReuse = true;
			Item.noMelee = Def.Magic == 1 || Def.Ranged == 1;
			if (Item.noMelee) {
				Item.useStyle = ItemUseStyleID.Shoot;
				Item.shoot = ModContent.ProjectileType<LolAbilityProjectile>();
				Item.shootSpeed = 12f;
				Item.UseSound = SoundID.Item8;
			}
			else {
				Item.useStyle = ItemUseStyleID.Swing;
				Item.UseSound = SoundID.Item1;
			}
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			// basic attack: ai0 = style 0 bolt, ai1 = champion hue (no CC flags)
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, Def.Hue * 8f);
			return false;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) {
			var d = Def;
			tooltips.Add(new TooltipLine(Mod, "LolLevel", $"Champion level {ChampionLevel} / {LolChampionProgression.MaxLevel}") { OverrideColor = d.EffectColor });
			void Add(string name, string text, Color? c = null) {
				foreach (string l in LolText.Wrap(text)) {
					var line = new TooltipLine(Mod, "Lol" + name, l);
					if (c.HasValue) line.OverrideColor = c;
					tooltips.Add(line);
				}
			}
			tooltips.Add(new TooltipLine(Mod, "LolKeys", $"Hold {LolKeyLabel.Of(0)} / {LolKeyLabel.Of(1)} / {LolKeyLabel.Of(2)} / {LolKeyLabel.Of(3)} to aim, release to cast Q / W / E / R") { OverrideColor = Color.LightGray });
			tooltips.Add(new TooltipLine(Mod, "LolMinionSlots", $"+{LolPlayer.BonusMinionSlots} summon slots while held or carried in your main inventory (does not stack)") { OverrideColor = Color.LightGreen });
			foreach (var s in d.Spells) {
				string cd = s.Key == "P" ? "" : $" ({LolPlayer.SpellCooldown:0.#}s)";
				string[] slot = { "P", "Q", "W", "E", "R" };
				tooltips.Add(new TooltipLine(Mod, "LolSpell" + s.Key, $"[{s.Key}] {s.Name}{cd}") { OverrideColor = d.EffectColor });
				Add("D" + s.Key, s.Desc, Color.Gray);
			}
		}

		public override void AddRecipes() => LolChampionProgression.AddRecipe(this);
	}
}
