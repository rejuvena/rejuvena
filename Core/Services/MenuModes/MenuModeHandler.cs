using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace Rejuvena.Core.Services.MenuModes
{
    public class MenuModeHandler : Service
    {
        public List<Menu> Menus = new();

        public override void Load()
        {
            base.Load();

            List<Type> types = Mod.Code.GetTypes().Where(
                x => x.IsSubclassOf(typeof(Menu))
                     && x.GetConstructor(Array.Empty<Type>()) != null
                     && !x.IsAbstract
            ).ToList();

            foreach (Type type in types)
            {
                if (Activator.CreateInstance(type) is not Menu menu)
                    continue;

                menu.Load();
                Menus.Add(menu);
            }
        }

        public override void Unload()
        {
            base.Unload();

            Menus.Clear();
        }

        public void DrawMenus(
            Main main,
            int selectedMenu,
            string[] buttonNames,
            float[] buttonScales,
            int[] buttonVerticalSpacing,
            ref int offY,
            ref int spacing,
            ref int numButtons,
            ref bool backButtonDown
        )
        {
            Menu? menu = Menus.FirstOrDefault(x => x.Id == selectedMenu);

            menu?.ModifyMenu(
                main,
                selectedMenu,
                buttonNames,
                buttonScales,
                buttonVerticalSpacing,
                ref offY,
                ref spacing,
                ref numButtons,
                ref backButtonDown
            );
        }
    }
}