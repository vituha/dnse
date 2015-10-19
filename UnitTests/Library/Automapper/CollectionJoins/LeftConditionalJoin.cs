namespace Ipreo.Vision.Utils.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class LeftConditionalJoin<TOuter, TInner> : IConditionalJoin<TOuter, TInner>
    {
        private readonly IEnumerable<TInner> m_inner;
        private readonly IEnumerable<TOuter> m_outer;

        public LeftConditionalJoin(IEnumerable<TOuter> outer, IEnumerable<TInner> inner)
        {
            if (outer == null)
            {
                throw new ArgumentNullException("outer");
            }
            if (inner == null)
            {
                throw new ArgumentNullException("inner");
            }

            m_outer = outer;
            m_inner = inner;
        }

        public IEnumerable<JoinMatch<TOuter, TInner>> On<T>(Func<TOuter, T> outerKeySelector, Func<TInner, T> innerKeySelector, IEqualityComparer<T> keyComparer = null)
        {
            if (outerKeySelector == null)
            {
                throw new ArgumentNullException("outerKeySelector");
            }
            if (innerKeySelector == null)
            {
                throw new ArgumentNullException("innerKeySelector");
            }
            if (keyComparer == null)
            {
                keyComparer = EqualityComparer<T>.Default;
            }

            ILookup<T, TInner> innerLookup = m_inner.ToLookup(innerKeySelector, keyComparer);

            foreach (TOuter outerItem in m_outer)
            {
                T key = outerKeySelector(outerItem);
                IEnumerable<TInner> innerMatches = innerLookup[key];

                bool innerIsEmpty = true;
                using (IEnumerator<TInner> innerEnumerator = innerMatches.GetEnumerator())
                {
                    if (innerEnumerator.MoveNext())
                    {
                        innerIsEmpty = false;
                        do
                        {
                            yield return new JoinMatch<TOuter, TInner>(outerItem, innerEnumerator.Current);
                        }
                        while (innerEnumerator.MoveNext());
                    }
                }

                if (innerIsEmpty)
                {
                    yield return new JoinMatch<TOuter, TInner>(outerItem, default(TInner));
                }
            }
        }
    }
}