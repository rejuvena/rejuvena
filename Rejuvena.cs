using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Rejuvena
{
    public class Rejuvena : Mod
    {
        public static Color[,] TextureTo2DArray(Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height]; //The hard to read,1D array
            Color[,] colors2D = new Color[texture.Width, texture.Height]; //The new, easy to read 2D array

            Main.QueueMainThreadAction(() =>
            {

                texture.GetData(colors1D); //Get the colors and add them to the array

                for (int x = 0; x < texture.Width; x++) //Convert!
                for (int y = 0; y < texture.Height; y++)
                    colors2D[x, y] = colors1D[x + y * texture.Width];
            });

            return colors2D; //Done!
        }
    }
}