using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using VS.Library.Common;
using System.Diagnostics;
using VS.Library.Validation;
using Wintellect.PowerCollections;

namespace VS.Library.Collections
{
	/// <summary>
	/// Provides extensions to ICollection and ICollection&gt;T&lt; that are not available as standard.
	/// When moving to .Net Framework 3.5 or above, consider convert everything here into C# 3.0 extension methods
	/// </summary>
	public static class CollectionExtensions {

		#region IsXXXX

		/// <summary>
		/// Checks whether collection has items
		/// </summary>
		/// <param name="collection">Collection to check</param>
		/// <returns><value>true</value> if collection has no items, <value>false</value> otherwise</returns>
		public static bool IsEmpty(this ICollection collection) {
			ValidationExtensions.EnsureNotNull(collection, "collection");
			return collection.Count <= 0;
		}

		/// <summary>
		/// Checks whether collection has items
		/// </summary>
		/// <param name="collection">Collection to check</param>
		/// <returns><value>true</value> if collection has no items, <value>false</value> otherwise</returns>
		public static bool IsEmpty<T>(this ICollection<T> collection) {
			ValidationExtensions.EnsureNotNull(collection, "collection");
			return collection.Count <= 0;
		}

		/// <summary>
		/// Checks whether collection is null or has no items
		/// </summary>
		/// <param name="collection">Collection to check</param>
		/// <returns><value>true</value> if collection is null or has no items, <value>false</value> otherwise</returns>
		public static bool IsNullOrEmpty(this ICollection collection) {
			return collection == null || collection.Count <= 0;
		}

		/// <summary>
		/// Checks whether collection is null or has no items
		/// </summary>
		/// <param name="collection">Collection to check</param>
		/// <returns><value>true</value> if collection is null or has no items, <value>false</value> otherwise</returns>
		public static bool IsNullOrEmpty<T>(this ICollection<T> collection) {
			return collection == null || collection.Count <= 0;
		}

		#endregion

		#region AddRange

        internal static int AddRangeInternal<T>(this ICollection<T> collection, IEnumerable<T> sourceCollection)
        {
            Debug.Assert(collection != null);
            Debug.Assert(sourceCollection != null);

            int countBefore = collection.Count;
            foreach (T item in sourceCollection)
            {
                collection.Add(item);
            }
            return collection.Count - countBefore;
        }

		/// <summary>
		/// Adds items to the <see cref="collection"/> one-by-one
		/// </summary>
		/// <param name="sourceCollection">Source collection</param>
		/// <param name="collection">Receiving collection</param>
		/// <returns>Number of items added</returns>
		public static int AddRange<T>(this ICollection<T> collection, IEnumerable<T> sourceCollection) {
			ValidationExtensions.EnsureNotNull(collection, "collection");
			ValidationExtensions.EnsureNotNull(sourceCollection, "sourceCollection");

			return AddRangeInternal(collection, sourceCollection);
		}

		#endregion

		#region Convert

		internal static ICollection<TOutput> ConvertInternal<TInput, TOutput>(this ICollection<TInput> collection, Converter<TInput, TOutput> converter) {
			Debug.Assert(collection != null);
			Debug.Assert(converter != null);

			var convertedCollection = Algorithms.Convert(collection, converter);
			return new Bag<TOutput>(convertedCollection);
		}

		/// <summary>
		/// Walks through the collection applying converter to each item. Returns converter results collection
		/// </summary>
		/// <typeparam name="TInput">Type of items in collection</typeparam>
		/// <typeparam name="TOutput">Type of items in result collection</typeparam>
		/// <param name="collection">Collection to walk through</param>
		/// <param name="converter">Item converter to apply</param>
		/// <returns>Collection of conversion results</returns>
		public static ICollection<TOutput> Convert<TInput, TOutput>(this ICollection<TInput> collection, Converter<TInput, TOutput> converter) {
			ValidationExtensions.EnsureNotNull(collection, "collection");
			ValidationExtensions.EnsureNotNull(converter, "converter");

			return ConvertInternal(collection, converter);
		}

        public static IList<T> ToList<T>(this ICollection<T> collection) { 
            return CollectionFactory.CreateList(collection);
        }

		#endregion

    }
}
