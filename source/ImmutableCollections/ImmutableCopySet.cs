using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using ImmutableCollections.Helpers;

// ReSharper disable CompareNonConstrainedGenericWithNull

namespace ImmutableCollections
{
    public class ImmutableCopySet<T> : IImmutableSet<T>
    {
        private readonly HashSet<T> _items;  

        // Constructors

        public ImmutableCopySet()
        {
            _items = new HashSet<T>();
        }

        private ImmutableCopySet(HashSet<T> items)
        {
            _items = items;
        }

        // IEnumerable

        [Pure]
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        // IImmutableSet

        [Pure]
        public ImmutableCopySet<T> Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (Contains(item))
                throw ExceptionHelper.GetItemAlreadyExistsException(item, "item");

            var newSet = new HashSet<T>(_items) { item };
            return new ImmutableCopySet<T>(newSet);
        }

        [Pure]
        IImmutableSet<T> IImmutableSet<T>.Add(T item)
        {
            return Add(item);
        }

        [Pure]
        IImmutableCollection<T> IImmutableCollection<T>.Add(T item)
        {
            return Add(item);
        }

        [Pure]
        public ImmutableCopySet<T> Remove(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (!Contains(item))
                return this;

            var newSet = new HashSet<T>(_items);
            newSet.Remove(item);

            return new ImmutableCopySet<T>(newSet);
        }

        [Pure]
        IImmutableCollection<T> IImmutableCollection<T>.Remove(T item)
        {
            return Remove(item);
        }

        [Pure]
        IImmutableSet<T> IImmutableSet<T>.Remove(T item)
        {
            return Remove(item);
        }

        [Pure]
        public int Length
        {
            get { return _items.Count; }
        }

        [Pure]
        public bool Contains(T item)
        {
            return _items.Contains(item);
        }
    }
}
