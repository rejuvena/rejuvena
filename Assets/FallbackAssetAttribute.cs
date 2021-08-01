using System;

namespace Rejuvena.Assets
{
    /// <summary>
    ///     Attribute used for specifying fallback assets. Not inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class FallbackAssetAttribute : Attribute
    {
        public FallbackAssetType AssetType { get; }

        public FallbackAssetAttribute(FallbackAssetType assetType)
        {
            AssetType = assetType;
        }
    }
}