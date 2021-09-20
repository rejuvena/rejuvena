#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using Rejuvena.Common.DataStructures.Matching;
using Rejuvena.Common.Utilities;
using Rejuvena.Content.Items.Weapons.Magic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Rejuvena.Content.Globals.NPCs.LootModifiers
{
    public class HarpyLootModifier : LootModifier
    {
        public override Matcher<int> NPCMatcher => new Matcher<int>().MatchExact(NPCID.Harpy);

        public override void ModifyNPCLoot(NPC npc, NPCLoot loot) =>
            loot.Add(new CommonDrop(ModContent.ItemType<Whirlwind>(), 90));
    }
}