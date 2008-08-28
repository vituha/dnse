using System;
using System.Collections.Generic;
using System.Text;
using Wintellect.PowerCollections;
using VS.Library.Validation;
using System.Diagnostics;

namespace VS.Library.Collections
{
    /// <summary>
    /// Provides services for efficient creation of various generic collections
    /// </summary>
    public static class CollectionFactory
    {
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
        public static IList<T> CreateList<T>(int capacity)
        {
            if (capacity >= BigListCapacityThreshold)
            {
                return new BigList<T>();
            }
            else
            {
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
        public static IList<T> CreateList<T>(IEnumerable<T> collection, int capacity)
        {
            collection.EnsureNotNull("collection");

            if (capacity >= BigListCapacityThreshold)
            {
                return new BigList<T>(collection);
            }
            else
            {
                var list = new List<T>(capacity);
                list.AddRange(collection);
                return list;
            }
        }

        /// <summary>
        /// Creates a new list efficiently, considering collection's capacity. 
        /// Also shallow copies elements from given collection into the list.
        /// </summary>
        /// <typeparam name="T">Type of elements</typeparam>
        /// <param name="collection">Source collection to copy items from</param>
        /// <returns>Created list</returns>
        public static IList<T> CreateList<T>(ICollection<T> collection)
        {
            return CreateList(collection, collection.Count);
        }

        #endregion

        #region Collections

        /// <summary>
        /// Creates collection that is generally efficient for the given capacity
        /// </summary>
        /// <typeparam name="T">Type of collection items</typeparam>
        /// <param name="capacity">Average expected capacity</param>
        /// <returns>New collection instance</returns>
        public static ICollection<T> CreateCollection<T>(int capacity) {
            if (capacity >= BigListCapacityThreshold)
            {
                return new Bag<T>();
            }
            else
            {
                return new List<T>(capacity);
            }
        }

        /// <summary>
        /// Creates collection that is generally efficient for the given capacity.
        /// Also copies elements from given collection
        /// </summary>
        /// <typeparam name="T">Type of collection items</typeparam>
        /// <param name="collection">Collection to copy items from</param>
        /// <param name="capacity">Average expected capacity</param>
        /// <returns>New collection instance</returns>
        public static ICollection<T> CreateCollection<T>(IEnumerable<T> collection, int capacity)
        {
            collection.EnsureNotNull("collection");

            if (capacity >= BigListCapacityThreshold)
            {
                var result = new Bag<T>();
                result.AddMany(collection);
                return result;
            }
            else
            {
                var result = new List<T>(capacity);
                result.AddRange(collection);
                return result;
            }
        }

        /// <summary>
        /// Creates collection that is generally efficient for the given collection capacity.
        /// Also copies elements from given collection
        /// </summary>
        /// <typeparam name="T">Type of collection items</typeparam>
        /// <param name="collection">Collection to copy items from</param>
        /// <returns>New collection instance</returns>
        public static ICollection<T> CreateCollection<T>(ICollection<T> collection)
        {
            return CreateCollection(collection, collection.Count);
        }

        #endregion
    }
}
