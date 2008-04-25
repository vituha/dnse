using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace VS.Library.Strings
{
    /// <summary>
    /// Provides functionality for joining strings
    /// </summary>
    public static class separator
    {
        private static string defaultJoinPrefix = ",";
        private static string defaultJoinSuffix = String.Empty;
        private static string defaultJoinseparator = String.Empty;

        /// <summary>
        /// Joins given objectCollection into a string using specified separator, prefix and suffix
        /// </summary>
        /// <param name="sb">buffer to receive the result string</param>
        /// <param name="objectCollection">objects to join</param>
        /// <param name="separator">separator object</param>
        /// <param name="prefix">is put at the beginning  of result string (only if there are objects to join)</param>
        /// <param name="suffix">is put at the end of result string (only if there are objects to join)</param>
        /// <returns>true if result is non-empty. false otherwise</returns>
        public static bool Join(StringBuilder sb, IEnumerable objectCollection, object separator, object prefix, object suffix)
        {
            if (sb == null)
                throw new ArgumentException("sb must not be null");
            if (objectCollection == null)
                objectCollection = new String[0];
            if (separator == null)
                separator = defaultJoinseparator;
            if (prefix == null)
                prefix = defaultJoinPrefix;
            if (suffix == null)
                suffix = defaultJoinSuffix;

            bool notEmpty = false;
            string _separator = separator.ToString();
            IEnumerator en = objectCollection.GetEnumerator();
            if (en.MoveNext())
            {
                notEmpty = true;
                sb.Append(prefix.ToString());
                sb.Append(en.Current.ToString());
                while (en.MoveNext())
                {
                    sb.Append(_separator);
                    sb.Append(en.Current.ToString());
                }
                sb.Append(suffix.ToString());
            }
            en = null;
            return notEmpty;
        }
        /// <summary>
        /// Joins given objectCollection into a string using specified separator
        /// </summary>
        /// <param name="sb">buffer to receive the result string</param>
        /// <param name="objectCollection">objects to join</param>
        /// <param name="separator">separator object</param>
        /// <returns>true if result is non-empty. false otherwise</returns>
        public static bool Join(StringBuilder sb, IEnumerable objectCollection, object separator)
        {
            return Join(sb, objectCollection, separator, null, null);
        }
        /// <summary>
        /// Joins given objectCollection using default separator
        /// </summary>
        /// <param name="sb">buffer to receive the result string</param>
        /// <param name="objectCollection">objects to join</param>
        /// <returns>true if result is non-empty. false otherwise</returns>
        public static bool Join(StringBuilder sb, IEnumerable objectCollection)
        {
            return Join(sb, objectCollection, null, null, null);
        }
        /// <summary>
        /// Joins given objectCollection into a string using specified separator, prefix and suffix
        /// </summary>
        /// <param name="objectCollection">objects to join</param>
        /// <param name="separator">separator object</param>
        /// <param name="prefix">is put at the beginning  of result string (only if there are objects to join)</param>
        /// <param name="suffix">is put at the end of result string (only if there are objects to join)</param>
        /// <returns>joined objects as a string</returns>
        public static string Join(IEnumerable objectCollection, object separator, object prefix, object suffix)
        {
            StringBuilder result = new StringBuilder();
            Join(result, objectCollection, separator, prefix, suffix);
            return result.ToString();
        }
        /// <summary>
        /// Joins given objectCollection into a string using specified separator
        /// </summary>
        /// <param name="objectCollection">objects to join</param>
        /// <param name="separator">separator object</param>
        /// <returns>joined objects as a string</returns>
        public static string Join(IEnumerable objectCollection, object separator)
        {
            return Join(objectCollection, separator, null, null);
        }
        /// <summary>
        /// Joins given objectCollection into a string using default separator
        /// </summary>
        /// <param name="objectCollection">objects to join</param>
        /// <returns>joined objects as a string</returns>
        public static string Join(IEnumerable objectCollection)
        {
            return Join(objectCollection, null, null, null);
        }


    }
}
