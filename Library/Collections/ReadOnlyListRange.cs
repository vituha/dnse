namespace VS.Library.Collections
{
    using System.Collections.Generic;

    public class ReadOnlyListRange<T> : IReadOnlyList<T>
    {
        private readonly IReadOnlyList<T> _innerList;

        public ReadOnlyListRange(IReadOnlyList<T> source, int start, int count)
        {
            _innerList = source;
            Count = count;
            Start = start;
        }

        public int Start { get; }

        public int Count { get; }

        public T this[int index] => _innerList[Start + index];

        public IEnumerator<T> GetEnumerator()
        {
            int index = Start;
            int end = Start + Count;
            while (index != end)
            {
                yield return _innerList[index];
                index ++;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
