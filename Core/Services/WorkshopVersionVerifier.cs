using Steamworks;

namespace Rejuvena.Core.Services
{
    public abstract class WorkshopVersionVerifier : Service
    {
        public abstract ulong PublishedFileId { get; }

        public bool NeedsUpdating { get; set; }

        public override void Load()
        {
            base.Load();
            
            Mod.Logger.Info("Mod version: " + Mod.Version);
            Mod.Logger.Info("Mod file ID: " + PublishedFileId);

            Mod.Logger.Info("Fetching mod item state...");

            uint state = SteamUGC.GetItemState(new PublishedFileId_t(PublishedFileId));
            EItemState itemState = (EItemState) state;

            NeedsUpdating = itemState == EItemState.k_EItemStateNeedsUpdate;

            Mod.Logger.Info("Mod item state: " + itemState);
            Mod.Logger.Info("Mod needs updating: " + NeedsUpdating);
        }
    }
}