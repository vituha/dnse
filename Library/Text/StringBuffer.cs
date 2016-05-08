using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace VS.Library.Text
{
    /// <summary>
    /// Provides a very simple and fast append-only alternative to StringBuilder.
    /// </summary>
    public sealed class StringBuffer
    {
        private readonly List<string> _buffer = new List<string>();

        public int Length { get; private set; }

        public void Add(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            _buffer.Add(text);
            Length += text.Length;
        }

        public void WriteTo(TextWriter writer)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));

            _buffer.ForEach(writer.Write);
        }

        /*
          Given enumerable of read-only lists of variable length,
          get range of items
        */

        //public void WriteTo(TextWriter writer, int startIndex, int charCount)
        //{
        //    if (writer == null) throw new ArgumentNullException(nameof(writer));
        //    if (startIndex < 0 || startIndex >= Length) throw new ArgumentOutOfRangeException(nameof(startIndex));
        //    if (charCount < 0 || startIndex + charCount > Length) throw new ArgumentOutOfRangeException(nameof(charCount));

        //    using (var enumerator = _buffer.GetEnumerator())
        //    {
                
        //    }
            
        //    _buffer.ForEach(writer.Write);
        //}

        public async Task WriteToAsync(TextWriter writer, CancellationToken token = default(CancellationToken))
        {
            foreach (string text in _buffer)
            {
                token.ThrowIfCancellationRequested();
                await writer.WriteAsync(text);
            }
        }

        public void Clear()
        {
            _buffer.Clear();
            Length = 0;
        }
    }
}