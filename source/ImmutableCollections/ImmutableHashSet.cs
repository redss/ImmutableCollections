using System;
using System.Collections;
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
        private readonly IPatriciaNode<T> _root;

        // Constructors

        public ImmutableHashSet()
        {
            _root = new EmptyPatriciaTrie<T>();
        }

        private ImmutableHashSet(IPatriciaNode<T> root)
        {
            _root = root ?? new EmptyPatriciaTrie<T>();
        }

        // IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return _root.GetItems().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        // IImmutableSet

        public ImmutableHashSet<T> Add(T item)
        {
            var newRoot = _root.Insert(item.GetHashCode(), item);
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
            var newRoot = _root.Remove(item.GetHashCode(), item);
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

        public ImmutableHashSet<T> ExceptWith(IEnumerable<T> other)
        {
            var newRoot = other.Aggregate(_root, (current, i) => current.Remove(i.GetHashCode(), i));
            return new ImmutableHashSet<T>(newRoot);
        }
        
        IImmutableSet<T> IImmutableSet<T>.ExceptWith(IEnumerable<T> other)
        {
            return ExceptWith(other);
        }

        public ImmutableHashSet<T> IntersectWith(IEnumerable<T> other)
        {
            IPatriciaNode<T> empty = new EmptyPatriciaTrie<T>();
            var newRoot = other
                .Where(i => _root.Contains(i.GetHashCode(), i))
                .Aggregate(empty, (c, i) => c.Insert(i.GetHashCode(), i));

            return new ImmutableHashSet<T>(newRoot);
        }
        
        IImmutableSet<T> IImmutableSet<T>.IntersectWith(IEnumerable<T> other)
        {
            return IntersectWith(other);
        }

        public ImmutableHashSet<T> SymmetricExceptWith(IEnumerable<T> other)
        {
            var items = other.ToArray();
            return ExceptWith(items).UnionWith(items.Except(this));
        }
        
        IImmutableSet<T> IImmutableSet<T>.SymmetricExceptWith(IEnumerable<T> other)
        {
            return SymmetricExceptWith(other);
        }

        public ImmutableHashSet<T> UnionWith(IEnumerable<T> other)
        {
            var newRoot = other.Aggregate(_root, (c, i) => c.Insert(i.GetHashCode(), i));
            return new ImmutableHashSet<T>(newRoot);
        }
        
        IImmutableSet<T> IImmutableSet<T>.UnionWith(IEnumerable<T> other)
        {
            return UnionWith(other);
        }

        public int Length
        {
            get { return _root.Count(); }
        }

        public bool Contains(T item)
        {
            return _root.Contains(item.GetHashCode(), item);
        }
    }
}
