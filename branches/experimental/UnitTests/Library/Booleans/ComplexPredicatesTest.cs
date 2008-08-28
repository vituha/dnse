using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using VS.Library.Booleans;

namespace VS.Library.UT.Booleans
{
    [TestFixture]
    public class ComplexPredicatesTest
    {
        Predicate<int>[][] collection = new Predicate<int>[2][] { 
            new Predicate<int>[] { x => x >= 0, x => x <= 0 }, 
            new Predicate<int>[] { x => x == 0, x => x != 0 }
        };

        [SetUp]
        public void Init()
        {
        }

        [TearDown]
        public void DeInit()
        {
        }

        [Test]
        public void And() {
            Assert.IsFalse(ComplexPredicates.And(collection[0])(-1));
            Assert.IsFalse(ComplexPredicates.And(collection[0])(1));
            Assert.IsTrue(ComplexPredicates.And(collection[0])(0));

            Assert.IsFalse(ComplexPredicates.And(collection[1])(-1));
            Assert.IsFalse(ComplexPredicates.And(collection[1])(1));
            Assert.IsFalse(ComplexPredicates.And(collection[1])(0));
        }

        [Test]
        public void Or()
        {
            Assert.IsTrue(ComplexPredicates.Or(collection[0])(-1));
            Assert.IsTrue(ComplexPredicates.Or(collection[0])(1));
            Assert.IsTrue(ComplexPredicates.Or(collection[0])(0));

            Assert.IsTrue(ComplexPredicates.Or(collection[1])(-1));
            Assert.IsTrue(ComplexPredicates.Or(collection[1])(1));
            Assert.IsTrue(ComplexPredicates.Or(collection[1])(0));
        }

        [Test]
        public void DNF()
        {
            Assert.IsFalse(ComplexPredicates.DNF(collection)(-1));
            Assert.IsFalse(ComplexPredicates.DNF(collection)(1));
            Assert.IsTrue(ComplexPredicates.DNF(collection)(0));
        }

        [Test]
        public void CNF()
        {
            Assert.IsTrue(ComplexPredicates.CNF(collection)(-1));
            Assert.IsTrue(ComplexPredicates.CNF(collection)(1));
            Assert.IsTrue(ComplexPredicates.CNF(collection)(0));
        }
    }
}
