using System;
using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    class PatriciaLeaf<T> : IPatriciaNode<T>
        where T : class
    {
        public readonly int Key;

        public readonly T Item;

        // Constructors

        public PatriciaLeaf(int key, T item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "Patricia Leaf cannot be empty!");

            Key = key;
            Item = item;
        }

        // IPatriciaNode

        public T Find(int key)
        {
            return key == Key ? Item : null;
        }

        public IPatriciaNode<T> Modify(int key, Func<T, T> operation)
        {
            if (operation == null)
                throw new ArgumentNullException("operation");

            if (key != Key)
            {
                var result = operation(null);

                // This situation shouldn't occur, maybe it should throw exception instead?
                if (result == null)
                    return this;

                // Create new leaf with new item.
                var newLeaf = new PatriciaLeaf<T>(key, result);
                return PatriciaHelper.Join(Key, this, key, newLeaf);
            }
            else
            {
                // Modify item.
                var result = operation(Item);

                if (result == null)
                    return null;

                if (result == Item)
                    return this;

                return new PatriciaLeaf<T>(key, result);
            }
        }

        public IEnumerable<T> GetItems()
        {
            yield return Item;
        }

        // Public methods

        public override string ToString()
        {
            return string.Format("Lf({0} -> {1})", Key, Item);
        }
    }
}
