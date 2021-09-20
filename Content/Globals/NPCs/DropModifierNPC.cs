#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System.Linq;
using Rejuvena.Common.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace Rejuvena.Content.Globals.NPCs
{
    public class DropModifierNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npc, npcLoot);
            
            // ReSharper disable once AccessToModifiedClosure - Justification: NPCLoot is readonly and will never be modified.
            ModContent.GetContent<LootModifier>().Where(x => x.NPCMatcher.Match(npc.type)).ForEach(x => x.ModifyNPCLoot(npc, npcLoot));
        }
    }
}