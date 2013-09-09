﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
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
            if (index > _count)
                throw new ArgumentOutOfRangeException("index");

            if (index == _count)
                return Add(item);

            var updatedRoot = _root.UpdateAndRemove(item, index);

            var count = index + 1;
            foreach (var i in EnumerateFrom(index))
            {
                updatedRoot = updatedRoot.Append(i, count);
                count++;
            }

            return new ImmutableVector<T>(updatedRoot, count);
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.Insert(int index, T item)
        {
            return Insert(index, item);
        }

        [Pure]
        public ImmutableVector<T> UpdateAt(int index, T item)
        {
            if (index >= _count || index < 0)
                throw new ArgumentOutOfRangeException("index");

            var newRoot = _root.UpdateAt(item, index);
            return new ImmutableVector<T>(newRoot, _count);
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.UpdateAt(int index, T item)
        {
            return UpdateAt(index, item);
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
            return Enumerable.Contains(this, item);
        }

        [Pure]
        public T this[int index]
        {
            get
            {
                if (index >= _count || index < 0)
                    throw new ArgumentOutOfRangeException("index");

                return _root.Nth(index);
            }
        }

        [Pure]
        public int IndexOf(T item)
        {
            var index = 0;
            foreach (var element in this)
            {
                if (element.Equals(item))
                    return index;

                index++;
            }

            return -1;
        }

        // Public methods

        [Pure]
        public ImmutableVector<T> AddRange(IEnumerable<T> items)
        {
            // TODO: Optimize.
            
            var newRoot = _root;
            var count = _count;

            foreach (var item in items)
            {
                newRoot = newRoot.Append(item, count);
                count++;
            }

            return new ImmutableVector<T>(newRoot, count);
        }

        [Pure]
        public IEnumerable<T> EnumerateFrom(int index)
        {
            // TODO: Optimize.
            return this.Skip(index);
        }
    }
}