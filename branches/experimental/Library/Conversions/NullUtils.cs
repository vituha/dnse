using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace VS.Library.Conversions
{
    public static class NullUtils
    {
        public static T Coalesce<T>(params T[] collection)
        {
            foreach (var item in collection)
            {
                if (item != null)
                {
                    return item;
                }
            }
            return default(T);
        }
    }
}
