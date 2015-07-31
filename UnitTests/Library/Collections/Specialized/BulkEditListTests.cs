using System;
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
            int[] startArray = Enumerable.Range(1, 20).ToArray();
            var maxValue = startArray[startArray.Length - 1];

            var list = new BulkEditList<int>();

            list.AddRange(startArray);
            list.Add(maxValue + 1);

            CollectionAssert.AreEqual(startArray.Concat(new[] { maxValue + 1 }), list.ToArray());
        }

        [Test]
        public void MassAddTest_BulkList()
        {
            var list = new BulkEditList<int>();

            for (int i = 0; i < 100; i++)
            {
                list.AddRange(Enumerable.Range(1, 1000));
            }

            Assert.AreEqual(100000, list.Count);
        }

        [Test]
        public void MassAddTest_List()
        {
            var list = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                list.AddRange(Enumerable.Range(1, 1000));
            }

            Assert.AreEqual(100000, list.Count);
        }

        [Test]
        public void MassInsertTest_BulkList()
        {
            var list = new BulkEditList<int>();

            for (int i = 0; i < 100; i++)
            {
                list.InsertRange(0, Enumerable.Range(1, 1000));
            }

            Assert.AreEqual(100000, list.Count);
        }

        [Test]
        public void MassInsertTest_List()
        {
            var list = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                list.InsertRange(0, Enumerable.Range(1, 1000));
            }

            Assert.AreEqual(100000, list.Count);
        }

        [Test]
        public void MassInsertOrAddTest_BulkList()
        {
            var rnd = new Random();
            var list = new BulkEditList<int>();

            list.AddRange(Enumerable.Range(1, 10000));
            for (int i = 0; i < 1000; i++)
            {
                switch (rnd.Next() % 4)
                {
                    case 0:
                        list.AddRange(Enumerable.Range(1, 1000));
                        break;
                    case 1:
                        list.InsertRange(0, Enumerable.Range(1, 1000));
                        break;
                    case 2:
                        list.RemoveRange(500);
                        break;
                    case 3:
                        list.RemoveRange(0, 500);
                        break;
                }
            }

            Assert.AreEqual(1, 1);
        }

        [Test]
        public void MassInsertOrAddTest_List()
        {
            var rnd = new Random();
            var list = new List<int>();

            list.AddRange(Enumerable.Range(1, 10000));
            for (int i = 0; i < 1000; i++)
            {
                switch (rnd.Next() % 4)
                {
                    case 0:
                        list.AddRange(Enumerable.Range(1, 1000));
                        break;
                    case 1:
                        list.InsertRange(0, Enumerable.Range(1, 1000));
                        break;
                    case 2:
                        list.RemoveRange(list.Count - 500, 500);
                        break;
                    case 3:
                        list.RemoveRange(0, 500);
                        break;
                }
            }

            Assert.AreEqual(1, 1);
        }
    }
}
