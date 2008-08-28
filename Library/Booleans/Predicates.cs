using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using VS.Library.Collections;

namespace VS.Library.Booleans
{
    /// <summary>
    /// Exposes commonly-used generic predicates
    /// </summary>
    public static class Predicates<T>
    {
        public static readonly Predicate<T> True = v => true;
        public static readonly Predicate<T> False = v => false;
        public static readonly Predicate<T> IsNull = v => v == null;
        public static readonly Predicate<T> IsNotNull = v => v != null;
        public static readonly Predicate<ICollection<T>> IsNonEmptyCollection = v => v != null && v.Count > 0;
    }

    /// <summary>
    /// Exposes commonly-used non-generic predicates
    /// </summary>
    public static class Predicates
    {
        public static readonly Predicate<int> IsNonNegative = v => v >= 0;
        public static readonly Predicate<int> IsPositive = v => v > 0;
        public static readonly Predicate<int> IsZero = v => v == 0;
        public static readonly Predicate<string> IsNonEmptyString = v => !String.IsNullOrEmpty(v);
        public static readonly Predicate<ICollection> IsNonEmptyCollection = v => v != null && v.Count > 0;
    }

}
