using System;
using System.Collections;
using System.Collections.Generic;
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

        public IEnumerator<T> GetEnumerator()
        {
            return _items.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        // IImmutableSet

        public ImmutableHashSet<T> Add(T item)
        {
            throw new NotImplementedException();
        }
        
        IImmutableSet<T> IImmutableSet<T>.Add(T item)
        {
            return Add(item);
        }

        IImmutableCollection<T> IImmutableCollection<T>.Add(T item)
        {
            return Add(item);
        }

        public ImmutableHashSet<T> Remove(T item)
        {
            throw new NotImplementedException();
        }

        IImmutableCollection<T> IImmutableCollection<T>.Remove(T item)
        {
            return Remove(item);
        }

        IImmutableSet<T> IImmutableSet<T>.Remove(T item)
        {
            return Remove(item);
        }

        public ImmutableHashSet<T> ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }
        
        IImmutableSet<T> IImmutableSet<T>.ExceptWith(IEnumerable<T> other)
        {
            return ExceptWith(other);
        }

        public ImmutableHashSet<T> IntersectWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }
        
        IImmutableSet<T> IImmutableSet<T>.IntersectWith(IEnumerable<T> other)
        {
            return IntersectWith(other);
        }

        public ImmutableHashSet<T> SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }
        
        IImmutableSet<T> IImmutableSet<T>.SymmetricExceptWith(IEnumerable<T> other)
        {
            return SymmetricExceptWith(other);
        }

        public ImmutableHashSet<T> UnionWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }
        
        IImmutableSet<T> IImmutableSet<T>.UnionWith(IEnumerable<T> other)
        {
            return UnionWith(other);
        }

        public int Length
        {
            get { throw new NotImplementedException(); }
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }
    }
}
