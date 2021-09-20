#region License

// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007

#endregion

using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Rejuvena.Content.Items
{
    public abstract partial class RaptureItem<TMainRapture>
    {
        public virtual TMainRapture MainRapture { get; } = new();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            ItemID.Sets.ItemNoGravity[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            SetDefaultsFromEnum(Defaults.Accessory);

            Item.Size = ItemToDrawAs == ItemID.None ? new Vector2(20f) : ContentSamples.ItemsByType[ItemToDrawAs].Size;
        }
    }
}