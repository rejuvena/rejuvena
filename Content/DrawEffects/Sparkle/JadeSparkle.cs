using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rejuvena.Common.Utilities;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using TomatoLib.Core.Drawing;

namespace Rejuvena.Content.DrawEffects
{
    public class JadeSparkle : EntityDrawEffect
    {
        public override Asset<Texture2D> Asset { get; set; } =
            ModContent.Request<Texture2D>("Rejuvena/Content/DrawEffects/Sparkle/Sparkle");

        public float Timer;
        public NPC Npc;
        public Vector2 TargetScale = new(Main.rand.NextFloat(0.2f, 0.5f));
        public float RotationIncrementation = Main.rand.NextFloat(-3f, 3f);

        public override Vector2 Scale { get; set; } = new(0.15f);

        public override Color DrawColor { get; set; } = new(82, 128, 140);

        public JadeSparkle(Vector2 pos, Vector2 vel)
        {
            position = pos;
            velocity = vel;
        }

        public override void Update(ref bool useOld)
        {
            velocity.X *= 0.9f;
            velocity.Y *= 0.9f;

            Timer++;

            if (Timer < 10f)
            {
                Scale = DrawUtilities.Vector2Lerp(Scale, TargetScale, 0.3f);

                if (Npc != null)
                    position += Npc.velocity;
            }
            else
            {
                RotationIncrementation *= 0.93f;
                Scale = DrawUtilities.Vector2Lerp(Scale, TargetScale, -0.23f);

                if (Npc != null)
                    position += Npc.velocity * 0.6f;

                if (Scale.X <= 0 || Scale.Y <= 0)
                    Destroy();
            }


            DrawRotation += RotationIncrementation;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteBatchSnapshot snapshot = new(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null,
                Main.GameViewMatrix.ZoomMatrix);

            using DisposableSpriteBatch disposable = new(spriteBatch, snapshot);

            base.Draw(spriteBatch);
        }
    }
}