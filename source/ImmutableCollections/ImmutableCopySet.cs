using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ImmutableCollections
{
    public class ImmutableCopySet<T> : IImmutableSet<T>
    {
        private readonly T[] _items;

        // Constructors

        public ImmutableCopySet()
        {
            _items = new T[0];
        }

        private ImmutableCopySet(T[] items)
        {
            _items = items;
        }

        // IEnumerable

        [Pure]
        public IEnumerator<T> GetEnumerator()
        {
            return _items.AsEnumerable().GetEnumerator();
        }

        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        // IImmutableSet

        [Pure]
        public ImmutableHashSet<T> Add(T item)
        {
            throw new NotImplementedException();
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
        public ImmutableHashSet<T> Remove(T item)
        {
            throw new NotImplementedException();
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
            get { throw new NotImplementedException(); }
        }

        [Pure]
        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }
    }
}
