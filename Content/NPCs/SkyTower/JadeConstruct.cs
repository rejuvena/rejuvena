using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Rejuvena.Content.NPCs;
using System;
using Rejuvena.Core.CoreSystems.DrawEffects;
using Rejuvena.Content.DrawEffects;
using Rejuvena.Content.Items.Materials;
using Rejuvena.Content.Items;
using ReLogic.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rejuvena.Content.NPCs.SkyTower
{
    class JadeConstruct : RejuvenaNPC
    {

        public override void SetDefaults()
        {
            NPC.Size = new Vector2(30, 32);
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

            Player Player = Main.player[NPC.target];

            NPC.velocity += NPC.DirectionTo(Player.Center) * 0.06f;
            NPC.velocity *= 0.95f;

            NPC.ai[0]++;

            JadeSparkle sparkle = new JadeSparkle(NPC.Center + new Vector2(0, (float)Math.Cos(NPC.ai[0] / 20) * 4), Vector2.Zero);
            sparkle.NPC = NPC;
            sparkle.TargetScale = 0.42f;
            DrawEffectManager.Instance.DrawEffects.Add(sparkle);
        }

        public override void OnKill()
        {
            for (int i = 0; i < 16; i++)
            {
                JadeSparkle sparkle = new JadeSparkle(NPC.Center + new Vector2(0, (float)Math.Cos(NPC.ai[0] / 20) * 4), new Vector2(0, -1.7f).RotatedBy(MathHelper.ToRadians(i * (360 / 16))));
                sparkle.TargetScale = 0.3f;
                sparkle.NPC = null;
                DrawEffectManager.Instance.DrawEffects.Add(sparkle);
            }
            for (int i = 0; i < 4; i++)
            {
                Vector2 dustSpawnPoint = NPC.Center + new Vector2(10, -10).RotatedBy(MathHelper.ToRadians(90 * i));

                for (i = 0; i < 20; i++)
                {
                    Dust.NewDust(dustSpawnPoint - new Vector2(5, 5), 10, 10, DustID.Stone);
                }
            }
            int item = Item.NewItem(NPC.Center, ModContent.ItemType<JadeGemstone>(), Main.rand.Next(4, 9)); // TODO: Set JadeGemstone.Floating to true here, and false in the base code for the item
            Main.item[item].velocity = Vector2.Zero;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Asset<Texture2D> core = ModContent.Request<Texture2D>("Rejuvena/Content/NPCs/SkyTower/JadeConstructCore");
            Asset<Texture2D> debris = ModContent.Request<Texture2D>("Rejuvena/Content/NPCs/SkyTower/JadeConstructDebris");

            spriteBatch.Draw(core.Value, NPC.Center - screenPos + new Vector2(0, (float)Math.Cos(NPC.ai[0] / 20) * 4),
                new Rectangle(0, 0, core.Value.Width, core.Value.Height),
                Lighting.GetColor(new Point((int)NPC.Center.X / 16, (int)NPC.Center.Y / 16)),
                0f, new Vector2(core.Value.Width / 2, core.Value.Height / 2), 1, SpriteEffects.None, 0f);

            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(debris.Value, NPC.Center - screenPos + new Vector2(0, (float)Math.Cos(NPC.ai[0] / 20 + (0.5f * i)) * 8),
                    new Rectangle(0, 0 + (debris.Value.Height / 4 * i), debris.Value.Width, debris.Value.Height / 4),
                    Lighting.GetColor(new Point((int)NPC.Center.X / 16, (int)NPC.Center.Y / 16)),
                    0f, new Vector2(debris.Value.Width / 2, debris.Value.Height / 4 / 2), 1, SpriteEffects.None, 0f);
            }

            return false;
        }
    }
}
