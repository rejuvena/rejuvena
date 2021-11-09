using System.Linq;
using Rejuvena.Core.Services;
using Rejuvena.Core.Services.Impl;
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
        }

        public TService GetService<TService>() where TService : Service => GetContent<TService>().First();
    }
}
