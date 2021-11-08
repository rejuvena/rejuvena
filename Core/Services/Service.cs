using JetBrains.Annotations;
using Terraria.ModLoader;

namespace Rejuvena.Core.Services
{
    [MeansImplicitUse]
    public abstract class Service : ModType
    {
        protected sealed override void Register()
        {
        }

        public override void Load()
        {
            base.Load();

            Mod.Logger.Debug("Initialized and loading service: " + Name);
        }
    }
}