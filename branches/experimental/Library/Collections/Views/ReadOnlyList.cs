namespace VS.Library.Collections.Views
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Implements a read-only collection wrapper.
    /// </summary>
    /// <typeparam name="T">Type of elements.</typeparam>
    public class ReadOnlyList<T> : ReadOnlyListBase<T>
    {
        protected IList<T> InnerList { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="source">Source collection.</param>
        public ReadOnlyList(IList<T> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            InnerList = source;
        }

        protected override IEnumerator<T> GetGenericEnumerator()
        {
            return InnerList.GetEnumerator();
        }

        protected override int GetCount()
        {
            return InnerList.Count;
        }

        protected override T GetAt(int index)
        {
            return InnerList[index];
        }

        protected override int GetIndex(T item)
        {
            return InnerList.IndexOf(item);
        }

        protected override void CopyToArray(T[] array, int arrayIndex)
        {
            InnerList.CopyTo(array, arrayIndex);
        }
    }
}
