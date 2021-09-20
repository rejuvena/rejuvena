#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using Rejuvena.Common.DataStructures.Matching;
using Terraria;
using Terraria.ModLoader;

namespace Rejuvena.Content.Globals.NPCs
{
    public abstract class LootModifier : ILoadable
    {
        public abstract Matcher<int> NPCMatcher { get; }

        public virtual void Load(Mod mod)
        {
        }

        public virtual void Unload()
        {
        }

        public abstract void ModifyNPCLoot(NPC npc, NPCLoot loot);
    }
}