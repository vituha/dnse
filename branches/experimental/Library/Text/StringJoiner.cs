using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VS.Library.Collections;

namespace VS.Library.Text
{
    public class StringJoiner<T> : CollectionVisitor<T>
    {
        public static StringJoiner<T> CreateSimple(string delimiter)
        {
            return new StringJoiner<T>()
            {
                MiddleItemPrefix = delimiter
            };
        }

        private int capacity = 10;

        public bool Visit(IEnumerable<T> collection, int capacity)
        {
            this.capacity = capacity;
            return Visit(collection);
        }

        private StringBuilder sb;

        private string lastResult;
        public override string ToString()
        {
            return lastResult;
        }

        private string firstItemPrefix = String.Empty;
        public string FirstItemPrefix
        {
            get
            {
                return firstItemPrefix;
            }
            set
            {
                firstItemPrefix = value ?? String.Empty;
            }
        }

        private string middleItemPrefix = String.Empty;
        public string MiddleItemPrefix
        {
            get
            {
                return middleItemPrefix;
            }
            set
            {
                middleItemPrefix = value ?? String.Empty;
            }
        }

        private string lastItemPrefix = String.Empty;
        public string LastItemPrefix
        {
            get
            {
                return lastItemPrefix;
            }
            set
            {
                lastItemPrefix = value ?? String.Empty;
            }
        }

        private string lastItemSuffix = String.Empty;
        public string LastItemSuffix
        {
            get
            {
                return lastItemSuffix;
            }
            set
            {
                lastItemSuffix = value ?? String.Empty;
            }
        }

        protected override void VisitFirstItem(T item)
        {
            sb = new StringBuilder(FirstItemPrefix + item.ToString(), capacity);
        }

        protected override void VisitMiddleItem(T item)
        {
            sb.Append(MiddleItemPrefix + item.ToString());
        }

        protected override void VisitLastItem(T item)
        {
            lastResult = sb.ToString() + ResolveLastItemPrefix() + item.ToString() + LastItemSuffix;
            sb = null;
        }

        private string ResolveLastItemPrefix()
        {
            var lastItemPrefix = LastItemPrefix;
            if (lastItemPrefix.Length > 0)
            {
                return lastItemPrefix;
            }
            return MiddleItemPrefix;
        }

        protected override void VisitSingleItem(T item)
        {
            lastResult = FirstItemPrefix + item.ToString() + ResolveLastItemPrefix();
        }
    }
}
