namespace VS.Library.Collections
{
    using System.Collections.Generic;
    
    public static class ListExtensions
    {
        public static IList<T> GetRange<T>(IList<T> source, int start, int count, bool reverse)
        {
            return new Range<T>(source, start, count, reverse);
        }
   }
}
