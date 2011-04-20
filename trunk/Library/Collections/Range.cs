namespace VS.Library.Collections
{
    using System;
    using System.Collections.Generic;

    public class Range<T> : IList<T>
    {
        private IList<T> _innerList;
        private int _start;
        private int _count;
        private int _increment;

        public Range(IList<T> source, int start, int count, bool reverse)
        {
            _innerList = source;
            _count = count;
            if (reverse)
            {
                this._start = start + count;
                _increment = -1;
            }
            else
            {
                _start = start;
                _increment = 1;
            }
        }

        #region IList<T> Members

        public int IndexOf(T item)
        {
            return GetOuterIndex(_innerList.IndexOf(item));
        }

        public void Insert(int index, T item)
        {
            ThrowFixed();
        }

        public void RemoveAt(int index)
        {
            ThrowFixed();
        }

        public T this[int index]
        {
            get
            {
                return _innerList[GetInnerIndex(index)];
            }
            set
            {
                _innerList[GetInnerIndex(index)] = value;
            }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            ThrowFixed();
        }

        public void Clear()
        {
            ThrowFixed();
        }

        public bool Contains(T item)
        {
            return _innerList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            int lastArrayIndex = Math.Min(_count, array.Length - arrayIndex);
            if (lastArrayIndex > 0)
            {
                int sourceIndex = _start;
                for (int i = 0; i < lastArrayIndex; i++, sourceIndex += _increment)
                {
                    array[i] = _innerList[sourceIndex];
                }
            }
        }

        public int Count
        {
            get { return _count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            ThrowFixed();
            return false; // needed for VS
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            int index = _start;
            int end = _start + _count * _increment;
            while (index != end)
            {
                yield return _innerList[index];
                index += _increment;
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        private int GetInnerIndex(int outerIndex)
        {
            return _start + outerIndex * _increment;
        }

        private int GetOuterIndex(int innerIndex)
        {
            return (innerIndex - _start) * _increment;
        }

        private void ThrowFixed()
        {
            throw new InvalidOperationException("list is fixed and cannot be resized");
        }
    }
}
