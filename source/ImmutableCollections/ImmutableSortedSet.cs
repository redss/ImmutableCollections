using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.DataStructures.TwoThreeTreeStructure;

// ReSharper disable CompareNonConstrainedGenericWithNull

namespace ImmutableCollections
{
    public class ImmutableSortedSet<T> : IImmutableSet<T>
    {
        private readonly ITwoThree<T> _root;

        private readonly IComparer<T> _comparer;
        
        // Constructors

        public ImmutableSortedSet()
        {
            _root = Empty<T>.Instance;
            _comparer = Comparer<T>.Default;
        }

        public ImmutableSortedSet(IComparer<T> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException("comparer");

            _comparer = comparer;
        }

        private ImmutableSortedSet(ITwoThree<T> root, IComparer<T> comparer)
        {
            _root = root;
            _comparer = comparer;
        }

        // IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return _root.GetValues().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // IImmutableSet

        public ImmutableSortedSet<T> Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var newRoot = TwoThreeHelper.Insert(_root, item, _comparer);
            return new ImmutableSortedSet<T>(newRoot, _comparer);
        }

        IImmutableSet<T> IImmutableSet<T>.Add(T item)
        {
            return Add(item);
        }

        IImmutableCollection<T> IImmutableCollection<T>.Add(T item)
        {
            return Add(item);
        }

        public ImmutableSortedSet<T> Remove(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var newRoot = TwoThreeHelper.Remove(_root, item, _comparer);
            return new ImmutableSortedSet<T>(newRoot, _comparer);
        }

        IImmutableCollection<T> IImmutableCollection<T>.Remove(T item)
        {
            return Remove(item);
        }

        IImmutableSet<T> IImmutableSet<T>.Remove(T item)
        {
            return Remove(item);
        }

        public int Length
        {
            get { return _root.GetValues().Count(); }
        }

        public bool Contains(T item)
        {
            var found = _root.TryFind(item, _comparer, out item);
            return found;
        }
    }
}
