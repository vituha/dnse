using System;
using System.Collections.Generic;
using System.Text;

using VS.Library.Generics.Common;

namespace VS.Library.Generics.Cache
{
    public interface ICache<TKey, TValue>
    {
        ICollection<TKey> Keys { get; }
        TValue Get(TKey key, Delegate1<TValue> getter);
        TValue Get(TKey key, Delegate2<TValue, TKey> getter);
        TValue Get(TKey key, TValue defaultValue);
        bool TryGetValue(TKey key, out TValue value);
    }
}
