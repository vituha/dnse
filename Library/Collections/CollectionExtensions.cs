using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using VS.Library.Common;

namespace VS.Library.Collections
{
    public class CollectionExtensions
    {
        /// <summary>
        /// Adds items to the <see cref="targetCollection"/> one-by-one
        /// </summary>
        /// <typeparam name="T">Type of collection items</typeparam>
        /// <param name="targetCollection">Receiving collection</param>
        /// <param name="collection">Source collection</param>
        public static void AddRange<T>(ICollection<T> targetCollection, IEnumerable<T> collection)
        {
            if (targetCollection == null)
            {
                throw new ArgumentNullException("targetCollection");
            }
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            foreach (var item in collection)
            {
                targetCollection.Add(item);
            }
        }

        public static bool IsNotEmpty(IEnumerable collection) {
            Validator.NotNull(collection, "collection");
            return collection.GetEnumerator().MoveNext();
        }

        public static bool IsNotEmpty(ICollection collection) {
            Validator.NotNull(collection, "collection");
            return collection.Count > 0;
        }

        public static bool IsNotEmpty<T>(ICollection<T> collection) {
            Validator.NotNull(collection, "collection");
            return collection.Count > 0;
        }

    }
}
