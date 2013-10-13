using System.Linq;
using ImmutableCollections.DataStructures.PatriciaTrieStructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.PatriciaTrieStructure
{
    [TestFixture]
    public class EmptyPatriciaNodeTests
    {
        [Test]
        public void Find_ReturnsNull()
        {
            var node = EmptyPatriciaTrie<string>.Instance;
            var result = node.Find(10);

            Assert.IsNull(result);
        }

        [Test]
        public void Modify_WithOperationReturningValue_ReturnsLeaf()
        {
            const int key = 10;
            const string item = "foo";

            var node = EmptyPatriciaTrie<string>.Instance;
            var operation = new OperationStub<string>(i => null, () => new[] { item });

            var result = node.Modify(key, operation);

            Assert.AreEqual(1, result.GetItems().Count());
            Assert.IsInstanceOf<PatriciaLeaf<string>>(result);
            Assert.AreEqual(item, result.Find(key)[0]);
        }

        [Test]
        public void GetItems_YieldsNothing()
        {
            var node = EmptyPatriciaTrie<string>.Instance;
            var result = node.GetItems();

            CollectionAssert.IsEmpty(result);
        }
    }
}
