using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Rejuvena.Assets;
using Terraria.ModLoader;
using TomatoLib.Common.Utilities.ItemCollections;

namespace Rejuvena.Content.Items
{
    /// <summary>
    ///     Abstract base class shared between all <see cref="Rejuvena"/> items.
    /// </summary>
    public abstract class RejuvenaItem : ModItem
    {
        [Flags]
        public enum Defaults
        {
            Accessory = 0x1,
            Staff = 0x2
        }

        public override string Texture => FallbackAsset.GetFallbackAsset(GetType(), base.Texture).Path;

        [CanBeNull] public virtual IEnumerable<(int, int)> SellEquivalent => null;

        public override void SetDefaults()
        {
            base.SetDefaults();

            if (SellEquivalent is not null)
                Item.value = new ItemCollectionProfile(SellEquivalent.ToArray()).ToValueCount();
        }

        public void SetDefaultsFromEnum(Defaults defaultsToSet)
        {
            if (defaultsToSet.HasFlag(Defaults.Accessory))
                Item.DefaultToAccessory(Item.width, Item.height);

            if (defaultsToSet.HasFlag(Defaults.Staff))
                Item.DefaultToStaff(Item.shoot, Item.useAnimation, Item.useTime, Item.mana);
        }
    }
}