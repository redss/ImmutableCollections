using System;
using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.Helpers;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    class SetBackend<T>
    {
        private readonly T[] _items;

        // Constructor

        public SetBackend(T item)
        {
            _items = new[] { item };
        }

        public SetBackend(T[] items)
        {
            _items = items;
        }

        // Public methods

        public IEnumerable<T> GetValues()
        {
            return _items.AsEnumerable();
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public SetBackend<T> Insert(T item)
        {
            return _items.Contains(item) ? this : new SetBackend<T>(_items.Append(item));
        }

        public SetBackend<T> Remove(T item)
        {
            var index = Array.IndexOf(_items, item);

            if (index == -1)
                return this;

            if (_items.Length == 1)
                return null;

            var newItems = _items.RemoveAt(index);
            return new SetBackend<T>(newItems);
        }
    }
}
