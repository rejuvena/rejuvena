using Microsoft.Xna.Framework;
using Rejuvena.Assets;
using Rejuvena.Content.Projectiles.Friendly.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Rejuvena.Content.Items.Weapons.Magic
{
    [FallbackAsset(FallbackAssetType.Tome)]
    public class Whirlwind : RejuvenaItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.Size = new Vector2(28f, 32f);
            Item.useTime = 20;
            Item.useAnimation = 20;
            SetDefaultsFromEnum(Defaults.Staff);

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<WhirlwindProjectile>();
            Item.damage = 20;
            Item.knockBack = 5;
            Item.shootSpeed = 0;
            Item.channel = true;
        }
        public override void UseItemFrame(Player player)
        {
            if (player.controlUseItem)
            {
                player.itemAnimation = 10;
                player.itemTime = 10;
            }
            else
            {
                player.itemAnimation = 0;
                player.itemTime = 0;
            }
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ItemID.DirtBlock, 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}