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
    public class ImmutableRedBlackDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
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
                throw new ArgumentNullException("keyComparer");

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
        public ImmutableRedBlackDictionary<TKey, TValue> Add(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null)
                throw new ArgumentNullException("item");

            var newRoot = RedBlackHelper.Insert(_root, item, _comparer);

            if (newRoot == _root)
                throw new ArgumentException("Key was already present in the collection.", "item");

            return new ImmutableRedBlackDictionary<TKey, TValue>(newRoot, _comparer);
        }

        [Pure]
        public ImmutableRedBlackDictionary<TKey, TValue> SetValue(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            var newRoot = RedBlackHelper.Update(_root, new KeyValuePair<TKey, TValue>(key, value), _comparer);

            if (newRoot == _root)
                return this;

            return new ImmutableRedBlackDictionary<TKey, TValue>(newRoot, _comparer);
        }

        [Pure]
        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException("key");

                KeyValuePair<TKey, TValue> foundValue;
                var found = _root.TryFind(KeyPair(key), _comparer, out foundValue);

                if (!found)
                    throw ExceptionHelper.GetKeyNotFoundException(key);

                return foundValue.Value;
            }
        }

        [Pure]
        public bool ContainsKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

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
                throw new ArgumentException("Key cannot be null.", "item");

            return _root.GetValues().Contains(item);
        }

        [Pure]
        public int Length
        {
            get { return _root.GetValues().Count(); }
        }

        // Private methods

        [Pure]
        private KeyValuePair<TKey, TValue> KeyPair(TKey key)
        {
            return new KeyValuePair<TKey, TValue>(key, default(TValue));
        } 
    }
}
