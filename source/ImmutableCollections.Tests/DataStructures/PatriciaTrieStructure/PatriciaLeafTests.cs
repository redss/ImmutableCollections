using System.Linq;
using ImmutableCollections.DataStructures.PatriciaTrieStructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.PatriciaTrieStructure
{
    class PatriciaLeafTests
    {
        const int Key = 10, SecondKey = 20;
        
        const string Item = "foo", SecondItem = "bar";

        // Tests

        [Test]
        public void Find_Test()
        {
            var node = CreateLeaf(Key, Item);

            Assert.AreEqual(Item, node.Find(Key)[0]);
            Assert.AreEqual(null, node.Find(Key + 1));
        }

        [Test]
        public void Modify_OnNewKey_WithOperationReturningNewElement_CreatesBranch()
        {
            var node = CreateLeaf(Key, Item);
            var operation = new OperationStub<string>(i => null, () => new[] { SecondItem });

            var result = node.Modify(SecondKey, operation);

            Assert.IsInstanceOf<PatriciaBranch<string>>(result);
            CollectionAssert.AreEquivalent(new[] { Item, SecondItem }, result.GetItems());
        }

        [Test]
        public void Modify_OnExistingKey_WithOperationReturningNewValue_CreatesModifiedLeaf()
        {
            var node = CreateLeaf(Key, Item);
            var operation = new OperationStub<string>(i => new[] { SecondItem }, () => null);

            var result = node.Modify(Key, operation);

            Assert.IsInstanceOf<PatriciaLeaf<string>>(result);
            Assert.AreEqual(SecondItem, result.GetItems().First());
        }

        //[Test]
        // TODO: Operations should take care of it
        public void Modify_OnExistingKey_WithOperationReturningSameValue_ReturnsSameLeaf()
        {
            var node = CreateLeaf(Key, Item);
            var operation = new OperationStub<string>(i => new[] { Item }, () => null);

            var result = node.Modify(Key, operation);

            Assert.AreSame(node, result);
        }

        [Test]
        public void Modify_OnExistingKey_WithOperationReturningNull_ReturnsNull()
        {
            var node = CreateLeaf(Key, Item);
            var operation = new OperationStub<string>(i => null, () => null);

            var result = node.Modify(Key, operation);

            Assert.IsNull(result);
        }

        [Test]
        public void GetItems_GetsIneItem()
        {
            var node = CreateLeaf(Key, Item);
            var result = node.GetItems().ToArray();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(Item, result.First());
        }

        // Private methods

        private IPatriciaNode<T> CreateLeaf<T>(int key, T item)
            where T : class
        {
            var operation = new OperationStub<T>(i => null, () => new[] { item });
            return EmptyPatriciaTrie<T>.Instance.Modify(key, operation);
        }
    }
}
