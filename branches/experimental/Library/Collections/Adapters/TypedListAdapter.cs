using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

namespace VS.Library.Collections.Adapters
{
    public class ReadOnlyTypedListAdapter<T> : ReadOnlyListBase<T>
    {
        public IList<T> Adaptee { get; private set; }

        public ReadOnlyTypedListAdapter(IList<T> adaptee)
        {
            Adaptee = adaptee;
        }

        public override int Count
        {
            get { return Adaptee.Count; }
        }

        public override void CopyTo(T[] array, int index)
        {
            Adaptee.CopyTo(array, index);
        }

        public override int IndexOf(T value)
        {
            return Adaptee.IndexOf(value);
        }

        public override T this[int index]
        {
            get
            {
                return Adaptee[index];
            }
            set
            {
                if (!Adaptee.IsReadOnly)
                {
                    Adaptee[index] = value;
                    return;
                }
                base[index] = value; // Throws exception
            }
        }
    }

    public class TypedListAdapter<T> : ListBase<T>
    {
        public IList<T> Adaptee { get; private set; }

        public TypedListAdapter(IList<T> adaptee)
        {
            Adaptee = adaptee;
        }

        public override void Clear()
        {
            Adaptee.Clear();
        }

        public override void Insert(int index, T value)
        {
            Adaptee.Insert(index, value);
        }

        public override void RemoveAt(int index)
        {
            Adaptee.RemoveAt(index);
        }

        public override T this[int index]
        {
            get
            {
                return Adaptee[index];
            }
            set
            {
                Adaptee[index] = value;
            }
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

        public override int IndexOf(T value)
        {
            return Adaptee.IndexOf((T)value);
        }
    }
}
