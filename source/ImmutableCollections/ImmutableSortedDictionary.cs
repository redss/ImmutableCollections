using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ImmutableCollections.DataStructures.TwoThreeTreeStructure;
using ImmutableCollections.Helpers;

// ReSharper disable CompareNonConstrainedGenericWithNull

namespace ImmutableCollections
{
    public class ImmutableSortedDictionary<TKey, TValue> : IImmutableDictionary<TKey, TValue>
    {
        private readonly ITwoThree<KeyValuePair<TKey, TValue>> _root;

        private readonly KeyComparer<TKey, TValue> _comparer;

        // Constructors

        public ImmutableSortedDictionary()
        {
            _root = EmptyTwoThree<KeyValuePair<TKey, TValue>>.Instance;
            _comparer = new KeyComparer<TKey, TValue>();
        }

        public ImmutableSortedDictionary(IComparer<TKey> keyComparer)
        {
            if (keyComparer == null)
                throw new ArgumentNullException(nameof(keyComparer));

            _root = EmptyTwoThree<KeyValuePair<TKey, TValue>>.Instance;
            _comparer = new KeyComparer<TKey, TValue>(keyComparer);
        }

        private ImmutableSortedDictionary(ITwoThree<KeyValuePair<TKey, TValue>> root, KeyComparer<TKey, TValue> comparer)
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

        // IImmutableDictionary

        [Pure]
        public ImmutableSortedDictionary<TKey, TValue> Add(TKey key, TValue value)
        {
            return Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        [Pure]
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            return Add(key, value);
        }

        [Pure]
        public ImmutableSortedDictionary<TKey, TValue> Add(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null)
                throw new ArgumentNullException(nameof(item));

            var newRoot = _root.Insert(item, _comparer);

            if (newRoot == _root)
                throw ExceptionHelper.GetKeyAlreadyExistsException(item.Key, "item");

            return new ImmutableSortedDictionary<TKey, TValue>(newRoot, _comparer);
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
        public ImmutableSortedDictionary<TKey, TValue> Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var newRoot = _root.Remove(new KeyValuePair<TKey, TValue>(key, default(TValue)), _comparer);

            if (newRoot == _root)
                throw ExceptionHelper.GetKeyNotFoundException(key);

            return new ImmutableSortedDictionary<TKey, TValue>(newRoot, _comparer);
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
        public ImmutableSortedDictionary<TKey, TValue> SetValue(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var item = new KeyValuePair<TKey, TValue>(key, value);

            var newRoot = _root.Update(item, _comparer);

            if (newRoot == null)
                return Add(item);

            if (newRoot == _root)
                return this;

            return new ImmutableSortedDictionary<TKey, TValue>(newRoot, _comparer);
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

                KeyValuePair<TKey, TValue> item;
                var found = _root.TryFind(KeyPair(key), _comparer, out item);

                if (!found)
                    throw ExceptionHelper.GetKeyNotFoundException(key);

                return item.Value;
            }
        }

        [Pure]
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            KeyValuePair<TKey, TValue> item;
            var found = _root.TryFind(KeyPair(key), _comparer, out item);

            value = item.Value;

            return found;
        }

        [Pure]
        public IEnumerable<TKey> Keys => _root.GetValues().Select(i => i.Key);

        [Pure]
        public IEnumerable<TValue> Values => _root.GetValues().Select(i => i.Value);

        [Pure]
        public bool ContainsKey(TKey key)
        {
            KeyValuePair<TKey, TValue> item;
            var found = _root.TryFind(KeyPair(key), _comparer, out item);

            return found;
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

            KeyValuePair<TKey, TValue> foundItem;
            var found = _root.TryFind(item, _comparer, out foundItem);

            return found && foundItem.Equals(item);
        }

        [Pure]
        public int Length => _root.GetValues().Count();

        // Private methods

        [Pure]
        private KeyValuePair<TKey, TValue> KeyPair(TKey key)
        {
            return new KeyValuePair<TKey, TValue>(key, default(TValue));
        }
    }
}
