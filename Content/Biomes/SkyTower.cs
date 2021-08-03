using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using ReLogic.Content;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using Terraria.WorldBuilding;
using Terraria.GameContent.Generation;
using Microsoft.Xna.Framework.Graphics;

namespace Rejuvena.Content.Biomes
{
    class SkyTowerGeneration : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            totalWeight += 1;

            tasks.Add(new PassLegacy("Rejuvena:SkyTower", (progress, _) =>
            {
                GenTower(new Point(Main.rand.Next(-200, 200), 240));
            }));

        }

        public static void GenTower(Point pos)
        {
            int center_x = pos.X;
            int center_y = pos.Y;

            //Asset<Texture2D> perlin = ModContent.Request<Texture2D>("Rejuvena/Masks/Perlin");

            //Color[,] texturecolor = Rejuvena.TextureTo2DArray(perlin.Value);

            IslandGen(new Point(center_x, center_y), Main.rand.Next(10, 40), Main.rand.Next(10, 40));//, perlin.Value, texturecolor);
        }

        public static void IslandGen(Point position, int width, int height)//, Texture2D? perlin, Color[,]? textureColor)
        {
            int GrassID = TileID.Grass; // Change this to Sky Grass
            int DirtID = TileID.Dirt; // Change this to Pale Dirt
            int CloudID = TileID.Cloud; // Change this to Pale Dirt
            int StoneID = TileID.Stone; // Change this to Skystone
            int BrickID = TileID.GrayBrick; // Change this to Dark Bricks

            int CenterX = position.X;
            int CenterY = position.Y;

            for (int i = 0; i < width; i++)
            {
                int xx = CenterX + i;
                int xx2 = CenterX - i;

                int ymod = Main.rand.Next(0, 2); // Replace this with Perlin sampling when that's done

                for (int j = 0; j < (int)Math.Ceiling((float)MathHelper.Lerp(1, height, MathHelper.Lerp(1, 0, (float)Math.Pow((float)i / width, 2))) / 2) + ymod; j++)
                {
                    int yy = CenterY + j;

                    WorldGen.PlaceTile(xx, yy, DirtID);
                    WorldGen.PlaceTile(xx2, yy, DirtID);
                }
                WorldGen.PlaceTile(xx, CenterY, GrassID);
                WorldGen.PlaceTile(xx2, CenterY, GrassID);
            }

            for (int i = 0; i < (int)(width * 0.9f); i++)
            {
                int xx = CenterX + i;
                int xx2 = CenterX - i;

                int xmod = Main.rand.Next(-2, 2);
                int xmod2 = Main.rand.Next(-2, 2);

                int yy = CenterY + (int)Math.Ceiling((float)MathHelper.Lerp(1, height, MathHelper.Lerp(1, 0, (float)Math.Pow((float)i / width, 2))) / 2);

                int ymod = Main.rand.Next(0, 4);
                int ymod2 = Main.rand.Next(0, 4);

                SmoothRunner(new Point(xx + xmod, yy + ymod), Math.Min(Main.rand.Next(width / 4, width / 3), Math.Abs(CenterY - yy + ymod)), CloudID, WallID.None);
                SmoothRunner(new Point(xx2 + xmod2, yy + ymod2), Math.Min(Main.rand.Next(width / 4, width / 3), Math.Abs(CenterY - yy + ymod)), CloudID, WallID.None);
            }
        }

        public static void SmoothRunner(Point position, int size, int type, int wallID) //Overrides blocks in a circle
        {
            for (int x = position.X - size; x <= position.X + size; x++)
            {
                for (int y = position.Y - size; y <= position.Y + size; y++)
                {
                    if (Vector2.Distance(new Vector2(position.X, position.Y), new Vector2(x, y)) < size + 1)
                    {
                        WorldGen.PlaceTile(x, y, type);
                        WorldGen.PlaceWall(x, y, wallID);
                    }
                }
            }
        }
    }
}
