namespace VS.Library.Collections
{
    using System.Collections.Generic;
    
    public static class ListExtensions
    {
        public static IReadOnlyList<T> GetRange<T>(IReadOnlyList<T> source, int start, int count, bool reverse)
        {
            return new ReadOnlyListRange<T>(source, start, count);
        }
    }
}
