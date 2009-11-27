using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using VS.Library.Diagnostics;
using System.Reflection;
using System.IO;
using System.Diagnostics;

using FixtNS = VS.Library.Pattern.Lifetime;
using VS.Library.Pattern.Enumerable;

namespace VS.Library.UT.Cache
{
    [TestFixture]
    public class EnyumeratorMonitorTest : AssertionHelper
    {
        private int[] values = { 1, 2, 3, 4, 5 };

        [SetUp]
        public void Init()
        {
        }

        [TearDown]
        public void DeInit()
        {
        }

        [Test(Description = "Perform some enumeration")]
        public void Enumerate()
        {
            EnumeratorMonitor<int> wrapper = EnumeratorMonitor<int>.Create((values as IEnumerable<int>).GetEnumerator());
            wrapper.EnumerationStarting += new EventHandler(wrapper_EnumerationStarting);
            wrapper.EnumerationProgress += new EventHandler(wrapper_EnumerationProgress);
            wrapper.EnumerationEnded += new EventHandler(wrapper_EnumerationEnded);

            foreach (int val in wrapper as IEnumerable<int>)
            {
                Console.WriteLine(val.ToString());
            }
            wrapper = null;
        }

        void wrapper_EnumerationEnded(object sender, EventArgs e)
        {
            Console.WriteLine("EnumerationEnded");
        }

        void wrapper_EnumerationProgress(object sender, EventArgs e)
        {
            Console.WriteLine("EnumerationProgress");
        }

        void wrapper_EnumerationStarting(object sender, EventArgs e)
        {
            Console.WriteLine("EnumerationStarting");
        }

    }
}
