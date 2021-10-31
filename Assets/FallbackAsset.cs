using System;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
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

        public FallbackAsset([NotNull] string path, int x, int y)
        {
            Path = path;
            X = x;
            Y = y;
        }

        public FallbackAsset([NotNull] string path, Vector2 size) : this(path, (int) size.X, (int) size.Y)
        {
        }
        
        [MustUseReturnValue("Otherwise pointless async-loading request.")]
        public Asset<Texture2D> GetTexture() => ModContent.Request<Texture2D>(Path);

        [MustUseReturnValue("Otherwise pointless retrieval.")]
        public static FallbackAsset GetFallbackAsset<T>([NotNull] string texture) =>
            GetFallbackAsset(typeof(T), texture);

        [MustUseReturnValue("Otherwise pointless retrieval.")]
        public static FallbackAsset GetFallbackAsset([NotNull] Type type, [NotNull] string texture)
        {
            if (ModContent.RequestIfExists<Texture2D>(texture, out _, AssetRequestMode.ImmediateLoad))
                return new FallbackAsset(texture, 0, 0);

            FallbackAssetType? assetType = type.GetCustomAttribute<FallbackAssetAttribute>()?.AssetType;

            if (assetType is null)
                throw new NullReferenceException("Null fallback asset.");

            return assetType switch
            {
                FallbackAssetType.Default => new FallbackAsset("ModLoader/UnloadedItem", new Vector2(20)),
                FallbackAssetType.Tome => new FallbackAsset("Rejuvena/Assets/Textures/Defaults/Fallbacks/Tome", new Vector2(28, 32)),
                _ => new FallbackAsset("ModLoader/UnloadedItem", new Vector2(20))
            };
        }
    }
}