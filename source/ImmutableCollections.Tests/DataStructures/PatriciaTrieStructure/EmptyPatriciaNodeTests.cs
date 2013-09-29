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
            var node = new EmptyPatriciaTrie<string>();
            var result = node.Find(10);

            Assert.IsNull(result);
        }

        [Test]
        public void Modify_WithOperationReturningValue_ReturnsLeaf()
        {
            const int key = 10;
            const string item = "foo";

            var node = new EmptyPatriciaTrie<string>();
            var result = node.Modify(key, i => item);

            Assert.AreEqual(1, result.GetItems().Count());
            Assert.IsInstanceOf<PatriciaLeaf<string>>(result);
            Assert.AreEqual(item, result.Find(key));
        }

        [Test]
        public void GetItems_YieldsNothing()
        {
            var node = new EmptyPatriciaTrie<string>();
            var result = node.GetItems();

            CollectionAssert.IsEmpty(result);
        }
    }
}
