using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using VS.Comparison;
using System.Diagnostics;

namespace VS.Cache
{
    public class LimitedCache: CacheBase
    {
        private struct CacheItem
        {
            public DateTime Created;
            public Delegate Key;
            public object Object;

            public CacheItem(Delegate key, object item)
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

        protected override void Add(Delegate retriever, object value)
        {
            if (this.Storage.Count > this.upperLimit)
            {
                Pack();
            }
            CacheItem item = new CacheItem(retriever, value);
            base.Add(retriever, item);
        }

        public override T Get<T>(ItemRetrieverDelegate<T> retriever)
        {
            CacheItem item;
            try
            {
                item = (CacheItem)this.Storage[retriever];
            }
            catch (KeyNotFoundException)
            {
                item = new CacheItem(retriever, retriever());
                this.Storage.Add(retriever, item);
            }
            return (T)item.Object;
        }

        protected virtual void Pack()
        {
            ArrayList items = new ArrayList(this.Storage.Values);
            items.Sort(new PropertyComparer("Created"));
            for (int i = 0; i < this.Storage.Count / 2; i++)
            {
                this.Storage.Remove(((CacheItem)items[i]).Key);
            }
            Trace.WriteLine(String.Format("Packed to {0}", this.Storage.Count));
        }
    }
}
