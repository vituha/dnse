
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VS.Library.Booleans;
using System.Collections;
using VS.Library.Collections;

namespace VS.Library.Common {
    /// <summary>
    /// Provides some really basic argument validation methods
    /// </summary>
    public static class Validator {
        /// <summary>
        /// Ensures that given object is not null reference
        /// </summary>
        /// <param name="obj">Argument value to check</param>
        /// <param name="objName">Argument name to pass in exception</param>
        /// <exception cref="ArgumentNullException">obj is null reference</exception>
        public static void NotNull(object obj, string objName) {
            var condition = CommonPredicates<object>.IsNotNull;
            if (condition(obj)) {
                throw new ArgumentNullException(objName);
            }
        }

        /// <summary>
        /// Ensures that given collection argument has elements
        /// </summary>
        /// <param name="collection">Collection argument to check</param>
        /// <param name="collectionName">Name of collection argument to pass in exception</param>
        public static void NotEmpty(IEnumerable collection, string collectionName) {
            if (!CollectionExtensions.IsNotEmpty(collection)) {
                ThrowCollectionEmpty(collectionName);
            }
        }

        /// <summary>
        /// Ensures that given collection argument has elements
        /// </summary>
        /// <param name="collection">Collection argument to check</param>
        /// <param name="collectionName">Name of collection argument to pass in exception</param>
        public static void NotEmpty(ICollection collection, string collectionName) {
            if (!CollectionExtensions.IsNotEmpty(collection)) {
                ThrowCollectionEmpty(collectionName);
            }
        }

        /// <summary>
        /// Ensures that given collection argument has elements
        /// </summary>
        /// <param name="collection">Collection argument to check</param>
        /// <param name="collectionName">Name of collection argument to pass in exception</param>
        public static void NotEmpty<T>(ICollection<T> collection, string collectionName) {
            if (!CollectionExtensions.IsNotEmpty(collection)) {
                ThrowCollectionEmpty(collectionName);
            }
        }

        /// <summary>
        /// Throws "Collection is empty" exception
        /// </summary>
        /// <param name="collectionName">Name of collection argument</param>
        /// <exception cref="ArgumentException">Always</exception>
        private static void ThrowCollectionEmpty(string collectionName) {
            throw new ArgumentException("Collection is empty: " + collectionName);
        }

    }
}
