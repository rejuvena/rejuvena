using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rejuvena.Core.Cutscenes;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Rejuvena.Content.Cutscenes
{
    public class WorldIntoScene : Cutscene
    {
        public override bool Visible => Timer < 1200;
        
        public float Alpha;
        public float Timer;

        public override void Load(Mod mod)
        {
        }

        public override void Unload()
        {
        }

        public override int InsertionIndex(List<GameInterfaceLayer> layers) =>
            layers.FindIndex(x => x.Name.Equals("Vanilla: Mouse Text"));

        public override void Draw()
        {
            Timer++;

            Vector2 screenCenter = new Vector2(Main.screenWidth, Main.screenHeight) / 2f;
            Asset<Texture2D> icon = ModContent.Request<Texture2D>("Rejuvena/Assets/Textures/Trelamium/Logo");
            Vector2 iconPos = new Vector2(screenCenter.X, 200f) - icon.Size() / 2f;

            Alpha = MathHelper.SmoothStep(Alpha, Timer < 300 ? 1f : 0f, 0.1f);
            Main.spriteBatch.Draw(icon.Value, iconPos, Color.White * Alpha);

            
        }
    }
}