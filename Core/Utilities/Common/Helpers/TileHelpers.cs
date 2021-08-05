using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Rejuvena.Core.Utilities.Common.Helpers
{
    /// <summary>
    ///     Helpful Tile-related methods.
    /// </summary>
    public static class TileHelpers
    {
        /// <summary>
        ///     Dictates the tile placement type.
        /// </summary>
        public enum PlacementType
        {
            /// <summary>
            ///     Places a tile given the ID.
            /// </summary>
            Tile,

            /// <summary>
            ///     Places a wall given the ID.
            /// </summary>
            Wall
        }

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
        public static Tile ParanoidTileRetrieval(int x, int y) => ParanoidTileRetrieval(x, y, out _);

        /// <summary>
        ///     Retrieves a tile instance from the <see cref="Main.tile"/> array. <br />
        ///     Creates a new dummy tile if the coordinates don't point to a valid location. <br />
        ///     In the event that the coordinates are validated and the tile is null, the tile will be set to a new instance. <br />
        ///     Thanks to Dominic Karma for the original code.
        /// </summary>
        /// <param name="x">The position on the x-axis, often referred to as <c>i</c>.</param>
        /// <param name="y">The position on the y-axis, often referred to as <c>j</c>.</param>
        /// <param name="inWorld">Whether the tile at the coordinates is truly in the world. If not, a dummy instance is returned.</param>
        /// <returns>The requested tile instance (with some exceptions, see: summary).</returns>
        /// <remarks>
        ///     It would be good to keep in mind that excessive use of this may indicate an underlying problem regarding our world generation.
        /// </remarks>
        public static Tile ParanoidTileRetrieval(int x, int y, out bool inWorld)
        {
            if (!WorldGen.InWorld(x, y))
            {
                inWorld = false;
                return new Tile();
            }

            inWorld = true;
            return Main.tile[x, y] ??= new Tile();
        }

        /// <summary>
        ///     Changes the tile type or wall type at the given coordinates in the <see cref="Main.tile"/> array. <br />
        ///     Retrieves the tile from <see cref="ParanoidTileRetrieval(int, int, out bool)"/>, so all rules about retrieving the tiles are the specified coordinates apply.
        /// </summary>
        /// <param name="x">The position on the x-axis, often referred to as <c>i</c>.</param>
        /// <param name="y">The position on the y-axis, often referred to as <c>j</c>.</param>
        /// <param name="id">The tile or wall ID.</param>
        /// <param name="placementType">The placement type.</param>
        /// <param name="mute">Muted placement. Applies to tiles and walls.</param>
        /// <param name="forced">Forced placement. Applies to tiles.</param>
        /// <param name="player">Player placement ID. Applies to tiles.</param>
        /// <param name="style">Placement style. Applies to tiles.</param>
        /// <returns>The tile instance at the given coordinates after modifying the tile.</returns>
        /// <remarks>
        ///     All rules in <see cref="ParanoidTileRetrieval(int, int, out bool)"/> apply here. This includes the creation of dummy tiles. <br />
        ///     The tile will not be replaced with the returned tile is that of a dummy instance.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="placementType"/> has an unexpected value.</exception>
        public static Tile ParanoidTilePlacement(int x, int y, int id, PlacementType placementType, bool mute = false, bool forced = false, int player = -1, int style = 0)
        {
            Tile tile = ParanoidTileRetrieval(x, y, out bool inWorld);

            if (!inWorld)
                return tile;

            switch (placementType)
            {
                case PlacementType.Tile:
                    WorldGen.PlaceTile(x, y, id, mute, forced, player, style);
                    break;

                case PlacementType.Wall:
                    WorldGen.PlaceWall(x, y, id, mute);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(placementType), placementType, null);
            }

            return tile;
        }

        /// <summary>
        ///     Kills the tile type or wall type at the given coordinates in the <see cref="Main.tile"/> array. <br />
        ///     Retrieves the tile from <see cref="ParanoidTileRetrieval(int, int, out bool)"/>, so all rules about retrieving the tiles are the specified coordinates apply.
        /// </summary>
        /// <param name="x">The position on the x-axis, often referred to as <c>i</c>.</param>
        /// <param name="y">The position on the y-axis, often referred to as <c>j</c>.</param>
        /// <param name="fail">Whether to fail and act as a hit (speculation).</param>
        /// <param name="effectOnly">Whether to only display the effect, without really killing the tile (speculation).</param>
        /// <param name="noItem">Do not drop an item (speculation).</param>
        /// <remarks>
        ///     All rules in <see cref="ParanoidTileRetrieval(int, int, out bool)"/> apply here. This includes the creation of dummy tiles. <br />
        ///     Dummy tiles will not be attempted to be killed.
        /// </remarks>
        public static void ParanoidKillTile(int x, int y, bool fail = false, bool effectOnly = false, bool noItem = false)
        {
            ParanoidTileRetrieval(x, y, out bool inWorld);

            if (inWorld)
                return;

            WorldGen.KillTile(x, y, fail, effectOnly, noItem);
        }

        /// <summary>
        ///     Places blocks in a circle at the given <paramref name="position"/> following the <paramref name="size"/> restrictions.
        /// </summary>
        /// <param name="position">The x-y coordinates position to place at.</param>
        /// <param name="size">The size of the circle.</param>
        /// <param name="tileId">The ID of the tile to place.</param>
        /// <param name="wallId">The ID of the wall to place.</param>
        /// <remarks>
        ///     Tiles are placed with <see cref="ParanoidTilePlacement"/>. Skip placing tiles or walls by settings their IDs to <c>-1</c>.
        /// </remarks>
        public static void SmoothCircleRunner(Point position, int size, int tileId = -1, int wallId = -1)
        {
            for (int x = position.X - size; x <= position.X + size; x++)
            for (int y = position.Y - size; y <= position.Y + size; y++)
            {
                if (!(Vector2.Distance(new Vector2(position.X, position.Y), new Vector2(x, y)) < size + 1))
                    continue;

                if (tileId != -1) 
                    ParanoidTilePlacement(x, y, tileId, PlacementType.Tile);

                if (wallId != -1) 
                    ParanoidTilePlacement(x, y, wallId, PlacementType.Wall);
            }
        }
    }
}