using System.Collections;
using System.Collections.Generic;
using VS.Library.Generics.Common;

namespace VS.Library.Generics.Cache
{
	public class CacheBase<TKey, TValue>: ICachedItemsCollection<TKey, TValue>
	{

		private Dictionary<TKey, TValue> storage = new Dictionary<TKey, TValue>();
		
		protected Dictionary<TKey, TValue> Storage
		{
			get { return this.storage; }
		}

		public ICollection<TKey> Keys
		{
			get { return this.storage.Keys; }
		}

		public TValue GetItem(TKey key, D0<TValue> getter)
		{
			TValue value;
			if (TryGetItem(key, out value))
			{
				AfterGet(key, value);
			}
			else
			{
				value = getter();
				Add(key, value);
			}
			return value;
		}

		public TValue GetItem(TKey key, D1<TValue, TKey> getter)
		{
			return GetItem(key, (D0<TValue>)delegate { return getter(key); });
		}

		public TValue GetItemOrDefault(TKey key, TValue defaultValue)
		{
			TValue value;
			if (TryGetItem(key, out value))
			{
				AfterGet(key, value);
			}
			else
			{
				value = defaultValue;
				Add(key, value);
			}
			return value;
		}

		public bool TryGetItem(TKey key, out TValue value)
		{ 
			return this.storage.TryGetValue(key, out value);
		}

		protected virtual void Add(TKey key, TValue value)
		{
			this.storage.Add(key, value);
		}

		protected virtual void AfterGet(TKey key, TValue value)
		{ 
		}

		protected virtual bool Remove(TKey key)
		{
			return this.storage.Remove(key);
		}

		#region IEnumerable<TKey> Members

		public IEnumerator<TKey> GetEnumerator()
		{
			return this.storage.Keys.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.storage.Keys.GetEnumerator();
		}

		#endregion
	}
}
