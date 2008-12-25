using System;
using Wintellect.PowerCollections;
using System.Collections;
using System.Collections.Generic;

namespace VS.Library.Collections.Adapters
{
    public class UntypedCollectionAdapter<TOutput> : ReadOnlyCollectionBase<TOutput>
    {
        public ICollection Adaptee { get; private set; }

        public UntypedCollectionAdapter(ICollection adaptee)
        {
            Adaptee = adaptee;
        }

        public override int Count
        {
            get { return Adaptee.Count; }
        }

        public override IEnumerator<TOutput> GetEnumerator()
        {
            foreach (TOutput item in Adaptee)
            {
                yield return item;
            }
        }
    }
}
