using System;
using Rejuvena.Assets;
using Terraria.ModLoader;

namespace Rejuvena.Content.Players
{
    /// <summary>
    ///     Base <see cref="ModPlayer"/> class for <see cref="Rejuvena"/>.
    /// </summary>
    public abstract class RejuvenaPlayer : ModPlayer, IModContent
    {
        public FallbackAsset GetFallbackAsset() =>
            throw new NotImplementedException("Unimplemented as there are no attached assets.");
    }
}