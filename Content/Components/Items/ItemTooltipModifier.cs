using System;
using Rejuvena.Common;
using Rejuvena.Common.Wrappers;
using Rejuvena.Content.Components.Base;
using Terraria;
using Terraria.ModLoader;

namespace Rejuvena.Content.Components.Items
{
    public class ItemTooltipModifier : RejuvenaGlobalItem
    {
        public override void ModifyTooltips(Item item, ToolTipWrapper tooltips)
        {
            base.ModifyTooltips(item, tooltips);

            AddBuffTooltip(item, tooltips);
        }

        public void AddBuffTooltip(Item item, ToolTipWrapper tooltips)
        {
            if (item.buffType < 0 || item.buffTime <= 0)
                return;

            int seconds = item.buffTime / 60;
            TimeSpan time = new(0, 0, seconds);
            string timeString = "";

            if (time.Hours > 0)
                timeString += time.Hours + "h";

            if (time.Minutes > 0)
                timeString += time.Minutes + "m";

            if (time.Seconds > 0)
                timeString += time.Seconds + "s";

            TooltipLine line = new(
                Mod,
                $"{Mod.Name}:BuffTooltip",
                Rejuvena.GetText("Tooltips.BuffTooltip", $"[buff:{item.buffType}]", timeString)
            );

            tooltips.InsertBefore(new Identifier("Terraria", "JourneyResearch"), line);
        }
    }
}