using System;
using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.DataStructures.RedBlackTreeStructure;
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
        public void Update_Test()
        {
            var items = RandomHelper.UniqueSequence(_random, 100);
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
    }
}
