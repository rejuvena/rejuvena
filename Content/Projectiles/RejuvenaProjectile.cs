using System;
using Rejuvena.Assets;
using Terraria;
using Terraria.ModLoader;

namespace Rejuvena.Content.Projectiles
{
    /// <summary>
    ///     Abstract base class shared between all <see cref="Rejuvena"/> projectiles.
    /// </summary>
    public abstract class RejuvenaProjectile : ModProjectile
    {
        [Flags]
        public enum Defaults
        {
            Friendly = 0x1,
            Hostile = 0x2
        }

        public override string Texture => FallbackAsset.GetFallbackAsset(GetType(), base.Texture).Path;

        public void SetDefaultsFromEnum(Defaults defaultsToSet)
        {
            bool Any(Defaults defaults) => (defaults & defaultsToSet) != 0;

            if (Any(Defaults.Friendly))
                Projectile.friendly = true;

            if (Any(Defaults.Hostile))
                Projectile.hostile = true;
        }

        public Player GetOwner() => Main.player[Projectile.owner];
    }
}