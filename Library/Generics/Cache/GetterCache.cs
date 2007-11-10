using System;
using System.Collections.Generic;
using System.Text;

using VS.Library.Generics.Common;

namespace VS.Library.Generics.Cache
{
    public interface IGetterCache<TValue>: ICache<Delegate1<TValue>, TValue>
    {
        TValue Get(Delegate1<TValue> key);
    }
    
    public class GetterCache<TValue> : CacheBase<Delegate1<TValue>, TValue>, IGetterCache<TValue>
    {
        public TValue Get(Delegate1<TValue> key)
        {
            return Get(key, key);
        }
    }
}
