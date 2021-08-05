using ReLogic.Content;

namespace Rejuvena.Core.Utilities.Common.Interfaces
{
    /// <summary>
    ///     Indicates that an object has an associated asset.
    /// </summary>
    public interface IHasAsset<T> where T : class
    {
        /// <summary>
        ///     The associated asset.
        /// </summary>
        Asset<T> Asset { get; }
    }
}