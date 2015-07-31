using System.Collections.Generic;

namespace VS.Library.Collections.Specialized
{
    public interface IBulkEditList<T> : IBulkEditCollection<T>, IList<T>
    {
        void ReplaceRange(int removeAt, int removeCount, int insertAt, IEnumerable<T> insertSource);
    }
}