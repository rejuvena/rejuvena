using System.Collections.Generic;
using System.IO;
using Terraria.Audio;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Rejuvena.Core.CoreSystems.MusicSystem
{
    public class MusicPlayer : ModSystem
    {
        public MP3AudioTrack Audio;

        public override void OnModLoad()
        {
            base.OnModLoad();

            Stream stream = Mod.GetFileStream("Assets/Music/Sky_Tower.mp3");

            Audio = new MP3AudioTrack(stream);
            Audio.SetVariable("Volume", 1f);
            Audio.Play();
        }

        public override void PostUpdateEverything()
        {
            base.PostUpdateEverything();
            
            if (Audio is null)
                return;

            Audio.Update();

            if (Audio.IsStopped)
            {
                Audio.Reuse();
                Audio.Play();
            }

            Mod.Logger.Debug(Audio.IsPlaying);
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            base.ModifyWorldGenTasks(tasks, ref totalWeight);

            totalWeight += 1f;
            tasks.Add(new PassLegacy("OurMod:OurName", (progress, configuration) =>
            {
                progress.Value += 1f;
            }));
        }
    }
}