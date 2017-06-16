using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ImmutableCollections.DataStructures.RedBlackTreeStructure;
using ImmutableCollections.Helpers;

// ReSharper disable CompareNonConstrainedGenericWithNull

namespace ImmutableCollections
{
    public class ImmutableRedBlackDictionary<TKey, TValue> : IImmutableDictionary<TKey, TValue>
    {
        private readonly IRedBlack<KeyValuePair<TKey, TValue>> _root;

        private readonly KeyComparer<TKey, TValue> _comparer;

        // Constructors

        public ImmutableRedBlackDictionary()
        {
            _root = RedBlackLeaf<KeyValuePair<TKey, TValue>>.Instance;
            _comparer = new KeyComparer<TKey, TValue>();
        }

        public ImmutableRedBlackDictionary(IComparer<TKey> keyComparer)
        {
            if (keyComparer == null)
                throw new ArgumentNullException(nameof(keyComparer));

            _root = RedBlackLeaf<KeyValuePair<TKey, TValue>>.Instance;
            _comparer = new KeyComparer<TKey, TValue>(keyComparer);
        }

        private ImmutableRedBlackDictionary(IRedBlack<KeyValuePair<TKey, TValue>> root, KeyComparer<TKey, TValue> comparer)
        {
            _root = root;
            _comparer = comparer;
        }

        // IEnumerable

        [Pure]
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _root.GetValues().GetEnumerator();
        }

        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Public methods

        [Pure]
        public ImmutableRedBlackDictionary<TKey, TValue> Add(TKey key, TValue value)
        {
            return Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        [Pure]
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            return Add(key, value);
        }

        [Pure]
        public ImmutableRedBlackDictionary<TKey, TValue> Add(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null)
                throw new ArgumentNullException(nameof(item));

            var newRoot = RedBlackHelper.Insert(_root, item, _comparer);

            if (newRoot == _root)
                throw ExceptionHelper.GetKeyAlreadyExistsException(item.Key, "item");

            return new ImmutableRedBlackDictionary<TKey, TValue>(newRoot, _comparer);
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
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
        {
            throw GetNotSupportedException();
        }

        [Pure]
        IImmutableCollection<KeyValuePair<TKey, TValue>> IImmutableCollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            throw GetNotSupportedException();
        }

        [Pure]
        public ImmutableRedBlackDictionary<TKey, TValue> SetValue(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var newRoot = RedBlackHelper.Update(_root, new KeyValuePair<TKey, TValue>(key, value), _comparer);

            if (newRoot == _root)
                return this;

            return new ImmutableRedBlackDictionary<TKey, TValue>(newRoot, _comparer);
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

                KeyValuePair<TKey, TValue> foundValue;
                var found = _root.TryFind(KeyPair(key), _comparer, out foundValue);

                if (!found)
                    throw ExceptionHelper.GetKeyNotFoundException(key);

                return foundValue.Value;
            }
        }

        [Pure]
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            KeyValuePair<TKey, TValue> foundValue;
            var found = _root.TryFind(KeyPair(key), _comparer, out foundValue);

            value = foundValue.Value;

            return found;
        }

        [Pure]
        public IEnumerable<TKey> Keys => _root.GetValues().Select(i => i.Key);

        [Pure]
        public IEnumerable<TValue> Values => _root.GetValues().Select(i => i.Value);

        [Pure]
        public bool ContainsKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            KeyValuePair<TKey, TValue> foundValue;
            return _root.TryFind(KeyPair(key), _comparer, out foundValue);
        }

        [Pure]
        public bool ContainsValue(TValue value)
        {
            return _root.GetValues().Any(i => i.Value.Equals(value));
        }

        [Pure]
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null)
                throw ExceptionHelper.GetKeyCannotBeNullException("item");

            return _root.GetValues().Contains(item);
        }

        [Pure]
        public int Length => _root.GetValues().Count();

        // Private methods

        [Pure]
        private KeyValuePair<TKey, TValue> KeyPair(TKey key)
        {
            return new KeyValuePair<TKey, TValue>(key, default(TValue));
        }

        [Pure]
        private InvalidOperationException GetNotSupportedException()
        {
            return new InvalidOperationException("Removing from this red-black tree is not supported.");
        }
    }
}
