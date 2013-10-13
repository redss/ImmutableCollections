﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.DataStructures.PatriciaTrieStructure;

namespace ImmutableCollections
{
    /// <summary>
    /// Immutable hash set based on Fast Mergable Integer Trie, a structure based on Patricia Trie.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    public class ImmutableHashSet<T> : IImmutableSet<T>
    {
        private readonly IPatriciaNode<SetBackend<T>> _root;

        // Constructors

        public ImmutableHashSet()
        {
            _root = new EmptyPatriciaTrie<SetBackend<T>>();
        }

        private ImmutableHashSet(IPatriciaNode<SetBackend<T>> root)
        {
            _root = root ?? new EmptyPatriciaTrie<SetBackend<T>>();
        }

        // IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return _root.GetItems().SelectMany(i => i.GetValues()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        // IImmutableSet

        public ImmutableHashSet<T> Add(T item)
        {
            var newRoot = Add(_root, item);
            return new ImmutableHashSet<T>(newRoot);
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
            var newRoot = Remove(_root, item);
            return new ImmutableHashSet<T>(newRoot);
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
            get { return _root.GetItems().Sum(x => x.GetValues().Count()); }
        }

        public bool Contains(T item)
        {
            var backend = _root.Find(item.GetHashCode());
            return backend != null && backend.Contains(item);
        }

        // Private methods

        private IPatriciaNode<SetBackend<T>> Add(IPatriciaNode<SetBackend<T>> node, T item)
        {
            return node.Modify(item.GetHashCode(), i => i == null ? new SetBackend<T>(item) : i.Insert(item));
        }

        private IPatriciaNode<SetBackend<T>> Remove(IPatriciaNode<SetBackend<T>> node, T item)
        {
            return node.Modify(item.GetHashCode(), i => i == null ? null : i.Remove(item));
        }
    }
}
