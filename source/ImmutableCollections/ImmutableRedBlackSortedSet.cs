using System;
using System.Collections;
using System.Collections.Generic;
using ImmutableCollections.DataStructures.RedBlackTreeStructure;
using System.Linq;

// ReSharper disable CompareNonConstrainedGenericWithNull

namespace ImmutableCollections
{
    public class ImmutableRedBlackSortedSet<T> : IEnumerable<T>
    {
        private readonly IRedBlack<T> _root;

        private readonly IComparer<T> _comparer; 

        // Constructors

        public ImmutableRedBlackSortedSet()
        {
            _root = RedBlackLeaf<T>.Instance;
            _comparer = Comparer<T>.Default;
        }

        public ImmutableRedBlackSortedSet(IComparer<T> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException();

            _root = RedBlackLeaf<T>.Instance;
            _comparer = comparer;
        }

        private ImmutableRedBlackSortedSet(IRedBlack<T> root, IComparer<T> comparer)
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

        // Public methods

        public ImmutableRedBlackSortedSet<T> Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var newRoot = RedBlackHelper.Insert(_root, item, _comparer);

            if (newRoot == _root)
                return this;

            return new ImmutableRedBlackSortedSet<T>(newRoot, _comparer);
        }

        public int Length
        {
            get { return this.Count(); }
        }

        public bool Contains(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            T foundItem;
            return _root.TryFind(item, _comparer, out foundItem);
        }
    }
}
