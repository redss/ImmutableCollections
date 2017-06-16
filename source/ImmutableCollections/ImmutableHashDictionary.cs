using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ImmutableCollections.DataStructures.PatriciaTrieStructure;
using ImmutableCollections.DataStructures.PatriciaTrieStructure.DictionaryOperations;
using ImmutableCollections.Helpers;

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
        private readonly IPatriciaNode<KeyValuePair<TKey, TValue>> _root;

        // Constructors

        public ImmutableHashDictionary()
        {
            _root = EmptyPatriciaTrie<KeyValuePair<TKey, TValue>>.Instance;
        }

        private ImmutableHashDictionary(IPatriciaNode<KeyValuePair<TKey, TValue>> root)
        {
            _root = root;
        }

        // IEnumerable

        [Pure]
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _root.GetItems().GetEnumerator();
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
                throw new ArgumentNullException(nameof(key));

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
                throw ExceptionHelper.GetKeyCannotBeNullException("item");

            var operation = new DictionaryAddOperation<TKey, TValue>(item);
            var newRoot = _root.Modify(item.Key.GetHashCode(), operation);

            if (newRoot == _root)
                throw ExceptionHelper.GetKeyAlreadyExistsException(item.Key, "item");
            
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
                throw new ArgumentNullException(nameof(key));

            var operation = new DictionaryRemoveOperation<TKey, TValue>(key);
            var newRoot = _root.Modify(key.GetHashCode(), operation);

            if (newRoot == null)
                return new ImmutableHashDictionary<TKey, TValue>();

            if (newRoot == _root)
                throw ExceptionHelper.GetKeyNotFoundException(key);

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
                throw new ArgumentNullException(nameof(key));

            var operation = new DictionarySetValueOperation<TKey, TValue>(key, value);
            var newRoot = _root.Modify(key.GetHashCode(), operation);

            if (newRoot == _root)
                return this;

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
                    throw new ArgumentNullException(nameof(key));

                var found = _root.Find(key.GetHashCode()) ?? Enumerable.Empty<KeyValuePair<TKey, TValue>>();

                foreach (var i in found)
                    if (i.Key.Equals(key))
                        return i.Value;

                throw ExceptionHelper.GetKeyNotFoundException(key);
            }
        }

        [Pure]
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var found = _root.Find(key.GetHashCode());

            if (found == null)
            {
                value = default(TValue);
                return false;
            }

            foreach (var i in found)
            {
                if (i.Key.Equals(key))
                {
                    value = i.Value;
                    return true;
                }
            }

            throw ExceptionHelper.GetKeyNotFoundException(key);
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
                throw new ArgumentNullException(nameof(key));

            var found = _root.Find(key.GetHashCode());
            return found != null && found.Any(i => i.Key.Equals(key));
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
                throw ExceptionHelper.GetKeyCannotBeNullException("item");

            var found = _root.Find(item.Key.GetHashCode());
            return found != null && found.Contains(item);
        }

        [Pure]
        public int Length
        {
            get { return _root.GetItems().Count(); }
        }
    }
}
