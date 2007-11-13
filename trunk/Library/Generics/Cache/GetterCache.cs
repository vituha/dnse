using System;
using System.Collections.Generic;
using System.Text;

using VS.Library.Generics.Common;
using VS.Library.Generics.Common.Delegates;

namespace VS.Library.Generics.Cache
{
    public interface IGetterCache<TValue>: ICache<D0<TValue>, TValue>
    {
        TValue Get(D0<TValue> key);
    }
    
    public class GetterCache<TValue> : CacheBase<D0<TValue>, TValue>, IGetterCache<TValue>
    {
        public TValue Get(D0<TValue> key)
        {
            return Get(key, key);
        }
    }
}
