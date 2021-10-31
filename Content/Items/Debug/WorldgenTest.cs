using Microsoft.Xna.Framework;
using Rejuvena.Assets;
using Rejuvena.Content.Biomes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Rejuvena.Content.Items.Debug
{
#if DEBUG
    [Autoload]
#else
[Autoload(false)]
#endif
    [FallbackAsset(FallbackAssetType.Default)]
    public class WorldgenTest : RejuvenaItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 1;
            Item.useTime = 1;
            Item.autoReuse = false;
        }

        public override bool? UseItem(Player player)
        {
            GenerationProgress discard = new();
            SkyTowerGeneration.GenTower(new Point((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16), ref discard);

            return base.UseItem(player);
        }
    }
}