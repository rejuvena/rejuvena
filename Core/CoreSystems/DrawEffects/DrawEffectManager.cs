using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Rejuvena.Core.CoreSystems.DrawEffects
{
    /// <summary>
    ///     Manages <see cref="IDrawEffect"/> instances.
    /// </summary>
    public class DrawEffectManager : ModSystem
    {
        public static DrawEffectManager Instance => ModContent.GetInstance<DrawEffectManager>();

        public List<IDrawEffect> DrawEffects { get; protected set; }

        public DrawEffectManager()
        {
            DrawEffects = new List<IDrawEffect>();
        }

        public override void PostUpdateProjectiles()
        {
            // Update all IDrawEffect instances.
            foreach (IDrawEffect effect in DrawEffects) 
                effect.Update();

            // Delete all IDrawEffect instances that were schedules for deletion.
            DrawEffects.RemoveAll(x => x.ScheduledForDeletion);
        }

        public override void PostDrawTiles()
        {
            // Draw all IDrawEffect instances
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            // Call PreDrawAll separately
            // as we want it to be ran before
            // actual instances are drawn
            foreach (IDrawEffect drawEffect in DrawEffects) 
                drawEffect.PreDrawAll(Main.spriteBatch);

            foreach (IDrawEffect drawEffect in DrawEffects.Where(drawEffect =>
                drawEffect.PreDraw(Main.spriteBatch)))
            {
                drawEffect.Draw(Main.spriteBatch);
                drawEffect.PostDraw(Main.spriteBatch);
            }

            Main.spriteBatch.End();
        }
    }
}
