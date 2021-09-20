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

            drawColor = drawColor.Multiply(Color.DarkGray.Multiply(Main.DiscoColor));
            drawColor.A = 220;

            Texture2D tex = TextureAssets.Item[ItemToDrawAs].ForceRequest().Value;

            SpriteBatchSnapshot immediate = new(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                SamplerState.LinearClamp, spriteBatch.GraphicsDevice.DepthStencilState,
                spriteBatch.GraphicsDevice.RasterizerState, null, Main.UIScaleMatrix);

            DrawTreasureBagLikeEffect(spriteBatch, tex, pos, 0f, orig, scale, frame);

            using DisposableSpriteBatch sb = new(spriteBatch, immediate);
            DrawData data = new()
            {
                position = pos,
                scale = new Vector2(scale),
                sourceRect = frame,
                texture = tex
            };

            GameShaders.Armor.GetSecondaryShader(ContentSamples.ItemsByType[ItemID.PhaseDye].dye, Main.LocalPlayer)
                .Apply(Main.LocalPlayer, data);
            sb.Draw(tex, pos, frame, drawColor, 0f, orig, scale, SpriteEffects.None, 0f);

            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color light, Color alpha, ref float rot,
            ref float scale, int whoAmI)
        {
            if (ItemToDrawAs == ItemID.None)
                return true;

            alpha = alpha.Multiply(Main.DiscoColor);
            alpha.A = 220;

            SpriteEffects effects =
                Main.LocalPlayer.gravity <= -1f ? SpriteEffects.FlipVertically : SpriteEffects.None;

            Texture2D tex = TextureAssets.Item[ItemToDrawAs].ForceRequest().Value;
            Vector2 drawPos = Item.position - Main.screenPosition;

            SpriteBatchSnapshot immediate = new(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                SamplerState.PointClamp, spriteBatch.GraphicsDevice.DepthStencilState,
                spriteBatch.GraphicsDevice.RasterizerState, null, Main.LocalPlayer.GetMatrix());

            Item.GetInWorldDrawData(out Rectangle frame, out Rectangle _, out Vector2 orig);
            DrawTreasureBagLikeEffect(spriteBatch, tex, drawPos, rot, orig, scale, frame);

            using DisposableSpriteBatch sb = new(spriteBatch, immediate);
            DrawData data = new()
            {
                position = drawPos,
                scale = new Vector2(scale),
                sourceRect = frame,
                texture = tex
            };

            GameShaders.Armor
                .GetSecondaryShader(ContentSamples.ItemsByType[ItemID.PhaseDye].dye,
                    Main.player[Item.playerIndexTheItemIsReservedFor])
                .Apply(Main.player[Item.playerIndexTheItemIsReservedFor], data);
            sb.Draw(tex, drawPos, frame, alpha, rot, orig, new Vector2(scale), effects, 0f);

            return false;
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