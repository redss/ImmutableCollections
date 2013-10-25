using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

// ReSharper disable CompareNonConstrainedGenericWithNull

namespace ImmutableCollections
{
    /// <summary>
    /// Array based immutable dictionary copying all values on every operation.
    /// It shouldn't be used in standard cased, as crucial operations are O(n).
    /// Its main application is being backend for more complex data structures.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in dictionary.</typeparam>
    public class ImmutableCopyDictionary<TKey, TValue> : IImmutableDictionary<TKey, TValue>
    {
        private static readonly Dictionary<TKey, TValue> EmptyDictionary = new Dictionary<TKey, TValue>();

        private readonly Dictionary<TKey, TValue> _items; 

        // Constructors

        public ImmutableCopyDictionary()
        {
            _items = EmptyDictionary;
        }

        private ImmutableCopyDictionary(Dictionary<TKey, TValue> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            _items = items;
        }

        // IEnumerable

        [Pure]
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _items.AsEnumerable().GetEnumerator();
        }

        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // IImmutableDictionary

        [Pure]
        public ImmutableCopyDictionary<TKey, TValue> Add(TKey key, TValue value)
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
        public ImmutableCopyDictionary<TKey, TValue> Add(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null)
                throw new ArgumentException("Key cannot be null.", "item");

            if (ContainsKey(item.Key))
            {
                var message = string.Format("An element with '{0}' key already exists in the dictionary", item.Key);
                throw new ArgumentException(message, "item");
            }

            var newItems = GetCopiedDictionary(Length + 1);
            newItems.Add(item.Key, item.Value);

            return new ImmutableCopyDictionary<TKey, TValue>(newItems);
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
        public ImmutableCopyDictionary<TKey, TValue> Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (!ContainsKey(key))
                throw GetKeyNotFoundException(key);

            var newDictionary = GetCopiedDictionary(Length - 1);
            newDictionary.Remove(key);

            return new ImmutableCopyDictionary<TKey, TValue>(newDictionary);
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
        public ImmutableCopyDictionary<TKey, TValue> SetValue(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            var newDictionary = GetCopiedDictionary(Length);
            newDictionary[key] = value;

            return new ImmutableCopyDictionary<TKey, TValue>(newDictionary);
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

                if (!ContainsKey(key))
                    throw GetKeyNotFoundException(key);

                return _items[key];
            }
        }

        [Pure]
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (ContainsKey(key))
            {
                value = _items[key];
                return true;
            }

            value = default (TValue);
            return false;
        }

        [Pure]
        public IEnumerable<TKey> Keys
        {
            get { return _items.Keys; }
        }

        [Pure]
        public IEnumerable<TValue> Values
        {
            get { return _items.Values; }
        }

        [Pure]
        public bool ContainsKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return _items.ContainsKey(key);
        }

        [Pure]
        public bool ContainsValue(TValue value)
        {
            return _items.ContainsValue(value);
        }

        [Pure]
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null)
                throw new ArgumentException("Key cannot be null.", "item");

            return _items.Contains(item);
        }

        [Pure]
        public int Length
        {
            get { return _items.Count; }
        }

        // Private methods

        [Pure]
        private KeyNotFoundException GetKeyNotFoundException(TKey key)
        {
            var message = string.Format("Key {0} was not found.", key);
            return new KeyNotFoundException(message);
        }

        [Pure]
        private Dictionary<TKey, TValue> GetCopiedDictionary(int capacity)
        {
            var newDictionary = new Dictionary<TKey, TValue>(capacity);

            foreach (var item in _items)
                newDictionary.Add(item.Key, item.Value);

            return newDictionary;
        }
    }
}
