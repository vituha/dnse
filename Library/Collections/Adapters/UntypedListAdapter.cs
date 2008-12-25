using System;
using Wintellect.PowerCollections;
using System.Collections;

namespace VS.Library.Collections.Adapters
{
    public class ReadOnlyUntypedListAdapter<TOutput> : ReadOnlyListBase<TOutput>
    {
        public IList Adaptee { get; private set; }

        public ReadOnlyUntypedListAdapter(IList adaptee)
        {
            Adaptee = adaptee;
        }

        public override int Count
        {
            get { return Adaptee.Count; }
        }

        public override TOutput this[int index]
        {
            get
            {
                return (TOutput)Adaptee[index];
            }
        }
    }

    public class UntypedListAdapter<TOutput> : ListBase<TOutput>
    {
        public IList Adaptee { get; private set; }

        public UntypedListAdapter(IList adaptee)
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

        public override void Insert(int index, TOutput item)
        {
            Adaptee.Insert(index, item);
        }

        public override void RemoveAt(int index)
        {
            Adaptee.RemoveAt(index);
        }

        public override TOutput this[int index]
        {
            get
            {
                return (TOutput)Adaptee[index];
            }
            set
            {
                    Adaptee[index] = value;
            }
        }
    }
}
