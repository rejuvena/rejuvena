#region License

// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007

#endregion

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rejuvena.Common.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using TomatoLib.Core.Drawing;

namespace Rejuvena.Content.Items
{
    public abstract partial class RaptureItem<TMainRapture>
    {
        public abstract int ItemToDrawAs { get; }

        public override string Texture => ItemToDrawAs switch
        {
            >= ItemID.Count => ItemLoader.GetItem(ItemToDrawAs).Texture,
            ItemID.None => base.Texture,
            _ => $"Terraria/Images/Item_{ItemToDrawAs}"
        };

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 pos, Rectangle frame, Color drawColor,
            Color itemColor, Vector2 orig, float scale)
        {
            if (ItemToDrawAs == ItemID.None)
                return true;

            drawColor = GetWantedColor(drawColor);
            Texture2D tex = TextureAssets.Item[ItemToDrawAs].ForceRequest().Value;

            DrawTreasureBagLikeEffect(spriteBatch, tex, pos, 0f, orig, scale, frame);

            using DisposableSpriteBatch sb = ApplyShader(Main.spriteBatch,
                SamplerState.LinearClamp,
                Main.UIScaleMatrix,
                Main.LocalPlayer,
                pos,
                scale,
                frame,
                tex
            );

            sb.Draw(tex, pos, frame, drawColor, 0f, orig, scale, SpriteEffects.None, 0f);

            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color light, Color alpha, ref float rot,
            ref float scale, int whoAmI)
        {
            if (ItemToDrawAs == ItemID.None)
                return true;

            alpha = GetWantedColor(alpha);
            SpriteEffects effects = Main.LocalPlayer.GetEffects();
            Texture2D tex = TextureAssets.Item[ItemToDrawAs].ForceRequest().Value;
            Vector2 drawPos = Item.position - Main.screenPosition;

            Item.GetInWorldDrawData(out Rectangle frame, out Rectangle _, out Vector2 orig);
            DrawTreasureBagLikeEffect(spriteBatch, tex, drawPos, rot, orig, scale, frame);

            using DisposableSpriteBatch sb = ApplyShader(Main.spriteBatch,
                SamplerState.PointClamp,
                Main.LocalPlayer.GetMatrix(),
                Main.player[Item.playerIndexTheItemIsReservedFor],
                drawPos,
                scale,
                frame,
                tex
            );

            sb.Draw(tex, drawPos, frame, alpha, rot, orig, new Vector2(scale), effects, 0f);

            return false;
        }

        public static DisposableSpriteBatch ApplyShader(
            SpriteBatch spriteBatch,
            SamplerState samplerState,
            Matrix matrix,
            Player player,
            Vector2 position,
            float scale,
            Rectangle sourceFrame,
            Texture2D texture)
        {
            SpriteBatchSnapshot immediate = new(
                SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                samplerState,
                spriteBatch.GraphicsDevice.DepthStencilState,
                spriteBatch.GraphicsDevice.RasterizerState,
                null,
                matrix
            );

            DisposableSpriteBatch sb = new(spriteBatch, immediate);

            DrawData data = new()
            {
                position = position,
                scale = new Vector2(scale),
                sourceRect = sourceFrame,
                texture = texture
            };

            int dye = ContentSamples.ItemsByType[ItemID.PhaseDye].dye;

            ArmorShaderData shader = GameShaders.Armor.GetSecondaryShader(dye, player);
            shader.Apply(player, data);

            return sb;
        }

        public static Color GetWantedColor(Color color)
        {
            color = color.Multiply(Main.DiscoColor);
            color.A = 220;
            return color;
        }

        public virtual void DrawTreasureBagLikeEffect(SpriteBatch spriteBatch, Texture2D texture, Vector2 position,
            float rotation, Vector2 origin, float scale, Rectangle frame)
        {
            float rotationSpeed = Main.GlobalTimeWrappedHourly;
            //float rotationDistance = Main.GlobalTimeWrappedHourly * 4f % 4f / 2f;

            //if (rotationDistance >= 1f)
            //    rotationDistance = 2f - rotationDistance;

            const float rotationModifier = /*rotationDistance * 0.5f +*/ 0.5f;

            Vector2 PosOffset(float i) => new Vector2(0f, 8f).RotatedBy((i + rotationSpeed) * MathHelper.TwoPi);

            void DrawTheThing(float i, Color color)
            {
                Vector2 drawPos = position + PosOffset(i) * rotationModifier;

                spriteBatch.Draw(texture, drawPos, frame, color, rotation, origin, scale, SpriteEffects.None, 0f);
            }

            for (float i = 0.0f; i < 1.0; i += 0.25f)
                DrawTheThing(i, new Color(90, 70, 255, 50));

            for (float i = 0.0f; i < 1.0; i += 0.34f)
                DrawTheThing(i, new Color(140, 120, 255, 77));
        }
    }
}