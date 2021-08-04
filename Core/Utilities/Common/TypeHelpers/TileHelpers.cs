using Terraria;

namespace Rejuvena.Core.Utilities.Common.TypeHelpers
{
    /// <summary>
    ///     Helpful Tile-related methods.
    /// </summary>
    public static class TileHelpers
    {
        /// <summary>
        ///     Retrieves a tile instance from the <see cref="Main.tile"/> array. <br />
        ///     Creates a new dummy tile if the coordinates don't point to a valid location. <br />
        ///     In the event that the coordinates are validated and the tile is null, the tile will be set to a new instance. <br />
        ///     Thanks to Dominic Karma for the original code.
        /// </summary>
        /// <param name="x">The position on the x-axis, often referred to as <c>i</c>.</param>
        /// <param name="y">The position on the y-axis, often referred to as <c>j</c>.</param>
        /// <returns>The requested tile instance (with some exceptions, see: summary).</returns>
        /// <remarks>
        ///     It would be good to keep in mind that excessive use of this may indicate an underlying problem regarding our world generation.
        /// </remarks>
        public static Tile ParanoidTileRetrieval(int x, int y)
        {
            if (!WorldGen.InWorld(x, y))
                return new Tile();

            return Main.tile[x, y] ??= new Tile();
        }
    }
}