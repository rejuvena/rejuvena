namespace Rejuvena.Assets
{
    // TODO: Investigate implementing a method for retrieving an asset at the specified path?
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
    }
}