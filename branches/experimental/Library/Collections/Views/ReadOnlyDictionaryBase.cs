namespace VS.Library.Collections.Views
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides base class for custom read-only dictionary implementation.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public abstract class ReadOnlyDictionaryBase<TKey, TValue> : IDictionary<TKey, TValue>
    {
        /// <summary>
        /// This method always throws <see cref="InvalidOperationException"/>.
        /// </summary>
        public void Add(TKey key, TValue value)
        {
            throw CollectionExceptions.ReadOnly();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the specified key.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the key; otherwise, false.
        /// </returns>
        /// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.
        ///                 </exception>
        public bool ContainsKey(TKey key)
        {
            TValue value;
            return LookUp(key, out value);
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the keys of the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        public ICollection<TKey> Keys
        {
            get { return GetKeys(); }
        }

        /// <summary>
        /// This method always throws <see cref="InvalidOperationException"/>.
        /// </summary>
        public bool Remove(TKey key)
        {
            throw CollectionExceptions.ReadOnly();
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <returns>
        /// true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the specified key; otherwise, false.
        /// </returns>
        /// <param name="key">The key whose value to get.
        ///                 </param><param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value"/> parameter. This parameter is passed uninitialized.
        ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.
        ///                 </exception>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return LookUp(key, out value);
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the values in the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        public ICollection<TValue> Values
        {
            get { return GetValues(); }
        }

        /// <summary>
        /// Gets the element with the specified key.
        /// Setter always throws <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <returns>
        /// The element with the specified key.
        /// </returns>
        /// <param name="key">The key of the element to get or set.</param>
        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                LookUp(key, out value);
                return value;
            }
            set
            {
                throw CollectionExceptions.ReadOnly();
            }
        }

        /// <summary>
        /// This method always throws <see cref="InvalidOperationException"/>.
        /// </summary>
        public void Add(KeyValuePair<TKey, TValue> item)
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
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        ///                 </param>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            TValue value;
            return LookUp(item.Key, out value) && Equals(value, item.Value);
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. 
        /// The <see cref="T:System.Array"/> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index in <paramref name="array"/> at which copying begins.
        /// </param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                array[arrayIndex++] = pair;
            }
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
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw CollectionExceptions.ReadOnly();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
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
        /// Gets key collection.
        /// </summary>
        /// <returns>a read-only key collection.</returns>
        protected virtual ICollection<TKey> GetKeys()
        {
            return new DelegateCollectionAdapter<KeyValuePair<TKey, TValue>, TKey>(this, pair => pair.Key);
        }

        /// <summary>
        /// Gets value collection.
        /// </summary>
        /// <returns>a read-only value collection.</returns>
        protected virtual ICollection<TValue> GetValues()
        {
            return new DelegateCollectionAdapter<KeyValuePair<TKey, TValue>, TValue>(this, pair => pair.Value);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}"/> that can be used to iterate through the collection.
        /// </returns>
        protected abstract IEnumerator<KeyValuePair<TKey, TValue>> GetGenericEnumerator();

        /// <summary>
        /// Gets count of items in this collection.
        /// </summary>
        /// <returns>pozitive or zero count</returns>
        protected abstract int GetCount();

        /// <summary>
        /// Gets value by its key.
        /// </summary>
        /// <param name="key">key to get value for</param>
        /// <param name="value">value associated with given key (if any) or <c>default(TValue)</c></param>
        /// <returns><c>true</c> if value was found, <c>false</c> otherwise</returns>
        protected abstract bool LookUp(TKey key, out TValue value);
    }
}
