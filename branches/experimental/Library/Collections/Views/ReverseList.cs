namespace VS.Library.Collections.Views
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReverseList<T> : IList<T>
    {
        private IList<T> InnerList { get; set; }

        public ReverseList(IList<T> source)
        {
            if (source == null) throw new ArgumentNullException("source");
            InnerList = source;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = InnerList.Count; i >= 0; i--)
            {
                yield return InnerList[i];
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">
        /// The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        public void Add(T item)
        {
            InnerList.Insert(0, item);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        public void Clear()
        {
            InnerList.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
        /// </summary>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
        /// </returns>
        /// <param name="item">
        /// The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        public bool Contains(T item)
        {
            return InnerList.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index in <paramref name="array"/> at which copying begins.
        /// </param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            InnerList.CopyTo(array, arrayIndex);
            Array.Reverse(array, arrayIndex, InnerList.Count);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <param name="item">
        /// The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        public bool Remove(T item)
        {
            int index = GetLastIndexOf(item);
            if (index < 0)
            {
                return false;
            }
            InnerList.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public int Count
        {
            get { return InnerList.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly
        {
            get { return InnerList.IsReadOnly; }
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </summary>
        /// <returns>
        /// The index of <paramref name="item"/> if found in the list; otherwise, -1.
        /// </returns>
        /// <param name="item">
        /// The object to locate in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </param>
        public int IndexOf(T item)
        {
            return GetReverseIndex(GetLastIndexOf(item));
        }

        /// <summary>
        /// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1"/> at the specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index at which <paramref name="item"/> should be inserted.
        /// </param>
        /// <param name="item">
        /// The object to insert into the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </param>
        public void Insert(int index, T item)
        {
            InnerList.Insert(GetReverseIndex(index), item);
        }

        /// <summary>
        /// Removes the <see cref="T:System.Collections.Generic.IList`1"/> item at the specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index of the item to remove.
        /// </param>
        public void RemoveAt(int index)
        {
            InnerList.RemoveAt(GetReverseIndex(index));
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <returns>
        /// The element at the specified index.
        /// </returns>
        /// <param name="index">
        /// The zero-based index of the element to get or set.
        /// </param>
        public T this[int index]
        {
            get { return InnerList[GetReverseIndex(index)]; }
            set { InnerList[GetReverseIndex(index)] = value; }
        }

        private int GetReverseIndex(int index)
        {
            return InnerList.Count - index - 1;
        }

        private int GetLastIndexOf(T item)
        {
            for (int i = InnerList.Count; i >= 0; i--)
            {
                if (Equals(InnerList[i], item))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
