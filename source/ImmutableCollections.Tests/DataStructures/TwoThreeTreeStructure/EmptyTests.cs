using System.Collections.Generic;
using ImmutableCollections.DataStructures.TwoThreeTreeStructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.TwoThreeTreeStructure
{
    [TestFixture]
    public class EmptyTests
    {
        [Test]
        public void Insert_PropagatesValueAndEmptyNodes()
        {
            const int item = 10;

            int splitValue;
            ITwoThree<int> splitLeft, splitRight;
            var result = Empty<int>.Instance.Insert(item, Comparer<int>.Default, out splitLeft, out splitRight, out splitValue);

            Assert.IsNull(result);

            Assert.AreEqual(item, splitValue);
            Assert.IsInstanceOf<Empty<int>>(splitLeft);
            Assert.IsInstanceOf<Empty<int>>(splitRight);
        }

        [Test]
        public void Update_ReturnsNull()
        {
            var result = Empty<int>.Instance.Update(10, Comparer<int>.Default);
            Assert.IsNull(result);
        }
    }
}
