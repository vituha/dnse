using System.Collections.Generic;

namespace VS.Library.Collections.Specialized
{
    public interface IBulkEditList<T> : IBulkEditCollection<T>, IList<T>
    {
        void AddRange(IEnumerable<T> source, int start);

        void RemoveRange(int start, int count);
    }
}