#if DEBUG
using Microsoft.Xna.Framework;
using Rejuvena.Assets;
using Rejuvena.Content.DrawEffects;
using Rejuvena.Content.Items.Accessories;
using Rejuvena.Core.CoreSystems.DrawEffects;
using Terraria;

namespace Rejuvena.Tests
{
    [FallbackAsset(FallbackAssetType.Default)]
    public class TestAccessory : AccessoryItem
    {
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            if (Main.GameUpdateCount % 4 == 0)
                DrawEffectManager.Instance.DrawEffects.Add(new JadeSparkle(
                    player.position + new Vector2(Main.rand.Next(player.width), Main.rand.Next(player.height)),
                    Vector2.Zero));
        }
    }
}
#endif