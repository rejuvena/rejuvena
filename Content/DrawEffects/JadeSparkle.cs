using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using TomatoLib.Common.Systems.DrawEffects;
using TomatoLib.Core.Drawing;

namespace Rejuvena.Content.DrawEffects
{
    public class JadeSparkle : BaseDrawEffect
    {
        public static Asset<Texture2D> Asset => ModContent.Request<Texture2D>("Rejuvena/Content/DrawEffects/Sparkle");

        public float Timer;
        public float Rotation;
        public NPC NPC;
        public float TargetScale = Main.rand.NextFloat(0.2f, 0.5f);
        public float RotationIncrementation = Main.rand.NextFloat(-3f, 3f);
        public float Scale = 0.15f;

        public JadeSparkle(Vector2 pos, Vector2 vel)
        {
            position = pos;
            velocity = vel;
        }

        public override void Update()
        {
            velocity.X *= 0.9f;
            velocity.Y *= 0.9f;

            Timer++;

            if (Timer < 10f)
            {
                Scale = MathHelper.Lerp(Scale, TargetScale, 0.3f);

                if (NPC != null)
                    position += NPC.velocity;
            }
            else
            {
                RotationIncrementation *= 0.93f;
                Scale = MathHelper.Lerp(Scale, TargetScale, -0.23f);

                if (NPC != null)
                    position += NPC.velocity * 0.6f;

                if (Scale <= 0)
                    Destroy();
            }


            Rotation += RotationIncrementation;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteBatchSnapshot snapshot = new(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null,
                Main.GameViewMatrix.ZoomMatrix);

            using DisposableSpriteBatch disposable = new(spriteBatch, snapshot);

            disposable.Draw(Asset.Value, position - Main.screenPosition, new Rectangle(0, 0, 54, 54),
                new Color(82, 128, 140), MathHelper.ToRadians(Rotation), new Vector2(54f / 2f, 54f / 2f), Scale,
                SpriteEffects.None, 0f);
        }
    }
}