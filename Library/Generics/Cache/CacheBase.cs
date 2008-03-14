using System.Collections;
using System.Collections.Generic;
using VS.Library.Generics.Common.Delegates;

namespace VS.Library.Generics.Cache
{
	public class CacheBase<TKey, TValue>: ICache<TKey, TValue>
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

		public TValue Get(TKey key, D0<TValue> getter)
		{
			TValue value;
			if (TryGetValue(key, out value))
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

		public TValue Get(TKey key, D1<TValue, TKey> getter)
		{
			return Get(key, (D0<TValue>)delegate { return getter(key); });
		}

		public TValue GetDefault(TKey key, TValue defaultValue)
		{
			TValue value;
			if (TryGetValue(key, out value))
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

		public bool TryGetValue(TKey key, out TValue value)
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
