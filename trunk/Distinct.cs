
		/// <summary>
		/// Filters collection by distinct keys 
		/// </summary>
		public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> collection, Converter<T, TKey> keySelector) {
			return Distinct(collection, keySelector, null);
		}

		/// <summary>
		/// Filters collection by custom filter and then by distinct keys 
		/// </summary>
		public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> collection, Converter<T, TKey> keySelector, Predicate<T> itemFilter) {
			collection.RequireArgumentNotNull("collection");
			keySelector.RequireArgumentNotNull("keySelector");

			if (itemFilter == null) {
				itemFilter = value => true;
			}
			var keySet = new Set<TKey>();
			foreach (T item in collection) {
				TKey key = keySelector(item);
				if (keySet.Contains(key) == false && itemFilter(item)) {
					keySet.Add(key);
					yield return item;
				}
			}
		}