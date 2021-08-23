using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Rejuvena.Common.Systems.Noise;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using TomatoLib.Common.Assets;
using TomatoLib.Common.Utilities;

namespace Rejuvena.Content.Biomes
{
    // TODO: Finish this.
    [Autoload(false)]
    public class SkyTowerGeneration : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            totalWeight += 4f;

            tasks.Add(new PassLegacy("Rejuvena:SkyTower", (progress, _) =>
            {
                progress.Message = Language.GetTextValue("Mods.Rejuvena.UI.GeneratingSkyTower");

                GenTower(new Point(Main.rand.Next(200, 600), 240), ref progress);
            }));
        }

        public static void GenTower(Point pos, ref GenerationProgress progress)
        {
            int centerX = pos.X;
            int centerY = pos.Y;

            IslandGen(new Point(centerX, centerY), Main.rand.Next(10, 40), Main.rand.Next(10, 40), ref progress);
        }

        public static void IslandGen(Point position, int width, int height, ref GenerationProgress progress)
        {
            const int grassId = TileID.Grass; // change this to Sky Grass
            const int dirtId = TileID.Dirt; // change this to Pale Dirt
            // const int stoneId = TileID.Stone; // change this to Skystone
            // const int brickId = TileID.GrayBrick; // change this to Dark Bricks
            const int cloudID = TileID.Cloud; // change this to funnier rain cloud

            int centerX = position.X;
            int centerY = position.Y;

            for (int i = -width; i <= width; i++)
            {
                int offsetX = centerX + i;

                Noise perlinMask = NoiseSampler.Instance.DefaultPerlinMask;
                Color[,] data = NoiseSampler.Instance.DefaultPerlinMask.NoiseData;
                int xData = Math.Min(offsetX % perlinMask.Texture.Value.Width * 2, data.GetLength(0) - 1);
                int yData = centerY % perlinMask.Texture.Value.Height;

                int yModifier =
                    data[xData, yData].R /
                    30; //Main.rand.Next(0, 2); // Replace this with Perlin sampling when that's done

                float initialLerpAmount = MathHelper.Lerp(1f, 0f, (float) Math.Pow((float) i / width, 2f));
                float properLerpAmount = MathHelper.Lerp(1f, height, initialLerpAmount) / 2f;
                double raisedModifier = Math.Ceiling(properLerpAmount) + yModifier;

                for (int j = 0; j < (int) raisedModifier; j++)
                {
                    int offsetY = centerY + j;

                    TileHelpers.ParanoidTilePlacement(offsetX, offsetY, dirtId, TileHelpers.PlacementType.Tile);
                }

                TileHelpers.ParanoidTilePlacement(offsetX, centerY, grassId, TileHelpers.PlacementType.Tile);
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

                TileHelpers.SmoothCircleRunner(
                    new Point(upperOffsetX + upperModifierX, offsetY + upperOffsetIncrementerY),
                    Math.Min(Main.rand.Next(width / 4, width / 3),
                        Math.Abs(centerY - offsetY + upperOffsetIncrementerY)), cloudID, WallID.None);

                TileHelpers.SmoothCircleRunner(
                    new Point(lowerOffsetX + lowerModifierX, offsetY + lowerOffsetIncrementerY),
                    Math.Min(Main.rand.Next(width / 4, width / 3),
                        Math.Abs(centerY - offsetY + upperOffsetIncrementerY)), cloudID, WallID.None);
            }

            progress.Value++;

            for (int i = 0; i < (int) (width * 0.9f); i++)
            {
                int upperOffsetX = centerX + i;
                int lowerOffsetX = centerX - i;

                int upperModifierX = Main.rand.Next(-2, 2);
                int lowerModifierX = Main.rand.Next(-2, 2);

                int offsetY = centerY + (int) Math.Ceiling((float) MathHelper.Lerp(1, height * 0.6f,
                    MathHelper.Lerp(1, 0, (float) Math.Pow((float) i / width, 2))) / 2);

                int upperOffsetIncrementerY = Main.rand.Next(0, 4);
                int lowerOffsetIncrementerY = Main.rand.Next(0, 4);

                CloudRunner(new Point(upperOffsetX + upperModifierX, offsetY + upperOffsetIncrementerY - 1),
                    Math.Min(Main.rand.Next(width / 4, width / 2), Math.Abs(offsetY + upperOffsetIncrementerY)),
                    cloudID, WallID.None);

                CloudRunner(new Point(lowerOffsetX + lowerModifierX, offsetY + lowerOffsetIncrementerY - 1),
                    Math.Min(Main.rand.Next(width / 4, width / 2), Math.Abs(offsetY + lowerOffsetIncrementerY)),
                    cloudID, WallID.None);
            }

            progress.Value++;

            for (int j = 1; j <= 4; j++)
            for (int i = 0; i < width + j; i++)
            {
                int upperXDelete = centerX + i;
                int lowerXDelete = centerX - i;

                int offsetY = centerY - j;

                if (TileHelpers.ParanoidTileRetrieval(upperXDelete, offsetY).type == cloudID)
                    TileHelpers.ParanoidKillTile(upperXDelete, offsetY, noItem: true);

                if (TileHelpers.ParanoidTileRetrieval(lowerXDelete, offsetY).type == cloudID)
                    TileHelpers.ParanoidKillTile(lowerXDelete, offsetY, noItem: true);

                if (TileHelpers.ParanoidTileRetrieval(upperXDelete, offsetY).type == cloudID)
                    TileHelpers.ParanoidKillTile(upperXDelete, offsetY, noItem: true);

                if (TileHelpers.ParanoidTileRetrieval(lowerXDelete, offsetY).type == cloudID)
                    TileHelpers.ParanoidKillTile(lowerXDelete, offsetY, noItem: true);

            }

            progress.Value++;
        }

        /// <summary>
        ///     Places multiple SmoothCircleRunners in a smooth horizontal formation.
        /// </summary>
        public static void CloudRunner(Point position, int size, int type, int wallID)
        {
            for (int x = position.X - size; x <= position.X + size; x++)
                TileHelpers.SmoothCircleRunner(new Point(x, position.Y), 5, type, wallID);
        }
    }
}