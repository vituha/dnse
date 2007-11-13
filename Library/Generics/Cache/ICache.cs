using System;
using System.Collections.Generic;
using System.Text;

using VS.Library.Generics.Common;
using VS.Library.Generics.Common.Delegates;

namespace VS.Library.Generics.Cache
{
    public interface ICache<TKey, TValue>
    {
        ICollection<TKey> Keys { get; }
        TValue Get(TKey key, D0<TValue> getter);
        TValue Get(TKey key, D1<TValue, TKey> getter);
        TValue Get(TKey key, TValue defaultValue);
        bool TryGetValue(TKey key, out TValue value);
    }
}
