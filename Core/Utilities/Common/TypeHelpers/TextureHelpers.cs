using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rejuvena.Core.CoreSystems;
using Terraria;

namespace Rejuvena.Core.Utilities.Common.TypeHelpers
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
            Color[] colors1D = new Color[texture.Width * texture.Height]; //The hard to read,1D array
            Color[,] colors2D = new Color[texture.Width, texture.Height]; //The new, easy to read 2D array

            texture.GetData(colors1D); //Get the colors and add them to the array

            for (int x = 0; x < texture.Width; x++) //Convert!
            for (int y = 0; y < texture.Height; y++)
                colors2D[x, y] = colors1D[x + y * texture.Width];

            return colors2D; //Done!
        }

        /// <summary>
        ///     Retrieves the output of <see cref="Texture2D.GetData{T}(T[])"/>.
        /// </summary>
        /// <param name="texture">The texture to extract data from.</param>
        /// <returns>A one-dimensional <see cref="Color"/> array taken from <see cref="Texture2D.GetData{T}(T[])"/>.</returns>
        public static Color[] GetData(this Texture2D texture)
        {
            Color[] colors = new Color[texture.Width * texture.Height];

            Main.QueueMainThreadAction(() => texture.GetData(colors));

            return colors;
        }
    }
}