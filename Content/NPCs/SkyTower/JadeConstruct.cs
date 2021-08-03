using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rejuvena.Content.DrawEffects;
using Rejuvena.Content.Items.Materials;
using Rejuvena.Core.CoreSystems.DrawEffects;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Rejuvena.Content.NPCs.SkyTower
{
    public class JadeConstruct : RejuvenaNPC
    {
        public static Asset<Texture2D> CoreTexture;
        public static Asset<Texture2D> DebrisTexture;

        public override void Load()
        {
            base.Load();

            CoreTexture = ModContent.Request<Texture2D>("Rejuvena/Content/NPCs/SkyTower/JadeConstructCore");
            DebrisTexture = ModContent.Request<Texture2D>("Rejuvena/Content/NPCs/SkyTower/JadeConstructDebris");
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Zombie];
            BestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Rotation = 0f
            };
        }

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
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];

            NPC.velocity += NPC.DirectionTo(player.Center) * 0.06f;
            NPC.velocity *= 0.95f;

            NPC.ai[0]++;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<JadeGemstone>(), minimumDropped: 4,
                maximumDropped: 9)); //Drop 4-9 Jade Gemstones. 
        }

        public override bool SpecialOnKill()
        {
            JadeSparkle sparkle =
                new(NPC.Center + new Vector2(0, (float) Math.Cos(NPC.ai[0] / 20) * 4), new Vector2(0, -1.7f))
                {
                    TargetScale = 0.3f,
                    NPC = null
                };

            DrawEffectManager.Instance.DrawEffects.Add(sparkle);

            for (int i = 0; i < 16; i++)
            {
                sparkle = new JadeSparkle(NPC.Center + new Vector2(0, (float) Math.Cos(NPC.ai[0] / 20) * 4),
                    new Vector2(0, -1.7f).RotatedBy(MathHelper.ToRadians(i * (360 / 16))))
                {
                    TargetScale = 0.3f,
                    NPC = null
                };

                DrawEffectManager.Instance.DrawEffects.Add(sparkle);
            }

            for (int i = 0; i < 4; i++)
            {
                Vector2 dustSpawnPoint = NPC.Center + new Vector2(10, -10).RotatedBy(MathHelper.ToRadians(90 * i));

                for (i = 0; i < 20; i++)
                    Dust.NewDust(dustSpawnPoint - new Vector2(5, 5), 10, 10, DustID.Stone);
            }

            Main.item[Item.NewItem(NPC.Center, ModContent.ItemType<JadeGemstone>(), Main.rand.Next(4, 10))].velocity =
                Vector2.Zero;

            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.IsABestiaryIconDummy &&
                NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(Type, out NPCID.Sets.NPCBestiaryDrawModifiers modifiers))
            {
                if (!modifiers.Hide)
                    NPC.ai[0]++;
            }

            spriteBatch.Draw(CoreTexture.Value,
                NPC.Center - screenPos + new Vector2(0, (float) Math.Cos(NPC.ai[0] / 20) * 4),
                new Rectangle(0, 0, CoreTexture.Value.Width, CoreTexture.Value.Height),
                Color.White,
                0f, new Vector2(CoreTexture.Value.Width / 2f, CoreTexture.Value.Height / 2f), NPC.scale, SpriteEffects.None,
                0f);

            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(DebrisTexture.Value,
                    NPC.Center - screenPos + new Vector2(0, (float) Math.Cos(NPC.ai[0] / 20 + 0.5f * i) * 8),
                    new Rectangle(0, 0 + DebrisTexture.Value.Height / 4 * i, DebrisTexture.Value.Width,
                        DebrisTexture.Value.Height / 4),
                    drawColor,
                    0f, new Vector2(DebrisTexture.Value.Width / 2f, DebrisTexture.Value.Height / 4f / 2f), NPC.scale,
                    SpriteEffects.None, 0f);
            }

            return false;
        }
    }
}