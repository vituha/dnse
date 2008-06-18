using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace VS.Library.Strings
{
    internal static class StringUtils
    {
        /// <summary>
        /// Provides Culture-specific string formatting services for internal strings
        /// </summary>
        public static string UserFormat(string format, params Object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, format, args);
        }

        /// <summary>
        /// Provides Culture-specific string formatting services for UI strings
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static string SystemFormat(string format, params Object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, format, args);
        }
    }
}
