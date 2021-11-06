using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.UI;

namespace Rejuvena.Core.Cutscenes
{
    public abstract class Cutscene : ILoadable
    {
        public abstract bool Visible { get; }

        public abstract void Load(Mod mod);

        public abstract void Unload();

        public abstract int InsertionIndex(List<GameInterfaceLayer> layers);

        public abstract void Draw();
    }
}