using Microsoft.Xna.Framework;
using Rejuvena.Assets;
using Terraria;

namespace Rejuvena.Content.Items.Materials
{
    /// <summary>
    ///     Basic material dropped outside of the tower on the Island.
    /// </summary>
    [FallbackAsset(FallbackAssetType.Tome)]
    public class JadeGemstone : RejuvenaItem
    {
        public bool Floating = true;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("It glimmers a pale green");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.maxStack = 999;
            Item.Size = new Vector2(16f, 16f);
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);

            Floating = false;
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if (Floating)
            {
                gravity = 0f;

                if (Item.timeSinceItemSpawned > 30f) 
                    Floating = false;
            }
            else
                gravity = 0.1f;
        }
    }
}