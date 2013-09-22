using System;
using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    /// <summary>
    /// An empty Patricia Trie.
    /// </summary>
    /// <typeparam name="T">Type stored in trie's leafs.</typeparam>
    class EmptyPatriciaTrie<T> : IPatriciaNode<T>
    {
        // IPatriciaNode

        public bool Contains(int key, T item)
        {
            return false;
        }

        public IPatriciaNode<T> Insert(int key, T item)
        {
            return new PatriciaLeaf<T>(key, item);
        }

        public IEnumerable<T> GetItems()
        {
            yield break;
        }

        public IPatriciaNode<T> Remove(int key, T item)
        {
            return null;
        }

        public IPatriciaNode<T> Promote(int prefix, int mask)
        {
            throw new InvalidOperationException("Patricia Trie is empty.");
        }

        public int Count()
        {
            return 0;
        }

        // Public methods

        public override string ToString()
        {
            return "Empty Patricia Trie";
        }
    }
}
