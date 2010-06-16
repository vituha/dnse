using System.Collections.Generic;

namespace Immutability
{
    public class ImmutableCollection<T> : ImmutableCollectionBase<T>
    {
        private readonly ICollection<T> _innerCollection;

        public ImmutableCollection(ICollection<T> innerCollection)
            : base(innerCollection)
        {
            _innerCollection = innerCollection;
        }

        protected override ICollection<T> InnerCollection
        {
            get { return _innerCollection; }
        }
    }
}
