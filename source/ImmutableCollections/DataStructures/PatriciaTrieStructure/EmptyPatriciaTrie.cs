using System.Collections.Generic;
using ImmutableCollections.DataStructures.AssociativeBackendStructure;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    /// <summary>
    /// An empty Patricia Trie.
    /// </summary>
    /// <typeparam name="TValue">Type stored in trie's leafs.</typeparam>
    /// <typeparam name="TBackend">Type of the backend to store the values in.</typeparam>
    class EmptyPatriciaTrie<TValue, TBackend> : IPatriciaNode<TValue, TBackend>
        where TBackend : IAssociativeBackend<TValue>, new()
    {
        // IPatriciaNode

        public bool Contains(int key, TValue item)
        {
            return false;
        }

        public IPatriciaNode<TValue, TBackend> Insert(int key, TValue item)
        {
            return new PatriciaLeaf<TValue, TBackend>(key, item);
        }

        public IEnumerable<TValue> GetItems()
        {
            yield break;
        }

        public IPatriciaNode<TValue, TBackend> Remove(int key, TValue item)
        {
            return null;
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
