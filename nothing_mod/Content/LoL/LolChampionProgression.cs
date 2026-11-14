using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace nothing_mod.Content.LoL
{
    public class LolChampionProgression : ModSystem
    {
        public const int MaxLevel = 8;
        public const string GoldGroup = "nothing_mod:ChampionGoldOrPlatinumBar";
        public const string CobaltGroup = "nothing_mod:ChampionCobaltOrPalladiumBar";

        public static int DamageAt(int level) => level switch
        {
            1 => 10,
            2 => 20,
            3 => 40,
            4 => 65,
            5 => 85,
            6 => 105,
            7 => 145,
            8 => 245,
            _ => throw new ArgumentOutOfRangeException(nameof(level)),
        };

        public static string ItemName(string championKey, int level) =>
            championKey + "Champion" + (level == 1 ? "" : "Level" + level);

        public override void AddRecipeGroups()
        {
            RecipeGroup.RegisterGroup(GoldGroup, new RecipeGroup(
                () => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.GoldBar),
                ItemID.GoldBar, ItemID.PlatinumBar));
            RecipeGroup.RegisterGroup(CobaltGroup, new RecipeGroup(
                () => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.CobaltBar),
                ItemID.CobaltBar, ItemID.PalladiumBar));
        }

        public static void AddRecipe(LolChampionItem champion)
        {
            Recipe recipe = champion.CreateRecipe();
            int level = champion.ChampionLevel;
            if (level == 1)
            {
                recipe.AddRecipeGroup(RecipeGroupID.Wood, 10);
            }
            else
            {
                recipe.AddIngredient(champion.Mod.Find<ModItem>(ItemName(champion.ChampionKey, level - 1)).Type);
                switch (level)
                {
                    case 2: recipe.AddRecipeGroup(GoldGroup, 5); break;
                    case 3: recipe.AddIngredient(ItemID.HellstoneBar, 5); break;
                    case 4: recipe.AddRecipeGroup(CobaltGroup, 5); break;
                    case 5: recipe.AddIngredient(ItemID.HallowedBar, 5); break;
                    case 6: recipe.AddIngredient(ItemID.ChlorophyteBar, 5); break;
                    case 7: recipe.AddIngredient(ItemID.SpectreBar, 5); break;
                    case 8: recipe.AddIngredient(ItemID.FragmentSolar, 5); break;
                    default: throw new ArgumentOutOfRangeException(nameof(level));
                }
            }
            recipe.Register();
        }
    }
}
