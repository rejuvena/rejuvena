using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna;
using Terraria.ModLoader;

namespace Rejuvena.Content
{
    public abstract class DrawEffect : Entity
    {
        public float scale = 1;
        public float[] ai = new float[3] { 0f, 0f, 0f };

        // PreDrawAll is called for ALL DrawEntities at once before any of the others are called for any.
        public virtual void PreDrawAll(SpriteBatch spriteBatch) 
        {

        }

        // PreDraw is called before Draw and PostDraw per individual DrawEntity, exactly as vanilla Terraria uses PreDraws.
        public virtual bool PreDraw(SpriteBatch spriteBatch)
        {
            return true;
        }

        //The rest of these methods work exactly as you'd expect.
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void PostDraw(SpriteBatch spriteBatch)
        {

        }

        // You are now thinking about PostPostDraw(). Why? Because I mentioned PostPostDraw(). You're welcome :)
        public virtual void Update()
        {
            
        }

        public virtual void Destroy()
        {
            Rejuvena.drawEffects.Remove(this);
        }
    }

    public class DrawEffectUpdater : ModSystem
    {
        public override void PostUpdateProjectiles()
        {
            //Update all DrawEntities

            foreach (DrawEffect drawEffect in Rejuvena.drawEffects)
            {
                drawEffect.Update();
            }
        }

        public override void PostDrawTiles()
        {
            //Draw all DrawEntities

            SpriteBatch spriteBatch = Main.spriteBatch;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            foreach (DrawEffect drawEffect in Rejuvena.drawEffects)
            {
                drawEffect.PreDrawAll(spriteBatch);
            }

            foreach (DrawEffect drawEffect in Rejuvena.drawEffects)
            {
                if (drawEffect.PreDraw(spriteBatch))
                {
                    drawEffect.Draw(spriteBatch);
                    drawEffect.PostDraw(spriteBatch);
                }
            }

            spriteBatch.End();
        }
    }
}
