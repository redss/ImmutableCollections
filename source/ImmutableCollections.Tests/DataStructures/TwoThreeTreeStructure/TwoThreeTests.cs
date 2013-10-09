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

            var absentMiddleElement = items[items.Length / 2];
            while (items.Contains(absentMiddleElement))
                absentMiddleElement++;

            var absentElements = new[] { -100, absentMiddleElement, 123123213 };

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

        // Private methods

        private ITwoThree<T> CreateTree<T>(IEnumerable<T> items, IComparer<T> comparer = null)
        {
            return items.Aggregate((ITwoThree<T>)Empty<T>.Instance, (current, item) => TwoThreeHelper.Insert(current, item, comparer));
        }
    }
}
