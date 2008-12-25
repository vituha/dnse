using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

namespace VS.Library.Booleans
{
    /// <summary>
    /// Exposes complex predicates (those combining many simple predicates)
    /// </summary>
    public static class ComplexPredicates
    {
        public static Predicate<T> Or<T>(params Predicate<T>[] collection)
        {
            return obj => null != Algorithms.FindFirstWhere(collection, p => p(obj));
        }

        public static Predicate<T> And<T>(params Predicate<T>[] collection)
        {
            return obj => null == Algorithms.FindFirstWhere(collection, p => !p(obj));
        }

        public static Predicate<T> DNF<T>(params IEnumerable<Predicate<T>>[] doubleCollection)
        {
            return obj => null != Algorithms.FindFirstWhere(
                doubleCollection,
                collection => null == Algorithms.FindFirstWhere(collection, p => !p(obj))
            );
        }

        public static Predicate<T> CNF<T>(params IEnumerable<Predicate<T>>[] doubleCollection)
        {
            return obj => null == Algorithms.FindFirstWhere(
                doubleCollection,
                collection => null == Algorithms.FindFirstWhere(collection, p => p(obj))
            );
        }
    }
}
