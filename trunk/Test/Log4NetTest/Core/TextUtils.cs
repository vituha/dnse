using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log4NetTest.Core
{
    public static class TextUtils
    {
        public static string Strip(this string source, string prefix) 
        {
            if (source.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
            {
                return "*" + source.Substring(prefix.Length);
            }
            return source;
        }
    }
}
