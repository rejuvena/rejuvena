#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using Rejuvena.Content.Items.Weapons.Magic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Rejuvena.Content.Globals.NPCs
{
    public class DropModifierNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npc, npcLoot);

            switch (npc.type)
            {
                case NPCID.Harpy:
                    ModifyHarpyLoot(ref npcLoot);
                    break;
            }
        }

        public static void ModifyHarpyLoot(ref NPCLoot npcLoot) =>
            npcLoot.Add(new CommonDrop(ModContent.ItemType<Whirlwind>(), 80));
    }
}