using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    /// <summary>
    /// Represents only empty Patricia Trie.
    /// </summary>
    /// <typeparam name="T">Type of items associated with keys.</typeparam>
    class EmptyPatriciaTrie<T> : IPatriciaNode<T>
    {
        // Singleton

        public static readonly EmptyPatriciaTrie<T> Instance = new EmptyPatriciaTrie<T>();

        private EmptyPatriciaTrie() { }

        // IPatriciaNode

        public T[] Find(int key)
        {
            return null;
        }

        public IPatriciaNode<T> Modify(int key, IPatriciaOperation<T> operation)
        {
            var result = operation.OnInsert();

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
