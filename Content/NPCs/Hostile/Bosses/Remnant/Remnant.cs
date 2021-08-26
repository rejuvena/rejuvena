using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rejuvena.Common.Utilities;
using Rejuvena.Content.DrawEffects;
using Rejuvena.Content.Items.Materials;
using Rejuvena.Content.Items.Materials.Jungle;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TomatoLib.Common.Systems.DrawEffects;

namespace Rejuvena.Content.NPCs.Hostile.Bosses.Remnant
{
    public class Remnant : RejuvenaNPC
    {
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Rejuvena.BestiaryText.JadeConstruct"))
            });
        }

        public override void SetDefaults()
        {
            NPC.Size = new Vector2(30f, 32f);
            NPC.lifeMax = 250;
            NPC.life = 250;
            NPC.defense = 3;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.damage = 50;
            NPC.HitSound = SoundID.DD2_SkeletonHurt; // placeholder
            NPC.DeathSound = SoundID.DD2_SkeletonDeath; // placeholder
        }

        public override void AI()
        {

        }
    }
}