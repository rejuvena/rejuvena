#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rejuvena.Common.DataStructures.ItemCollections;
using Rejuvena.Common.Raptures;
using Rejuvena.Common.Utilities;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using TomatoLib.Core.Drawing;

namespace Rejuvena.Content.Items
{
    public abstract class RaptureItem<TMainRapture> : RejuvenaItem where TMainRapture : IItemRapture, new()
    {
        public virtual TMainRapture MainRapture { get; } = new();

        public override string Texture => ItemToDrawAs switch
        {
            >= ItemID.Count => ItemLoader.GetItem(ItemToDrawAs).Texture,
            ItemID.None => base.Texture,
            _ => $"Terraria/Images/Item_{ItemToDrawAs}"
        };

        public abstract int ItemToDrawAs { get; }

        [CanBeNull]
        public virtual TReturn ExecuteFromInheritedRaptures<TReturn>(Func<IRapture, TReturn, TReturn> action,
            IRapture rapture = default, int inheritCount = -1)
        {
            rapture ??= MainRapture;
            inheritCount++;

            if (inheritCount > 50)
                throw new Exception("Recursive watchdog for raptures triggered.");

            TReturn retVal = default;

            foreach (IRapture iRapture in rapture.InheritedRaptures)
            {
                retVal = action(iRapture, retVal);
                retVal = ExecuteFromInheritedRaptures(action, iRapture, inheritCount);
            }

            return retVal;
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            ItemID.Sets.ItemNoGravity[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            SetDefaultsFromEnum(Defaults.Accessory);

            Item.Size = ItemToDrawAs == ItemID.None ? new Vector2(20f) : ContentSamples.ItemsByType[ItemToDrawAs].Size;
        }

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

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            base.Update(ref gravity, ref maxFallSpeed);

            ExecuteFromInheritedRaptures<object>((rapture, _) =>
            {
                if (rapture is IUpdateableRapture<ModItem> modItemRapture)
                    modItemRapture.Update(this);

                return null;
            });

            MainRapture.Update(this);

            float passedGravity = gravity;
            float passedFallSpeed = maxFallSpeed;

            ExecuteFromInheritedRaptures<object>((rapture, _) =>
            {
                if (rapture is IItemRapture itemRapture)
                    itemRapture.UpdateInWorld(this, ref passedGravity, ref passedFallSpeed);

                return null;
            });

            MainRapture.UpdateInWorld(this, ref gravity, ref maxFallSpeed);
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);

            ExecuteFromInheritedRaptures<object>((rapture, _) =>
            {
                if (rapture is IItemRapture itemRapture)
                    itemRapture.UpdateInInventory(this, player);

                return null;
            });

            MainRapture.UpdateInInventory(this, player);
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);

            ExecuteFromInheritedRaptures<object>((rapture, _) =>
            {
                if (rapture is IItemRapture itemRapture)
                    itemRapture.UpdateWhenEquipped(this, player);

                return null;
            });

            MainRapture.UpdateWhenEquipped(this, player);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            ExecuteFromInheritedRaptures<object>((rapture, _) =>
            {
                if (rapture is IItemRapture itemRapture)
                    itemRapture.UpdateAsAccessory(this, player, hideVisual);

                return null;
            });

            MainRapture.UpdateAsAccessory(this, player, hideVisual);
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            Recipe recipe = CreateRecipe();

            foreach (ItemCollectionProfile profile in MainRapture.AssociatedItems)
            {
                if (!profile.CanBeApplied())
                    continue;

                foreach ((int item, int count) in profile.ItemData)
                    recipe.AddIngredient(item, count);
            }

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}