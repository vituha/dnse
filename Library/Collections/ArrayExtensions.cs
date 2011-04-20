namespace VS.Library.Collections
{
    using System;

    public static class ArrayExtensions
    {
        public static bool HasItems(this Array source)
        {
            return source != null && source.Length > 0;
        }

        public static bool IsNullOrEmpty(this Array source)
        {
            return source == null || source.Length <= 0;
        }
    }
}
