using System.Linq;
using Rejuvena.Core.Services;
using Rejuvena.Core.Services.Impl;
using Terraria;
using Terraria.GameContent.UI.States;
using Terraria.ID;
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
                if (Main.menuMode != MenuID.Title) 
                    return false;

                Main.menuMode = MenuID.FancyUI;
                Main.MenuUI.SetState(new UIAchievementsMenu());

                return true;
            });
        }

        public TService GetService<TService>() where TService : Service => GetContent<TService>().First();
    }
}