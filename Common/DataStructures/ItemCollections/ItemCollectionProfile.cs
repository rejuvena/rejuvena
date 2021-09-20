#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Terraria;

namespace Rejuvena.Common.DataStructures.ItemCollections
{
    /// <summary>
    ///     Buildable profile of items, supports appending a bonus value as well as dynamically modifying the underlying item collection.
    /// </summary>
    public class ItemCollectionProfile
    {
        public virtual List<(int, int)> ItemData { get; }

        public virtual int ExtraValue { get; set; }

        public ItemCollectionProfile()
        {
            ItemData = new List<(int, int)>();
        }

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

        public ItemCollectionProfile WithExtraValue(int extraValue)
        {
            ExtraValue = extraValue;
            return this;
        }

        /// <summary>
        ///     Whether this collection should be taken into account for raptures or item value calculation.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanBeApplied() => true;

        [MustUseReturnValue("Wasted calculation if it isn't used.")]
        public virtual int ToValueCount()
        {
            int total = 0;

            if (!CanBeApplied())
                return total;

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