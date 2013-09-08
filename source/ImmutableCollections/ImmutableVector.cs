using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using ImmutableCollections.DataStructures.ImmutableVectorStructure;

namespace ImmutableCollections
{
    /// <summary>
    /// Collection based on Bitmapped Vector Trie.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ImmutableVector<T> : IImmutableList<T>
    {
        private readonly IVectorNode<T> _root;

        private readonly int _count;

        // Constructors

        public ImmutableVector()
        {
            _count = 0;
            _root = new EmptyVector<T>();
        }

        private ImmutableVector(IVectorNode<T> root, int count)
        {
            _count = count;
            _root = root;
        }

        // IEnumerable

        [Pure]
        public IEnumerator<T> GetEnumerator()
        {
            return _root.GetValues().GetEnumerator();
        }

        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // IImmutableCollection

        [Pure]
        public ImmutableVector<T> Add(T item)
        {
            var newRoot = _root.Append(item, _count);
            return new ImmutableVector<T>(newRoot, _count + 1);
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.Add(T item)
        {
            return Add(item);
        }

        [Pure]
        IImmutableCollection<T> IImmutableCollection<T>.Add(T item)
        {
            return Add(item);
        }

        [Pure]
        public ImmutableVector<T> Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.Insert(int index, T item)
        {
            return Insert(index, item);
        }

        [Pure]
        public ImmutableVector<T> Remove(T item)
        {
            throw new NotImplementedException();
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.Remove(T item)
        {
            return Remove(item);
        }

        [Pure]
        public ImmutableVector<T> RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.RemoveAt(int index)
        {
            return RemoveAt(index);
        }

        [Pure]
        IImmutableCollection<T> IImmutableCollection<T>.Remove(T item)
        {
            return Remove(item);
        }

        [Pure]
        public int Length
        {
            get { return _count; }
        }

        [Pure]
        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        [Pure]
        public T this[int index]
        {
            get { return _root.Nth(index); }
            set { throw new NotImplementedException(); }
        }

        [Pure]
        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }
    }
}
