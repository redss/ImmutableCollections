using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ImmutableCollections.DataStructures.PatriciaTrieStructure;

// ReSharper disable CompareNonConstrainedGenericWithNull

namespace ImmutableCollections
{
    /// <summary>
    /// Immutable hash dictionary based on Fast Mergable Integer Trie (Patricia Trie).
    /// </summary>
    /// <typeparam name="TKey">The type of keys in dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in dictionary.</typeparam>
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

        [Pure]
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _root.GetItems().SelectMany(i => i).GetEnumerator();
        }

        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // IImmutableDictionary

        [Pure]
        public ImmutableHashDictionary<TKey, TValue> Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        [Pure]
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            return Add(key, value);
        }

        [Pure]
        public ImmutableHashDictionary<TKey, TValue> Add(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null)
                throw new ArgumentException("Key cannot be null.", "item");

            var newRoot = _root.Modify(item.Key.GetHashCode(), i => i.Add(item), () => CreateNewBackend(item));
            return new ImmutableHashDictionary<TKey, TValue>(newRoot);
        }

        [Pure]
        IImmutableCollection<KeyValuePair<TKey, TValue>> IImmutableCollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            return Add(item);
        }

        [Pure]
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(KeyValuePair<TKey, TValue> item)
        {
            return Add(item);
        }

        [Pure]
        public ImmutableHashDictionary<TKey, TValue> Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            var newRoot = _root.Modify(key.GetHashCode(), i => null, () => { throw GetKeyNotFoundException(key); });
            return new ImmutableHashDictionary<TKey, TValue>(newRoot);
        }

        [Pure]
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
        {
            return Remove(key);
        }

        [Pure]
        IImmutableCollection<KeyValuePair<TKey, TValue>> IImmutableCollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        [Pure]
        public ImmutableHashDictionary<TKey, TValue> SetValue(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            var newRoot = _root.Modify(key.GetHashCode(), i => i.SetValue(key, value), () => { throw GetKeyNotFoundException(key); });
            return new ImmutableHashDictionary<TKey, TValue>(newRoot);
        }
        
        [Pure]
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetValue(TKey key, TValue value)
        {
            return SetValue(key, value);
        }

        [Pure]
        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException("key");

                var found = _root.Find(key.GetHashCode());

                if (found == null)
                    throw GetKeyNotFoundException(key);

                return found[key];
            }
        }

        [Pure]
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            var found = _root.Find(key.GetHashCode());

            if (found == null)
            {
                value = default(TValue);
                return false;
            }

            return found.TryGetValue(key, out value);
        }

        [Pure]
        public IEnumerable<TKey> Keys
        {
            get { return this.Select(i => i.Key); }
        }

        [Pure]
        public IEnumerable<TValue> Values
        {
            get { return this.Select(i => i.Value); }
        }

        [Pure]
        public bool ContainsKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            var found = _root.Find(key.GetHashCode());
            return found != null && found.ContainsKey(key);
        }

        [Pure]
        public bool ContainsValue(TValue value)
        {
            return Values.Contains(value);
        }

        [Pure]
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null)
                throw new ArgumentException("Key cannot be null.", "item");

            var found = _root.Find(item.Key.GetHashCode());
            return found != null && found.Contains(item);
        }

        [Pure]
        public int Length
        {
            get { return _root.GetItems().Sum(i => i.Length); }
        }

        // Private methods

        private ImmutableCopyDictionary<TKey, TValue> CreateNewBackend(KeyValuePair<TKey, TValue> item)
        {
            return new ImmutableCopyDictionary<TKey, TValue>().Add(item);
        }

        private KeyNotFoundException GetKeyNotFoundException(TKey key)
        {
            var message = string.Format("Key {0} was not found.", key);
            return new KeyNotFoundException(message);
        }
    }
}
