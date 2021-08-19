﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rejuvena.Core.Utilities.Common.Helpers
{
    /// <summary>
    ///     Provides helper TextureXD-related methods. Often <see cref="Texture2D"/>.
    /// </summary>
    public static class TextureHelpers
    {
        /// <summary>
        ///     Retrieves a two-dimensional array of <see cref="Color"/><c>s</c> from this given <paramref name="texture"/>.
        /// </summary>
        /// <param name="texture">The texture to extract data from.</param>
        /// <returns>A two-dimensional array of <see cref="Color"/><c>s</c> formed from a one-dimensional array.</returns>
        public static Color[,] GetColors(this Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            Color[,] colors2D = new Color[texture.Width, texture.Height];

            texture.GetData(colors1D);

            for (int x = 0; x < texture.Width; x++)
            for (int y = 0; y < texture.Height; y++)
                colors2D[x, y] = colors1D[x + y * texture.Width];

            return colors2D;
        }

        /// <summary>
        ///     Retrieves the output of <see cref="Texture2D.GetData{T}(T[])"/>.
        /// </summary>
        /// <param name="texture">The texture to extract data from.</param>
        /// <returns>A one-dimensional <see cref="Color"/> array taken from <see cref="Texture2D.GetData{T}(T[])"/>.</returns>
        public static Color[] GetData(this Texture2D texture)
        {
            Color[] colors = new Color[texture.Width * texture.Height];

            texture.GetData(colors);

            return colors;
        }
    }
}