using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Microsoft.Xna.Framework;
using Rejuvena.Core.CoreSystems.DrawEffects;
using ReLogic.Content;
using Terraria.ModLoader;

namespace Rejuvena.Content.DrawEffects
{
    public class JadeSparkle : RejuvenaDrawEffect
    {
        public override Asset<Texture2D> Asset => ModContent.Request<Texture2D>("Rejuvena/Content/DrawEffects/Sparkle");

        public float Timer;
        public float Rotation;
        public NPC NPC;
        public float TargetScale = Main.rand.NextFloat(0.2f, 0.5f);
        public float RotationIncrementation = Main.rand.NextFloat(-3f, 3f);

        public override float Scale { get; set; } = 0.15f;

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

                position += NPC.velocity;
            }
            else
            {
                RotationIncrementation *= 0.93f;
                Scale = MathHelper.Lerp(Scale, TargetScale, -0.23f);

                position += NPC.velocity * 0.6f;

                if (Scale <= 0)
                    Destroy();
            }


            Rotation += RotationIncrementation;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null,
                Main.GameViewMatrix.ZoomMatrix);

            spriteBatch.Draw(Asset.Value, position - Main.screenPosition, new Rectangle(0, 0, 54, 54), new Color(82, 128, 140),
                MathHelper.ToRadians(Rotation), new Vector2(54f / 2f, 54f / 2f), Scale, SpriteEffects.None, 0f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                Main.GameViewMatrix.ZoomMatrix);
        }
    }
}