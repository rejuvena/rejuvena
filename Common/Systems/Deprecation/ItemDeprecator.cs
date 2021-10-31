using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using TomatoLib.Common.Systems;

namespace Rejuvena.Common.Systems.Deprecation
{
    public sealed class ItemDeprecator : SingletonSystem<ItemDeprecator>
    {
        private Dictionary<int, bool> DeprecationCache;

        public override void Load()
        {
            base.Load();

            DeprecationCache = new Dictionary<int, bool>();
        }

        public override void Unload()
        {
            base.Unload();

            foreach ((int item, bool deprecated) in DeprecationCache)
                ItemID.Sets.Deprecated[item] = deprecated;
        }

        public void Add(Item item) => Add(item.type);

        public void Add(int item)
        {
            if (!DeprecationCache.ContainsKey(item))
                DeprecationCache[item] = ItemID.Sets.Deprecated[item];

            ItemID.Sets.Deprecated[item] = true;
        }

        public void Remove(Item item) => Remove(item.type);
    
        public void Remove(int item)
        {
            if (!DeprecationCache.ContainsKey(item))
                DeprecationCache[item] = ItemID.Sets.Deprecated[item];

            ItemID.Sets.Deprecated[item] = false;
        }
    }
}