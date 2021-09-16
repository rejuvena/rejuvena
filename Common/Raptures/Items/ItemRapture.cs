#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using System.Collections.Generic;
using Rejuvena.Common.DataStructures;
using Terraria.ModLoader;

namespace Rejuvena.Common.Raptures.Items
{
    public abstract class ItemRapture : IItemRapture
    {
        public virtual IEnumerable<IRapture> InheritedRaptures { get; } = Array.Empty<IRapture>();

        public abstract IEnumerable<ItemCollectionProfile> AssociatedItems { get; }

        public abstract void Update(ModItem updateableEntity);
    }
}