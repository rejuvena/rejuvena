using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
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
            totalWeight += 1;

            tasks.Add(new PassLegacy("Rejuvena:SkyTower",
                (progress, _) => { GenTower(new Point(Main.rand.Next(-200, 200), 240)); }));
        }

        public static void GenTower(Point pos)
        {
            int center_x = pos.X;
            int center_y = pos.Y;

            //Asset<Texture2D> perlin = ModContent.Request<Texture2D>("Rejuvena/Masks/Perlin");

            //Color[,] texturecolor = Rejuvena.TextureTo2DArray(perlin.Value);

            IslandGen(pos, Main.rand.Next(10, 20), Main.rand.Next(10, 20)); //, perlin.Value, texturecolor);
        }

        public static void
            IslandGen(Point position, int width, int height) //, Texture2D? perlin, Color[,]? textureColor)
        {
            const int grassId = TileID.Grass; // change this to Sky Grass
            const int dirtId = TileID.Dirt; // change this to Pale Dirt
            const int stoneId = TileID.Stone; // change this to Skystone
            const int brickId = TileID.GrayBrick; // change this to Dark Bricks

            int centerX = position.X;
            int centerY = position.Y;

            for (int i = 0; i < width; i++)
            {
                int upperOffsetX = centerX + i;
                int lowerOffsetX = centerX - i;

                //int ymod = textureColor[xx % perlin.Width, CenterY % perlin.Height].R / 20;

                for (int j = 0;
                    j < (int) (Math.Ceiling(MathHelper.Lerp(1, height,
                        MathHelper.Lerp(1, 0, (float) Math.Pow((float) i / width, 2))) / 2));
                    j++)
                {
                    int yy = centerY + j;

                    WorldGen.PlaceTile(upperOffsetX, yy, dirtId);
                    WorldGen.PlaceTile(lowerOffsetX, yy, dirtId);
                }

                WorldGen.PlaceTile(upperOffsetX, centerY, grassId);
                WorldGen.PlaceTile(lowerOffsetX, centerY, grassId);
            }
        }

        /// <summary>
        ///     Overrides blocks in a circle.
        /// </summary>
        public static void SmoothCircleRunner(Point position, int size, int type, int wallID)
        {
            for (int x = position.X - size; x <= position.X + size; x++)
            for (int y = position.Y - size; y <= position.Y + size; y++)
            {
                if (!(Vector2.Distance(new Vector2(position.X, position.Y), new Vector2(x, y)) <= size))
                    continue;

                WorldGen.PlaceTile(x, y, type);
                WorldGen.PlaceWall(x, y, wallID);
            }
        }
    }
}