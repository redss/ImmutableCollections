using ImmutableCollections.DataStructures.TwoThreeTreeStructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.TwoThreeTreeStructure
{
    [TestFixture]
    public class EmptyTests
    {
        [Test]
        public void Insert_ReturnsLeaf()
        {
            const int item = 10;
            var result = TwoThreeHelper.Insert(Empty<int>.Instance, item);

            Assert.IsInstanceOf<TwoNode<int>>(result);
        }
    }
}