using System.Collections.Generic;
using ImmutableCollections.DataStructures.AssociativeBackendStructure;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    /// <summary>
    /// Patricia Trie's leafs; they contain trie values. 
    /// Leaf can have many values if they have tha same key.
    /// </summary>
    /// <typeparam name="TValue">Type stored in trie's leafs.</typeparam>
    /// <typeparam name="TBackend">Type of the backend to store the values in.</typeparam>
    class PatriciaLeaf<TValue, TBackend> : IPatriciaNode<TValue, TBackend>
        where TBackend : IAssociativeBackend<TValue>, new()
    {
        public readonly int Key;

        public readonly IAssociativeBackend<TValue> Values;

        // Constructor

        public PatriciaLeaf(int key, TValue item)
        {
            Key = key;
            Values = new TBackend().Insert(item);
        }

        private PatriciaLeaf(int key, TBackend backend)
        {
            Key = key;
            Values = backend;
        }

        // IPatriciaNode

        public bool Contains(int key, TValue item)
        {
            return (key == Key) && Values.Contains(item);
        }

        public IPatriciaNode<TValue, TBackend> Insert(int key, TValue item)
        {
            if (key != Key)
                return PatriciaHelper.Join(Key, this, key, new PatriciaLeaf<TValue, TBackend>(key, item));

            if (Values.Contains(item))
                return this;

            var newValues = (TBackend) Values.Insert(item);
            return new PatriciaLeaf<TValue, TBackend>(key, newValues);
        }

        public IEnumerable<TValue> GetItems()
        {
            return Values.GetValues();
        }

        public IPatriciaNode<TValue, TBackend> Remove(int key, TValue item)
        {
            if (key != Key || !Values.Contains(item))
                return this;

            if (Values.IsSingle())
                return null;

            var newValues = (TBackend) Values.Remove(item);
            return new PatriciaLeaf<TValue, TBackend>(key, newValues);
        }

        public int Count()
        {
            return Values.Count();
        }

        // Public methods

        public override string ToString()
        {
            return string.Format("Lf({0} -> {1})", Key, Values);
        }
    }
}
