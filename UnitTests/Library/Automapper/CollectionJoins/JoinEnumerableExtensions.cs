using System;
using System.Collections.Generic;
using System.Linq;

namespace Ipreo.Vision.Utils.Collections
{
    public static class JoinEnumerableExtensions
    {

        /// <summary>
        /// Creates cross join (cartesian product) of two sequences.
        /// NOTE: For performance reasons, it is recommended to have OUTER sequence bigger than inner.
        /// </summary>
        /// <typeparam name="TOuter">Type of outer sequence elements.</typeparam>
        /// <typeparam name="TInner">Type of inner sequence elements.</typeparam>
        /// <param name="outer">Outer sequence.</param>
        /// <param name="inner">Inner sequence.</param>
        /// <returns>Cartesian product of two sequences.</returns>
        public static IEnumerable<JoinMatch<TOuter, TInner>> CrossJoin<TOuter, TInner>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner)
        {
            if (outer == null)
            {
                throw new ArgumentNullException("outer");
            }
            if (inner == null)
            {
                throw new ArgumentNullException("inner");
            }

            TInner[] innerArray = inner.ToArray();
            foreach (TOuter outerItem in outer)
            {
                foreach (TInner innerItem in innerArray)
                {
                    yield return new JoinMatch<TOuter, TInner>(outerItem, innerItem);
                }
            }
        }

        /// <summary>
        /// Creates inner join (only matching elements) of two sequences. Use methods of <see cref="IConditionalJoin{TOuter,TInner}"/> to specify join conditions.
        /// NOTE: For performance reasons, it is recommended to have OUTER sequence bigger than inner.
        /// </summary>
        /// <typeparam name="TOuter">Type of outer sequence elements.</typeparam>
        /// <typeparam name="TInner">Type of inner sequence elements.</typeparam>
        /// <param name="outer">Outer sequence.</param>
        /// <param name="inner">Inner sequence.</param>
        /// <returns>An object that can be used to perform actual join based on specific conditions.</returns>
        public static IConditionalJoin<TOuter, TInner> InnerJoin<TOuter, TInner>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner)
        {
            return new InnerConditionalJoin<TOuter, TInner>(outer, inner);
        }

        /// <summary>
        /// Creates left outer join (outer - all elements, inner - matching element or null) of two sequences. Use methods of <see cref="IConditionalJoin{TOuter,TInner}"/> to specify join conditions.
        /// NOTE: For performance reasons, it is recommended to have OUTER sequence bigger than inner.
        /// </summary>
        /// <typeparam name="TOuter">Type of outer sequence elements.</typeparam>
        /// <typeparam name="TInner">Type of inner sequence elements.</typeparam>
        /// <param name="outer">Outer sequence.</param>
        /// <param name="inner">Inner sequence.</param>
        /// <returns>An object that can be used to perform actual join based on specific conditions.</returns>
        public static IConditionalJoin<TOuter, TInner> LeftJoin<TOuter, TInner>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner)
        {
            return new LeftConditionalJoin<TOuter, TInner>(outer, inner);
        }

        /// <summary>
        /// Creates right join (outer - matching element or null, inner - all) of two sequences. Use methods of <see cref="IConditionalJoin{TOuter,TInner}"/> to specify join conditions.
        /// NOTE: For performance reasons, it is recommended to have INNER sequence bigger than outer.
        /// </summary>
        /// <typeparam name="TOuter">Type of outer sequence elements.</typeparam>
        /// <typeparam name="TInner">Type of inner sequence elements.</typeparam>
        /// <param name="outer">Outer sequence.</param>
        /// <param name="inner">Inner sequence.</param>
        /// <returns>An object that can be used to perform actual join based on specific conditions.</returns>
        public static IConditionalJoin<TOuter, TInner> RightJoin<TOuter, TInner>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner)
        {
            return new RightConditionalJoin<TOuter, TInner>(outer, inner);
        }

        /// <summary>
        /// Creates full join (outer - matching element or null, inner - matching element or null) of two sequences. Use methods of <see cref="IConditionalJoin{TOuter,TInner}"/> to specify join conditions.
        /// NOTE: For performance reasons, it is recommended to have OUTER sequence bigger than inner.
        /// </summary>
        /// <typeparam name="TOuter">Type of outer sequence elements.</typeparam>
        /// <typeparam name="TInner">Type of inner sequence elements.</typeparam>
        /// <param name="outer">Outer sequence.</param>
        /// <param name="inner">Inner sequence.</param>
        /// <returns>An object that can be used to perform actual join based on specific conditions.</returns>
        public static IConditionalJoin<TOuter, TInner> FullJoin<TOuter, TInner>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner)
        {
            return new FullConditionalJoin<TOuter, TInner>(outer, inner);
        }
    }
}
