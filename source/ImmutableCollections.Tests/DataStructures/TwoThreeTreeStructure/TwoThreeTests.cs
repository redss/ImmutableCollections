using System;
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
            
            var node = items.Aggregate((ITwoThree<int>) Empty<int>.Instance, (current, item) => TwoThreeHelper.Insert(current, item));
            var values = node.GetValues().ToArray();

            CollectionAssert.AllItemsAreUnique(values);
            CollectionAssert.AreEqual(sorted, values);
        }
    }
}
