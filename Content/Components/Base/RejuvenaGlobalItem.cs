using System.Collections.Generic;
using Rejuvena.Common.Wrappers;
using Terraria;
using Terraria.ModLoader;

namespace Rejuvena.Content.Components.Base
{
    public abstract class RejuvenaGlobalItem : GlobalItem
    {
        public sealed override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(item, tooltips);

            ModifyTooltips(item, new ToolTipWrapper(tooltips));
        }

        public virtual void ModifyTooltips(Item item, ToolTipWrapper tooltips)
        {
        }
    }
}