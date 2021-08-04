using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Rejuvena.Core.CoreSystems
{
    /// <summary>
    ///     Queues actions to execute on the main thread.
    /// </summary>
    public class ThreadQueue : ModSystem
    {
        public List<Action> QueuedTasks = new();

        public override void Load()
        {
            base.Load();

            On.Terraria.Main.Update += ThreadInsertion;
        }

        private void ThreadInsertion(On.Terraria.Main.orig_Update orig, Main self, GameTime gameTime)
        {
            if (QueuedTasks.Count > 0)
            {
                foreach (Action queuedTask in QueuedTasks)
                    queuedTask?.Invoke();

                QueuedTasks.Clear();
            }

            orig(self, gameTime);
        }

        public static void AddToQueue(Action action) => ModContent.GetInstance<ThreadQueue>().QueuedTasks.Add(action);
    }
}