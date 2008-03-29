using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace VS.Library.Strings
{
	/// <summary>
	/// Provides functionality for joining strings
	/// </summary>
	public static class Joiner
	{
		private static string defaultJoinPrefix = ",";
		private static string defaultJoinSuffix = String.Empty;
		private static string defaultJoinDelimiter = String.Empty;

		/// <summary>
		/// Joins given objects by adding prefix and suffix only if object list contains at least one object
		/// </summary>
		/// <param name="result"></param>
		/// <param name="objects"></param>
		/// <param name="joiner"></param>
		/// <param name="prefix"></param>
		/// <param name="suffix"></param>
		/// <returns></returns>
		public static bool Join(StringBuilder result, IEnumerable objects, object joiner, object prefix, object suffix)
		{
			bool notEmpty = false;
			string _joiner = joiner.ToString();
			IEnumerator en = objects.GetEnumerator();
			if (en.MoveNext())
			{
				notEmpty = true;
				result.Append(prefix.ToString());
				result.Append(en.Current.ToString());
				while (en.MoveNext())
				{
					result.Append(_joiner);
					result.Append(en.Current.ToString());
				}
				result.Append(suffix.ToString());
			}
			en = null;
			return notEmpty;
		}
		public static bool Join(StringBuilder result, IEnumerable objects, object delimiter)
		{
			return Join(result, objects, delimiter, defaultJoinPrefix, defaultJoinSuffix);
		}
		public static bool Join(StringBuilder result, IEnumerable objects)
		{
			return Join(result, objects, defaultJoinDelimiter, defaultJoinPrefix, defaultJoinSuffix);
		}
		public static string Join(IEnumerable objects, object delimiter, object prefix, object suffix)
		{
			StringBuilder result = new StringBuilder();
			Join(result, objects, delimiter, prefix, suffix);
			return result.ToString();
		}
		public static string Join(IEnumerable objects, object delimiter)
		{
			return Join(objects, delimiter, defaultJoinPrefix, defaultJoinSuffix);
		}
		public static string Join(IEnumerable objects)
		{
			return Join(objects, defaultJoinDelimiter, defaultJoinPrefix, defaultJoinSuffix);
		}


	}
}
