using System;
using System.Linq;
using ImmutableCollections.DataStructures.PatriciaTrieStructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.PatriciaTrieStructure
{
    [TestFixture]
    public class PatriciaTrieTests
    {
        [Test]
        public void InsertingIntoEmptyNode_ReturnsLeaf()
        {
            var key = 10;
            var value = "hello";

            var empty = new EmptyPatriciaTrie<string>();
            var result = empty.Insert(key, "hello") as PatriciaLeaf<string>;

            Assert.NotNull(result);
            Assert.AreEqual(value, result.Values.First());
        }

        [Test]
        public void InsertingElementWithSameKeyToLeaf_ReturnsLeaf()
        {
            var key = 10;
            var values = new[] {"one", "two", "three"};

            IPatriciaNode<string> node = new EmptyPatriciaTrie<string>();
            node = values.Aggregate(node, (current, v) => current.Insert(key, v));

            var leaf = node as PatriciaLeaf<string>;

            Assert.NotNull(leaf);
            CollectionAssert.AreEquivalent(values, leaf.Values);
        }

        [Test]
        public void InsertingSameValuesToLeaf_ReturnsNull()
        {
            var key = 10;
            var value = "foo";

            var node = new EmptyPatriciaTrie<string>().Insert(key, value).Insert(key, value);

            Assert.IsNull(node);
        }

        [Test]
        public void InsertingUniqueKeyToLeaf_ReturnsBranch()
        {
            var first = Tuple.Create("foo", 0x1FFF);
            var second = Tuple.Create("bar", 0x2FFF);

            var leaf = new EmptyPatriciaTrie<string>().Insert(first.Item2, first.Item1) as PatriciaLeaf<string>;
            var branch = leaf.Insert(second.Item2, second.Item1) as PatriciaBranch<string>;

            Assert.NotNull(branch);
            Assert.AreNotEqual(branch.Left, branch.Right);
            Assert.True(branch.Contains(first.Item2, first.Item1));
            Assert.True(branch.Contains(second.Item2, second.Item1));
        }

        [Test]
        public void Inserting_Test()
        {
            var items = "foo bar lorem ipsum hello world".Split(' ');

            IPatriciaNode<string> node = new EmptyPatriciaTrie<string>();
            node = items.Aggregate(node, (current, i) => current.Insert(i.GetHashCode(), i));

            foreach (var i in items)
                Assert.True(node.Contains(i.GetHashCode(), i));

            CollectionAssert.AreEquivalent(items, node.GetItems());
        }
    }
}
