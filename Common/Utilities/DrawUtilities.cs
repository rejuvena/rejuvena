#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using Microsoft.Xna.Framework;
using Terraria;
using TomatoLib.Common.Utilities.Extensions;

namespace Rejuvena.Common.Utilities
{
    public static class DrawUtilities
    {
        /// <summary>
        ///     Linearly interpolates the <see cref="Vector2.X"/> and <see cref="Vector2.Y"/> values of two vectors by the <c>X</c> and <c>Y</c> values of <paramref name="byValue"/>.
        /// </summary>
        public static Vector2 Vector2Lerp(Vector2 fromValue, Vector2 toValue, Vector2 byValue)
        {
            Vector2 from = fromValue;

            from.X = MathHelper.Lerp(fromValue.X, toValue.X, byValue.X);
            from.Y = MathHelper.Lerp(fromValue.Y, toValue.Y, byValue.Y);

            return from;
        }

        /// <summary>
        ///     Linearly interpolates the <see cref="Vector2.X"/> and <see cref="Vector2.Y"/> values of two vectors by the value of <paramref name="byValue"/>.
        /// </summary>
        public static Vector2 Vector2Lerp(Vector2 fromValue, Vector2 toValue, float byValue) =>
            Vector2Lerp(fromValue, toValue, new Vector2(byValue));

        /// <summary>
        ///     Get item draw data provided by <see cref="Main.DrawItem_GetBasics"/>.
        /// </summary>
        public static void GetInWorldDrawData(this Item item, out Rectangle frame, out Rectangle glowMaskFrame,
            out Vector2 origin)
        {
            object[] parameters = {item, item.whoAmI, null, null, null};
            typeof(Main).GetCachedMethod("DrawItem_GetBasics").Invoke(Main.instance, parameters);

            frame = (Rectangle) parameters[3];
            glowMaskFrame = (Rectangle)parameters[4];

            origin = frame.Size() / 2f;
        }

        public static Color Multiply(this Color colorOne, Color colorTwo, bool multiplyAlpha = false) =>
            new(ByteToFloat(colorOne.R) * ByteToFloat(colorTwo.R),
                ByteToFloat(colorOne.B) * ByteToFloat(colorTwo.G),
                ByteToFloat(colorOne.B) * ByteToFloat(colorTwo.G),
                multiplyAlpha ? ByteToFloat(colorOne.R) * ByteToFloat(colorTwo.R) : ByteToFloat(colorOne.A));

        public static byte FloatToByte(float fl) => (byte) MathHelper.Clamp(fl * byte.MaxValue, 0f, byte.MaxValue);

        public static float ByteToFloat(byte b) => MathHelper.Clamp(b / (float) byte.MaxValue, 0f, 1f);
    }
}