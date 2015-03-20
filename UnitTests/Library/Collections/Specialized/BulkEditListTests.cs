using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using VS.Library.Collections.Specialized;

namespace VS.Library.UT.Collections.Specialized
{
    [TestFixture]
    internal sealed class BulkEditListTests
    {
        [Test]
        public void BasicTest()
        {
            int[] startArray = {1, 2, 3};

            var list = new BulkEditList<int>();

            list.AddRange(startArray);
            list.Add(4);

            CollectionAssert.AreEqual(new[] {1, 2, 3, 4}, list);
        }

        [Test]
        public void MassInsertTest()
        {
            var list = new BulkEditList<int>();

            for (int i = 0; i < 100; i++)
            {
                list.AddRange(Enumerable.Range(1, 1000));
                list.RemoveRange(list.Count / 2 - 200, 300);
            }

            Assert.AreEqual(100000 - 30000, list.Count);
        }

        [Test]
        public void MassInsertListTest()
        {
            var list = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                list.AddRange(Enumerable.Range(1, 1000));
                list.RemoveRange(list.Count / 2 - 200, 300);
            }

            Assert.AreEqual(100000 - 30000, list.Count);
        }
    }
}
