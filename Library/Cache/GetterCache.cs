using System;
using System.Collections.Generic;
using System.Text;

using VS.Library.Generics.Common;
using VS.Library.Generics.Common.Delegates;
using VS.Library.Generics.Cache;
using System.Collections;

namespace VS.Library.Cache
{
    public static class GetterCache
    {
        private static SortedDictionary<int, object> cacheInstance = new SortedDictionary<int, object>();

        public static TValue Get<TValue>(D0<TValue> getter)
        {
            object value;
            int key = getter.GetHashCode();
            if(cacheInstance.TryGetValue(key, out value))
                return (TValue)value;
            TValue tvalue = getter();
            lock ((cacheInstance as ICollection).SyncRoot) 
            {
                cacheInstance.Add(key, tvalue);
            }
            return tvalue;
        }
    }
}
