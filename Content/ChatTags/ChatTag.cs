using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Rejuvena.Content.ChatTags
{
    public abstract class ChatTag<TTag> : ILoadable, ITagHandler where TTag : ITagHandler, new()
    {
        public abstract string[] Aliases { get; }

        public virtual void Load(Mod mod) => ChatManager.Register<TTag>(Aliases);

        public virtual void Unload()
        {
        }

        public abstract TextSnippet Parse(string text, Color baseColor = default, string? options = null);
    }
}
