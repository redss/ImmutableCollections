using System.Collections.Generic;
using ImmutableCollections.DataStructures.TwoThreeTreeStructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.TwoThreeTreeStructure
{
    [TestFixture]
    public class TwoNodeTests
    {
        [Test]
        public void InsertNewElement_ReturnsThreeNode()
        {
            const int first = 10, second = 20;
            var node = new TwoNode<int>(first, EmptyTwoThree<int>.Instance, EmptyTwoThree<int>.Instance);

            int splitValue;
            ITwoThree<int> splitLeft, splitRight;
            var result = node.Insert(second, Comparer<int>.Default, out splitLeft, out splitRight, out splitValue);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ThreeNode<int>>(result);
        }

        [Test]
        public void InsertSameElement_ReturnsSameNode()
        {
            const int item = 10;
            var node = new TwoNode<int>(item, EmptyTwoThree<int>.Instance, EmptyTwoThree<int>.Instance);

            int splitValue;
            ITwoThree<int> splitLeft, splitRight;
            var result = node.Insert(item, Comparer<int>.Default, out splitLeft, out splitRight, out splitValue);

            Assert.AreSame(node, result);
        }
    }
}
