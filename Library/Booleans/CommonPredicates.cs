using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace VS.Library.Booleans
{
    public static class CommonPredicates<T>
    {
        public static Predicate<T> True { get { return obj => true; } }
        public static Predicate<T> False { get { return obj => false; } }
        public static Predicate<T> IsNull { get { return obj => obj == null; } }
        public static Predicate<T> IsNotNull { get { return obj => obj != null; } }
        public static Predicate<ICollection<T>> HasItems { get { return collection => collection.Count > 0; } }
    }
}
