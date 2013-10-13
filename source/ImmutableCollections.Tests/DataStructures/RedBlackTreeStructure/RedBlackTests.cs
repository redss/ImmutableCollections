using System;
using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.DataStructures.RedBlackTreeStructure;
using ImmutableCollections.Helpers;
using ImmutableCollections.Tests.TestInfrastructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.RedBlackTreeStructure
{
    [TestFixture]
    public class RedBlackTests
    {
        private readonly Random _random = new Random(0);

        private readonly IComparer<int> _comparer = Comparer<int>.Default;

        // Tests

        [Test]
        public void Insert_Test()
        {
            var items = RandomHelper.UniqueSequence(_random, 100);
            var node = CreateTree(items);

            node.Validate();

            foreach (var i in items)
            {
                int result;
                var found = node.TryFind(i, _comparer, out result);

                Assert.True(found);
                Assert.AreEqual(i, result);
            }

            CollectionAssert.AreEqual(items.OrderBy(x => x, _comparer), node.GetValues());
        }

        [Test]
        public void Insert_ExistingElement_ReturnsSameNode()
        {
            var items = RandomHelper.UniqueSequence(_random, 100);
            var node = CreateTree(items);

            foreach (var i in items)
            {
                var inserted = RedBlackHelper.Insert(node, i);
                Assert.AreSame(node, inserted);
            }
        }

        [Test]
        public void Update_Test()
        {
            const int value = 10, newValue = 20;
            var items = RandomHelper.UniqueSequence(_random, 100).Select(i => new KeyValuePair<int, int>(i, value)).ToArray();
            var comparer = new KeyComparer<int, int>();

            var node = CreateTree(items, comparer);

            foreach (var i in items)
            {
                var newitem = new KeyValuePair<int, int>(i.Key, newValue);
                var updated = RedBlackHelper.Update(node, newitem, comparer);

                KeyValuePair<int, int> foundItem;
                var found = updated.TryFind(i, comparer, out foundItem);

                Assert.True(found);
                Assert.AreEqual(newitem, foundItem);
            }
        }

        [Test]
        public void Update_NotExistingElement_ReturnsSameNode()
        {
            var items = RandomHelper.UniqueSequence(_random, 100);
            var insertedItems = items.Take(50).ToArray();
            var otherItems = items.Skip(50).ToArray();

            var node = CreateTree(insertedItems);

            foreach (var i in otherItems)
            {
                var updated = RedBlackHelper.Update(node, i);
                Assert.AreSame(node, updated);
            }
        }

        // Private methods

        private IRedBlack<T> CreateTree<T>(IEnumerable<T> items, IComparer<T> comparer = null)
        {
            return items.Aggregate(RedBlackLeaf<T>.Instance, (c, i) => RedBlackHelper.Insert(c, i, comparer));
        }
    }
}
