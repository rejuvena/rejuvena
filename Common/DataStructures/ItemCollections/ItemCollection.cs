#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rejuvena.Common.DataStructures.ItemCollections
{
    public class ItemCollection : IList<ItemCollectionProfile>
    {
        #region Interface Implementation

        protected List<ItemCollectionProfile> UnderlyingCollection = new();

        public virtual IEnumerator<ItemCollectionProfile> GetEnumerator() => UnderlyingCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public virtual void Add(ItemCollectionProfile item) => UnderlyingCollection.Add(item);

        public virtual void Clear() => UnderlyingCollection.Clear();

        public virtual bool Contains(ItemCollectionProfile item) => UnderlyingCollection.Contains(item);

        public virtual void CopyTo(ItemCollectionProfile[] array, int arrayIndex) =>
            UnderlyingCollection.CopyTo(array, arrayIndex);

        public virtual bool Remove(ItemCollectionProfile item) => UnderlyingCollection.Remove(item);

        public virtual int Count => UnderlyingCollection.Count;

        public virtual bool IsReadOnly => false;

        public virtual int IndexOf(ItemCollectionProfile item) => UnderlyingCollection.IndexOf(item);

        public void Insert(int index, ItemCollectionProfile item) => UnderlyingCollection.Insert(index, item);

        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        public ItemCollectionProfile this[int index]
        {
            get => UnderlyingCollection[index];

            set => UnderlyingCollection[index] = value;
        }

        #endregion

        #region Added Stuff

        public class CombinedItemCollectionProfile : ItemCollectionProfile
        {
            public ItemCollectionProfile[] Profiles { get; }

            public override List<(int, int)> ItemData => Profiles.Where(x => x.CanBeApplied())
                .SelectMany(profile => profile.ItemData).ToList();

            public override int ExtraValue
            {
                get => Profiles.Where(x => x.CanBeApplied()).Sum(profile => profile.ExtraValue);

                set => throw new Exception("Cannot set the value of a combined item collection profile!");
            }



            public CombinedItemCollectionProfile(params ItemCollectionProfile[] profiles)
            {
                Profiles = profiles;
            }

            public override bool CanBeApplied() => true;

            // no need to check for CanBeApplied, already checked in ToValueCount!
            public override int ToValueCount() => Profiles.Sum(profile => profile.ToValueCount());
        }

        public static implicit operator ItemCollection(ItemCollectionProfile profile) => new() {profile};

        public static implicit operator ItemCollectionProfile(ItemCollection collection) =>
            new CombinedItemCollectionProfile(collection.ToArray());

        public static implicit operator CombinedItemCollectionProfile(ItemCollection collection) =>
            (ItemCollectionProfile) collection as CombinedItemCollectionProfile;

        #endregion
    }
}