using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;

using VS.Library.Comparison;

namespace VS.Library.Cache
{
    public class LimitedCache: CacheBase
    {
        public struct CacheItem
        {
            public DateTime Created;
            public object Key;
            public object Object;

            public CacheItem(object key, object item)
            {
                this.Key = key;
                this.Object = item;
                this.Created = DateTime.Now;
            }
        }

        private int upperLimit;

        public LimitedCache(int upperLimit)
        {
            this.upperLimit = upperLimit;
        }

        protected override void Add(object key, object value)
        {
            if (this.Storage.Count > this.upperLimit)
            {
                Pack();
            }
            CacheItem item = new CacheItem(key, value);
            base.Add(key, item);
        }

        public override T Get<T>(object key, ItemRetrieverDelegate<T> retriever)
        {
            try
            {
                CacheItem item = (CacheItem)this.Storage[key];
                item.Created = DateTime.Now;
                return (T)item.Object;
            }
            catch (KeyNotFoundException)
            {
                T value = retriever();
                Add(key, value);
                return value;
            }
        }

        protected virtual void Pack()
        {
            int itemsToDelete = this.Storage.Count / 2;
            ArrayList items = new ArrayList(this.Storage.Values);
            items.Sort(new PropertyComparer("Created"));
            for (int i = 0; i < itemsToDelete; i++)
            {
                this.Storage.Remove(((CacheItem)items[i]).Key);
            }
            Trace.WriteLine(String.Format("Packed to {0}", this.Storage.Count));
        }
    }
}
