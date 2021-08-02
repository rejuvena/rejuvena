using Microsoft.Xna.Framework;
using Rejuvena.Assets;
using Rejuvena.Content.Players.AccessoryHandlers;
using Terraria;

namespace Rejuvena.Content.Items.Accessories.Classless
{
    /// <summary>
    ///     Classless accessory that allows for reversing knock-back inflicted on NPCs.
    /// </summary>
    [FallbackAsset(FallbackAssetType.Tome)]
    public class WhirlwindAccessory : AccessoryItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.Size = new Vector2(28f, 32f);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            player.GetModPlayer<WhirlwindPlayer>().Whirlwind = true;
        }
    }
}