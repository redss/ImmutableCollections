﻿using System;
using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.DataStructures.PatriciaTrieStructure;
using NUnit.Framework;
using Backend = ImmutableCollections.DataStructures.AssociativeBackendStructure.SetBackend<string>;

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

            var empty = new EmptyPatriciaTrie<string, Backend>();
            var result = empty.Insert(key, "hello") as PatriciaLeaf<string, Backend>;

            Assert.NotNull(result);
            Assert.AreEqual(value, result.GetItems().First());
        }

        [Test]
        public void InsertingElementWithSameKeyToLeaf_ReturnsLeaf()
        {
            var key = 10;
            var values = new[] {"one", "two", "three"};

            IPatriciaNode<string, Backend> node = new EmptyPatriciaTrie<string, Backend>();
            node = values.Aggregate(node, (current, v) => current.Insert(key, v));

            var leaf = node as PatriciaLeaf<string, Backend>;

            Assert.NotNull(leaf);
            CollectionAssert.AreEquivalent(values, leaf.GetItems());
        }

        [Test]
        public void InsertingSameValuesToLeaf_ReturnsSameLeaf()
        {
            var key = 10;
            var value = "foo";

            var leaf = new EmptyPatriciaTrie<string, Backend>().Insert(key, value);
            var node = leaf.Insert(key, value);

            Assert.AreSame(leaf, node);
        }

        [Test]
        public void InsertingUniqueKeyToLeaf_ReturnsBranch()
        {
            var first = Tuple.Create("foo", 0x1FFF);
            var second = Tuple.Create("bar", 0x2FFF);

            var leaf = new EmptyPatriciaTrie<string, Backend>().Insert(first.Item2, first.Item1) as PatriciaLeaf<string, Backend>;

            Assert.NotNull(leaf);

            var branch = leaf.Insert(second.Item2, second.Item1) as PatriciaBranch<string, Backend>;

            Assert.NotNull(branch);
            Assert.AreNotEqual(branch.Left, branch.Right);
            Assert.True(branch.Contains(first.Item2, first.Item1));
            Assert.True(branch.Contains(second.Item2, second.Item1));
        }

        [Test]
        public void Inserting_Test()
        {
            var items = "foo bar lorem ipsum hello world foo".Split(' ');

            var fakeHash = items.First().GetHashCode();
            var fakeItem = "far";

            IPatriciaNode<string, Backend> node = new EmptyPatriciaTrie<string, Backend>();
            node = items.Aggregate(node, (current, i) => current.Insert(i.GetHashCode(), i) ?? current);

            foreach (var i in items)
                Assert.True(node.Contains(i.GetHashCode(), i));

            CollectionAssert.AreEquivalent(items.Distinct(), node.GetItems());

            node = node.Insert(fakeHash, fakeItem);
            Assert.True(node.Contains(fakeHash, fakeItem));
        }

        [Test]
        public void RemovingLastElementFromLeaf_ReturnsNull()
        {
            var item = "foo";
            var hash = item.GetHashCode();

            var leaf = new EmptyPatriciaTrie<string, Backend>().Insert(hash, item).Remove(hash, item);

            Assert.IsNull(leaf);
        }

        [Test]
        public void RemovingOneElementFromLeaf_ReturnsLeaf()
        {
            var items = "foo bar zar boo baz".Split(' ');
            var key = 10;

            IPatriciaNode<string, Backend> node = new EmptyPatriciaTrie<string, Backend>();
            node = items.Aggregate(node, (current, value) => current.Insert(key, value));

            var set = new HashSet<string>(items);

            foreach (var i in items)
            {
                CollectionAssert.AreEquivalent(set, node.GetItems());
                Assert.IsInstanceOf<PatriciaLeaf<string, Backend>>(node);

                node = node.Remove(key, i);
                set.Remove(i);
            }

            Assert.IsNull(node);
        }

        [Test]
        public void Remove_Test()
        {
            var items = GetKeyWords("lorem ipsum foo bar hello moto");
            var node = BuildPatriciaTrie(items);
            var set = new HashSet<string>(items.Select(i => i.Value));

            foreach (var i in items)
            {
                CollectionAssert.AreEquivalent(set, node.GetItems());

                node = node.Remove(i.Key, i.Value);
                set.Remove(i.Value);
            }

            Assert.IsNull(node);
        }

        private IPatriciaNode<string, Backend> BuildPatriciaTrie(IEnumerable<KeyValuePair<int, string>> values)
        {
            IPatriciaNode<string, Backend> node = new EmptyPatriciaTrie<string, Backend>();
            return values.Aggregate(node, (current, value) => current.Insert(value.Key, value.Value));
        }

        private KeyValuePair<int, string>[] GetKeyWords(string str)
        {
            return GetKeyValues(str.Split(' '));
        }

        private KeyValuePair<int, T>[] GetKeyValues<T>(IEnumerable<T> values)
        {
            return values.Select(v => new KeyValuePair<int, T>(v.GetHashCode(), v)).ToArray();
        }
    }
}
