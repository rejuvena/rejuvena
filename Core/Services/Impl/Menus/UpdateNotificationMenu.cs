using System.Collections.Generic;
using Rejuvena.Core.Services.MenuModes;
using Terraria.ModLoader;

namespace Rejuvena.Core.Services.Impl.Menus
{
    public class UpdateNotificationMenu : Menu
    {
        public override void ModifyMenu(List<MenuButton> buttons)
        {
            buttons.Clear();

            buttons.Add(new MenuButton(ModContent.GetInstance<Rejuvena>(), "TestButton", "Test"));
        }
    }
}