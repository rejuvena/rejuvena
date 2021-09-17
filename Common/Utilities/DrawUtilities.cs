#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using TomatoLib.Common.Utilities.Extensions;

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

        public static void GetInWorldDrawData(this Item item, out Rectangle frame, out Rectangle glowMaskFrame,
            out Vector2 origin)
        {
            object[] parameters = {item, item.whoAmI, null, null, null};
            typeof(Main).GetCachedMethod("DrawItem_GetBasics").Invoke(Main.instance, parameters);

            frame = (Rectangle) parameters[3];
            glowMaskFrame = (Rectangle)parameters[4];

            origin = frame.Size() / 2f;
        }
    }
}