using System;
using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    class EmptyPatriciaTrie<T> : IPatriciaNode<T>
        where T : class
    {
        // IPatriciaNode

        public T Find(int key)
        {
            return null;
        }

        public IPatriciaNode<T> Modify(int key, Func<T, T> operation)
        {
            var result = operation(null);

            if (result != null)
                return new PatriciaLeaf<T>(key, result);

            return this;
        }

        public IEnumerable<T> GetItems()
        {
            yield break;
        }

        // Public methods

        public override string ToString()
        {
            return "Empty Patricia Trie";
        }
    }
}
