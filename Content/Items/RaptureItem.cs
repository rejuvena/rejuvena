#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using JetBrains.Annotations;
using Rejuvena.Common.DataStructures;
using Rejuvena.Common.Raptures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Rejuvena.Content.Items
{
    public abstract class RaptureItem<TMainRapture> : RejuvenaItem where TMainRapture : IItemRapture, new()
    {
        public virtual TMainRapture MainRapture { get; } = new();

        [CanBeNull]
        public virtual TReturn ExecuteFromInheritedRaptures<TReturn>(Func<IRapture, TReturn, TReturn> action,
            int inheritCount = -1)
        {
            inheritCount++;

            if (inheritCount > 50)
                throw new Exception("Recursive watchdog for raptures triggered.");

            TReturn retVal = default;

            foreach (IRapture rapture in MainRapture.InheritedRaptures)
            {
                retVal = action(rapture, retVal);
                retVal = ExecuteFromInheritedRaptures(action, inheritCount);
            }

            return retVal;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            SetDefaultsFromEnum(Defaults.Accessory);
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