using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using TomatoLib.Common.Assets;
using TomatoLib.Common.Systems;

// ReSharper disable CommentTypo
// ReSharper disable UnusedMember.Global

namespace Rejuvena.Common.Systems.Noise
{
    /// <summary>
    ///     Handles the loading of <see cref="NoiseAsset"/> instances used in <see cref="Rejuvena"/>.
    /// </summary>
    public sealed class NoiseSampler : SingletonSystem<NoiseSampler>
    {
        public bool Initialized;

        public NoiseAsset DefaultPerlinMask;

        public override void Load()
        {
            base.Load();

            On.Terraria.Main.Update += LoadNoise;
        }

        public override void Unload()
        {
            base.Unload();

            On.Terraria.Main.Update -= LoadNoise;
        }

        // Force our noise to get loaded on the main thread.
        private void LoadNoise(On.Terraria.Main.orig_Update orig, Main self, GameTime gameTime)
        {
            if (!Initialized)
            {
                DefaultPerlinMask = new NoiseAsset(ModContent.Request<Texture2D>("Rejuvena/Assets/Masks/Perlin", AssetRequestMode.ImmediateLoad));

                Initialized = true;
            }

            orig(self, gameTime);
        }
    }
}