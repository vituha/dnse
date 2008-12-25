using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VS.Library.Collections
{
    public abstract class CollectionVisitor<T>
    {
        public bool Visit(IEnumerable<T> collection)
        {
            var enumerator = collection.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                enumerator.Dispose();
                enumerator = null;
                return false;
            }

            T item = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                enumerator.Dispose();
                enumerator = null;

                VisitSingleItem(item);

                return true;
            }

            VisitFirstItem(item);

            item = enumerator.Current;
            while (enumerator.MoveNext())
            {
                VisitMiddleItem(item);
                item = enumerator.Current;
            }
            enumerator.Dispose();
            enumerator = null;

            VisitLastItem(item);

            return true;
        }

        protected abstract void VisitFirstItem(T item);

        protected abstract void VisitMiddleItem(T item);

        protected abstract void VisitLastItem(T item);

        protected abstract void VisitSingleItem(T item);
    }
}
