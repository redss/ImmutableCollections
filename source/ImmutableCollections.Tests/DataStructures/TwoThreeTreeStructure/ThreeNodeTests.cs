using System.Collections.Generic;
using ImmutableCollections.DataStructures.TwoThreeTreeStructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.TwoThreeTreeStructure
{
    [TestFixture]
    public class ThreeNodeTests
    {
        [Test]
        public void InsertNewElement_OnLeaf_SplitsNode()
        {
            const int a = 10, b = 20, c = 30;
            var node = new ThreeNode<int>(a, b, EmptyTwoThree<int>.Instance, EmptyTwoThree<int>.Instance, EmptyTwoThree<int>.Instance);

            int splitValue;
            ITwoThree<int> splitLeft, splitRight;
            var result = node.Insert(c, Comparer<int>.Default, out splitLeft, out splitRight, out splitValue);

            Assert.IsNull(result);

            Assert.AreEqual(b, splitValue);
            Assert.IsInstanceOf<TwoNode<int>>(splitLeft);
            Assert.IsInstanceOf<TwoNode<int>>(splitRight);
        }

        [Test]
        public void InsertExistingElement_OnLeaf_ReturnsSameNode()
        {
            const int a = 10, b = 20;
            var node = new ThreeNode<int>(a, b, EmptyTwoThree<int>.Instance, EmptyTwoThree<int>.Instance, EmptyTwoThree<int>.Instance);

            int splitValue;
            ITwoThree<int> splitLeft, splitRight;
            var result = node.Insert(a, Comparer<int>.Default, out splitLeft, out splitRight, out splitValue);

            Assert.AreSame(node, result);
        }
    }
}
