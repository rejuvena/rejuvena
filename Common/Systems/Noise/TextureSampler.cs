using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using TomatoLib.Common.Assets;
using TomatoLib.Common.Systems;
using TomatoLib.Core.Threading;

namespace Rejuvena.Common.Systems.Noise
{
    /// <summary>
    ///     Handles the loading of <see cref="NoiseAsset"/> instances used in <see cref="Rejuvena"/>. Also caches <see cref="Texture2D"/> <see cref="Asset{T}"/>s.
    /// </summary>
    public sealed class TextureSampler : SingletonSystem<TextureSampler>
    {
        public NoiseAsset DefaultPerlinMask;

        public override async void Load()
        {
            base.Load();

            static async Task<NoiseAsset> GetAsset(Asset<Texture2D> texture) => await GlThreadLocker.Instance.InvokeAsync(
                () => new NoiseAsset(texture)
            );

            static async Task<NoiseAsset> RequestAsset(string texture) => await GetAsset(
                ModContent.Request<Texture2D>(texture)
            );

            DefaultPerlinMask = await RequestAsset("Rejuvena/Assets/Textures/Masks/Perlin");
        }
    }
}