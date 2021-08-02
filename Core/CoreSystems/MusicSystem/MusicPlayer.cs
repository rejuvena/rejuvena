using Terraria.Audio;
using Terraria.ModLoader;

namespace Rejuvena.Core.CoreSystems.MusicSystem
{
    public class MusicPlayer : ModSystem
    {
        public override void OnModLoad()
        {
            base.OnModLoad();

#if MUSICFIX
            int music = MusicLoader.GetMusicSlot("Rejuvena/Sounds/Music/Sky_Tower");
            Mod.Logger.Debug(music);
            Mod.Logger.Debug(((LegacyAudioSystem)Terraria.Main.audioSystem).AudioTracks.Length);
            ;
#endif
        }

        public override void PostSetupContent()
        {
            base.PostSetupContent();

#if MUSICFIX
            Mod.Logger.Debug(((LegacyAudioSystem)Terraria.Main.audioSystem).AudioTracks.Length);
            IAudioTrack musicTrack = ModContent.GetMusic("Rejuvena/Sounds/Music/Sky_Tower");
            ;
#endif
        }
    }

#if MUSICFIX
    public class TestMenu : ModMenu
    {
        public override int Music => MusicLoader.GetMusicSlot("Rejuvena/Sounds/Music/Sky_Tower");
    }
#endif
}