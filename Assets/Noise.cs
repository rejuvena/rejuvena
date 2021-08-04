using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rejuvena.Core.Utilities.Common.TypeHelpers;
using ReLogic.Content;

namespace Rejuvena.Assets
{
    /// <summary>
    ///     Holds an <see cref="Asset{T}"/>-wrapped <see cref="Texture2D"/> asset as well was a two-dimensional <see cref="Color"/> array.
    /// </summary>
    public sealed class Noise
    {
        /// <summary>
        ///     The noise's texture, wrapped in an <see cref="Asset{T}"/>.
        /// </summary>
        public Asset<Texture2D> Texture { get; }

        /// <summary>
        ///     A two-dimensional array of <see cref="Color"/><c>s</c> retrieved from <see cref="TextureHelpers.GetColors"/>.
        /// </summary>
        public Color[,] NoiseData { get; }

        /// <summary>
        ///     Initialized a new instance.
        /// </summary>
        public Noise(Asset<Texture2D> texture)
        {
            Texture = texture;
            NoiseData = texture.Value.GetColors();
        }
    }
}