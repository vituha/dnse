using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

namespace VS.Library.Collections.Adapters
{
    public class ReadOnlyCollectionAdapter<TInput, TOutput> : ReadOnlyCollectionBase<TOutput>
        where TInput : TOutput
    {
        public ICollection<TInput> Adaptee { get; private set; }

        public ReadOnlyCollectionAdapter(ICollection<TInput> adaptee)
        {
            Adaptee = adaptee;
        }

        public override int Count
        {
            get { return Adaptee.Count; }
        }

        public override IEnumerator<TOutput> GetEnumerator()
        {
            foreach (var item in Adaptee)
            {
                yield return item;
            }
        }
    }

    public class CollectionAdapter<TInput, TOutput> : CollectionBase<TOutput>
        where TInput : TOutput
    {
        public ICollection<TInput> Adaptee { get; private set; }

        public CollectionAdapter(ICollection<TInput> adaptee)
        {
            Adaptee = adaptee;
        }

        public override void Clear()
        {
            Adaptee.Clear();
        }

        public override int Count
        {
            get { return Adaptee.Count; }
        }

        public override IEnumerator<TOutput> GetEnumerator()
        {
            foreach (var item in Adaptee)
            {
                yield return item;
            }
        }

        public override void Add(TOutput item)
        {
            Adaptee.Add((TInput)item);
        }

        public override bool Remove(TOutput item)
        {
            return Adaptee.Remove((TInput)item);
        }
    }
}
