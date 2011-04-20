namespace VS.Library.Collections
{
    using System.Collections;
    using System.Collections.Generic;
    using System;
    
    public static class EnumerableExtensions
    {
        public static bool HasItems<T>(this IEnumerable<T> source)
        {
            if (source != null)
            {
                using (var enumerator = source.GetEnumerator())
                {
                    return enumerator.MoveNext();
                }
            }
            return false;
        }

        public static bool HasItemsUntyped(this IEnumerable source)
        {
            return source != null && source.GetEnumerator().MoveNext();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            if (source != null)
            {
                using (var enumerator = source.GetEnumerator())
                {
                    return enumerator.MoveNext() == false;
                }
            }
            return true;
        }

        public static bool IsNullOrEmptyUntyped(this IEnumerable source)
        {
            return source == null || source.GetEnumerator().MoveNext() == false;
        }

        public static void Split<T>(this IEnumerable<T> source, Func<T, int> splitter, params ICollection<T>[] targets)
        {
            foreach (T item in source)
            {
                int index = splitter(item);
                if (index >= 0)
                {
                    targets[index].Add(item);
                }
            }
        }
    }
}
