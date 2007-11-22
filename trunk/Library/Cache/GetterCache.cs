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
        // TODO make something
        private static Dictionary<Delegate, object> cacheInstance = new Dictionary<Delegate, object>();

        public static TValue Get<TValue>(D0<TValue> getter)
        {
            object value;
            if(cacheInstance.TryGetValue(getter, out value))
                return (TValue)value;
            TValue tvalue = getter();
            lock ((cacheInstance as ICollection).SyncRoot) 
            {
                cacheInstance.Add(getter, tvalue);
            }
            return tvalue;
        }
    }
}
