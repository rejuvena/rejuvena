using Microsoft.Xna.Framework;

namespace Rejuvena.Content.Components.Generic
{
    public interface IColorProvider
    {
        Color? GetColor(string? context = null);
    }
}