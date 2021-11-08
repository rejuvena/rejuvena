using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Rejuvena.Core.Services.MenuModes;
using Terraria.ModLoader;

namespace Rejuvena.Core.Services.Impl.Menus
{
    public class UpdateNotificationMenu : Menu
    {
        public override void ModifyMenu(List<MenuButton> buttons)
        {
            buttons.Clear();

            MenuButton button = new(ModContent.GetInstance<Rejuvena>(), "TestButton", "Test")
            {
                Color = Color.Goldenrod
            };

            buttons.Add(button);
        }
    }
}