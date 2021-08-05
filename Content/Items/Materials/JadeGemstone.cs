using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rejuvena.Common.Systems.DrawEffects;
using Rejuvena.Content.DrawEffects;
using Terraria;
using Terraria.GameContent;

namespace Rejuvena.Content.Items.Materials
{
    /// <summary>
    ///     Basic material dropped outside of the tower on the Island.
    /// </summary>
    public class JadeGemstone : RejuvenaItem
    {
        /// <summary>
        ///     Whether newly-dropped effects are initialized and should be shown.
        /// </summary>
        public bool InitializedEffects;

        /// <summary>
        ///     Whether the item is floating when it's first spawned.
        /// </summary>
        public bool Floating;

        /// <summary>
        ///     The saved spawn time, allowing for calculating the difference between the time it was initialized and its current real spawn time.
        /// </summary>
        public int SavedSpawnTime;

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.maxStack = 999;
            Item.Size = new Vector2(16f, 16f);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
            float rotation, float scale,
            int whoAmI)
        {
            base.PostDrawInWorld(spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);

            if (!InitializedEffects)
                return;

            byte difference =
                (byte) Math.Clamp(Item.timeSinceItemSpawned - SavedSpawnTime, byte.MinValue, byte.MaxValue);

            lightColor.A = (byte) (byte.MaxValue - difference);

            spriteBatch.Draw(TextureAssets.Item[Type].Value,
                Item.Center - Main.screenPosition, null,
                new Color(lightColor.A, lightColor.A, lightColor.A, lightColor.A), rotation,
                TextureAssets.Item[Type].Size() / 2f, scale,
                SpriteEffects.None, 0f);

            float divisor = 3f + difference / 25f;

            if (difference / 25f > 3f)
                divisor -= 3f;

            Lighting.AddLight(Item.Center, new Color(82, 128, 140).ToVector3() / divisor);
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if (!InitializedEffects)
                return;

            if (Floating)
            {
                gravity = 0f;

                if (Item.timeSinceItemSpawned - SavedSpawnTime > 30)
                    Floating = false;
            }
            else if (gravity == 0f)
                gravity = 0.1f;

            byte difference =
                (byte) Math.Clamp(Item.timeSinceItemSpawned - SavedSpawnTime, byte.MinValue, byte.MaxValue);

            switch (difference)
            {
                case >= byte.MaxValue:
                    return;

                case > 45 when Main.rand.NextBool(difference / 5):
                    DrawEffectManager.Instance.DrawEffects.Add(
                        new JadeSparkle(Item.Center, Main.rand.NextVector2Circular(5f, 5f))
                        {
                            TargetScale = Main.rand.NextFloat(0.2f, 0.4f)
                        });
                    break;
            }
        }

        /// <summary>
        ///     Sets/resets initial NPC-dropped spawn effects.
        /// </summary>
        public void SetInitialSpawn()
        {
            Floating = true;
            SavedSpawnTime = Item.timeSinceItemSpawned;
            Item.velocity = Vector2.Zero;
            InitializedEffects = true;
        }
    }
}