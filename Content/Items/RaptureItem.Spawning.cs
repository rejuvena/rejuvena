#region License

// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007

#endregion

using Microsoft.Xna.Framework;
using Rejuvena.Common.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace Rejuvena.Content.Items
{
    public enum RaptureSpawnStatus
    {
        OnCraft,
        Other
    }

    public abstract partial class RaptureItem<TMainRapture>
    {
        public RaptureSpawnStatus SpawnStatus { get; set; } = RaptureSpawnStatus.OnCraft;

        public bool InSpawnProcess { get; protected set; }

        public int SpawnTimer { get; protected set; }
        
        public virtual void UpdateOnSpawn(ref float gravity, ref float maxFallSpeed)
        {
            if (SpawnStatus != RaptureSpawnStatus.OnCraft && !InSpawnProcess)
                return;

            gravity = 0f;
            Item.velocity = Vector2.Zero;

            if (SpawnStatus == RaptureSpawnStatus.OnCraft)
            {
                InSpawnProcess = true;
                SpawnStatus = RaptureSpawnStatus.Other;
            }

            switch (SpawnTimer++)
            {
                case < 120:
                    Item.velocity.Y = -0.35f;
                    break;

                case 180:
                    DustUtilities.DrawStar(Item.position, DustID.Shadowflame);
                    SoundEngine.PlaySound(new LegacySoundStyle(SoundID.Item, 14));
                    break;

                case > 240:
                    InSpawnProcess = false;
                    break;
            }
        }

        public override bool CanPickup(Player player) => !InSpawnProcess && SpawnStatus != RaptureSpawnStatus.OnCraft;
    }
}