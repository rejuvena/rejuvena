using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rejuvena.Content.Components.TitleLinkButtons;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Rejuvena.Content.TitleLinkButtons
{
    public class RejuvenaDiscordButton : TitleLinkButton, IColoredLinkButton
    {
        Color? IColoredLinkButton.GetHoverColor() => Main.DiscoColor;

        Color? IColoredLinkButton.GetDrawColor() => Main.DiscoColor;

        public RejuvenaDiscordButton()
        {
            TooltipTextKey = "Mods.Rejuvena.TitleLinkButtons.Discord";
            LinkUrl = "https://discord.com/invite/qrZ4Bpz";
            Image = ModContent.Request<Texture2D>("Terraria/Images/UI/TitleLinkButtons");
            Rectangle rectangle1 = Image.Frame(7, 2);
            Rectangle rectangle2 = Image.Frame(7, 2, frameY: 1);
            --rectangle1.Width;
            --rectangle1.Height;
            --rectangle2.Width;
            --rectangle2.Height;
            FrameWhenNotSelected = rectangle1;
            FrameWehnSelected = rectangle2;
        }
    }
}