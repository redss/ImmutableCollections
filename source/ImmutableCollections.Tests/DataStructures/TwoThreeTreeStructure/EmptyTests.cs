using System.Collections.Generic;
using ImmutableCollections.DataStructures.TwoThreeTreeStructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.TwoThreeTreeStructure
{
    class EmptyTests
    {
        [Test]
        public void Insert_PropagatesValueAndEmptyNodes()
        {
            const int item = 10;

            int splitValue;
            ITwoThree<int> splitLeft, splitRight;
            var result = EmptyTwoThree<int>.Instance.Insert(item, Comparer<int>.Default, out splitLeft, out splitRight, out splitValue);

            Assert.IsNull(result);

            Assert.AreEqual(item, splitValue);
            Assert.IsInstanceOf<EmptyTwoThree<int>>(splitLeft);
            Assert.IsInstanceOf<EmptyTwoThree<int>>(splitRight);
        }

        [Test]
        public void Update_ReturnsNull()
        {
            var result = EmptyTwoThree<int>.Instance.Update(10, Comparer<int>.Default);
            Assert.IsNull(result);
        }
    }
}
