using ImmutableCollections.DataStructures.TwoThreeTreeStructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.TwoThreeTreeStructure
{
    class TwoThreeHelperTests
    {
        [Test]
        public void Insert_OnEmptyNode_ReturnsTwoThreeNode()
        {
            const int item = 10;
            var result = EmptyTwoThree<int>.Instance.Insert(item);

            Assert.IsInstanceOf<TwoNode<int>>(result);
        }
    }
}
