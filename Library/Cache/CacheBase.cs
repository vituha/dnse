using System;
using System.Collections.Generic;
using System.Text;

namespace VS.Library.Cache
{
    public class CacheBase
    {
        private Dictionary<object, object> cache = new Dictionary<object, object>();
        protected Dictionary<object, object> Storage
        {
            get { return this.cache; }
        }

        public delegate T ItemRetrieverDelegate<T>();
        public virtual T Get<T>(object key, ItemRetrieverDelegate<T> retriever)
        {
            T value;
            try
            {
                value = (T)this.cache[key];
            }
            catch (KeyNotFoundException)
            {
                value = retriever();
                this.cache.Add(key, value);
            }
            return value;
        }

        public T Get<T>(ItemRetrieverDelegate<T> retriever)
        {
            return Get<T>(retriever, retriever);
        }

        protected virtual void Add(object key, object value)
        {
            this.cache.Add(key, value);
        }

        protected virtual bool Remove(Delegate retriever)
        {
            return this.cache.Remove(retriever);
        }
    }
}
