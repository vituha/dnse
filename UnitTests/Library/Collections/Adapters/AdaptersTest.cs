using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Reflection;
using System.Collections;

using VS.Library.Collections;
using VS.Library.Collections.Adapters;
using NUnit.Framework.SyntaxHelpers;
using VS.Library.Validation;

namespace VS.Library.UT.Collections.Adapters
{
	[TestFixture]
    public class AdaptersTest
	{
        int[] _intArray = new int[] { 1, 2, 3, 4, 5 };
        ICollection<int> _typedCollection;
        ICollection _untypedCollection;
        IList<int> _typedList = new List<int>();
        IList _untypedList = new ArrayList();

        ICollection<object> objectCollection;
        ICollection<int> typedCollection;
        ICollection untypedCollection;
        IList<object> objectList;
        IList<int> typedList;
        IList untypedList;

        ICollection<object> readOnlyObjectCollection;

        IList<object> readOnlyObjectList;
        IList<int> readOnlyTypedList;
        IList readOnlyUntypedList;
        
        [SetUp]
		public void Init()
		{
            foreach (var item in _intArray)
            {
                _typedList.Add(item);
                _untypedList.Add(item);
            }

            _typedCollection = _typedList;
            _untypedCollection= _untypedList;

            objectCollection = _typedCollection.Adapt<int, object>();
            typedCollection = _untypedCollection.Adapt<int>();
            untypedCollection = _typedCollection.AdaptUntyped();

            objectList = _typedList.Adapt<int, object>();
            typedList = _untypedList.Adapt<int>();
            untypedList = _typedList.AdaptUntyped();

            readOnlyObjectCollection = _intArray.Adapt<int, object>(true);

            readOnlyObjectList = _intArray.Adapt<int, object>(true);
            readOnlyTypedList = _intArray.Adapt<int>(true);
            readOnlyUntypedList = _intArray.AdaptUntyped(true);
		}

		[TearDown]
		public void DeInit()
		{
            _typedList.Clear();
            _untypedList.Clear();
        }

        [Test]
        public void CollectionAdapter()
        {
            Assert.That(objectCollection, Is.EquivalentTo(_intArray));
            Assert.That(!objectCollection.IsReadOnly);

            Assert.That(typedCollection, Is.EquivalentTo(_intArray));
            Assert.That(typedCollection.IsReadOnly);

            Assert.That(untypedCollection, Is.EquivalentTo(_intArray));
        }

        [Test]
        public void ListAdapter()
        {
            Assert.That(objectList, Is.EquivalentTo(_intArray));
            Assert.That(!objectList.IsReadOnly);

            Assert.That(typedList, Is.EquivalentTo(_intArray));
            Assert.That(!typedList.IsReadOnly);

            Assert.That(untypedList, Is.EquivalentTo(_intArray));
            Assert.That(!untypedList.IsReadOnly);
        }

        [Test]
        public void ReadOnlyCollectionAdapter()
        {
            Assert.That(readOnlyObjectCollection, Is.EquivalentTo(_intArray));
        }

        [Test]
        public void TypedListAdapter()
        {
            Assert.That(readOnlyObjectList, Is.EquivalentTo(_intArray));
            Assert.That(readOnlyObjectList.IsReadOnly);

            Assert.That(readOnlyTypedList, Is.EquivalentTo(_intArray));
            Assert.That(readOnlyTypedList.IsReadOnly);

            Assert.That(readOnlyUntypedList, Is.EquivalentTo(_intArray));
            Assert.That(readOnlyUntypedList.IsReadOnly);
            Assert.That(readOnlyUntypedList.IsFixedSize);
        }

        [Test]
        public void Modifications() {
            ModifyElements(objectList);
            ModifyElements(objectList);
            Assert.That(_typedList, Is.EquivalentTo(_intArray));

            ModifyElements(typedList);
            ModifyElements(typedList);
            Assert.That(_untypedList, Is.EquivalentTo(_intArray));

            ModifyElements(untypedList);
            ModifyElements(untypedList);
            Assert.That(_typedList, Is.EquivalentTo(_intArray));
        }

        [Test]
        public void SizeModifications()
        {
            AddElements(objectList);
            RemoveElements(objectList);
            Assert.That(_typedList, Is.EquivalentTo(_intArray));

            AddElements(typedList);
            RemoveElements(typedList);
            Assert.That(_untypedList, Is.EquivalentTo(_intArray));

            InsertElements(untypedList);
            RemoveElementsAt(untypedList);
            Assert.That(_typedList, Is.EquivalentTo(_intArray));
        }

        #region Private Helpers

        private void ModifyElements(IList<object> list)
        {
            var element = list[0];
            list[0] = list[1];
            list[1] = element;
        }

        private void ModifyElements(IList<int> list) {
            var element = list[0];
            list[0] = list[1];
            list[1] = element;
        }

        private void ModifyElements(IList list)
        {
            var element = list[0];
            list[0] = list[1];
            list[1] = element;
        }

        private void AddElements(ICollection<object> collection)
        {
            collection.Add(8);
        }

        private void AddElements(ICollection<int> collection)
        {
            collection.Add(8);
        }

        private void AddElements(IList collection)
        {
            collection.Add(8);
        }

        private void RemoveElements(ICollection<object> collection)
        {
            collection.Remove(8);
        }

        private void RemoveElements(ICollection<int> collection)
        {
            collection.Remove(8);
        }

        private void RemoveElements(IList collection)
        {
            collection.Remove(8);
        }

        private void InsertElements(IList<object> collection)
        {
            collection.Insert(0, 9);
        }

        private void InsertElements(IList<int> collection) {
            collection.Insert(0, 9);
        }

        private void InsertElements(IList collection) {
            collection.Insert(0, 9);
        }

        private void RemoveElementsAt(IList<object> collection)
        {
            collection.RemoveAt(0);
        }

        private void RemoveElementsAt(IList<int> collection)
        {
            collection.RemoveAt(0);
        }

        private void RemoveElementsAt(IList collection)
        {
            collection.RemoveAt(0);
        }

        private void CopyTo(IList<object> collection, object[] array)
        {
            collection.CopyTo(array, 0);
        }

        private void CopyTo(IList<int> collection, int[] array) {
            collection.CopyTo(array, 0);
        }

        private void CopyTo(IList collection, Array array)
        {
            collection.CopyTo(array, 0);
        }

        #endregion Private Helpers
    }
}
