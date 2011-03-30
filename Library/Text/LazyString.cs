using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VS.Library.Text
{
    public sealed class LazyString
    {
        private List<string> buffer;
        private string value = String.Empty;
        public int Length { get; private set; }

        public LazyString() {
            buffer = new List<string>();
        }

        public LazyString(int capacity)
        {
            buffer = new List<string>(capacity);
        }

        public override string ToString() {
            if (value == null)
            {
                BuildValue();
            }
            return value;
        }

        public void Clear() {
            buffer.Clear();
            Length = 0;
            value = String.Empty;
        }

        public bool Append(string item) {
            if (String.IsNullOrEmpty(item))
            {
                return false;
            }
            value = null;
            buffer.Add(item);
            Length += item.Length;
            return true;
        }
        public bool AppendRange(IEnumerable<string> items) {
            var modified = false;
            foreach (var item in items)
            {
                if (!String.IsNullOrEmpty(item))
                {
                    buffer.Add(item);
                    Length += item.Length;
                    modified = true;
                }
            }
            if (modified)
            {
                value = null;
            }
            return modified;
        }

        private void BuildValue()
        {
            var sb = new StringBuilder(Length);
            buffer.ForEach(s => sb.Append(s));
            value = sb.ToString();
        }
    }
}
