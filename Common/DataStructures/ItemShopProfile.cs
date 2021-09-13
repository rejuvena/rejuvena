#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Terraria;

namespace Rejuvena.Common.DataStructures
{
    public class ItemShopProfile
    {
        public List<(int, int)> ItemData;
        public int ExtraValue;

        public ItemShopProfile(params (int, int)[] itemData)
        {
            ItemData = itemData.ToList();
        }

        public ItemShopProfile(params int[] items)
        {
            ItemData = items.Select(x => (x, 1)).ToList();
        }

        public ItemShopProfile(IEnumerable<(int, int)> itemData)
        {
            ItemData = itemData.ToList();
        }

        public ItemShopProfile(IEnumerable<int> items)
        {
            ItemData = items.Select(x => (x, 1)).ToList();
        }

        public ItemShopProfile WithItems(params (int, int)[] itemData)
        {
            ItemData = itemData.ToList();
            return this;
        }

        public ItemShopProfile WithItems(params int[] items)
        {
            ItemData = items.Select(x => (x, 1)).ToList();
            return this;
        }

        public ItemShopProfile WithItems(IEnumerable<(int, int)> itemData)
        {
            ItemData = itemData.ToList();
            return this;
        }

        public ItemShopProfile WithItems(IEnumerable<int> items)
        {
            ItemData = items.Select(x => (x, 1)).ToList();
            return this;
        }

        public ItemShopProfile WithExtraValue(int extraValue)
        {
            ExtraValue = extraValue;
            return this;
        }

        [MustUseReturnValue("Wasted calculation if it isn't used.")]
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

            return total + ExtraValue;
        }
    }
}