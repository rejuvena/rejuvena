using Microsoft.Xna.Framework;

namespace Rejuvena.Content.Components.TitleLinkButtons
{
    public interface IColoredLinkButton
    {
        Color? GetHoverColor();

        Color? GetDrawColor();
    }
}