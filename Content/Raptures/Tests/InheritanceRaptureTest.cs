#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using System.Collections.Generic;
using Rejuvena.Common.DataStructures;
using Rejuvena.Common.Raptures;
using Rejuvena.Content.Raptures.Items.PreHardMode.Wood;
using Terraria;
using Terraria.ModLoader;

namespace Rejuvena.Content.Raptures.Tests
{
    public class InheritanceRaptureTest : WoodenRapture
    {
        public override IEnumerable<IRapture> InheritedRaptures
        {
            get
            {
                yield return new NestedRapture();
            }
        }

        public override void UpdateAsAccessory(ModItem modItem, Player player, bool hideVisual)
        {
            Main.NewText("Hello from rapture-land!");
        }

        public class NestedRapture : ItemRapture
        {
            public override IEnumerable<ItemCollectionProfile> AssociatedItems { get; } =
                Array.Empty<ItemCollectionProfile>();

            public override void UpdateInInventory(ModItem modItem, Player player)
            {
                Main.NewText("Hello from inheritance-land!");
            }
        }
    }
}