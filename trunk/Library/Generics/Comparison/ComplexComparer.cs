using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace VS.Library.Generics.Comparison
{
    public class ComplexComparer<T> : IComparer<T>
    {
        public delegate object ValueGetterDelegate(T context);
        public class SortKey
        {
            public ValueGetterDelegate ValueGetter;
            public bool Negative;
            public IComparer Comparer;

            public SortKey(ValueGetterDelegate getter, bool ascending, IComparer comparer)
            {
                this.ValueGetter = getter;
                this.Negative = ascending;
                this.Comparer = comparer;
            }

            public SortKey(ValueGetterDelegate getter, bool ascending)
                : this(getter, ascending, System.Collections.Comparer.Default) { }

            public SortKey(ValueGetterDelegate getter)
                : this(getter, true, System.Collections.Comparer.Default) { }
        }

        private IEnumerable<SortKey> keys;

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
                IEnumerator<SortKey> en = keys.GetEnumerator();
                bool itemAvailable = en.MoveNext();
                if (itemAvailable)
                {
                    do
                    {
                        SortKey key = en.Current;
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
                ApplicationException outer = new ApplicationException("PropertyComparer: Unable to compare two objects", e);
                throw outer;
            }
        }

        #endregion
    }
}
