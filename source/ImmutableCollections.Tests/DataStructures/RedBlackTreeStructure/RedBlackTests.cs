using System;
using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.DataStructures.RedBlackTreeStructure;
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
        public void Update_Test()
        {
            var items = GetRandomNumbers(100);
            var node = items.Aggregate(RedBlackLeaf<int>.Instance, (current, i) => current.Update(i, _comparer).MakeRoot());

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
        public void Remove_Test()
        {
            var items = GetRandomNumbers(100);
            var expected = new SortedSet<int>(items);

            var node = items.Aggregate(RedBlackLeaf<int>.Instance, (current, i) => current.Update(i, _comparer).MakeRoot());

            foreach (var i in items)
            {
                node = node.Remove(i, _comparer).MakeRoot();
                node.Validate();

                expected.Remove(i);

                CollectionAssert.AreEqual(expected, node.GetValues());
            }

            Assert.IsInstanceOf<RedBlackLeaf<int>>(node);
        }

        // Private methods

        private int[] GetRandomNumbers(int count)
        {
            return Enumerable.Repeat(0, count).Select(x => _random.Next()).ToArray();
        }
    }
}
