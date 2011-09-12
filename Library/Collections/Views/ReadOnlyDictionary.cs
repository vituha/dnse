namespace VS.Library.Collections.Views
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Implements a read-only collection wrapper.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class ReadOnlyDictionary<TKey, TValue> : ReadOnlyDictionaryBase<TKey, TValue>
    {
        protected IDictionary<TKey, TValue> InnerDictionary { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyDictionary&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="source">Source collection.</param>
        public ReadOnlyDictionary(IDictionary<TKey, TValue> source)
        {
            if (source == null) throw new ArgumentNullException("source");
            InnerDictionary = source;
        }

        protected override int GetCount()
        {
            return InnerDictionary.Count;
        }

        protected override bool LookUp(TKey key, out TValue value)
        {
            return InnerDictionary.TryGetValue(key, out value);
        }

        protected override IEnumerator<KeyValuePair<TKey, TValue>> GetGenericEnumerator()
        {
            return InnerDictionary.GetEnumerator();
        }
    }
}
