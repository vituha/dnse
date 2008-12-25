namespace VS.Library.Collections.Adapters
{
    using System;
    using System.Collections.Generic;
    using System.Collections;

    using VS.Library.Collections.Adapters;
    using VS.Library.Validation;

    public static class AdapterExtensions
    {
        #region List Adapters

        public static IList<TOutput> Adapt<TInput, TOutput>(this IList<TInput> collection)
    where TInput : TOutput
        {
            ValidateAdaptArguments(collection);
            return CreateAdapter<TInput, TOutput>(collection, collection.IsReadOnly);
        }

        public static IList<TOutput> Adapt<TInput, TOutput>(this IList<TInput> collection, bool readOnly)
    where TInput : TOutput
        {
            ValidateAdaptArguments(collection, readOnly);
            return CreateAdapter<TInput, TOutput>(collection, readOnly);
        }

        public static IList<TOutput> Adapt<TOutput>(this IList collection)
        {
            ValidateAdaptArguments(collection);
            return CreateUntypedAdapter<TOutput>(collection, collection.IsReadOnly);
        }

        public static IList<TOutput> Adapt<TOutput>(this IList collection, bool readOnly)
        {
            ValidateAdaptArguments(collection, readOnly);
            return CreateUntypedAdapter<TOutput>(collection, readOnly);
        }

        public static IList AdaptUntyped<TInput>(this IList<TInput> collection)
        {
            ValidateAdaptArguments(collection);
            return CreateTypedAdapter(collection, collection.IsReadOnly);
        }

        public static IList AdaptUntyped<TInput>(this IList<TInput> collection, bool readOnly)
        {
            ValidateAdaptArguments(collection, readOnly);
            return CreateTypedAdapter(collection, readOnly);
        }

        #region Private Methods

        private static IList<TOutput> CreateAdapter<TInput, TOutput>(IList<TInput> collection, bool readOnly)
            where TInput : TOutput
        {
            if (readOnly)
            {
                return new ReadOnlyListAdapter<TInput, TOutput>(collection);
            }
            return new ListAdapter<TInput, TOutput>(collection);
        }

        private static IList<TOutput> CreateUntypedAdapter<TOutput>(IList collection, bool readOnly)
        {
            if (readOnly)
            {
                return new ReadOnlyUntypedListAdapter<TOutput>(collection);
            }
            return new UntypedListAdapter<TOutput>(collection);
        }

        private static IList CreateTypedAdapter<TInput>(IList<TInput> collection, bool readOnly)
        {
            if (readOnly)
            {
                return new ReadOnlyTypedListAdapter<TInput>(collection);
            }

            return new TypedListAdapter<TInput>(collection);
        }

        #endregion Private Methods

        #endregion List Adapters


        #region Collection Adapters

        public static ICollection<TOutput> Adapt<TInput, TOutput>(this ICollection<TInput> collection)
    where TInput : TOutput
        {
            ValidateAdaptArguments(collection);
            return CreateAdapter<TInput, TOutput>(collection, collection.IsReadOnly);
        }

        public static ICollection<TOutput> Adapt<TInput, TOutput>(this ICollection<TInput> collection, bool readOnly)
    where TInput : TOutput
        {
            ValidateAdaptArguments(collection, readOnly);
            return CreateAdapter<TInput, TOutput>(collection, readOnly);
        }

        public static ICollection<TOutput> Adapt<TOutput>(this ICollection collection)
        {
            ValidateAdaptArguments(collection);
            return CreateUntypedAdapter<TOutput>(collection);
        }

        public static ICollection AdaptUntyped<TInput>(this ICollection<TInput> collection)
        {
            ValidateAdaptArguments(collection);
            return CreateTypedAdapter(collection);
        }

        #region Private Methods

        private static ICollection<TOutput> CreateAdapter<TInput, TOutput>(ICollection<TInput> collection, bool readOnly)
            where TInput : TOutput
        {
            if (readOnly)
            {
                return new ReadOnlyCollectionAdapter<TInput, TOutput>(collection);
            }
            return new CollectionAdapter<TInput, TOutput>(collection);
        }

        private static ICollection<TOutput> CreateUntypedAdapter<TOutput>(ICollection collection)
        {
            return new UntypedCollectionAdapter<TOutput>(collection);
        }

        private static ICollection CreateTypedAdapter<TInput>(ICollection<TInput> collection)
        {
            return new TypedCollectionAdapter<TInput>(collection);
        }

        #endregion Private Methods

        #endregion Collection Adapters


        #region Enumerable Adapters

        public static ICollection<T> Adapt<T>(this IEnumerable<T> collection)
        {
            ICollection<T> result = AdaptInternal(collection);
            if (result != null)
            {
                return result;
            }

            return new CountingEnumerableCollectionAdapter<T>(collection);
        }

        public static ICollection AdaptUntyped<T>(this IEnumerable<T> collection)
        {
            ICollection result = AdaptUntypedInternal(collection);
            if (result != null)
            {
                return result;
            }

            return new CountingEnumerableCollectionAdapter<T>(collection);
        }

        public static ICollection<T> Adapt<T>(this IEnumerable<T> collection, int count) {
            ICollection<T> result = AdaptInternal(collection);
            if (result != null)
            {
                return result;
            }

            return new EnumerableCollectionAdapter<T>(collection, count);
        }

        public static ICollection AdaptUntyped<T>(this IEnumerable<T> collection, int count)
        {
            ICollection result = AdaptUntypedInternal(collection);
            if (result != null)
            {
                return result;
            }

            return new EnumerableCollectionAdapter<T>(collection, count);
        }

        #region Private Methods

        private static ICollection<T> AdaptInternal<T>(IEnumerable<T> collection)
        {
            ICollection<T> typedCollection = collection as ICollection<T>;
            if (typedCollection != null)
            {
                return typedCollection;
            }

            ICollection untypedCollection = collection as ICollection;
            if (untypedCollection != null)
            {
                new EnumerableCollectionAdapter<T>(collection, untypedCollection.Count);
            }

            return null;
        }

        private static ICollection AdaptUntypedInternal<T>(IEnumerable<T> collection)
        {
            ICollection untypedCollection = collection as ICollection;
            if (untypedCollection != null)
            {
                return untypedCollection;
            }

            ICollection<T> typedCollection = collection as ICollection<T>;
            if (typedCollection != null)
            {
                new EnumerableCollectionAdapter<T>(collection, typedCollection.Count);
            }

            return null;
        }

        #endregion Private Methods

        #endregion Enumerable Adapters


        #region Validation Helpers

        private static void ValidateAdaptArguments<TInput>(ICollection<TInput> collection)
        {
            collection.RequireArgumentNotNull("collection");
        }

        private static void ValidateAdaptArguments<TInput>(ICollection<TInput> collection, bool readOnly)
        {
            collection.Validate("collection").Require(c => readOnly || !collection.IsReadOnly);
        }

        private static void ValidateAdaptArguments(ICollection collection)
        {
            collection.RequireArgumentNotNull("collection");
        }

        private static void ValidateAdaptArguments(IList collection, bool readOnly)
        {
            collection.Validate("collection").Require(c => readOnly || !collection.IsReadOnly);
        }

        #endregion
    }
}
