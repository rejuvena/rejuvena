#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using System.Collections.Generic;
using Rejuvena.Common.DataStructures.ItemCollections;
using Terraria;
using Terraria.ModLoader;

namespace Rejuvena.Common.Raptures
{
    /// <summary>
    ///     Default implementation of <see cref="IItemRapture"/>.
    /// </summary>
    public abstract class ItemRapture : IItemRapture
    {
        public virtual IEnumerable<IRapture> InheritedRaptures { get; } = Array.Empty<IRapture>();

        public abstract IEnumerable<ItemCollectionProfile> AssociatedItems { get; }

        public virtual void Update(ModItem updateableEntity)
        {
        }

        public virtual void UpdateInWorld(ModItem modItem, ref float gravity, ref float maxFallSpeed)
        {
        }

        public virtual void UpdateInInventory(ModItem modItem, Player player)
        {
        }

        public virtual void UpdateWhenEquipped(ModItem modItem, Player player)
        {
        }

        public virtual void UpdateAsAccessory(ModItem modItem, Player player, bool hideVisual)
        {
        }
    }
}