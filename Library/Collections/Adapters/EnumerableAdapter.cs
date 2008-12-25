using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

namespace VS.Library.Collections.Adapters
{
    internal class EnumerableCollectionAdapter<T> : ReadOnlyCollectionBase<T>
    {
        public EnumerableCollectionAdapter(IEnumerable<T> adaptee, int itemCount) {
            this.adaptee = adaptee;
            this.adapteeItemCount = itemCount;
        }

        private IEnumerable<T> adaptee;
        private int adapteeItemCount;

        public override int Count
        {
            get { return adapteeItemCount; }
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return adaptee.GetEnumerator();
        }
    }

    internal class CountingEnumerableCollectionAdapter<T> : ReadOnlyCollectionBase<T>
    {
        public CountingEnumerableCollectionAdapter(IEnumerable<T> adaptee)
        {
            this.adaptee = adaptee;
        }

        private IEnumerable<T> adaptee;

        public override int Count
        {
            get {
                return CountItems();
            }
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return adaptee.GetEnumerator();
        }

        private int CountItems()
        {
            int i = 0;
            foreach (var item in this.adaptee)
            {
                i++;
            }
            return i;
        }
    }
}
