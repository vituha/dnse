using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Collections;

using VS.Library.Generics.Common;
using VS.Library.Generics.Common.Delegates;

namespace VS.Library.Generics.Cache
{
    public class CacheBase<TKey, TValue>: ICache<TKey, TValue>
    {

        private Dictionary<TKey, TValue> cache = new Dictionary<TKey, TValue>();
        protected Dictionary<TKey, TValue> Storage
        {
            get { return this.cache; }
        }

        public ICollection<TKey> Keys
        {
            get { return this.cache.Keys; }
        }

        public TValue Get(TKey key, D0<TValue> getter)
        {
            TValue value;
            if (!TryGetValue(key, out value))
            {
                value = getter();
                Add(key, value);
            }
            return value;
        }

        public TValue Get(TKey key, D1<TValue, TKey> getter)
        {
            return Get(key, (D0<TValue>)delegate { return getter(key); });
        }

        public TValue GetDefault(TKey key, TValue defaultValue)
        {
            TValue value;
            if (!TryGetValue(key, out value))
            {
                value = defaultValue;
                Add(key, value);
            }
            return value;
        }

        public bool TryGetValue(TKey key, out TValue value)
        { 
            return this.Storage.TryGetValue(key, out value);
        }

        protected virtual void Add(TKey key, TValue value)
        {
            this.cache.Add(key, value);
        }

        protected virtual bool Remove(TKey key)
        {
            return this.cache.Remove(key);
        }
    }
}
