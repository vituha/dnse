namespace VS.Library.Collections.Views
{
    using System;
    using System.Collections.Generic;

    public abstract class ReadOnlyListBase<T> : IList<T>
    {
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
            return GetIndex(item);
        }

        /// <summary>
        /// This method always throws <see cref="InvalidOperationException"/>.
        /// </summary>
        public void Insert(int index, T item)
        {
            throw CollectionExceptions.ReadOnly();
        }

        /// <summary>
        /// This method always throws <see cref="InvalidOperationException"/>.
        /// </summary>
        public void RemoveAt(int index)
        {
            throw CollectionExceptions.ReadOnly();
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// The setter always throws <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <returns>
        /// The element at the specified index.
        /// </returns>
        /// <param name="index">
        /// The zero-based index of the element to get or set.
        /// </param>
        public T this[int index]
        {
            get
            {
                return GetAt(index);
            }
            set
            {
                throw CollectionExceptions.ReadOnly();
            }
        }

        /// <summary>
        /// This method always throws <see cref="InvalidOperationException"/>.
        /// </summary>
        public void Add(T item)
        {
            throw CollectionExceptions.ReadOnly();
        }

        /// <summary>
        /// This method always throws <see cref="InvalidOperationException"/>.
        /// </summary>
        public void Clear()
        {
            throw CollectionExceptions.ReadOnly();
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
            return IndexOf(item) >= 0;
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
            CopyToArray(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public int Count
        {
            get { return GetCount(); }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <returns>
        /// always true.
        /// </returns>
        public bool IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// This method always throws <see cref="InvalidOperationException"/>.
        /// </summary>
        public bool Remove(T item)
        {
            throw CollectionExceptions.ReadOnly();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public virtual IEnumerator<T> GetEnumerator()
        {
            return GetGenericEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}"/> that can be used to iterate through the collection.
        /// </returns>
        protected abstract IEnumerator<T> GetGenericEnumerator();

        /// <summary>
        /// Gets count of items in this collection.
        /// </summary>
        /// <returns>pozitive or zero count</returns>
        protected abstract int GetCount();

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <returns>The element at the specified index.</returns>
        protected abstract T GetAt(int index);

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </summary>
        /// <returns>
        /// The index of <paramref name="item"/> if found in the list; otherwise, -1.
        /// </returns>
        /// <param name="item">
        /// The object to locate in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </param>
        protected abstract int GetIndex(T item);

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index in <paramref name="array"/> at which copying begins.
        /// </param>
        protected abstract void CopyToArray(T[] array, int arrayIndex);
    }
}