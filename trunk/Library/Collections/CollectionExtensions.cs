namespace VS.Library.Collections
{
    using System.Collections.Generic;
    using System.Collections;
    using System;

    public static class CollectionExtensions
    {
        public static bool HasItems<T>(this ICollection<T> source)
        {
            return source != null && source.Count > 0;
        }

        public static bool HasItemsUntyped(this ICollection source)
        {
            return source != null && source.Count > 0;
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        public static bool IsNullOrEmptyUntyped(this ICollection source)
        {
            return source == null && source.Count <= 0;
        }
    }
}
