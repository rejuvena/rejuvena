using Rejuvena.Assets;

namespace Rejuvena.Content
{
    /// <summary>
    ///     Interface for adding rarely-useful features to <c>ModX</c> types.
    /// </summary>
    public interface IModContent
    {
        FallbackAsset GetFallbackAsset();
    }
}