using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Rejuvena.Core.Services.MenuModes;
using Terraria.ModLoader;

// TODO: Localization.
namespace Rejuvena.Core.Services.Impl.Menus
{
    public class UpdateNotificationMenu : Menu
    {
        public override void ModifyMenu(List<MenuButton> buttons)
        {
            // Clear filler button provided for an IL hack.
            buttons.Clear();
            
            Mod mod = ModContent.GetInstance<Rejuvena>();

            MenuButton button = new(mod, "UpdateNotif", "Update available!")
            {
                Color = Color.Goldenrod
            };

            buttons.Add(new MenuButton(mod, "UpdateAvailable", "An update for Rejuvena is available."));
            
            // TODO: Continue button after we finish some other IL stuff.
            buttons.Add(new MenuButton(mod, "ContinueButton", "Continue")
            {
                Scale = 0.55f,
                XOffsetPos = 80,
                ReadonlyText = false,
                UnhoverableText = false
            });
        }
    }
}
