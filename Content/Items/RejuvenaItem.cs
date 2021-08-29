using System;
using Rejuvena.Assets;
using Terraria.ModLoader;

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

        public void SetDefaultsFromEnum(Defaults defaultsToSet)
        {
            bool Any(Defaults defaults) => (defaults & defaultsToSet) != 0;

            if (Any(Defaults.Accessory))
                Item.DefaultToAccessory(Item.width, Item.height);

            if (Any(Defaults.Staff))
                Item.DefaultToStaff(Item.shoot, Item.useAnimation, Item.useTime, Item.mana);
        }
    }
}