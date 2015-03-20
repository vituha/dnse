using System.Collections.Generic;

namespace VS.Library.Collections.Specialized
{
    public interface IBulkEditCollection<T> : ICollection<T>
    {
        void AddRange(IEnumerable<T> source);

        void RemoveRange(int count);
    }
}
