using System;
using System.Collections;
using System.Collections.Generic;

namespace VS.Library.Collections.Specialized
{
    public class BulkEditList<T> : IBulkEditList<T>
    {
        private const int MinBufferSize = 16;

        private static readonly T[] EmptyArray = new T[0];

        private T[] _buffer = EmptyArray;
        private int _start;
        private int _count;

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = _start; i < _count; i++)
            {
                yield return _buffer[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            AddRange(EnumerableUtil.Yield(item));
        }

        public void Clear()
        {
            _count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(_buffer, _start, array, arrayIndex, _count);
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index < 0)
            {
                return false;
            }

            RemoveRange(index, 1);
            return true;
        }

        public int Count
        {
            get { return _count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        private void AddRange(IEnumerable<T> source)
        {
            AddRange(source, _count);
        }

        private void RemoveRange(int count)
        {
            RemoveRange(_count - count, count);
        }

        public void ReplaceRange(int removeCount, IEnumerable<T> addSource)
        {
            RemoveRange(removeCount);
            AddRange(addSource);
        }

        public int IndexOf(T item)
        {
            var comparer = EqualityComparer<T>.Default;
            var hashCode = comparer.GetHashCode(item);
            for (int i = _start; i < _count; i++)
            {
                T currentItem = _buffer[i];
                if (hashCode == comparer.GetHashCode(currentItem) && comparer.Equals(item, currentItem))
                {
                    return ExportIndex(i);
                }
            }
            return -1;
        }

        private int ExportIndex(int internalIndex)
        {
            return internalIndex - _start;
        }

        private int ImportIndex(int externalIndex)
        {
            return externalIndex + _start;
        }

        public void Insert(int index, T item)
        {
            AddRange(EnumerableUtil.Yield(item), index);
        }

        public void RemoveAt(int index)
        {
            RemoveRange(index, 1);
        }

        public T this[int index]
        {
            get { return _buffer[ImportIndex(index)]; }
            set { _buffer[ImportIndex(index)] = value; }
        }

        private void AddRange(IEnumerable<T> source, int start)
        {
            var collectionSource = source as IReadOnlyCollection<T>;
            if (collectionSource != null)
            {
                BulkAdd(collectionSource, start);
                _count += collectionSource.Count;
                return;
            }

            var itemArray = new T[1];
            foreach (T item in source)
            {
                itemArray[0] = item;
                BulkAdd(itemArray, start++);
                _count++;
            }
        }

        private void BulkAdd(IReadOnlyCollection<T> collectionSource, int start)
        {
            if (start * 2 < _count && AttemptToInsertByMovingLeftSide(collectionSource, start))
            {
                return;
            }
            if (AttemptToInsertByMovingRightSide(collectionSource, start))
            {
                return;
            }
            InsertByReallocation(collectionSource, start);
        }

        private bool AttemptToInsertByMovingLeftSide(IReadOnlyCollection<T> collectionSource, int start)
        {
            int insertCount = collectionSource.Count;
            int moveCount = start;
            if (_start < insertCount) return false;

            if (moveCount > 0)
            {
                Array.Copy(_buffer, _start, _buffer, _start - insertCount, moveCount);
            }
            int insertPosition = ImportIndex(start);
            foreach (T insertedItem in collectionSource)
            {
                _buffer[insertPosition++] = insertedItem;
            }
            _start -= insertCount;
            return true;
        }

        private bool AttemptToInsertByMovingRightSide(IReadOnlyCollection<T> collectionSource, int start)
        {
            int insertCount = collectionSource.Count;
            int moveCount = _count - start;
            if (_start + _count + insertCount > _buffer.Length) return false;

            if (moveCount > 0)
            {
                Array.Copy(_buffer, ImportIndex(start), _buffer, ImportIndex(start) + insertCount, moveCount);
            }
            int insertPosition = ImportIndex(start);
            foreach (T insertedItem in collectionSource)
            {
                _buffer[insertPosition++] = insertedItem;
            }
            return true;
        }

        private void InsertByReallocation(IReadOnlyCollection<T> collectionSource, int start)
        {
            int insertCount = collectionSource.Count;
            int leftMoveCount = start;
            int rightMoveCount = _count - leftMoveCount;

            int newCount = _count + insertCount;
            int newBufferLength = Math.Max(_buffer.Length * 2, MinBufferSize);
            while (newBufferLength < newCount)
            {
                newBufferLength *= 2;
            }

            var newBuffer = new T[newBufferLength];

            int newStart;
            if (_count > 0)
            {
                int newFreeSpace = newBufferLength - newCount;
                int minReserved = newFreeSpace / 2;
                newStart = (int)(minReserved / 2 + (newFreeSpace - minReserved) * (long)rightMoveCount / _count);
            }
            else
            {
                newStart = (newBufferLength - insertCount) / 2;
            }
            if (leftMoveCount > 0)
            {
                Array.Copy(_buffer, _start, newBuffer, newStart, leftMoveCount);
            }
            if (rightMoveCount > 0)
            {
                Array.Copy(_buffer, _start + leftMoveCount, newBuffer, newStart + leftMoveCount + insertCount, rightMoveCount);
            }

            int insertPosition = newStart + leftMoveCount;
            foreach (T insertedItem in collectionSource)
            {
                newBuffer[insertPosition++] = insertedItem;
            }
            _start = newStart;
            _buffer = newBuffer;
        }

        private void RemoveRange(int start, int count)
        {
            if (start < 0)
            {
                throw new IndexOutOfRangeException("start");
            }
            int firstSurviverIndex = start + count;
            if (count < 0 || firstSurviverIndex > _count)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            int surviverCount = _count - firstSurviverIndex;
            if (surviverCount > 0)
            {
                Array.Copy(_buffer, ImportIndex(firstSurviverIndex), _buffer, ImportIndex(start), surviverCount);
            }
            _count -= count;
        }

        public void ReplaceRange(int removeAt, int removeCount, int insertAt, IEnumerable<T> insertSource)
        {
            RemoveRange(removeAt, removeCount);
            AddRange(insertSource, insertAt);
        }
    }
}