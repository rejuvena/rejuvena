#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System.Linq;
using JetBrains.Annotations;
using Terraria;

namespace Rejuvena.Common.DataStructures
{
    public struct ItemShopProfile
    {
        public (int, int)[] ItemData;

        public ItemShopProfile(params (int, int)[] itemData)
        {
            ItemData = itemData;
        }

        public ItemShopProfile(params int[] items)
        {
            ItemData = items.Select(item => (item, 1)).ToArray();
        }

        [MustUseReturnValue]
        public int ToValueCount()
        {
            int total = 0;

            foreach ((int item, int count) in ItemData)
            {
                try
                {
                    Item i = new();
                    i.SetDefaults(item);
                    total += i.value * count;
                }
                catch
                {
                    // ignore
                }
            }

            return total;
        }
    }
}