using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using VS.Library.Generics.Common;
using VS.Library.Generics.Common.Delegates;
using System.Diagnostics;

namespace VS.Library.Generics.Comparison
{
    /// <summary>
    /// Represents a single key participating in complex comparison
    /// </summary>
    public class SortKey<T>
    {
        /// <summary>
        /// comparable value getter
        /// </summary>
        public D1<object, T> ValueGetter;
        /// <summary>
        /// indicates whether to negate the result received from comparer
        /// </summary>
        public bool Negative;
        /// <summary>
        /// comparer to use for the value
        /// </summary>
        public IComparer Comparer;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="getter">comparable value getter</param>
        /// <param name="negative">indicates whether to negate the result of comparison</param>
        /// <param name="comparer">comparer to use for the value</param>
        public SortKey(D1<object, T> getter, bool negative, IComparer comparer)
        {
            this.ValueGetter = getter;
            this.Negative = negative;
            this.Comparer = comparer;
        }

        /// <summary>
        /// Creates a new instance using default comparer
        /// </summary>
        /// <param name="getter">comparable value getter</param>
        /// <param name="negative">indicates whether to negate the result of comparison</param>
        public SortKey(D1<object, T> getter, bool negative)
            : this(getter, negative, System.Collections.Comparer.Default) { }

        /// <summary>
        /// Creates a new instance using default comparer and no negation 
        /// </summary>
        /// <param name="getter">comparable value getter</param>
        public SortKey(D1<object, T> getter)
            : this(getter, true, System.Collections.Comparer.Default) { }
    }
    
    public class ComplexComparer<T> : IComparer<T>, IComparer
    {
        private IEnumerable<SortKey<T>> keys;
        public IEnumerable<SortKey<T>> Keys
        {
            get { return keys; }
            set 
            {
                Debug.Assert(value != null);
                keys = value; 
            }
        }

        #region IComparer<T> Members

        /// <summary>
        /// Compares two objects.
        /// If number of participating property names is bigger than number of custom comparers then default comparers will be used for the rest.
        /// </summary>
        /// <param name="x">1st object</param>
        /// <param name="y">2nd object</param>
        /// <returns>
        /// a configured comparer's return value optionally negated if <c>negative</c> is true
        /// </returns>
        int IComparer<T>.Compare(T x, T y)
        {
            try
            {
                int result = 0;
                IEnumerator<SortKey<T>> en = keys.GetEnumerator();
                bool itemAvailable = en.MoveNext();
                if (itemAvailable)
                {
                    do
                    {
                        SortKey<T> key = en.Current;
                        result = key.Comparer.Compare(key.ValueGetter(x), key.ValueGetter(y));
                        if (key.Negative) result = -result;
                        itemAvailable = en.MoveNext();
                    }
                    while (itemAvailable && result == 0);
                }
                return result;
            }
            catch (Exception e)
            {
                ApplicationException outer = new ApplicationException("Unable to compare two objects", e);
                throw outer;
            }
        }

        #endregion

        #region IComparer Members

        int IComparer.Compare(object x, object y)
        {
            return (this as IComparer<T>).Compare((T)x, (T)y);
        }

        #endregion
    }
}
