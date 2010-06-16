using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Immutability;
using NUnit.Framework;

namespace ImmutabilityTest
{
    [TestFixture]
    public class ImmutableCollectionTest
    {
        [SetUp]
        void SetUp()
        {
        }

        [TearDown]
        void TearDown()
        {
        }

        [Test]
        public void MutableMethods()
        {
            var test = new ImmutableCollection<int>(GenerateCollection(false));

            Assert.IsFalse(test.IsImmutable);

            DoModify(test);

            var mutator = test.Lock();
            AttemptModify(() => test.Add(1));
            AttemptModify(() => test.Remove(1));
            AttemptModify(test.Clear);

            mutator.Unlock();
            DoModify(test);
        }

        private static void DoModify(ImmutableCollection<int> test)
        {
            int count = test.Count;
            test.Add(5); Assert.AreEqual(count + 1, test.Count);
            test.Remove(5); Assert.AreEqual(count, test.Count);
            test.Clear(); Assert.AreEqual(0, test.Count);
        }

        private static void AttemptModify(Action action)
        {
            try
            {
                action();
                Assert.Fail("Exception is xpected but not thrown");
            }
            catch (ImmutabilityException ex)
            {
                Trace.WriteLine("Caught expected ImmutabilityException: " + ex.Message);
            }
        }

        private static IList<int> GenerateCollection(bool readOnly)
        {
            var result = new List<int>() {1, 2, 3, 4};

            if (readOnly)
            {
                return result.AsReadOnly();
            }
            return result;
        }
    }
}
