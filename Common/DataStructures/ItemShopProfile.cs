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
    public class ItemCollectionProfile
    {
        public List<(int, int)> ItemData;
        public int ExtraValue;

        public ItemCollectionProfile(params (int, int)[] itemData)
        {
            ItemData = itemData.ToList();
        }

        public ItemCollectionProfile(params int[] items)
        {
            ItemData = items.Select(x => (x, 1)).ToList();
        }

        public ItemCollectionProfile(IEnumerable<(int, int)> itemData)
        {
            ItemData = itemData.ToList();
        }

        public ItemCollectionProfile(IEnumerable<int> items)
        {
            ItemData = items.Select(x => (x, 1)).ToList();
        }

        public ItemCollectionProfile WithItems(params (int, int)[] itemData)
        {
            ItemData = itemData.ToList();
            return this;
        }

        public ItemCollectionProfile WithItems(params int[] items)
        {
            ItemData = items.Select(x => (x, 1)).ToList();
            return this;
        }

        public ItemCollectionProfile WithItems(IEnumerable<(int, int)> itemData)
        {
            ItemData = itemData.ToList();
            return this;
        }

        public ItemCollectionProfile WithItems(IEnumerable<int> items)
        {
            ItemData = items.Select(x => (x, 1)).ToList();
            return this;
        }

        public ItemCollectionProfile WithExtraValue(int extraValue)
        {
            ExtraValue = extraValue;
            return this;
        }

        public virtual bool CanBeApplied() => true;

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