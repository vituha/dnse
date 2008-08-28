using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VS.Library.Common;
using Wintellect.PowerCollections;
using System.Collections;
using VS.Library.Validation;

namespace VS.Library.Collections
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Checks whether collection has items
        /// </summary>
        /// <param name="collection">Collection to check</param>
        /// <returns><value>true</value> if collection has items, <value>false</value> otherwise</returns>
        public static bool HasItems(this IEnumerable collection)
        {
            ValidationExtensions.EnsureNotNull(collection, "collection");
            return collection.GetEnumerator().MoveNext();
        }

        /// <summary>
        /// Invokes a method on every item in collection
        /// </summary>
        /// <typeparam name="T">Type of collection items</typeparam>
        /// <param name="collection">collection to look in for items</param>
        /// <param name="method">Method to invoke on an item</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Proc1<T> method)
        {
            collection.EnsureNotNull("collection");
            method.EnsureNotNull("method");

            foreach (var item in collection)
            {
                method(item);
            }
        }

        /// <summary>
        /// Invokes a delegate on every item in collection. 
        /// Passes ordinal number of every item to the delegate as additional parameter.
        /// </summary>
        /// <typeparam name="T">Type of collection items</typeparam>
        /// <param name="collection">collection to look in for items</param>
        /// <param name="method">Method to invoke on an item</param>
        /// <returns>Total number of items in collection</returns>
        public static int ForEachCounted<T>(this IEnumerable<T> collection, Proc2<T, int> method)
        {
            collection.EnsureNotNull("collection");
            method.EnsureNotNull("method");

            int count = 0;
            foreach (var item in collection)
            {
                method(item, count);
                count++;
            }
            return count;
        }

        /// <summary>
        /// Converts all items in collection using given item converter
        /// </summary>
        /// <typeparam name="T">Type of collection items</typeparam>
        /// <param name="collection">collection to look in for items</param>
        /// <param name="converter">Converter delegate to apply on an item</param>
        /// <returns>Collection of converted items</returns>
        public static IEnumerable<TDst> Convert<TSrc, TDst>(this IEnumerable<TSrc> collection, Func1<TDst, TSrc> converter)
        {
            collection.EnsureNotNull("collection");
            converter.EnsureNotNull("converter");

            foreach (var item in collection)
            {
                yield return converter(item);
            }
        }

        /// <summary>
        /// Converts all items in collection using given item converter delegate.
        /// Passes ordinal number of every item to the delegate as additional parameter.
        /// </summary>
        /// <typeparam name="T">Type of collection items</typeparam>
        /// <param name="collection">collection to look in for items</param>
        /// <param name="converter">Converter delegate to apply on an item</param>
        /// <returns>Collection of converted items</returns>
        public static IEnumerable<TDst> ConvertCounted<TSrc, TDst>(this IEnumerable<TSrc> collection, Func2<TDst, TSrc, int> converter)
        {
            collection.EnsureNotNull("collection");
            converter.EnsureNotNull("converter");

            int count = 0;
            foreach (var item in collection)
            {
                yield return converter(item, count);
                count++;
            }
        }

        /// <summary>
        /// Creates a collection using given factory method and shallow copies all elements from source collection to the new collection.
        /// </summary>
        /// <typeparam name="T">Type of collection items</typeparam>
        /// <param name="sourceCollection">Source collection</param>
        /// <param name="targetCollection">Collection to add items to</param>
        /// <returns>Number of items added</returns>
        /// <exception cref="ArgumentNullException"><see cref="targetCollection"/> is null</exception>
        public static int FillCollection<T>(this ICollection<T> collection, IEnumerable<T> sourceCollection)
        {
            collection.EnsureNotNull("collection");
            sourceCollection.EnsureNotNull("sourceCollection");

            int countBefore = collection.Count;
            foreach (var item in sourceCollection)
            {
                collection.Add(item);
            }
            return collection.Count - countBefore;
        }

        /// <summary>
        /// Creates a collection using default constructor and shallow copies all elements from source collection.
        /// </summary>
        /// <typeparam name="T">Type of collection items</typeparam>
        /// <param name="collection">Source collection</param>
        /// <returns>New collection</returns>
        public static ICollection<T> ToCollection<T>(this IEnumerable<T> collection, int capacity)
        {
            return CollectionFactory.CreateCollection(collection, capacity);
        }

        /// <summary>
        /// Creates a list using default constructor and shallow copies all elements from source collection.
        /// </summary>
        /// <typeparam name="T">Type of collection items</typeparam>
        /// <param name="collection">Source collection</param>
        /// <returns>New list</returns>
        public static IList<T> ToList<T>(this IEnumerable<T> collection, int capacity)
        {
            return CollectionFactory.CreateList<T>(collection, capacity);
        }

        /// <summary>
        /// Creates dictionary from given collection using given key extractor and factory methods.
        /// </summary>
        /// <typeparam name="TKey">Type of keys</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        /// <param name="collection">Source collection</param>
        /// <param name="keyExtractor">Key extraction method</param>
        /// <param name="collectionFactory">Dictionary creation method</param>
        /// <returns>Created dictionary</returns>
        /// <exception cref="ArgumentNullException"><see cref="keyExtractor"/> or <see cref="collectionFactory"/> is null</exception>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<TValue> collection, Converter<TValue, TKey> keyExtractor, Func<Dictionary<TKey, TValue>> collectionFactory)
        {
            keyExtractor.EnsureNotNull("keyExtractor");
            collectionFactory.EnsureNotNull("collectionFactory");

            var result = collectionFactory();
            foreach (var item in collection)
            {
                var key = keyExtractor(item);
                result.Add(key, item);
            }
            return result;
        }

        /// <summary>
        /// Creates dictionary from given collection using given key extractor.
        /// </summary>
        /// <typeparam name="TKey">Type of keys</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        /// <param name="collection">Source collection</param>
        /// <param name="keyExtractor">Key extraction method</param>
        /// <returns>Created dictionary</returns>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<TValue> collection, Converter<TValue, TKey> keyExtractor)
        {
            return ToDictionary(collection, keyExtractor, () => new Dictionary<TKey, TValue>());
        }

        /// <summary>
        /// Creates dictionary from given collection using and given capacity hint and key extractor.
        /// </summary>
        /// <typeparam name="TKey">Type of keys</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        /// <param name="collection">Source collection</param>
        /// <param name="capacity">Suggested capacity</param>
        /// <param name="keyExtractor">Key extraction method</param>
        /// <returns>Created dictionary</returns>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<TValue> collection, int capacity, Converter<TValue, TKey> keyExtractor)
        {
            return ToDictionary(collection, keyExtractor, () => new Dictionary<TKey, TValue>(capacity));
        }

        /// <summary>
        /// Gets element at specified position
        /// </summary>
        /// <typeparam name="T">Type of items</typeparam>
        /// <param name="collection">Collection to look into</param>
        /// <param name="index">Position to look at</param>
        /// <returns>Item at given position</returns>
        /// <exception cref="IndexOutOfRangeException">Index is out of collection bounds</exception>
        public static T GetAt<T>(this IEnumerable<T> collection, int index)
        {
            bool found = false;
            T result = default(T);
            foreach (var item in collection)
            {
                if (index <= 0)
                {
                    result = item;
                    found = true;
                    break;
                }
                index--;
            }
            if (!found)
            {
                throw new IndexOutOfRangeException("index");
            }
            return result;
        }

        /// <summary>
        /// Retrieves first item from enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns>First item of collection, <value>default(T)</value> if collection is empty</returns>
        public static T GetFirst<T>(this IEnumerable<T> collection)
        {
            T result;

            var enumerator = collection.GetEnumerator();
            try
            {
                if (enumerator.MoveNext())
                {
                    result = enumerator.Current;
                }
                else
                {
                    result = default(T);
                }
            }
            finally
            {
                enumerator.Dispose();
            }

            return result;
        }
    }
}
