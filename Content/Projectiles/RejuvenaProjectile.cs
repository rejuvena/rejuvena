using System;
using System.Collections.Generic;
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
        public enum DrawBehindType
        {
            Default,
            NPCsAndTiles,
            NPCs,
            Projectiles,
            OverPlayers,
            OverWiresUI
        }

        [Flags]
        public enum Defaults
        {
            Friendly = 0x1,
            Hostile = 0x2
        }

        public override string Texture => FallbackAsset.GetFallbackAsset(GetType(), base.Texture).Path;

        public virtual DrawBehindType BehindType { get; set; } = DrawBehindType.Default;

        public void SetDefaultsFromEnum(Defaults defaultsToSet)
        {
            bool Any(Defaults defaults) => (defaults & defaultsToSet) != 0;

            if (Any(Defaults.Friendly))
                Projectile.friendly = true;

            if (Any(Defaults.Hostile))
                Projectile.hostile = true;
        }

        public Player GetOwner() => Main.player[Projectile.owner];

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs,
            List<int> behindProjectiles, List<int> overPlayers,
            List<int> overWiresUI)
        {
            static void SafeRemove(int index, ICollection<int> list)
            {
                if (list.Contains(index))
                    list.Remove(index);
            }

            void ClearIndex()
            {
                SafeRemove(index, behindNPCsAndTiles);
                SafeRemove(index, behindNPCs);
                SafeRemove(index, behindProjectiles);
                SafeRemove(index, overPlayers);
                SafeRemove(index, overWiresUI);
            }

            switch (BehindType)
            {
                case DrawBehindType.Default:
                    ClearIndex();
                    break;

                case DrawBehindType.NPCsAndTiles:
                    behindNPCsAndTiles.Add(index);
                    break;

                case DrawBehindType.NPCs:
                    behindNPCs.Add(index);
                    break;

                case DrawBehindType.Projectiles:
                    behindProjectiles.Add(index);
                    break;

                case DrawBehindType.OverPlayers:
                    overPlayers.Add(index);
                    break;

                case DrawBehindType.OverWiresUI:
                    overWiresUI.Add(index);
                    break;

                default:
                    ClearIndex();
                    break;
            }
        }
    }
}