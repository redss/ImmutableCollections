using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using ImmutableCollections.DataStructures.RedBlackTreeStructure;
using System.Linq;

// ReSharper disable CompareNonConstrainedGenericWithNull

namespace ImmutableCollections
{
    public class ImmutableRedBlackSet<T> : IImmutableSet<T>
    {
        private readonly IRedBlack<T> _root;

        private readonly IComparer<T> _comparer; 

        // Constructors

        public ImmutableRedBlackSet()
        {
            _root = RedBlackLeaf<T>.Instance;
            _comparer = Comparer<T>.Default;
        }

        public ImmutableRedBlackSet(IComparer<T> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException();

            _root = RedBlackLeaf<T>.Instance;
            _comparer = comparer;
        }

        private ImmutableRedBlackSet(IRedBlack<T> root, IComparer<T> comparer)
        {
            _root = root;
            _comparer = comparer;
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

        // Public methods

        [Pure]
        public ImmutableRedBlackSet<T> Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var newRoot = RedBlackHelper.Insert(_root, item, _comparer);

            if (newRoot == _root)
                return this;

            return new ImmutableRedBlackSet<T>(newRoot, _comparer);
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
        IImmutableCollection<T> IImmutableCollection<T>.Remove(T item)
        {
            throw GetNotSupportedException();
        }

        [Pure]
        IImmutableSet<T> IImmutableSet<T>.Remove(T item)
        {
            throw GetNotSupportedException();
        }

        [Pure]
        public int Length => this.Count();

        [Pure]
        public bool Contains(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            T foundItem;

            return _root.TryFind(item, _comparer, out foundItem);
        }

        [Pure]
        private InvalidOperationException GetNotSupportedException()
        {
            return new InvalidOperationException("Removing from this red-black set is not supported.");
        }
    }
}
