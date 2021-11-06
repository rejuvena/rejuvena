using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Terraria.ModLoader;
using Terraria.UI;
using TomatoLib.Common.Systems;

namespace Rejuvena.Core.Cutscenes
{
    public sealed class CutsceneLoader : SingletonSystem<CutsceneLoader>
    {
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            base.ModifyInterfaceLayers(layers);

            foreach (Cutscene cutscene in GetScenes())
            {
                string name = cutscene.GetType().Name;

                layers.Insert(
                    cutscene.InsertionIndex(layers),
                    new LegacyGameInterfaceLayer(Mod.Name + ":" + name,
                        delegate
                        {
                            if (cutscene.Visible)
                                cutscene.Draw();

                            return true;
                        },
                        InterfaceScaleType.UI
                    )
                );
            }
        }
        
        [CanBeNull]
        public static T Get<T>() where T : Cutscene => GetScenes().Where(x => x is T) as T;

        public static IEnumerable<Cutscene> GetScenes() => ModContent.GetContent<Cutscene>();
    }
}