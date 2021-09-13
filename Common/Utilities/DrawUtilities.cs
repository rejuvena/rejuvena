#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using Microsoft.Xna.Framework;

namespace Rejuvena.Common.Utilities
{
    public static class DrawUtilities
    {
        public static Vector2 Vector2Lerp(Vector2 fromValue, Vector2 toValue, Vector2 byValue)
        {
            Vector2 from = fromValue;

            from.X = MathHelper.Lerp(fromValue.X, toValue.X, byValue.X);
            from.Y = MathHelper.Lerp(fromValue.Y, toValue.Y, byValue.Y);

            return from;
        }

        public static Vector2 Vector2Lerp(Vector2 fromValue, Vector2 toValue, float byValue) =>
            Vector2Lerp(fromValue, toValue, new Vector2(byValue));
    }
}