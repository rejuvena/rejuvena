using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

// Copied from my old tML PR: https://github.com/tModLoader/tModLoader/pull/1358

namespace Rejuvena.Core.Services.MenuModes
{
    /// <summary>
    ///     This class serves as a way to store information about menu buttons on the main menu. <br />
    /// </summary>
    public class MenuButton
    {
        /// <summary>
        /// The name of the mod that added this menu button. For vanilla buttons, the mod name will be "Terraria".
        /// </summary>
        public string Mod { get; }

        /// <summary>
        /// The name of this menu button, which is used to help identify its function.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Allows you to tell the button what to do when it is left-clicked.
        /// </summary>
        public Action? OnLeftClick;

        /// <summary>
        /// Allows you to tell the button what to do when it is right-clicked;
        /// </summary>
        public Action? OnRightClick;

        /// <summary>
        /// Allows you to tell the button what to do when it's being hovered on.
        /// </summary>
        public Action? OnHover;

        /// <summary>
        /// The text this menu button displays.
        /// </summary>
        public string Text;

        /// <summary>
        /// The button's scale.
        /// </summary>
        public float Scale = 1f;

        /// <summary>
        /// Offset position on the y-axis.
        /// </summary>
        public int YOffsetPos = 0;

        /// <summary>
        /// Offset position on the x-axis.
        /// </summary>
        public int XOffsetPos = 0;

        /// <summary>
        /// If set to true, this text will be colored to white and the player will be unable to click on or hover over the button.
        /// </summary>
        public bool ReadonlyText = true;

        /// <summary>
        /// Similar to <see cref="readonlyText"/>, but just disallows clicking and hovering while still keeping the menu button color the same.
        /// </summary>
        public bool UnhoverableText = true;

        /// <summary>
        /// The color of the button text.
        /// </summary>
        public Color Color = Color.White;

        // Internal fields used for Vanilla code.
        internal bool LoweredAlpha = false;
        internal bool NoCenterOffset = false;
        internal byte ColorByte = 0;

        public MenuButton(Mod mod, string name, string text)
        {
            Mod = mod.Name;
            Name = name;
            Text = text;
        }

        internal MenuButton(string name, string text)
        {
            Mod = "Terraria";
            Name = name;
            Text = text;
        }
	}
}