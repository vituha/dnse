		#region Pre-LINQ methods (to be removed when switching to LINQ)

		//public static bool Any<T>(this IEnumerable<T> collection, Predicate<T> predicate) {
		//    collection.RequireArgumentNotNull("collection");
		//    predicate.RequireArgumentNotNull("predicate");
		//    foreach (var item in collection) {
		//        if (predicate(item)) {
		//            return true;
		//        }
		//    }
		//    return false;
		//}

		//public static bool All<T>(this IEnumerable<T> collection, Predicate<T> predicate) {
		//    collection.RequireArgumentNotNull("collection");
		//    predicate.RequireArgumentNotNull("predicate");
		//    foreach (var item in collection) {
		//        if (predicate(item) == false) {
		//            return false;
		//        }
		//    }
		//    return true;
		//}
	
		//public static IEnumerable<T> Where<T>(this IEnumerable<T> collection, Predicate<T> predicate) {
		//    collection.RequireArgumentNotNull("collection");
		//    predicate.RequireArgumentNotNull("predicate");
		//    foreach (var item in collection) {
		//        if (predicate(item)) {
		//            yield return item;
		//        }
		//    }
		//}

		//public static IEnumerable<T> Skip<T>(this IEnumerable<T> collection, int count) {
		//    collection.RequireArgumentNotNull("collection");
		//    foreach (var item in collection) {
		//        if (count > 0) {
		//            count--;
		//        } else {
		//            yield return item;
		//        }
		//    }
		//}

		//public static IEnumerable<T> Take<T>(this IEnumerable<T> collection, int count) {
		//    collection.RequireArgumentNotNull("collection");
		//    foreach (var item in collection) {
		//        if (count > 0) {
		//            yield return item;
		//            count--;
		//        }
		//    }
		//}

		//public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> collection, Converter<TSource, TResult> selector) {
		//    collection.RequireArgumentNotNull("collection");
		//    selector.RequireArgumentNotNull("selector");
		//    foreach (var item in collection) {
		//        yield return selector(item);
		//    }
		//}

		#endregion Pre-LINQ methods (to be removed when switching to LINQ)