using System;
using System.Collections.Generic;
using System.Text;
using Wintellect.PowerCollections;

namespace VS.Library.Collections
{
    /// <summary>
    /// Provides services for efficient creation of various generic collections
    /// </summary>
    public static class CollectionFactory {
        #region Lists
        
        /// <summary>
        /// Number of items, starting from which <see cref="BigList"/> is more effective then standard <see cref="List"/>
        /// </summary>
        private const int BigListCapacityThreshold = 100;

        /// <summary>
        /// Creates a new list efficiently, considering suggested capacity
        /// </summary>
        /// <typeparam name="T">Type of elements</typeparam>
        /// <param name="capacity">Expected number of elements in the list</param>
        /// <returns>Created list</returns>
        public static IList<T> CreateList<T>(int capacity) {
            if (capacity >= BigListCapacityThreshold) {
                return new BigList<T>();
            } else {
                return new List<T>(capacity);
            }
        }

        /// <summary>
        /// Creates a new list efficiently, considering suggested capacity. 
        /// Also shallow copies elements from given collection into the list.
        /// </summary>
        /// <typeparam name="T">Type of elements</typeparam>
        /// <param name="collection">Source collection to copy items from</param>
        /// <param name="capacity">Expected number of elements in the list</param>
        /// <returns>Created list</returns>
        public static IList<T> CreateList<T>(IEnumerable<T> collection, int capacity) {
            if (collection == null) {
                throw new ArgumentNullException("collection");
            }

            if (capacity >= BigListCapacityThreshold) {
                return new BigList<T>(collection);
            } else {
                var list = new List<T>(capacity);
                list.AddRange(collection);
                return list;
            }
        }

        #endregion
    }
}
