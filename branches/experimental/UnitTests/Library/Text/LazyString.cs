using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace VS.Library.UT.Text
{
    [TestFixture]
    public class LazyString : AssertionHelper
    {
        private const int testCount = 1000000;
        private const int length = 6;
        private const string s = "##############################################################################";

        [Test]
        public void TestLazyString() {
            var ticks = Environment.TickCount;
            for (int j = 0; j < testCount; j++)
            {
                var ls = new VS.Library.Text.LazyString(length);
                for (int i = 0; i < length; i++)
                {
                    ls.Append(s);
                }
                var l = ls.ToString().Length;
            }
            Console.WriteLine((Environment.TickCount - ticks).ToString());
        }

        [Test]
        public void TestStringBuilder()
        {
            var ticks = Environment.TickCount;
            for (int j = 0; j < testCount; j++)
            {
                var sb = new StringBuilder();
                for (int i = 0; i < length; i++)
                {
                    sb.Append(s);
                }
                var l = sb.ToString().Length;
            }
            Console.WriteLine((Environment.TickCount - ticks).ToString());
        }
    }
}
