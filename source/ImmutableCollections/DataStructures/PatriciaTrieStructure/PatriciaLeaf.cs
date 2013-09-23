using System.Collections.Generic;
using System.Linq;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    /// <summary>
    /// Patricia Trie's leafs; they contain trie values. 
    /// Leaf can have many values if they have tha same key.
    /// </summary>
    /// <typeparam name="T">Type stored in trie's leafs.</typeparam>
    class PatriciaLeaf<T> : IPatriciaNode<T>
    {
        public readonly int Key;

        public readonly ImmutableLinkedList<T> Values;

        // Constructor

        public PatriciaLeaf(int key, ImmutableLinkedList<T> values)
        {
            Key = key;
            Values = values;
        }

        public PatriciaLeaf(int key, T item)
        {
            Key = key;
            Values = new ImmutableLinkedList<T>().Add(item);
        }

        // IPatriciaNode

        public bool Contains(int key, T item)
        {
            return (key == Key) && Values.Contains(item);
        }

        public IPatriciaNode<T> Insert(int key, T item)
        {
            if (key != Key)
                return PatriciaHelper.Join(Key, this, key, new PatriciaLeaf<T>(key, item));

            if (Values.Contains(item))
                return this;

            var newValues = Values.Insert(0, item);
            return new PatriciaLeaf<T>(key, newValues);
        }

        public IEnumerable<T> GetItems()
        {
            return Values;
        }

        public IPatriciaNode<T> Remove(int key, T item)
        {
            if (key != Key || !Values.Contains(item))
                return this;

            if (Values.Length == 1)
                return null;

            var newValues = Values.Remove(item);
            return new PatriciaLeaf<T>(key, newValues);
        }

        public int Count()
        {
            return Values.Length;
        }

        // Public methods

        public override string ToString()
        {
            var values = Values.Length == 1 ? Values.First().ToString() : string.Join(" ", Values);
            return string.Format("Lf({0} -> {1})", Key, values);
        }
    }
}
