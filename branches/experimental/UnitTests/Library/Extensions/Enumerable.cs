using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using VS.Library.Collections.Enumerable;

namespace VS.Library.UT.Extensions
{
    [TestFixture]
    public class Enumerable
    {
        private IList<string> collection;

        [SetUp]
        public void SetUp()
        {
            collection = new List<string>() {
                "Alpha",
                "Omega",
                "Delta",
                "Kappa"
            };
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void ApplyTest()
        {
            int count;
            int letterCount = 0;
            count = collection.ForEachCounted((item, index) => letterCount += item.Length);
            Assert.AreEqual(collection.Count, count);
            Assert.AreEqual(20, letterCount);
        }
    }
}
