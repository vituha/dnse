using System;
using System.Collections.Generic;
using System.Linq;

namespace VS.Library.Collections.Specialized
{
    public static class BulkEditCollectionExtensions
    {
        public static void AddRange<T>(this IBulkEditCollection<T> target, IEnumerable<T> source)
        {
            if (target == null) throw new ArgumentNullException("target");
            if (source == null) throw new ArgumentNullException("source");

            target.ReplaceRange(0, source);
        }

        public static void RemoveRange<T>(this IBulkEditCollection<T> target, int count)
        {
            if (target == null) throw new ArgumentNullException("target");

            target.ReplaceRange(count, Enumerable.Empty<T>());
        }
    }
}
