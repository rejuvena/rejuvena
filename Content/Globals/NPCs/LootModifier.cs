#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using Terraria;
using Terraria.ModLoader;
using TomatoLib.Common.Utilities.Matching;

namespace Rejuvena.Content.Globals.NPCs
{
    public abstract class LootModifier : ILoadable
    {
        public abstract Matcher<int> NpcMatcher { get; }

        public virtual void Load(Mod mod)
        {
        }

        public virtual void Unload()
        {
        }

        public abstract void ModifyNpcLoot(NPC npc, NPCLoot loot);
    }
}