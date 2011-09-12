namespace VS.Library.Collections.Views
{
    using System;
    using System.Collections.Generic;

    public class ListRange<T> : ReadOnlyListBase<T>
    {
        private IList<T> InnerList { get; set; }
        private int Start { get; set; }
        private int Length { get; set; }

        public ListRange(IList<T> source, int start, int length)
        {
            if (source == null) throw new ArgumentNullException("source");
            InnerList = source;
            Start = start;
            Length = length;
        }

        protected override IEnumerator<T> GetGenericEnumerator()
        {
            IList<T> innerList = InnerList;
            int end = Start + Length;
            for (int i = Start; i < end; i++)
            {
                yield return innerList[i];
            }
        }

        protected override int GetCount()
        {
            return Length;
        }

        protected override T GetAt(int index)
        {
            return InnerList[index + Start];
        }

        protected override int GetIndex(T item)
        {
            int index = InnerList.IndexOf(item);
            if (index <= 0)
            {
                return index;
            }
            return index - Start;
        }

        protected override void CopyToArray(T[] array, int arrayIndex)
        {
            IList<T> innerList = InnerList;
            int end = Start + Length;
            for (int i = Start; i < end; i++)
            {
                array[arrayIndex++] = innerList[i];
            }
        }
    }
}
