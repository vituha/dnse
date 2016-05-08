using System.Collections.Generic;

namespace VS.Library.Text
{
    public static class StringExtensions
    {
        public static IReadOnlyList<char> AsReadOnlyList(this string source)
        {
            return new StringToReadOnlyListAdapter(source);
        }
    }
}