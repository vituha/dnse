using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VS.Library.Booleans
{
    public static class PredicateExtensions
    {
        public static Predicate<T> And<T>(this Predicate<T> x, Predicate<T> y)
        {
            return obj => x(obj) && y(obj); 
        }

        public static Predicate<T> Or<T>(this Predicate<T> x, Predicate<T> y)
        {
            return obj => x(obj) || y(obj);
        }

        public static Predicate<T> Not<T>(this Predicate<T> x)
        {
            return obj => !x(obj);
        }

        public static Predicate<T> Xor<T>(this Predicate<T> x, Predicate<T> y)
        {
            return obj => x(obj) ^ y(obj);
        }

    }
}
