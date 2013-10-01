using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.Helpers;

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
        private readonly KeyValuePair<TKey, TValue>[] _items;

        // Constructors

        public ImmutableCopyDictionary()
        {
            _items = new KeyValuePair<TKey, TValue>[] {};
        }

        private ImmutableCopyDictionary(KeyValuePair<TKey, TValue>[] items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            _items = items;
        }

        // IEnumerable

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _items.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // IImmutableDictionary

        public ImmutableCopyDictionary<TKey, TValue> Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            return Add(key, value);
        }

        public ImmutableCopyDictionary<TKey, TValue> Add(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null)
                throw new ArgumentException("Key cannot be null.", "item");

            if (ContainsKey(item.Key))
            {
                var message = string.Format("An element with '{0}' key already exists in the dictionary", item.Key);
                throw new ArgumentException(message, "item");
            }

            var newItems = _items.Append(item);
            return new ImmutableCopyDictionary<TKey, TValue>(newItems);
        }

        IImmutableCollection<KeyValuePair<TKey, TValue>> IImmutableCollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            return Add(item);
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(KeyValuePair<TKey, TValue> item)
        {
            return Add(item);
        }

        public ImmutableCopyDictionary<TKey, TValue> Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            var length = _items.Length;

            if (length == 1)
                return new ImmutableCopyDictionary<TKey, TValue>();

            for (var i = 0; i < length; i++)
            {
                if (_items[i].Key.Equals(key))
                {
                    var newItems = new KeyValuePair<TKey, TValue>[length - 1];

                    if (i > 0)
                        Array.Copy(_items, 0, newItems, 0, i);

                    if (i < length)
                        Array.Copy(_items, i + 1, newItems, i, Length - i - 1);

                    return new ImmutableCopyDictionary<TKey, TValue>(newItems);
                }
            }

            throw GetKeyNotFoundException(key);
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
        {
            return Remove(key);
        }

        IImmutableCollection<KeyValuePair<TKey, TValue>> IImmutableCollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public ImmutableCopyDictionary<TKey, TValue> SetValue(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            var length = _items.Length;

            for (var i = 0; i < length; i++)
            {
                if (_items[i].Key.Equals(key))
                {
                    var newItems = new KeyValuePair<TKey, TValue>[length];
                    Array.Copy(_items, newItems, length);
                    newItems[i] = new KeyValuePair<TKey, TValue>(key, value);

                    return new ImmutableCopyDictionary<TKey, TValue>(newItems);
                }
            }

            throw GetKeyNotFoundException(key);
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetValue(TKey key, TValue value)
        {
            return SetValue(key, value);
        }

        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException("key");

                foreach (var item in _items)
                    if (item.Key.Equals(key))
                        return item.Value;

                throw GetKeyNotFoundException(key);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            foreach (var item in _items)
            {
                if (item.Key.Equals(key))
                {
                    value = item.Value;
                    return true;
                }
            }

            value = default (TValue);
            return false;
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

            return _items.Any(i => i.Key.Equals(key));
        }

        public bool ContainsValue(TValue value)
        {
            return _items.Any(i => i.Value.Equals(value));
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null)
                throw new ArgumentException("Key cannot be null.", "item");

            return _items.Contains(item);
        }

        public int Length
        {
            get { return _items.Length; }
        }

        // Private methods

        private KeyNotFoundException GetKeyNotFoundException(TKey key)
        {
            var message = string.Format("Key {0} was not found.", key);
            return new KeyNotFoundException(message);
        }
    }
}
