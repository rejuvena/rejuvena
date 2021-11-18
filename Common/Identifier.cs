namespace Rejuvena.Common
{
    public readonly struct Identifier
    {
        public readonly string Mod;
        public readonly string Id;

        public Identifier(string mod, string id)
        {
            Mod = mod;
            Id = id;
        }
    }
}