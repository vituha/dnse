using System.Collections.Generic;

namespace VS.Library.Collections.Specialized
{
    public interface IBulkEditCollection<T> : ICollection<T>
    {
        void ReplaceRange(int removeCount, IEnumerable<T> addSource);
    }
}
