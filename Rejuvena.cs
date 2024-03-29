using System.Linq;
using Rejuvena.Content.TitleLinkButtons;
using Rejuvena.Core.Services;
using Rejuvena.Core.Services.Impl;
using Terraria;
using Terraria.Initializers;
using Terraria.Localization;
using TomatoLib;

namespace Rejuvena
{
    public class Rejuvena : TomatoMod
    {
        public override void PostSetupContent()
        {
            base.PostSetupContent();

            GetService<TaskScheduler>().Tasks.Add(() =>
            {
                return true;
                /*
                // Display update menu for debugging or if an update menu is actually available.
                bool update = GetService<RejuvenaVersionVerifier>().NeedsUpdating || Debugger.IsAttached;
                
                if (Main.menuMode != MenuID.Title || !update)
                    return false;

                Main.menuMode = MenuModeHandler.GetMenu<UpdateNotificationMenu>()?.Id ?? MenuID.Title;

                return true;
                */
            });
            
            Main.TitleLinks.Add(new RejuvenaDiscordButton());
        }

        public override void Unload()
        {
            base.Unload();
            
            Main.TitleLinks.Clear();
            LinkButtonsInitializer.Load();
        }

        public TService GetService<TService>() where TService : Service => GetContent<TService>().First();

        public static string GetText(string key, params object[] args) =>
            Language.GetTextValue("Mods.Rejuvena." + key, args);
    }
}
