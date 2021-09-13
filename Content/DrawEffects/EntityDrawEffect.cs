#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using TomatoLib.Common.Systems.DrawEffects;

namespace Rejuvena.Content.DrawEffects
{
    public abstract class EntityDrawEffect : BaseDrawEffect
    {
        public abstract Asset<Texture2D> Asset { get; set; }

        public virtual Color DrawColor { get; set; } = Color.White;

        public virtual Vector2 Scale { get; set; } = new(1f);

        public float DrawRotation { get; set; } = 0f;

        public sealed override void Update()
        {
            // base.Update();
            oldDirection = direction;
            oldPosition = position;
            oldVelocity = velocity;

            bool useOld = false;
            Update(ref useOld);

            if (useOld)
            {
                direction = oldDirection;
                position = oldPosition;
                velocity = oldVelocity;
            }

            position += velocity;
        }

        public virtual void Update(ref bool useOld)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            SpriteEffects drawEffects = Main.LocalPlayer.gravDir == -1f 
                ? SpriteEffects.FlipVertically
                : SpriteEffects.None;

            if (direction == -1)
            {
                drawEffects &= ~SpriteEffects.None;
                drawEffects |= SpriteEffects.FlipHorizontally;
            }

            spriteBatch.Draw(Asset.Value, Main.ReverseGravitySupport(position - Main.screenPosition), null, DrawColor,
                MathHelper.ToRadians(DrawRotation), Asset.Size() / 2f, Scale, drawEffects, 0f);
        }
    }
}