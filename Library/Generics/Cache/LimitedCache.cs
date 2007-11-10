using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;

using VS.Library.Generics.Common;
using VS.Library.Generics.Comparison;

namespace VS.Library.Generics.Cache
{
    public class Default<T>
    {
        public static T Value;
    }

    public class LimitedCache<TKey, TValue> : CacheBase<TKey, LimitedCache<TKey, TValue>.CacheItem>, ICache<TKey, TValue>
    {
        public struct CacheItem
        {
            public DateTime Created;
            public TKey Key;
            public TValue Object;

            public CacheItem(TKey key, TValue item)
            {
                this.Key = key;
                this.Object = item;
                this.Created = DateTime.Now;
            }

            public TValue ValueGetter()
            {
                return this.Object;
            }
        }

        private int upperLimit;

        public LimitedCache(int upperLimit)
        {
            this.upperLimit = upperLimit;
        }

        protected override void Add(TKey key, CacheItem value)
        {
            base.Add(key, value);
            if (this.Storage.Count > this.upperLimit)
            {
                Pack();
            }
        }

        protected virtual void Add(TKey key, TValue value)
        {
            Add(key, new CacheItem(key, value));
        }

        public TValue Get(TKey key, Delegate1<TValue> getter)
        {
            return base.Get(key, (Delegate1<CacheItem>)delegate { return new CacheItem(key, getter()); }).Object;
        }

        public TValue Get(TKey key, Delegate2<TValue, TKey> getter)
        {
            return Get(key, (Delegate1<TValue>)delegate { return getter(key); });
        }

        public TValue Get(TKey key, TValue defaultValue)
        {
            return Get(key, (Delegate1<TValue>)delegate { return defaultValue; });
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            CacheItem item;
            if (base.TryGetValue(key, out item))
            {
                value = item.Object;
                return true;
            }
            else
            {
                value = Default<TValue>.Value;
                return false;
            }
        }
        
        protected virtual void Pack()
        {
            int itemsToDelete = this.Storage.Count / 2;
            ArrayList items = new ArrayList(this.Storage.Values);
            items.Sort(new FieldComparer<CacheItem, DateTime>("Created"));
            for (int i = 0; i < itemsToDelete; i++)
            {
                this.Storage.Remove(((CacheItem)items[i]).Key);
            }
            Trace.WriteLine(String.Format("Packed to {0}", this.Storage.Count));
        }
    }
}
