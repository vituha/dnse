using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VS.Library.Collections
{
    public static class Empty<T>
    {
        public static T[] Collection { get { return CollectionUtil<T>.EmptyArray; } }
    }
}
