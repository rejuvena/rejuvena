using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.UI.Chat;

namespace Rejuvena.Content.ChatTags
{
    public sealed class BuffIconChatTag : ChatTag<BuffIconChatTag>
    {
        public override string[] Aliases => new string[]
        {
            "b",
            "buff"
        };

        public override TextSnippet Parse(string text, Color baseColor = default, string? options = null)
        {
            if (!int.TryParse(text, out int buffId) || buffId < 0 || buffId >= TextureAssets.Buff.Length)
                return new TextSnippet(text);

            Asset<Texture2D> buffIcon = TextureAssets.Buff[buffId];

            if (buffIcon?.Value is null)
                return new TextSnippet("null texture")
                {
                    Color = Color.Red
                };

            return new BuffTextSnippet(buffIcon);
        }
    }
}