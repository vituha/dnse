namespace Ipreo.Vision.Utils.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class RightConditionalJoin<TOuter, TInner> : IConditionalJoin<TOuter, TInner>
    {
        private readonly IEnumerable<TInner> m_inner;
        private readonly IEnumerable<TOuter> m_outer;

        public RightConditionalJoin(IEnumerable<TOuter> outer, IEnumerable<TInner> inner)
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

            ILookup<T, TOuter> outerLookup = m_outer.ToLookup(outerKeySelector, keyComparer);

            foreach (TInner innerItem in m_inner)
            {
                T key = innerKeySelector(innerItem);
                IEnumerable<TOuter> outerMatches = outerLookup[key];

                bool outerIsEmpty = true;
                using (IEnumerator<TOuter> outerEnumerator = outerMatches.GetEnumerator())
                {
                    if (outerEnumerator.MoveNext())
                    {
                        outerIsEmpty = false;
                        do
                        {
                            yield return new JoinMatch<TOuter, TInner>(outerEnumerator.Current, innerItem);
                        }
                        while (outerEnumerator.MoveNext());
                    }
                }

                if (outerIsEmpty)
                {
                    yield return new JoinMatch<TOuter, TInner>(default(TOuter), innerItem);
                }
            }
        }
    }
}