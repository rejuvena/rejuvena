using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;

namespace Rejuvena.Core.Services.Impl
{
    public class TaskScheduler : Service
    {
        public List<Func<bool>> Tasks = new();

        public override void Load()
        {
            base.Load();

            On.Terraria.Main.Update += ExecutePostTasks;
        }

        public override void Unload()
        {
            base.Unload();

            Tasks.Clear();
        }

        private void ExecutePostTasks(On.Terraria.Main.orig_Update orig, Main self, GameTime gameTime)
        {
            orig(self, gameTime);

            Tasks.RemoveAll(x => x.Invoke());
        }
    }
}