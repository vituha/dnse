using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using VS.Library.Text;
using NUnit.Framework.SyntaxHelpers;

namespace VS.Library.UT.Text
{
    [TestFixture]
    public class StringJoiner : AssertionHelper
    {
        IList<int> testCollection;
        const int collectionSize = 10000;
        const int runCount = 1000;

        [SetUp]
        public void SetUp() {
            var collection = new int[collectionSize];
            for (int i = 0; i < collectionSize; i++)
            {
                collection[i] = i;
            }
            testCollection = collection;
        }

        [Test]
        public void SimpleJoin()
        {
            string delimiter = ", ";
            var sb = new StringBuilder(collectionSize);
            sb.Append(testCollection[0]);
            for (int i = 1; i < collectionSize; i++)
            {
                sb.Append(delimiter + testCollection[i]);
            }
            var expected = sb.ToString();
            sb = null;

            var joiner = StringJoiner<int>.CreateSimple(delimiter);
            bool isEmpty = true;

            int tickCount = Environment.TickCount;

            for (int i = 0; i < runCount; i++)
            {
                isEmpty = !joiner.Visit(testCollection, collectionSize);
            }

            tickCount = Environment.TickCount - tickCount;
            Console.WriteLine(((double)collectionSize / tickCount * runCount).ToString() + " iterations per tick");

            if (!isEmpty)
            {
                Is.EqualTo(expected).Matches(joiner.ToString());
            }
            else {
                Assert.Fail();
            }
        }
    }
}
