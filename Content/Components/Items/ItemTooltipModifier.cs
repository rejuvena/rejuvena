using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Rejuvena.Content.Components.Items
{
    public class ItemTooltipModifier : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(item, tooltips);

            AddBuffTooltip(item, tooltips);
        }

        public void AddBuffTooltip(Item item, List<TooltipLine> tooltips)
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
                $"[buff:{item.buffType}] - Lasts for {timeString}"
            );

            tooltips.Add(line);
        }
    }
}