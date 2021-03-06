﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ImmutableCollections.DataStructures.BitmappedVectorTrieStructure;
using ImmutableCollections.Helpers;

namespace ImmutableCollections
{
    /// <summary>
    /// Collection based on Bitmapped Vector Trie.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ImmutableVector<T> : IImmutableList<T>
    {
        private static readonly IVectorNode<T> EmptyVector = new EmptyVector<T>();

        private readonly IVectorNode<T> _root;

        // Constructors

        public ImmutableVector()
        {
            Length = 0;

            _root = EmptyVector;
        }

        private ImmutableVector(IVectorNode<T> root, int count)
        {
            Length = count;

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

        // IImmutableList

        [Pure]
        public ImmutableVector<T> Add(T item)
        {
            var newRoot = _root.Append(item, Length);
            return new ImmutableVector<T>(newRoot, Length + 1);
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
            if (index == Length)
                return Add(item);

            AssertIndexRange(index);
            
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
            AssertIndexRange(index);

            var newRoot = _root.UpdateAt(item, index);
            return new ImmutableVector<T>(newRoot, Length);
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.UpdateAt(int index, T item)
        {
            return UpdateAt(index, item);
        }

        [Pure]
        public ImmutableVector<T> Remove(T item)
        {
            // TODO: Not unit tested!

            var index = IndexOf(item);

            if (index >= 0)
                return RemoveAt(index);

            return this;
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.Remove(T item)
        {
            return Remove(item);
        }

        [Pure]
        IImmutableCollection<T> IImmutableCollection<T>.Remove(T item)
        {
            return Remove(item);
        }

        [Pure]
        public ImmutableVector<T> RemoveAt(int index)
        {
            AssertIndexRange(index);

            var newRoot = index > 0 ? _root.Remove(index - 1) : EmptyVector;

            if (index == Length - 1)
                return new ImmutableVector<T>(newRoot, Length - 1);

            var count = index;
            foreach (var i in EnumerateFrom(index + 1))
            {
                newRoot = newRoot.Append(i, count);
                count++;
            }

            return new ImmutableVector<T>(newRoot, count);
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.RemoveAt(int index)
        {
            return RemoveAt(index);
        }

        [Pure]
        public int Length { get; }

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
                if (index >= Length || index < 0)
                    throw new ArgumentOutOfRangeException(nameof(index));

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
            var count = Length;

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

        // Private methods

        private void AssertIndexRange(int index, string argumentName = "index")
        {
            if (index >= Length)
                throw ExceptionHelper.GetIndexTooBigException(index, Length - 1, argumentName);

            if (index < 0)
                throw ExceptionHelper.GetIndexNegativeException(index, argumentName);
        }
    }
}
