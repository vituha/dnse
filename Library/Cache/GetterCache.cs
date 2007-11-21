using System;
using System.Collections.Generic;
using System.Text;

using VS.Library.Generics.Common;
using VS.Library.Generics.Common.Delegates;
using VS.Library.Generics.Cache;

namespace VS.Library.Cache
{
    public static class GetterCache
    {
        private static CacheBase<object, object> cacheInstance = new CacheBase<object, object>();
        
        public static TValue Get<TValue>(D0<TValue> getter)
        {
            return (TValue)cacheInstance.Get(getter, (D0<object>) delegate { return getter(); });
        }
    }
}
