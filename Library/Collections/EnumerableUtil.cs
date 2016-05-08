using System;
using System.Collections.Generic;

namespace VS.Library.Collections
{
    public static class EnumerableUtil
    {
        public static IEnumerable<T> Yield<T>(T item)
        {
            yield return item;
        }

        public static IEnumerable<T> Yield<T>(T item1, T item2)
        {
            yield return item1;
            yield return item2;
        }

        public static IEnumerable<T> Yield<T>(T item1, T item2, T item3)
        {
            yield return item1;
            yield return item2;
            yield return item3;
        }

        public static IEnumerable<T> Yield<T>(T item1, T item2, T item3, T item4, params T[] otherItems)
        {
            if (otherItems == null) throw new ArgumentNullException("otherItems");

            yield return item1;
            yield return item2;
            yield return item3;
            yield return item4;
            foreach (T item in otherItems)
            {
                yield return item;
            }
        }
    }
}
