using System.Collections;
using System.Collections.Generic;
using VS.Library.Generics.Common;

namespace VS.Library.Generics.Cache
{
	public class Default<T>
	{
		public static T Value;
	}

	public class LimitedCache<TKey, TValue> : ICache<TKey, TValue>
	{
		private class CacheItem
		{
			private LinkedListNode<CacheItem> node;
			public LinkedListNode<CacheItem> Node
			{
				get { return node; }
			}

			private TKey key;
			public TKey Key
			{
				get { return key; }
			}

			private TValue value;
			public TValue Value
			{
				get { return this.value; }
			}

			public CacheItem(LinkedListNode<CacheItem> node, TKey key, TValue value)
			{
				this.node = node;
				this.key = key;
				this.value = value;
			}
		}

		private LinkedList<CacheItem> bubbleQueue = new LinkedList<CacheItem>();
		private Dictionary<TKey, CacheItem> realCache = new Dictionary<TKey, CacheItem>();
		private int upperLimit;

		public LimitedCache(int upperLimit)
		{
			this.upperLimit = upperLimit;
		}

		private CacheItem Add(TKey key, TValue value)
		{
			LinkedListNode<CacheItem> newNode = new LinkedListNode<CacheItem>(null);
			CacheItem newItem = new CacheItem(newNode, key, value);
			newNode.Value = newItem;

			if (this.bubbleQueue.Count >= this.upperLimit)
			{
				CacheItem last = this.bubbleQueue.Last.Value;
				this.bubbleQueue.RemoveLast();
				this.realCache.Remove(last.Key);
			}

			this.bubbleQueue.AddFirst(newItem);
			this.realCache.Add(key, newItem);

			return newItem;
		}

		private void Accessed(CacheItem item)
		{
			// bubble the item
			this.bubbleQueue.Remove(item);
			this.bubbleQueue.AddFirst(item);
		}

		#region ICache<TKey,TValue> Members

		public ICollection<TKey> Keys
		{
			get { return this.realCache.Keys; }
		}

		public TValue Get(TKey key, D0<TValue> getter)
		{
			CacheItem item;
			if (this.realCache.TryGetValue(key, out item))
			{
				Accessed(item);
			}
			else
			{ 
				item = Add(key, getter());
			}
			return item.Value;
		}

		public TValue Get(TKey key, D1<TValue, TKey> getter)
		{
			CacheItem item;
			if (this.realCache.TryGetValue(key, out item))
			{
				Accessed(item);
			}
			else
			{
				item = Add(key, getter(key));
			}
			return item.Value;
		}

		public TValue GetDefault(TKey key, TValue defaultValue)
		{
			CacheItem item;
			if (this.realCache.TryGetValue(key, out item))
			{
				Accessed(item);
			}
			else
			{
				item = Add(key, defaultValue);
			}
			return item.Value;
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			CacheItem item;
			if (realCache.TryGetValue(key, out item))
			{
				value = item.Value;
				return true;
			}
			else
			{
				value = Default<TValue>.Value;
				return false;
			}

		}

		#endregion

		#region IEnumerable<TKey> Members

		public IEnumerator<TKey> GetEnumerator()
		{
			return this.realCache.Keys.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.realCache.Keys.GetEnumerator();
		}

		#endregion
	}
}
