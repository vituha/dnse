using System;
using System.Collections.Generic;
using System.Linq;

namespace VS.Library.Collections.Specialized
{
    public static class BulkEditExtensions
    {
        public static void InsertRange<T>(this IBulkEditList<T> target, int insertAt, IEnumerable<T> source)
        {
            if (target == null) throw new ArgumentNullException("target");
            if (source == null) throw new ArgumentNullException("source");

            target.ReplaceRange(0, 0, insertAt, source);
        }

        public static void RemoveRange<T>(this IBulkEditList<T> target, int removeAt, int count)
        {
            if (target == null) throw new ArgumentNullException("target");

            target.ReplaceRange(removeAt, count, 0, Enumerable.Empty<T>());
        }
    }
}