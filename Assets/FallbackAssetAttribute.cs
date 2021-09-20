using System;

namespace Rejuvena.Assets
{
    /// <summary>
    ///     Attribute used for specifying fallback assets. Not inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class FallbackAssetAttribute : Attribute
    {
        /// <summary>
        ///     The chosen asset type.
        /// </summary>
        public FallbackAssetType AssetType { get; }

        public FallbackAssetAttribute(FallbackAssetType assetType)
        {
            AssetType = assetType;
        }
    }
}