namespace Rejuvena.Assets
{
    /// <summary>
    ///     Represents a fallback asset object. Does not hold a texture.
    /// </summary>
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