namespace VS.Library.Collections.Views
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Implements a read-only wrapper class that converts collection elemtns on the fly.
    /// </summary>
    /// <typeparam name="TInput">The type of the input elements.</typeparam>
    /// <typeparam name="TOutput">The type of the output elements.</typeparam>
    public abstract class CollectionAdapterBase<TInput, TOutput> : ICollection<TOutput>
    {
        protected ICollection<TInput> InnerCollection { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionAdapterBase&lt;TInput, TOutput&gt;"/> class.
        /// </summary>
        /// <param name="source">Source collection.</param>
        protected CollectionAdapterBase(ICollection<TInput> source)
        {
            InnerCollection = source;
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">
        /// The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        public void Add(TOutput item)
        {
            var sourceItem = AdaptBack(item);
            InnerCollection.Add(sourceItem);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        public void Clear()
        {
            InnerCollection.Clear();
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
        public bool Contains(TOutput item)
        {
            TInput sourceItem = AdaptBack(item);
            return InnerCollection.Contains(sourceItem);
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
        public void CopyTo(TOutput[] array, int arrayIndex)
        {
            foreach (TInput sourceItem in InnerCollection)
            {
                TOutput item = Adapt(sourceItem);
                array[arrayIndex++] = item;
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
            get { return InnerCollection.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly
        {
            get { return InnerCollection.IsReadOnly; }
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
        public bool Remove(TOutput item)
        {
            TInput sourceItem = AdaptBack(item);
            return InnerCollection.Remove(sourceItem);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<TOutput> GetEnumerator()
        {
            return InnerCollection.Select(Adapt).GetEnumerator();
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
        /// Converts output element back to an input element.
        /// </summary>
        /// <param name="item">The output element.</param>
        /// <returns>The input element.</returns>
        /// <remarks>
        /// Override this method to get a read-write collection adapter.
        /// </remarks>
        protected virtual TInput AdaptBack(TOutput item)
        {
            throw CollectionExceptions.ReadOnly();
        }

        /// <summary>
        /// Converts input element to an outnput element.
        /// </summary>
        /// <param name="item">The input element.</param>
        /// <returns>The output element.</returns>
        protected abstract TOutput Adapt(TInput item);
    }
}
