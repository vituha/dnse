using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

namespace VS.Library.Collections.Adapters
{
    public class TypedCollectionAdapter<T> : ReadOnlyCollectionBase<T>
    {
        public ICollection<T> Adaptee { get; private set; }

        public TypedCollectionAdapter(ICollection<T> adaptee)
        {
            Adaptee = adaptee;
        }

        public override int Count
        {
            get { return Adaptee.Count; }
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return Adaptee.GetEnumerator();
        }

        public override void CopyTo(T[] array, int index)
        {
            Adaptee.CopyTo(array, index);
        }
    }
}
