using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Rejuvena.Core.Utilities.Common.TypeHelpers;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Rejuvena.Content.Biomes
{
    public class SkyTowerGeneration : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            totalWeight += 4f;

            tasks.Add(new PassLegacy("Rejuvena:SkyTower", (progress, _) =>
            {
                progress.Message = "Generating sky tower...";

                GenTower(new Point(Main.rand.Next(-200, 200), 240), ref progress);
            }));
        }

        public static void GenTower(Point pos, ref GenerationProgress progress)
        {
            int centerX = pos.X;
            int centerY = pos.Y;

            //Asset<Texture2D> perlin = ModContent.Request<Texture2D>("Rejuvena/Masks/Perlin");

            IslandGen(new Point(centerX, centerY), Main.rand.Next(10, 40), Main.rand.Next(10, 40), ref progress);
        }

        public static void IslandGen(Point position, int width, int height, ref GenerationProgress progress)
        {
            const int grassId = TileID.Grass; // change this to Sky Grass
            const int dirtId = TileID.Dirt; // change this to Pale Dirt
            const int stoneId = TileID.Stone; // change this to Skystone
            const int brickId = TileID.GrayBrick; // change this to Dark Bricks
            const int cloudID = TileID.Cloud; // change this to funnier rain cloud

            int centerX = position.X;
            int centerY = position.Y;

            for (int i = 0; i < width; i++)
            {
                int upperOffsetX = centerX + i;
                int lowerOffsetX = centerX - i;

                int yModifier = Main.rand.Next(0, 2); // Replace this with Perlin sampling when that's done

                for (int j = 0; j < (int) Math.Ceiling(MathHelper.Lerp(1, height, MathHelper.Lerp(1, 0, (float) Math.Pow((float) i / width, 2))) / 2) + yModifier; j++)
                {
                    int offsetY = centerY + j;

                    WorldGen.PlaceTile(upperOffsetX, offsetY, dirtId);
                    WorldGen.PlaceTile(lowerOffsetX, offsetY, dirtId);
                }

                WorldGen.PlaceTile(upperOffsetX, centerY, grassId);
                WorldGen.PlaceTile(lowerOffsetX, centerY, grassId);
            }

            progress.Value++;

            for (int i = 0; i < (int) (width * 0.9f); i++)
            {
                int upperOffsetX = centerX + i;
                int lowerOffsetX = centerX - i;

                int upperModifierX = Main.rand.Next(-2, 2);
                int lowerModifierX = Main.rand.Next(-2, 2);

                int offsetY = centerY + (int) Math.Ceiling(MathHelper.Lerp(1, height,
                    MathHelper.Lerp(1, 0, (float) Math.Pow((float) i / width, 2))) / 2);

                int upperOffsetIncrementerY = Main.rand.Next(0, 4);
                int lowerOffsetIncrementerY = Main.rand.Next(0, 4);

                SmoothCircleRunner(new Point(upperOffsetX + upperModifierX, offsetY + upperOffsetIncrementerY),
                    Math.Min(Main.rand.Next(width / 4, width / 3),
                        Math.Abs(centerY - offsetY + upperOffsetIncrementerY)), cloudID, WallID.None);

                SmoothCircleRunner(new Point(lowerOffsetX + lowerModifierX, offsetY + lowerOffsetIncrementerY),
                    Math.Min(Main.rand.Next(width / 4, width / 3),
                        Math.Abs(centerY - offsetY + upperOffsetIncrementerY)), cloudID, WallID.None);
            }

            progress.Value++;

            for (int i = 0; i < (int)(width * 0.9f); i++)
            {
                int upperOffsetX = centerX + i;
                int lowerOffsetX = centerX - i;

                int upperModifierX = Main.rand.Next(-2, 2);
                int lowerModifierX = Main.rand.Next(-2, 2);

                int offsetY = centerY + (int)Math.Ceiling((float)MathHelper.Lerp(1, height * 0.6f, MathHelper.Lerp(1, 0, (float)Math.Pow((float)i / width, 2))) / 2);

                int upperOffsetIncrementerY = Main.rand.Next(0, 4);
                int lowerOffsetIncrementerY = Main.rand.Next(0, 4);

                CloudRunner(new Point(upperOffsetX + upperModifierX, offsetY + upperOffsetIncrementerY - 1), 
                    Math.Min(Main.rand.Next(width / 4, width / 2), Math.Abs(offsetY + upperOffsetIncrementerY)), cloudID, WallID.None);

                CloudRunner(new Point(lowerOffsetX + lowerModifierX, offsetY + lowerOffsetIncrementerY - 1), 
                    Math.Min(Main.rand.Next(width / 4, width / 2), Math.Abs(offsetY + lowerOffsetIncrementerY)), cloudID, WallID.None);
            }

            progress.Value++;

            for (int j = 1; j <= 3; j++)
            {
                for (int i = 0; i < width + j; i++)
                {
                    int upperXDelete = centerX + i;
                    int lowerXDelete = centerX - i;

                    int offsetY = centerY - j;

                    if (TileHelpers.ParanoidTileRetrieval(upperXDelete, offsetY).type == cloudID) WorldGen.KillTile(upperXDelete, offsetY, noItem: true);
                    if (TileHelpers.ParanoidTileRetrieval(lowerXDelete, offsetY).type == cloudID) WorldGen.KillTile(lowerXDelete, offsetY, noItem: true);
                }
            }

            progress.Value++;
        }

        /// <summary>
        ///     Overrides blocks in a circle.
        /// </summary>
        public static void SmoothCircleRunner(Point position, int size, int type, int wallID)
        {
            for (int x = position.X - size; x <= position.X + size; x++)
            for (int y = position.Y - size; y <= position.Y + size; y++)
            {
                if (!(Vector2.Distance(new Vector2(position.X, position.Y), new Vector2(x, y)) < size + 1))
                    continue;

                WorldGen.PlaceTile(x, y, type);
                WorldGen.PlaceWall(x, y, wallID);
            }
        }


        /// <summary>
        ///     Places multiple SmoothCircleRunners in a smooth horizontal formation.
        /// </summary>
        public static void CloudRunner(Point position, int size, int type, int wallID)
        {
            for (int x = position.X - size; x <= position.X + size; x++)
                SmoothCircleRunner(new Point(x, position.Y), 4 - (Math.Abs(x - position.X) / 2), type, wallID);
        }
    }
}