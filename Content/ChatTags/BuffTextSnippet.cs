using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria;
using Terraria.UI.Chat;

namespace Rejuvena.Content.ChatTags
{
    public class BuffTextSnippet : TextSnippet
    {
        public Asset<Texture2D> BuffIcon { get; }

        public BuffTextSnippet(Asset<Texture2D> buffIcon)
        {
            BuffIcon = buffIcon;
        }

        public override float GetStringLength(DynamicSpriteFont font) => BuffIcon.Width() * Scale;

        public override bool UniqueDraw(bool justCheckingString, out Vector2 size, SpriteBatch spriteBatch, Vector2 position = default, Color color = default, float scale = 1)
        {
            scale *= 0.75f;

            if (!justCheckingString)
                spriteBatch.Draw(BuffIcon.Value, position, null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            size = BuffIcon.Size() * scale;
            size.Y /= 2f;
            return true;
        }
    }
}
