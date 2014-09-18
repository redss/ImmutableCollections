using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

// ReSharper disable NotAccessedVariable
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace ImmutableCollections.Tests
{
    [TestFixture(typeof(ImmutableHashDictionary<string, int>))]
    [TestFixture(typeof(ImmutableSortedDictionary<string, int>))]
    class ImmutableDirectoryTests<TDictionary>
        where TDictionary : IImmutableDictionary<string, int>, new()
    {
        private readonly IImmutableDictionary<string, int> _emptyDictionary = new TDictionary();

        private readonly KeyValuePair<string, int> _item = new KeyValuePair<string, int>("foo", 10);

        private readonly KeyValuePair<string, int>[] _items = GetSampleData();

        // Tests

        [Test]
        public void Add_Test()
        {
            var dictionary = _emptyDictionary.Add(_item.Key, _item.Value);
            CollectionAssert.AreEquivalent(new[] { _item }, dictionary);
        }

        [Test]
        public void AddKeyValuePair_Test()
        {
            var dictionary = _emptyDictionary.Add(_item);
            CollectionAssert.AreEquivalent(new[] { _item }, dictionary);
        }

        [Test]
        public void MultipleAdd_Test()
        {
            var dictionary = GetDictionaryWithSampleData();
            CollectionAssert.AreEquivalent(_items, dictionary);
        }

        [Test]
        public void Add_ThrowsArgumentNullException_WhenKeyIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _emptyDictionary.Add(null, 10));
        }

        [Test]
        public void Add_ThrowsArgumentException_WhenKeyIsPresent()
        {
            var dictionary = _emptyDictionary.Add(_item.Key, 10);
            Assert.Throws<ArgumentException>(() => dictionary.Add(_item.Key, 20));
        }

        [Test]
        public void Remove_Test()
        {
            var dictionary = GetDictionaryWithSampleData();
            var expected = _items.ToList();

            foreach (var item in _items)
            {
                dictionary = dictionary.Remove(item.Key);
                expected.Remove(item);

                CollectionAssert.AreEquivalent(expected, dictionary);
            }
        }

        [Test]
        public void Remove_ThrowsArgumentNullException_WhenKeyIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => GetDictionaryWithSampleData().Remove(null));
        }

        [Test]
        public void Remove_ThrowsKeyNotFoundException_WhenKeyIsNotPresent()
        {
            Assert.Throws<KeyNotFoundException>(() => GetDictionaryWithSampleData().Remove(_item.Key));
        }

        [Test]
        public void SetValue_Test()
        {
            const int value = 1000;
            var dictionary = GetDictionaryWithSampleData();

            foreach (var item in _items)
            {
                var v = dictionary.SetValue(item.Key, value)[item.Key];
                Assert.AreEqual(value, v);
            }
        }

        [Test]
        public void SetValue_ThrowsArgumentNullException_WhenKeyIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => GetDictionaryWithSampleData().SetValue(null, 10));
        }

        [Test]
        public void SetValue_AddsValue_WhenKeyExists()
        {
            var dict = GetDictionaryWithSampleData();
            var result = dict.SetValue(_item.Key, _item.Value);

            CollectionAssert.Contains(result, _item);
        }

        [Test]
        public void Indexer_ReturnsAssociatedValue()
        {
            var dictionary = GetDictionaryWithSampleData();

            foreach (var item in _items)
            {
                var value = dictionary[item.Key];
                Assert.AreEqual(item.Value, value);
            }
        }

        [Test]
        public void Indexer_ThrowsArgumentNullException_WhenKeyIsNull()
        {
            int value;
            Assert.Throws<ArgumentNullException>(() => value = GetDictionaryWithSampleData()[null]);
        }

        [Test]
        public void TryGetValue_WithExistingKey_Test()
        {
            var dictionary = GetDictionaryWithSampleData();

            foreach (var items in _items)
            {
                int value;
                var result = dictionary.TryGetValue(items.Key, out value);

                Assert.AreEqual(value, items.Value);
                Assert.IsTrue(result);
            }
        }

        [Test]
        public void TryGetValue_WithNewKey_Test()
        {
            int value;
            var result = GetDictionaryWithSampleData().TryGetValue(_item.Key, out value);

            Assert.IsFalse(result);
        }

        [Test]
        public void TryGetValue_WithNullKey_ThrowsArgumentNullException()
        {
            int value;
            Assert.Throws<ArgumentNullException>(() => GetDictionaryWithSampleData().TryGetValue(null, out value));
        }

        [Test]
        public void Keys_ReturnsAllKeys()
        {
            var keys = GetDictionaryWithSampleData().Keys;
            CollectionAssert.AreEquivalent(_items.Select(i => i.Key), keys);
        }

        [Test]
        public void Values_ReturnsAllValues()
        {
            var values = GetDictionaryWithSampleData().Values;
            CollectionAssert.AreEquivalent(_items.Select(i => i.Value), values);
        }

        [Test]
        public void ContainsKey_Test()
        {
            var dictionary = GetDictionaryWithSampleData();

            foreach (var item in _items)
                Assert.True(dictionary.ContainsKey(item.Key));

            Assert.False(dictionary.ContainsKey(_item.Key));
        }

        [Test]
        public void ContainsValue_Test()
        {
            var dictionary = GetDictionaryWithSampleData();

            foreach (var item in _items)
                Assert.True(dictionary.ContainsValue(item.Value));

            Assert.False(dictionary.ContainsValue(1000));
        }

        [Test]
        public void Contains_WithExactItem_ReturnsTrue()
        {
            var item = _items.First();
            Assert.True(GetDictionaryWithSampleData().Contains(item));
        }

        [Test]
        public void Contains_WithSameKey_ReturnsFalse()
        {
            var item = new KeyValuePair<string, int>(_items.First().Key, 100);
            Assert.False(GetDictionaryWithSampleData().Contains(item));
        }

        [Test]
        public void Contains_WithNewItem_ReturnsFalse()
        {
            Assert.False(GetDictionaryWithSampleData().Contains(_item));
        }

        [Test]
        public void Contains_WithNullKey_ThrowsArgumentException()
        {
            var item = new KeyValuePair<string, int>(null, 10);
            Assert.Throws<ArgumentException>(() => GetDictionaryWithSampleData().Contains(item));
        }

        [Test]
        public void Length_Test()
        {
            var length = GetDictionaryWithSampleData().Length;
            Assert.AreEqual(_items.Length, length);
        }

        // Private methods

        private static KeyValuePair<string, int>[] GetSampleData()
        {
            var keys = "Lorem ipsum dolor sit amet consectetur adipisicing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua".Split(' ');
            return keys.Select((k, i) => new KeyValuePair<string, int>(k, i)).ToArray();
        }

        private IImmutableDictionary<string, int> GetDictionaryWithSampleData()
        {
            return _items.Aggregate(_emptyDictionary, (current, item) => current.Add(item));            
        }
    }
}
