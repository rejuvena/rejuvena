using System;
using Microsoft.Xna.Framework;
using Rejuvena.Common;
using Rejuvena.Common.Wrappers;
using Rejuvena.Content.Components.Base;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Rejuvena.Content.Components.Items
{
    public class ItemTooltipModifier : RejuvenaGlobalItem
    {
        public override void ModifyTooltips(Item item, ToolTipWrapper tooltips)
        {
            base.ModifyTooltips(item, tooltips);

            AddBuffTooltip(item, tooltips);
            AddTestTooltip(item, tooltips);
        }

        public override bool PreDrawTooltipLine(Item item, DrawableTooltipLine line, ref int yOffset)
        {
            bool draw = base.PreDrawTooltipLine(item, line, ref yOffset);

            PreDrawTestTooltip(item, line, ref yOffset, ref draw);

            return draw;
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
                "BuffTooltip",
                Rejuvena.GetText("Tooltips.BuffTooltip", $"[buff:{item.buffType}]", timeString)
            );

            tooltips.InsertBefore(new Identifier("Terraria", "JourneyResearch"), line);
        }

        public void AddTestTooltip(Item item, ToolTipWrapper tooltips)
        {
            if (item.ModItem is not ITestItem)
                return;

            TooltipLine testLine = new(Mod, "TestTooltip", "Developer Testing Item")
            {
                overrideColor = Color.Lerp(Color.Yellow, Color.Black, (float) (Math.Sin(Main.GameUpdateCount / 25f) + 1f) / 2f)
            };

            tooltips.Add(testLine);
        }

        public void PreDrawTestTooltip(Item item, DrawableTooltipLine line, ref int yOffset, ref bool draw)
        {
            if (line.mod != Mod.Name || line.Name != "TestTooltip")
                return;

            if (item.ModItem is not ITestItem)
                return;

            void DrawLine(Vector2 position, Color color) => ChatManager.DrawColorCodedString(
                Main.spriteBatch,
                line.font,
                line.text,
                position, color,
                line.rotation,
                line.origin,
                line.baseScale
            );

            int yCopy = yOffset;
            yCopy += (int) (line.font.MeasureString(line.text).Y * line.baseScale.Y);

            Vector2 GetPoint(Vector2 toRotate) =>
                new Vector2(line.X, line.Y + yCopy) + toRotate.RotatedBy(Main.GameUpdateCount / 10f) * 2f;

            DrawLine(GetPoint(Vector2.One), Color.Black * 0.5f);
            DrawLine(GetPoint(Vector2.Zero), Color.Black * 0.5f);
            DrawLine(GetPoint(Vector2.UnitX), Color.Yellow * 0.5f);
            DrawLine(GetPoint(Vector2.UnitY), Color.Yellow * 0.5f);
        }
    }
}