using System;
using Microsoft.Xna.Framework.Graphics;
using Rejuvena.Core.Utilities.Common;
using ReLogic.Content;
using Terraria;

namespace Rejuvena.Core.CoreSystems.DrawEffects
{
    public abstract class RejuvenaDrawEffect : Entity, IDrawEffect, IHasAsset<Texture2D>
    {
        public virtual float Scale { get; set; } = 1f;

        public virtual float[] SyncedData { get; set; } = new float[3];

        public abstract Asset<Texture2D> Asset { get; }

        public Action Destroy { get; }

        protected RejuvenaDrawEffect()
        {
            Destroy += () => DrawEffectManager.Instance.DrawEffects.Remove(this);
        }
        
        public virtual void PreDrawAll(SpriteBatch spriteBatch)
        {
        }
        
        public virtual bool PreDraw(SpriteBatch spriteBatch) => true;
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual void PostDraw(SpriteBatch spriteBatch)
        {
        }
        
        public virtual void Update()
        {
        }
    }
}