#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rejuvena.Common.DataStructures;
using Rejuvena.Common.Raptures;
using Rejuvena.Common.Shaders;
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
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor,
            Vector2 origin, float scale)
        {
            if (ItemToDrawAs == ItemID.None)
                return true;

            Asset<Texture2D> itemTexture = TextureAssets.Item[ItemToDrawAs].ForceRequest();

            DrawTreasureBagLikeEffect(spriteBatch, itemTexture.Value, position, 0f, origin, scale, frame);

            Matrix matrix = Main.LocalPlayer.gravDir >= 1f
                ? Main.GameViewMatrix.ZoomMatrix
                : Main.GameViewMatrix.TransformationMatrix;

            SpriteBatchSnapshot immediate = new(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                SamplerState.PointClamp, spriteBatch.GraphicsDevice.DepthStencilState,
                spriteBatch.GraphicsDevice.RasterizerState, null, matrix);

            using DisposableSpriteBatch sb = new(spriteBatch, immediate);
            DrawData data = new()
            {
                position = position,
                scale = new Vector2(scale),
                sourceRect = frame,
                texture = itemTexture.Value
            };

            ArmorShaderData shader = ShaderContainer.Instance.OutlineShader;
            shader.UseColor(Main.DiscoColor);
            ShaderContainer.Instance.Apply(shader, Main.player[Item.playerIndexTheItemIsReservedFor], data);

            sb.Draw(itemTexture.Value, position, frame, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);

            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale,
            int whoAmI)
        {
            if (ItemToDrawAs == ItemID.None)
                return true;

            SpriteEffects sEffects = Main.LocalPlayer.gravity <= -1f ? SpriteEffects.FlipVertically : SpriteEffects.None;
            
            Asset<Texture2D> itemTexture = TextureAssets.Item[ItemToDrawAs].ForceRequest();

            Item.GetInWorldDrawData(out Rectangle frame, out Rectangle _, out Vector2 origin);

            DrawTreasureBagLikeEffect(spriteBatch, itemTexture.Value, Item.position - Main.screenPosition, rotation,
                origin, scale, frame);

            SpriteBatchSnapshot immediate = new(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                SamplerState.LinearClamp, spriteBatch.GraphicsDevice.DepthStencilState,
                spriteBatch.GraphicsDevice.RasterizerState, null, Main.UIScaleMatrix);

            using DisposableSpriteBatch sb = new(spriteBatch, immediate);
            DrawData data = new()
            {
                position = Item.position - Main.screenPosition,
                scale = new Vector2(scale),
                sourceRect = frame,
                texture = itemTexture.Value
            };

            ArmorShaderData shader = ShaderContainer.Instance.OutlineShader;
            shader.UseColor(Main.DiscoColor);
            ShaderContainer.Instance.Apply(shader, Main.player[Item.playerIndexTheItemIsReservedFor], data);

            spriteBatch.Draw(itemTexture.Value, Item.position - Main.screenPosition, frame, alphaColor, rotation,
                origin, new Vector2(scale), sEffects, 0f);

            return false;
        }

        public virtual void DrawTreasureBagLikeEffect(SpriteBatch spriteBatch, Texture2D texture, Vector2 position,
            float rotation, Vector2 origin, float scale, Rectangle frame)
        {
            float rotationSpeed = Main.GlobalTimeWrappedHourly;
            float rotationDistance = Main.GlobalTimeWrappedHourly * 4f % 4f / 2f;

            if (rotationDistance >= 1.0)
                rotationDistance = 2f - rotationDistance;

            float rotationModifier = (float) (rotationDistance * 0.5 + 0.5);

            Vector2 PosOffset(float i) => new Vector2(0.0f, 8f).RotatedBy((i + rotationSpeed) * MathHelper.TwoPi);

            void DrawTheThing(float i, Color color) => spriteBatch.Draw(texture,
                position + PosOffset(i) * rotationModifier, frame, color, rotation, origin, scale, SpriteEffects.None,
                0.0f);

            for (float num4 = 0.0f; num4 < 1.0; num4 += 0.25f)
                DrawTheThing(num4, new Color(90, 70, 255, 50));
            for (float num4 = 0.0f; num4 < 1.0; num4 += 0.34f)
                DrawTheThing(num4, new Color(140, 120, 255, 77));
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