using System.Collections.Generic;

using VS.Library.Generics.Common;

namespace VS.Library.Generics.Cache
{
	public interface ICachedItemsCollection<TKey, TValue>: IEnumerable<TKey>
	{
		ICollection<TKey> Keys { get; }
		TValue GetItem(TKey key, D0<TValue> getter);
		TValue GetItem(TKey key, D1<TValue, TKey> getter);
		TValue GetItemOrDefault(TKey key, TValue defaultValue);
		bool TryGetItem(TKey key, out TValue value);
	}
}
