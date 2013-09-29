using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.DataStructures.PatriciaTrieStructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.PatriciaTrieStructure
{
    [TestFixture]
    class PatriciaBranchTests
    {
        private readonly string[] _items = "Lorem ipsum dolor sit amet consectetur adipisicing elit sed".Split(' ');

        private readonly string[] _secondItems = "do eiusmod tempor incididunt ut labore et dolore".Split(' ');

        // Tests

        [Test]
        public void Find_Test()
        {
            var node = CreateNode(_items);

            foreach (var i in _items)
                Assert.AreEqual(i, node.Find(i.GetHashCode()));

            foreach (var i in _secondItems)
                Assert.IsNull(node.Find(i.GetHashCode()));
        }

        [Test]
        public void RemovingFromTwoElementBranch_ReturnsOtherLeaf()
        {
            const string item = "foo", otherItem = "bar";

            var node = CreateNode(new[] { item, otherItem });

            var result = node.Modify(item.GetHashCode(), i => null);

            Assert.IsInstanceOf<PatriciaLeaf<string>>(result);
            CollectionAssert.AreEquivalent(new[] { otherItem }, result.GetItems());
        }

        [Test]
        public void RemovingSequentialKeys_Test()
        {
            var items = Enumerable.Range(0, _items.Length).
                Zip(_items, (i, s) => new KeyValuePair<int, string>(i, s)).ToArray();

            IPatriciaNode<string> empty = new EmptyPatriciaTrie<string>();
            var node = items.Aggregate(empty, (current, item) => current.Modify(item.Key, i => item.Value));

            foreach (var item in items)
            {
                node = node.Modify(item.Key, i => null) ?? new EmptyPatriciaTrie<string>();
                CollectionAssert.DoesNotContain(node.GetItems(), item.Value);
            }
        }

        [Test]
        public void RemovingRandomKeys_Test()
        {
            var node = CreateNode(_items);

            foreach (var item in _items)
            {
                node = node.Modify(item.GetHashCode(), i => null) ?? new EmptyPatriciaTrie<string>();
                CollectionAssert.DoesNotContain(node.GetItems(), item);
            }
        }

        [Test]
        public void GetItems_Test()
        {
            var node = CreateNode(_items);
            var result = node.GetItems();
            
            CollectionAssert.AreEquivalent(_items, result);
        }

        // Private methods

        private IPatriciaNode<T> CreateNode<T>(IEnumerable<T> items)
            where T : class
        {
            IPatriciaNode<T> empty = new EmptyPatriciaTrie<T>();
            return items.Aggregate(empty, (current, item) => current.Modify(item.GetHashCode(), i => item));
        }

        
    }
}
