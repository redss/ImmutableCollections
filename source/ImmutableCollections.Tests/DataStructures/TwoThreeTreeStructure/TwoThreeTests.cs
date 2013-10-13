using System;
using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.DataStructures.TwoThreeTreeStructure;
using ImmutableCollections.Helpers;
using ImmutableCollections.Tests.TestInfrastructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.TwoThreeTreeStructure
{
    [TestFixture]
    public class TwoThreeTests
    {
        private readonly Random _random = new Random(0);

        // Tests

        [Test]
        public void TryFind_Test()
        {
            const int count = 100;

            var keys = RandomHelper.UniqueSequence(_random, count);
            var values = RandomHelper.UniqueSequence(_random, count);
            var items = keys.Zip(values, (k, v) => new KeyValuePair<int, int>(k, v)).ToArray();
            var comparer = new KeyComparer<int, int>();

            var node = CreateTree(items, comparer);

            foreach (var item in items)
            {
                var searched = new KeyValuePair<int, int>(item.Key, 12345);
                KeyValuePair<int, int> foundItem;

                var result = node.TryFind(searched, comparer, out foundItem);

                Assert.IsTrue(result);
                Assert.AreEqual(item, foundItem);
            }

            var absentKeys = GetAbsentElements(keys);
            
            foreach (var key in absentKeys)
            {
                var searched = new KeyValuePair<int, int>(key, 123);
                KeyValuePair<int, int> foundItem;

                var result = node.TryFind(searched, comparer, out foundItem);

                Assert.IsFalse(result);
            }
        }

        // Insert tests

        [Test]
        public void Insert_Test()
        {
            var items = RandomHelper.UniqueSequence(_random, 100);
            var sorted = items.OrderBy(x => x).ToArray();

            var node = CreateTree(items);
            var values = node.GetValues().ToArray();

            CollectionAssert.AllItemsAreUnique(values);
            CollectionAssert.AreEqual(sorted, values);
        }

        [Test]
        public void InsertingExistingElement_ReturnsSameRoot()
        {
            var items = RandomHelper.UniqueSequence(_random, 100);
            var node = CreateTree(items);

            foreach (var item in items)
            {
                var result = TwoThreeHelper.Insert(node, item);
                Assert.AreSame(node, result);
            }
        }

        // Update tests

        [Test]
        public void UpdateExistingEqualElement_ReturnsSameNode()
        {
            var items = RandomHelper.UniqueSequence(_random, 100);
            var node = CreateTree(items);

            foreach (var item in items)
            {
                var result = node.Update(item, Comparer<int>.Default);
                Assert.AreSame(result, node);
            }
        }

        [Test]
        public void UpdateAbsentElement_ReturnsNull()
        {
            var items = RandomHelper.UniqueSequence(_random, 100);
            var node = CreateTree(items);
            var absentElements = GetAbsentElements(items);

            foreach (var item in absentElements)
            {
                var result = node.Update(item, Comparer<int>.Default);
                Assert.IsNull(result);
            }
        }

        [Test]
        public void UpdateExistingElementWithNewValue_ReturnsNewNode()
        {
            const int count = 100, newValue = 123456;

            var keys = RandomHelper.UniqueSequence(_random, count);
            var values = RandomHelper.UniqueSequence(_random, count);

            var items = keys.Zip(values, (a, b) => new KeyValuePair<int, int>(a, b));
            var comparer = new KeyComparer<int, int>();

            var node = CreateTree(items, comparer);

            foreach (var key in keys)
            {
                var item = new KeyValuePair<int, int>(key, newValue);
                var result = node.Update(item, comparer);

                Assert.AreNotSame(node, result);
                CollectionAssert.Contains(result.GetValues(), item);
            }
        }

        // Remove tests

        [Test]
        public void RemoveRandomData_Test()
        {
            const int count = 100;
            var items = RandomHelper.UniqueSequence(_random, count);
            var itemSet = new SortedSet<int>(items);

            var node = CreateTree(items);

            var shuffled = RandomHelper.Shuffle(_random, items);

            foreach (var i in shuffled)
            {
                node = TwoThreeHelper.Remove(node, i);
                itemSet.Remove(i);

                AssertValid(node);
                CollectionAssert.AreEqual(itemSet, node.GetValues());
            }

            Assert.IsInstanceOf<Empty<int>>(node);
        }

        [Test]
        public void RemoveSequentialData_Test()
        {
            const int count = 100;
            var items = Enumerable.Range(0, count).ToArray();
            var itemSet = new SortedSet<int>(items);

            var node = CreateTree(items);

            foreach (var i in items)
            {
                node = TwoThreeHelper.Remove(node, i);
                itemSet.Remove(i);

                AssertValid(node);
                CollectionAssert.AreEqual(itemSet, node.GetValues());
            }
        }

        [Test]
        public void RemoveSequentialData_InReversedOrder_Test()
        {
            const int count = 100;
            var items = Enumerable.Range(0, count).ToArray();
            var itemSet = new SortedSet<int>(items);

            var node = CreateTree(items);

            foreach (var i in items.Reverse())
            {
                node = TwoThreeHelper.Remove(node, i);
                itemSet.Remove(i);

                AssertValid(node);
                CollectionAssert.AreEqual(itemSet, node.GetValues());
            }
        }

        // Private methods

        private ITwoThree<T> CreateTree<T>(IEnumerable<T> items, IComparer<T> comparer = null)
        {
            return items.Aggregate((ITwoThree<T>)Empty<T>.Instance, (current, item) => TwoThreeHelper.Insert(current, item, comparer));
        }

        private static int[] GetAbsentElements(int[] items)
        {
            var absentMiddleElement = items.ElementAt(items.Count() / 2);

            while (items.Contains(absentMiddleElement))
                absentMiddleElement++;

            return new[] { -100, absentMiddleElement, 123123213 };
        }

        private void AssertValid<T>(ITwoThree<T> tree)
        {
            var values = tree.GetValues().ToArray();

            Assert.True(tree.IsBalanced());
            CollectionAssert.IsOrdered(values);
            CollectionAssert.AllItemsAreUnique(values);
        }
    }
}
