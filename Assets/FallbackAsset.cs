using System;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace Rejuvena.Assets
{
    /// <summary>
    ///     Represents a fallback asset object. Does not hold a texture.
    /// </summary>
    /// <remarks>
    ///     This could be used for simplified asset retrieval.
    /// </remarks>
    public readonly struct FallbackAsset
    {
        public readonly string Path;
        public readonly int X;
        public readonly int Y;

        public FallbackAsset(string path, int x, int y)
        {
            Path = path;
            X = x;
            Y = y;
        }

        public Asset<Texture2D> GetTexture() => ModContent.Request<Texture2D>(Path);

        public static FallbackAsset GetFallbackAsset<T>(string texture) => GetFallbackAsset(typeof(T), texture);

        public static FallbackAsset GetFallbackAsset(Type type, string texture)
        {
            if (ModContent.RequestIfExists<Texture2D>(texture, out _, AssetRequestMode.ImmediateLoad))
                return new FallbackAsset(texture, 0, 0);

            FallbackAssetType? assetType = type.GetCustomAttribute<FallbackAssetAttribute>()?.AssetType;

            if (assetType is null)
                throw new NullReferenceException("Null fallback asset.");

            return assetType switch
            {
                FallbackAssetType.Default => new FallbackAsset("ModLoader/UnloadedItem", 20, 20),
                FallbackAssetType.Tome => new FallbackAsset("Rejuvena/Assets/Textures/Defaults/Fallbacks/Tome", 28, 32),
                _ => new FallbackAsset("ModLoader/UnloadedItem", 20, 20)
            };
        }
    }
}