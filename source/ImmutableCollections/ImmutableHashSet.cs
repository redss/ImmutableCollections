using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ImmutableCollections.DataStructures.PatriciaTrieStructure;
using ImmutableCollections.DataStructures.PatriciaTrieStructure.SetOperations;

// ReSharper disable CompareNonConstrainedGenericWithNull

namespace ImmutableCollections
{
    /// <summary>
    /// Immutable hash set based on Fast Mergable Integer Trie, a structure based on Patricia Trie.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    public class ImmutableHashSet<T> : IImmutableSet<T>
    {
        private readonly IPatriciaNode<T> _root;

        // Constructors

        public ImmutableHashSet()
        {
            _root = EmptyPatriciaTrie<T>.Instance;
        }

        private ImmutableHashSet(IPatriciaNode<T> root)
        {
            if (root == null)
                throw new ArgumentNullException("root");

            _root = root;
        }

        // IEnumerable

        [Pure]
        public IEnumerator<T> GetEnumerator()
        {
            return _root.GetItems().GetEnumerator();
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
            if (item == null)
                throw new ArgumentNullException("item");

            var operation = new SetAddOperation<T>(item);
            var newRoot = _root.Modify(item.GetHashCode(), operation);

            return newRoot == _root ? this : new ImmutableHashSet<T>(newRoot);
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
            if (item == null)
                throw new ArgumentNullException("item");

            var operation = new SetRemoveOperation<T>(item);
            var newRoot = _root.Modify(item.GetHashCode(), operation);

            return newRoot == _root ? this : new ImmutableHashSet<T>(newRoot);
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
            get { return _root.GetItems().Count(); }
        }

        [Pure]
        public bool Contains(T item)
        {
            var items = _root.Find(item.GetHashCode());
            return items != null && items.Contains(item);
        }
    }
}
