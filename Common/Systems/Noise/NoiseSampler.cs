using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using TomatoLib.Common.Assets;
using TomatoLib.Common.Systems;
using TomatoLib.Core.Threading;

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

        public override async void Load()
        {
            base.Load();

            static async Task<NoiseAsset> GetAsset(Asset<Texture2D> texture) 
                => await GLCallLocker.Instance.InvokeAsync(() => new NoiseAsset(texture));

            DefaultPerlinMask = await GetAsset(ModContent.Request<Texture2D>("Rejuvena/Assets/Textures/Masks/Perlin",
                AssetRequestMode.ImmediateLoad));
        }
    }
}