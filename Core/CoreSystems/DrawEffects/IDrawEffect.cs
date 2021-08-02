using System;
using Microsoft.Xna.Framework.Graphics;

namespace Rejuvena.Core.CoreSystems.DrawEffects
{
    /// <summary>
    ///     Simple base for a effect drawing.
    /// </summary>
    public interface IDrawEffect
    {
        /// <summary>
        ///     The scale of the DrawEffect. Not necessarily used, but useful.
        /// </summary>
        float Scale { get; }

        /// <summary>
        ///     Network-synced data. Often an array with three elements.
        /// </summary>
        float[] SyncedData { get; }

        /// <summary>
        ///     Action called when the effect is destroyed. This should be used to "kill" the effect instance.
        /// </summary>
        Action Destroy { get; }

        /// <summary>
        ///     Whether this effect is schedules to get deleted after an <see cref="Update"/> enumeration operation.
        /// </summary>
        bool ScheduledForDeletion { get; }

        /// <summary>
        ///     Called before and drawing-related hooks are called.
        /// </summary>
        void PreDrawAll(SpriteBatch spriteBatch);

        /// <summary>
        ///     Called before <see cref="Draw"/>. Return <c>false</c> to prevent normal drawing. Returns <c>true</c> by default.
        /// </summary>
        bool PreDraw(SpriteBatch spriteBatch);

        // self-explanatory
        /// <summary>
        /// </summary>
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        ///     Called after <see cref="Draw"/>.
        /// </summary>
        void PostDraw(SpriteBatch spriteBatch);

        /// <summary>
        ///     Called once per frame when this effect is updated. Useful for AI shenanigans and the like.
        /// </summary>
        void Update();
    }
}