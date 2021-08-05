using Rejuvena.Assets;

namespace Rejuvena.Core.Utilities.Common.Interfaces
{
    /// <summary>
    ///     Interface for adding rarely-useful features to <c>ModX</c> types.
    /// </summary>
    public interface IModContent
    {
        FallbackAsset GetFallbackAsset();
    }
}