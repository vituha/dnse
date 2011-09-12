namespace VS.Library.Collections.Views
{
    using System;

    internal static class CollectionExceptions
    {
        public static Exception ReadOnly()
        {
            return new CollectionReadOnlyException("Collection is read-only");
        }

        public static Exception IndexIsOutOfRange()
        {
            return new IndexOutOfRangeException("Index is out of range.");
        }
    }
}
