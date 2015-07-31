using System;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace VS.Library.UT.Collections
{
    [TestFixture]
    internal sealed class JoinTests
    {
        private const string Separator = ",";
        private const int ItemCount = 10000000;

        [Test]
        public void SimpleIntJoin()
        {
            string.Join(Separator, GenerateInts());
        }

        [Test]
        public void SmartIntJoin()
        {
            string.Join(Separator, SmartSelect(GenerateInts()));
        }

        private IEnumerable<string> SmartSelect(IEnumerable<int> items)
        {
            CultureInfo invariantCulture = CultureInfo.InvariantCulture;

            return items.Select(v => v.ToString(invariantCulture));
        }

        private IEnumerable<int> GenerateInts()
        {
            return Enumerable.Range(0, ItemCount);
        }
    }
}

