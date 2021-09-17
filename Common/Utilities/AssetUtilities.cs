#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace Rejuvena.Common.Utilities
{
    public static class AssetUtilities
    {
        public static Asset<T> LoadAsset<T>(this IAssetRepository repository, string path) where T : class
        {
            Asset<T> asyncLoadedAsset = repository.Request<T>(path);

            if (asyncLoadedAsset.IsLoaded)
                return asyncLoadedAsset;
            
            asyncLoadedAsset = repository.Request<T>(path, AssetRequestMode.ImmediateLoad);

            return asyncLoadedAsset;
        }

        public static Asset<T> LoadAsset<T>(string path) where T : class
        {
            Asset<T> asyncLoadedAsset = ModContent.Request<T>(path);

            if (asyncLoadedAsset.IsLoaded)
                return asyncLoadedAsset;
            
            asyncLoadedAsset = ModContent.Request<T>(path, AssetRequestMode.ImmediateLoad);

            return asyncLoadedAsset;
        }

        public static Asset<T> ForceRequest<T>(this Asset<T> asset) where T : class
        {
            if (asset.IsLoaded)
                return asset;

            return asset.Name.StartsWith("Images\\")
                ? Main.Assets.Request<T>(asset.Name, AssetRequestMode.ImmediateLoad)
                : ModContent.Request<T>(asset.Name, AssetRequestMode.ImmediateLoad);
        }
    }
}