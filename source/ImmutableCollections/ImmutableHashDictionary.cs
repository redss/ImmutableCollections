using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.DataStructures.PatriciaTrieStructure;

// ReSharper disable CompareNonConstrainedGenericWithNull

namespace ImmutableCollections
{
    public class ImmutableHashDictionary<TKey, TValue> : IImmutableDictionary<TKey, TValue>
    {
        private readonly IPatriciaNode<ImmutableCopyDictionary<TKey, TValue>> _root;

        // Constructors

        public ImmutableHashDictionary() : this(null) { }

        private ImmutableHashDictionary(IPatriciaNode<ImmutableCopyDictionary<TKey, TValue>> root)
        {
            _root = root ?? new EmptyPatriciaTrie<ImmutableCopyDictionary<TKey, TValue>>();
        }

        // IEnumerable

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _root.GetItems().SelectMany(i => i).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // IImmutableDictionary

        public ImmutableHashDictionary<TKey, TValue> Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            return Add(key, value);
        }

        public ImmutableHashDictionary<TKey, TValue> Add(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null)
                throw new ArgumentException("Key cannot be null.", "item");

            if (ContainsKey(item.Key))
            {
                var message = string.Format("An element with '{0}' key already exists in the dictionary", item.Key);
                throw new ArgumentException(message, "item");
            }

            throw new NotImplementedException();
        }

        IImmutableCollection<KeyValuePair<TKey, TValue>> IImmutableCollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            return Add(item);
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(KeyValuePair<TKey, TValue> item)
        {
            return Add(item);
        }

        public ImmutableHashDictionary<TKey, TValue> Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            throw new NotImplementedException();
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
        {
            return Remove(key);
        }

        IImmutableCollection<KeyValuePair<TKey, TValue>> IImmutableCollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public IImmutableDictionary<TKey, TValue> SetValue(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            throw new NotImplementedException();
        }

        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException("key");

                return _root.Find(key.GetHashCode())[key];
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return _root.Find(key.GetHashCode()).TryGetValue(key, out value);
        }

        public IEnumerable<TKey> Keys
        {
            get { return this.Select(i => i.Key); }
        }

        public IEnumerable<TValue> Values
        {
            get { return this.Select(i => i.Value); }
        }

        public bool ContainsKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return _root.Find(key.GetHashCode()).ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            return Values.Contains(value);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null)
                throw new ArgumentException("Key cannot be null.", "item");

            return _root.Find(item.Key.GetHashCode()).Contains(item);
        }

        public int Length
        {
            get { return _root.GetItems().Sum(i => i.Length); }
        }
    }
}
