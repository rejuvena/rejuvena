#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System.Collections.Generic;
using Rejuvena.Common.DataStructures.ItemCollections;
using Rejuvena.Common.Raptures;
using Terraria.ID;

namespace Rejuvena.Content.Raptures.Items.PreHardMode.Wood
{
    public class WoodenRapture : ItemRapture
    {
        public override IEnumerable<ItemCollectionProfile> AssociatedItems
        {
            get
            {
                yield return new ItemCollectionProfile
                (
                    (ItemID.WoodHelmet, 1),
                    (ItemID.WoodBreastplate, 1),
                    (ItemID.WoodGreaves, 1),
                    (ItemID.WoodenSword, 1),
                    (ItemID.WoodenBow, 1),
                    (ItemID.WoodenHammer, 1)
                );
            }
        }
    }
}