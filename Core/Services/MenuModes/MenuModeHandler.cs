using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using TomatoLib.Common.Utilities.Extensions;

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

        public static List<MenuButton> ButtonList(
            ref int numButtons,
            ref bool[] readonlyText,
            ref bool[] unhoverableText,
            ref bool[] loweredAlpha,
            ref int[] yOffsetPos,
            ref int[] xOffsetPos,
            ref byte[] color,
            ref float[] scale,
            ref bool[] noCenterOffset,
            ref string[] text,
            Color defaultColor,
            out Color[] buttonColor,
            out Action?[] onLeftClick,
            out Action?[] onRightClick,
            out Action?[] onHover
        )
        {
            List<MenuButton> buttons = new();

            for (int i = 0; i < numButtons; i++)
            {
                string buttonText = text[i]; // Can't use ref params in lambdas.

                // Find the respective localization key for a button.
                // Fallback to <MenuMod>_<Index>Button for non-localized buttons.
                string buttonName = LanguageManager.Instance.GetFieldValue<
                    LanguageManager, Dictionary<string, LocalizedText
                    >>("_localizedTexts").Values.FirstOrDefault(
                    x => x.Value == buttonText
                )?.Key ?? $"{Main.menuMode}_{i}Button";

                // Create a Vanilla menu button instance.
                MenuButton button = new(buttonName, text[i]);

                // Copy ALL data over to the newly-created MenuButton instance.
                button.Color = GetColorFromByte(button.ColorByte = color[i], defaultColor); // Find the color based on the byte value Vanilla uses.
                button.LoweredAlpha = loweredAlpha[i];
                button.NoCenterOffset = noCenterOffset[i];
                button.ReadonlyText = readonlyText[i];
                button.Scale = scale[i];
                button.Text = text[i];
                button.UnhoverableText = unhoverableText[i];
                button.XOffsetPos = xOffsetPos[i];
                button.YOffsetPos = yOffsetPos[i];
                buttons.Add(button);
            }

            // TODO: Modification method goes here.

            // Resize and repopulate arrays with new data.
            numButtons = buttons.Count;
            readonlyText = new bool[numButtons];
            unhoverableText = new bool[numButtons];
            loweredAlpha = new bool[numButtons];
            yOffsetPos = new int[numButtons];
            xOffsetPos = new int[numButtons];
            color = new byte[numButtons];
            buttonColor = new Color[numButtons];
            scale = new float[numButtons];
            noCenterOffset = new bool[numButtons];
            text = new string[numButtons];
            onLeftClick = new Action[numButtons];
            onRightClick = new Action[numButtons];
            onHover = new Action[numButtons];


            for (int i = 0; i < numButtons; i++)
            {
                readonlyText[i] = buttons[i].ReadonlyText;
                unhoverableText[i] = buttons[i].UnhoverableText;
                loweredAlpha[i] = buttons[i].LoweredAlpha;
                yOffsetPos[i] = buttons[i].YOffsetPos;
                xOffsetPos[i] = buttons[i].XOffsetPos;
                color[i] = buttons[i].ColorByte;
                buttonColor[i] = buttons[i].Color;
                scale[i] = buttons[i].Scale;
                noCenterOffset[i] = buttons[i].NoCenterOffset;
                text[i] = buttons[i].Text;
                onLeftClick[i] = buttons[i].OnLeftClick;
                onRightClick[i] = buttons[i].OnRightClick;
                onHover[i] = buttons[i].OnHover;
            }

            return buttons;
        }

        /// <summary>
        /// Returns a <see cref="Color"/> identical whatever color vanilla would use. <br />
        /// This replaces the code vanilla uses for getting <see cref="MenuButton"/> colors.
        /// </summary>
        private static Color GetColorFromByte(byte color, Color defaultColor) =>
            color switch
            {
                1 => Main.mcColor,
                2 => Main.hcColor,
                3 => Main.highVersionColor,
                4 => Main.errorColor,
                5 => Main.errorColor,
                6 => Main.errorColor,
                _ => defaultColor
            };
    }
}