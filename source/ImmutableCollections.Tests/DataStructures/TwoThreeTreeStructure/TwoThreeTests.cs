using System;
using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.DataStructures.TwoThreeTreeStructure;
using ImmutableCollections.Tests.TestInfrastructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.TwoThreeTreeStructure
{
    [TestFixture]
    public class TwoThreeTests
    {
        private readonly Random _random = new Random(0);

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
            var item = items[20];

            var node = CreateTree(items);
            var result = TwoThreeHelper.Insert(node, item);

            Assert.AreSame(node, result);
        }

        private ITwoThree<T> CreateTree<T>(IEnumerable<T> items)
        {
            return items.Aggregate((ITwoThree<T>)Empty<T>.Instance, (current, item) => TwoThreeHelper.Insert(current, item));
        }
    }
}
