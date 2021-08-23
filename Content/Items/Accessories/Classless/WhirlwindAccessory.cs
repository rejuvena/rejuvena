using Microsoft.Xna.Framework;
using Rejuvena.Assets;
using Rejuvena.Content.Players.AccessoryHandlers;
using Terraria;

namespace Rejuvena.Content.Items.Accessories.Classless
{
    [FallbackAsset(FallbackAssetType.Tome)]
    public class WhirlwindAccessory : RejuvenaItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.Size = new Vector2(28f, 32f);

            SetDefaultsFromEnum(Defaults.Accessory);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            player.GetModPlayer<WhirlwindPlayer>().Whirlwind = true;
        }
    }
}