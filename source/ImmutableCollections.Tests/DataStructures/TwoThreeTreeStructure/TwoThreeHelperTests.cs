using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImmutableCollections.DataStructures.TwoThreeTreeStructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.TwoThreeTreeStructure
{
    [TestFixture]
    public class TwoThreeHelperTests
    {
        [Test]
        public void Insert_OnEmptyNode_ReturnsTwoThreeNode()
        {
            const int item = 10;
            var result = TwoThreeHelper.Insert(Empty<int>.Instance, item);

            Assert.IsInstanceOf<TwoNode<int>>(result);
        }
    }
}
