using System;
using System.Collections.Generic;
using System.Text;

namespace VS.Cache
{
    public class CacheBase
    {
        private Dictionary<Delegate, object> cache = new Dictionary<Delegate, object>();
        protected Dictionary<Delegate, object> Storage
        {
            get { return this.cache; }
        }

        public delegate T ItemRetrieverDelegate<T>();
        public virtual T Get<T>(ItemRetrieverDelegate<T> retriever)
        {
            T value;
            try
            {
                value = (T)this.cache[retriever];
            }
            catch (KeyNotFoundException)
            {
                value = retriever();
                this.cache.Add(retriever, value);
            }
            return value;
        }

        protected virtual void Add(Delegate retriever, object value)
        {
            this.cache.Add(retriever, value);
        }

        protected virtual bool Remove(Delegate retriever)
        {
            return this.cache.Remove(retriever);
        }
    }
}
