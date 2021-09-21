#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Rejuvena.Common.Utilities
{
    public static class DustUtilities
    {
        // The legendary spit mod
        public static void DrawStar(
            Vector2 position,
            int dustType,
            float pointAmount = 5,
            float mainSize = 1,
            float dustDensity = 1,
            float dustSize = 1f,
            float pointDepthMultiplier = 1f,
            float pointDepthMultiplierOffset = 0.5f,
            bool noGravity = false,
            float randomAmount = 0,
            float rotationAmount = -1)
        {
            float rot = rotationAmount < 0 ? Main.rand.NextFloat(0, (float) Math.PI * 2) : rotationAmount;
            float density = 1 / dustDensity * 0.1f;

            for (float k = 0; k < 6.28f; k += density)
            {
                float rand = 0;

                if (randomAmount > 0)
                    rand = Main.rand.NextFloat(-0.01f, 0.01f) * randomAmount;

                float x = (float) Math.Cos(k + rand);
                float y = (float) Math.Sin(k + rand);

                // Triangle wave function
                float multiplier = Math.Abs(k * (pointAmount / 2) % (float) Math.PI - (float) Math.PI / 2) *
                    pointDepthMultiplier + pointDepthMultiplierOffset;

                Dust.NewDustPerfect(
                    position,
                    dustType,
                    new Vector2(x, y).RotatedBy(rot) * multiplier * mainSize,
                    0,
                    default,
                    dustSize).noGravity = noGravity;
            }
        }


        public static void DrawCircle(
            Vector2 position,
            int dustType,
            float mainSize = 1,
            float ratioX = 1,
            float ratioY = 1,
            float dustDensity = 1,
            float dustSize = 1f,
            float randomAmount = 0,
            float rotationAmount = 0,
            bool noGravity = false)
        {
            float rot = rotationAmount < 0 ? Main.rand.NextFloat(0, (float) Math.PI * 2) : rotationAmount;
            float density = 1 / dustDensity * 0.1f;

            for (float k = 0; k < 6.28f; k += density)
            {
                float rand = 0;
                if (randomAmount > 0) 
                    rand = Main.rand.NextFloat(-0.01f, 0.01f) * randomAmount;

                float x = (float) Math.Cos(k + rand) * ratioX;
                float y = (float) Math.Sin(k + rand) * ratioY;

                if (dustType is 222 or 130 || noGravity)
                    Dust.NewDustPerfect(
                        position,
                        dustType,
                        new Vector2(x, y).RotatedBy(rot) * mainSize,
                        0,
                        default,
                        dustSize).noGravity = true;
                else
                    Dust.NewDustPerfect(
                        position,
                        dustType,
                        new Vector2(x, y).RotatedBy(rot) * mainSize,
                        0,
                        default,
                        dustSize);
            }
        }
    }
}