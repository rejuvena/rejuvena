using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rejuvena.Common.Utilities;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using TomatoLib.Core.Drawing;

namespace Rejuvena.Content.Projectiles.Friendly.Magic
{
    public class WhirlwindProjectile : RejuvenaProjectile
    {
        private static Asset<Texture2D> ProjectileTexture;

        public override void SetDefaults()
        {
            SetDefaultsFromEnum(Defaults.Friendly);
            Projectile.Size = new Vector2(62, 62);
            Main.projFrames[Projectile.type] = 1;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }

        public override void Load()
        {
            base.Load();

            ProjectileTexture =
                ModContent.Request<Texture2D>("Rejuvena/Content/Projectiles/Friendly/Magic/WhirlwindProjectile");
        }

        public override Color? GetAlpha(Color lightColor) => Color.Lerp(Color.LightBlue, Color.LightSeaGreen,
            (float) Math.Sin(Projectile.ai[0] / 20) / (float) Math.PI);

        public override void AI()
        {
            base.AI();
            Projectile.ai[0]++;

            //Projectile.frame = (int)(Math.Floor(Projectile.ai[0] / 4) % 2);

            Player owner = GetOwner();

            // Movement
            Projectile.Center = owner.Center + owner.DirectionTo(Main.MouseWorld) * 10;
            int manaCost = 1;
            manaCost += (int) (Projectile.ai[0] / 10f);
            manaCost /= 2;

            if (Projectile.ai[0] % 10 == 0)
                owner.CheckMana(manaCost == 0 ? 1 : manaCost, true);

            if (Projectile.ai[0] > 10 && !owner.CheckMana(manaCost == 0 ? 1 : manaCost))
                owner.CancelItemUsage();

            if (owner.controlUseItem && owner.itemTime >= 10)
            {
                if (Projectile.ai[1] <= 0.9f)
                    Projectile.ai[1] = MathHelper.Lerp(Projectile.ai[1], 1f, 0.1f);
            }
            else
            {
                Projectile.ai[1] = MathHelper.Lerp(Projectile.ai[1], 1f, -0.1f);

                if (Projectile.ai[1] <= 0)
                    Projectile.Kill();
            }
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            Vector2 center = Projectile.Center + Projectile.DirectionFrom(GetOwner().Center) * 90;
            hitbox = new Rectangle((int) center.X - 90, (int) center.Y - 90, 180, 180);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            float max = MathHelper.Lerp(0, 180, Projectile.ai[1]);

            for (float i = 0; i < max; i++)
            {
                Main.EntitySpriteDraw(
                    ProjectileTexture.Value,
                    Projectile.Center + (Projectile.DirectionFrom(GetOwner().Center) * i)
                    .RotatedBy(Math.Sin((-Projectile.ai[0] / 6 - i / 4) / 6
                        ) * MathHelper.Lerp(1, 0, Projectile.ai[1])
                    ) - Main.screenPosition,
                    new Rectangle(0, Projectile.frame * Projectile.height, Projectile.width, Projectile.height),
                    new Color(15, 15, 15, 2),
                    MathHelper.ToRadians(-(i + Projectile.ai[0] * 8)),
                    Projectile.Size / 2f,
                    MathHelper.Lerp(0.2f, 1.5f, (float) Math.Pow(i / max, 2)),
                    SpriteEffects.None,
                    1
                );
            }

            return false;
        }
    }
}