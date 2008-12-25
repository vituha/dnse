using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

namespace VS.Library.Collections.Adapters
{
    public class ReadOnlyListAdapter<TInput, TOutput> : ReadOnlyListBase<TOutput>
        where TInput : TOutput
    {
        public IList<TInput> Adaptee { get; private set; }

        public ReadOnlyListAdapter(IList<TInput> adaptee)
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
                return Adaptee[index];
            }
        }
    }

    public class ListAdapter<TInput, TOutput> : ListBase<TOutput>
        where TInput : TOutput
    {
        public IList<TInput> Adaptee { get; private set; }

        public ListAdapter(IList<TInput> adaptee)
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
            Adaptee.Insert(index, (TInput)item);
        }

        public override void RemoveAt(int index)
        {
            Adaptee.RemoveAt(index);
        }

        public override TOutput this[int index]
        {
            get
            {
                return Adaptee[index];
            }
            set
            {
                    Adaptee[index] = (TInput)value;
            }
        }
    }
}
