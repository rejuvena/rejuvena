#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using JetBrains.Annotations;
using Rejuvena.Common.DataStructures.ItemCollections;
using Rejuvena.Common.Raptures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Rejuvena.Content.Items
{
    public abstract partial class RaptureItem<TMainRapture> : RejuvenaItem where TMainRapture : IItemRapture, new()
    {
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

        public virtual void UpdateInWorld(ref float gravity, ref float maxFallSpeed)
        {
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