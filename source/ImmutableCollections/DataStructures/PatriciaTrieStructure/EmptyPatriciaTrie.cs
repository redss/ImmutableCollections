using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    class EmptyPatriciaTrie<T> : IPatriciaNode<T>
    {
        private static readonly EmptyPatriciaTrie<T> Empty = new EmptyPatriciaTrie<T>();  

        // Constructor

        private EmptyPatriciaTrie() { }

        // Singleton

        public static IPatriciaNode<T> Instance { get { return Empty; } }
        
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
