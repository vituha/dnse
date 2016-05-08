using System;
using System.Collections;
using System.Collections.Generic;

namespace VS.Library.Text
{
    internal sealed class StringToReadOnlyListAdapter : IReadOnlyList<char>
    {
        private readonly string _source;

        public StringToReadOnlyListAdapter(string source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            _source = source;
        }

        public IEnumerator<char> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _source.Length;

        public char this[int index] => _source[index];
    }
}
