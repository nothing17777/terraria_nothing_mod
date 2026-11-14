using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace nothing_mod.Content.Systems
{
    public class UnrestrictedBossSummonItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => UnrestrictedBossSummons.Supports(entity.type);

        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.LihzahrdPowerCell)
            {
                item.useStyle = ItemUseStyleID.HoldUp;
                item.useTime = 45;
                item.useAnimation = 45;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // Replace vanilla restrictions and the sigil's old countdown description.
            tooltips.RemoveAll(line => line.Mod == "Terraria" && line.Name.StartsWith("Tooltip"));
            string boss = item.type == ItemID.MechdusaSummon ? "Mechdusa"
                : item.type == ItemID.MechanicalEye ? "the Twins"
                : Lang.GetNPCNameValue(UnrestrictedBossSummons.Bosses[item.type][0]);
            tooltips.Add(new TooltipLine(Mod, "UnrestrictedBossName", $"Summons {boss}"));
            tooltips.Add(new TooltipLine(Mod, "UnrestrictedBossConditions", "Usable at any time, in any biome, without progression or an altar")
            { OverrideColor = Color.LightGreen });
        }
    }
}
