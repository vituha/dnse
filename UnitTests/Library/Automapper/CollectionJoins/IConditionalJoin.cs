namespace Ipreo.Vision.Utils.Collections
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides fluent syntax for joins.
    /// </summary>
    /// <typeparam name="TOuter">Type of elements in outer sequence.</typeparam>
    /// <typeparam name="TInner">Type of elements in inner sequence.</typeparam>
    public interface IConditionalJoin<TOuter, TInner>
    {
        /// <summary>
        /// Joins underlined sequences on their keys using specified key selectors and key comparer.
        /// </summary>
        /// <typeparam name="T">Type of keys.</typeparam>
        /// <param name="outerKeySelector">Outer key selector.</param>
        /// <param name="innerKeySelector">Inner key selector.</param>
        /// <param name="keyComparer">Comparer for keys.</param>
        /// <returns>Sequence of outer and inner element matches.</returns>
        IEnumerable<JoinMatch<TOuter, TInner>> On<T>(Func<TOuter, T> outerKeySelector, Func<TInner, T> innerKeySelector, IEqualityComparer<T> keyComparer = null);
    }
}
